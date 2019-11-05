namespace AmericaVirtualChallengue.Web.Models.Data.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Sale
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal Price { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Date")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        public User User { get; set; }

        public Option Option { get; set; }
    }
}
