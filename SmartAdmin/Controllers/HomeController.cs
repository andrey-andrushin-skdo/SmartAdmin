using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ReflectionIT.Mvc.Paging;
using SmartAdmin.Models;
using SmartAdmin.ViewModels;

namespace SmartAdmin.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        private static IEnumerable<UserViewModel> users = new[]
        {
            new UserViewModel {Id = 1, Name = "Aaa", IsActive = true},
            new UserViewModel {Id = 2, Name = "Bbb", IsActive = false}
        };

        private readonly ILogger<HomeController> logger;
        
        public HomeController(ILogger<HomeController> logger)
        {
            this.logger = logger;
        }
        
        public IActionResult Index(string sortMember = "name", string sortOrder = "asc", string currentFilter = "", int page = 1)
        {            
            if (!string.IsNullOrWhiteSpace(currentFilter))
            {
                users = users.Where(u => u.Name.StartsWith(currentFilter, StringComparison.OrdinalIgnoreCase));
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
                    ? users.OrderByDescending(u => u.IsActive)
                    : users.OrderBy(u => u.IsActive) ;
            }
            else
            {
                users = string.Equals(sortOrder, "asc", StringComparison.OrdinalIgnoreCase) 
                    ? users.OrderBy(u => u.Name) 
                    : users.OrderByDescending(u => u.Name);
            }
            
            ViewBag.SortOrder = string.Equals(sortOrder, "asc", StringComparison.OrdinalIgnoreCase) 
                ? "desc"
                : "asc";
            
            return View(PagingList.Create(users, 2, page));
        }

        [HttpPut]
        public IActionResult Activate(ActivateUserViewModel vm)
        {
            if (ModelState.IsValid)
            {
                if (User.IsInRole("administrator"))
                {
                    var user = users.FirstOrDefault(u => u.Id == vm.Id);
                    if (user != null)
                    {
                        user.IsActive = vm.IsActive;

                        logger.LogInformation(
                            vm.IsActive
                                ? $"Пользователь {user.Id} активирован администратором {User.Identity.Name}"
                                : $"Пользователь {user.Id} отключен администратором {User.Identity.Name}");
                    }

                    return Ok();
                }

                return Forbid("Данное действие доступно только администраторам");
            }

            return BadRequest(ModelState);
        }
        
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}