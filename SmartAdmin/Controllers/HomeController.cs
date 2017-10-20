using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

using ReflectionIT.Mvc.Paging;

using SmartAdmin.Models;
using SmartAdmin.ViewModels;

namespace SmartAdmin.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly SmartAdminDataContext dataContext;

        public HomeController(SmartAdminDataContext dataContext, ILogger<HomeController> logger)
        {
            this.dataContext = dataContext;
            this.logger = logger;
        }
        
        public IActionResult Index(string sortMember = "name", string sortOrder = "asc", string currentFilter = "", int page = 1)
        {
            IQueryable<User> users = dataContext.Users;

            if (!string.IsNullOrWhiteSpace(currentFilter))
            {
                users = users.Where(u => u.LastName.StartsWith(currentFilter, StringComparison.OrdinalIgnoreCase));
            }

            if (string.Equals(sortMember, "id", StringComparison.OrdinalIgnoreCase))
            {
                users = string.Equals(sortOrder, "asc", StringComparison.OrdinalIgnoreCase) 
                    ? users.OrderBy(u => u.Id) 
                    : users.OrderByDescending(u => u.Id);
            }
            else if (string.Equals(sortMember, "isActive", StringComparison.OrdinalIgnoreCase))
            {
                users = string.Equals(sortOrder, "asc", StringComparison.OrdinalIgnoreCase) 
                    ? users.OrderBy(u => u.IsActive)
                    : users.OrderByDescending(u => u.IsActive);
            }
            else
            {
                users = string.Equals(sortOrder, "asc", StringComparison.OrdinalIgnoreCase) 
                    ? users.OrderBy(u => u.LastName) 
                    : users.OrderByDescending(u => u.LastName);
            }
            
            ViewBag.SortOrder = string.Equals(sortOrder, "asc", StringComparison.OrdinalIgnoreCase) 
                ? "desc"
                : "asc";

            var model = users.Select(u => new UserViewModel {
                Id = u.Id,
                Name = u.LastName + 
                    (string.IsNullOrEmpty(u.FirstName) ? string.Empty : " " + char.ToUpper(u.FirstName[0])) + 
                    (string.IsNullOrEmpty(u.SecondName) ? string.Empty : " " + char.ToUpper(u.SecondName[0])),
                IsActive = u.IsActive
            });

            var pagedList = PagingList.Create(model, 15, page);

            pagedList.RouteValue = new RouteValueDictionary {
                { "sortMember", sortMember },
                { "sortOrder",  sortOrder },
                { "currentFilter", currentFilter },
            };

            return View(pagedList);
        }

        [HttpPut]
        public async Task<IActionResult> Activate(ActivateUserViewModel vm)
        {
            if (ModelState.IsValid)
            {
                //if (User.IsInRole("ADM"))
                //{
                    var user = await dataContext.Users.FirstOrDefaultAsync(u => u.Id == vm.Id);
                    if (user != null)
                    {
                        user.IsActive = vm.IsActive;

                        logger.LogInformation(
                            vm.IsActive
                                ? $"Пользователь {user.Id} активирован администратором {User.Identity.Name}"
                                : $"Пользователь {user.Id} отключен администратором {User.Identity.Name}");

                        await dataContext.SaveChangesAsync();
                    }

                    return Ok();
                //}

                //return Forbid("Данное действие доступно только администраторам");
            }

            return BadRequest(ModelState);
        }
        
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}