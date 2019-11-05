using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AmericaVirtualChallengue.Web.Models.Data.Entities
{
    public class SalesDetail
    {
        public int Id { get; set; }

        //[Required(ErrorMessage = "The field {0} is required")]
        //public Sale SaleId { get; set; }

        //[Required(ErrorMessage = "The field {0} is required")]
        //[Display(Name = "Product")]
        //public Product ProductId { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal Price { get; set; }

        [Display(Name = "Quantity")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        public int Quantity { get; set; }

        //[Display(Name = "User")]
        //[Required(ErrorMessage = "The field {0} is required")]
        //public User User { get; set; }
    }
}
