using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FamiliesWebAPI.Models;
using FamiliesWebAPI.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FamiliesWebAPI.Data.Impl
{
    public class WebUserService : IUserService
    {
        private readonly FamiliesContext familiesContext;

        public WebUserService(FamiliesContext familiesContext)
        {
            this.familiesContext = familiesContext;
        }

        public async Task<User> ValidateUserAsync(string userName, string password)
        {
            User first = await familiesContext.Users.FirstOrDefaultAsync(user => user.Username.Equals(userName));
            if (first == null)
            {
                throw new Exception("User not found");
            }

            if (!first.Password.Equals(password))
            {
                throw new Exception("Incorrect password");
            }

            return first;
        }
    }
}