using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AmericaVirtualChallengue.Web.Models.Data.Entities
{
    public class ProductsTopic
    {
        public int Id { get; set; }

        //[Required(ErrorMessage = "The field {0} is required")]
        //[Display(Name = "Topic")]
        //public Topic TopicId { get; set; }

        //[Required(ErrorMessage = "The field {0} is required")]
        //[Display(Name = "Product")]
        //public Product ProductId { get; set; }
    }
}
