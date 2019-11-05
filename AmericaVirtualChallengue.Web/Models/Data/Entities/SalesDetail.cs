using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AmericaVirtualChallengue.Web.Models.Data.Entities
{
    public class SalesDetail : IEntity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        public Sale Sale { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        public Product Product { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal Price { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        public int Quantity { get; set; }
    }
}
