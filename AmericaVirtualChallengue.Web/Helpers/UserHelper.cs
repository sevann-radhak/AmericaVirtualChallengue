namespace AmericaVirtualChallengue.Web.Helpers
{
    using AmericaVirtualChallengue.Web.Models.Data.Entities;
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class UserHelper : IUserHelper
    {
        private readonly UserManager<User> userManager;

        public UserHelper(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<IdentityResult> CreateAsync(User user, string password)
        {
            return await this.userManager.CreateAsync(user, password);
        }

        public async Task<User> FindByEmailAsync(string email)
        {
            return await this.userManager.FindByEmailAsync(email);
        }
    }

}
