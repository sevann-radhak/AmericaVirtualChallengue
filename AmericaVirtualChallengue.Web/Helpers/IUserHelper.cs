namespace AmericaVirtualChallengue.Web.Helpers
{
    using AmericaVirtualChallengue.Web.Models.Data.Entities;
    using Microsoft.AspNetCore.Identity;
    using System.Threading.Tasks;

    public interface IUserHelper
    {
        Task<User> FindByEmailAsync(string email);
        Task<IdentityResult> CreateAsync(User user, string password);
    }
}
