using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AmericaVirtualChallengue.Web.Models.Data.Entities
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [MaxLength(60, ErrorMessage = "The field {0} only can contain {1} characters length")]
        public string Name { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal Price { get; set; }

        [Display(Name = "Image")]
        public string ImageUrl { get; set; }

        [Display(Name = "Is Availabe?")]
        public bool IsAvailabe { get; set; }

        //[Display(Name = "Option")]
        //public Option OptionId { get; set; }
    }
}
