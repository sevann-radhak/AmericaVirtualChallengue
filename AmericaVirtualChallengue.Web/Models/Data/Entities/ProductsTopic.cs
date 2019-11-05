namespace AmericaVirtualChallengue.Web.Models.Data.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class ProductsTopic : IEntity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        public Topic Topic { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        public Product Product { get; set; }
    }
}
