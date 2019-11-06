namespace AmericaVirtualChallengue.Web.Models.Data.Entities
{
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Identity;

    public class User : IdentityUser
    {
        [Required(ErrorMessage = "The field {0} is required")]
        [MaxLength(60, ErrorMessage = "The field {0} only can contain {1} characters length")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [MaxLength(60, ErrorMessage = "The field {0} only can contain {1} characters length")]
        public string LastName { get; set; }
        
        [Display(Name = "Full Name")]
        public string FullName { get { return $"{this.FirstName} {this.LastName}"; } }

    }
}
