namespace AmericaVirtualChallengue.Web.Models.Data.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Microsoft.AspNetCore.Identity;

    public class User : IdentityUser
    {
        [Required(ErrorMessage = "The field {0} is required")]
        [MaxLength(60, ErrorMessage = "The field {0} only can contain {1} characters length")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [MaxLength(60, ErrorMessage = "The field {0} only can contain {1} characters length")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        
        [Display(Name = "Full Name")]
        public string FullName { get { return $"{this.FirstName} {this.LastName}"; } }

        [NotMapped]
        [Display(Name = "Is Admin?")]
        public bool IsAdmin { get; set; }
    }
}
