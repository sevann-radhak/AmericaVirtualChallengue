namespace AmericaVirtualChallengue.Web.Helpers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Models.Data.Entities;
    using Models.ModelsView;

    public interface IUserHelper
    {
        Task<User> FindByEmailAsync(string email);
        
        Task<IdentityResult> CreateAsync(User user, string password);

        Task<SignInResult> LoginAsync(LoginViewModel model);

        Task LogoutAsync();

        Task<IdentityResult> UpdateUserAsync(User user);

        Task<IdentityResult> ChangePasswordAsync(User user, string oldPassword, string newPassword);

        Task<SignInResult> ValidatePasswordAsync(User user, string password);
    }
}
