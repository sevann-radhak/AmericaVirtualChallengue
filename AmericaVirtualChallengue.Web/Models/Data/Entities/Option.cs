namespace AmericaVirtualChallengue.Web.Models.Data.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class Option : IEntity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [MaxLength(60, ErrorMessage = "The field {0} only can contain {1} characters length")]
        public string Description { get; set; }
    }
}
