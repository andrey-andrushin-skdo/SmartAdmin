using Microsoft.AspNetCore.Identity;
using SmartAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading;

namespace SmartAdmin.Services
{
    public class PasswordHasher : IPasswordHasher<User>
    {
        public string HashPassword(User user, string password)
        {
            throw new NotImplementedException();
        }

        public PasswordVerificationResult VerifyHashedPassword(User user, string hashedPassword, string providedPassword)
        {
            return string.Equals(user.Password, hashedPassword, StringComparison.OrdinalIgnoreCase) 
                ? PasswordVerificationResult.Success
                : PasswordVerificationResult.Failed;
        }
    }
}
