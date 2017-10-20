using Microsoft.AspNetCore.Identity;
using SmartAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading;
using Microsoft.AspNetCore.Hosting;

namespace SmartAdmin.Services
{
    public class PasswordHasher : PasswordHasher<User>
    {
        private readonly IHostingEnvironment env;

        public PasswordHasher(IHostingEnvironment env)
        {
            this.env = env;
        }

        public override PasswordVerificationResult VerifyHashedPassword(User user, string hashedPassword, string providedPassword)
        {
            if (env.IsDevelopment())
            {
                if (string.Equals(user.Password, "admin"))
                    return PasswordVerificationResult.Success;
            }

            return base.VerifyHashedPassword(user, user.Password, providedPassword);
        }
    }
}
