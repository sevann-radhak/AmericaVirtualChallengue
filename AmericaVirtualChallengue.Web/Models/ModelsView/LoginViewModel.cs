namespace AmericaVirtualChallengue.Web.Models.ModelsView
{
    using System.ComponentModel.DataAnnotations;

    public class LoginViewModel
    {
        [Required(ErrorMessage = "The field {0} is required")]
        [EmailAddress]
        public string Username { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [MinLength(6)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }

}
