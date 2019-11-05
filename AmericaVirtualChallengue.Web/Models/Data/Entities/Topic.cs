using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AmericaVirtualChallengue.Web.Models.Data.Entities
{
    public class Topic : IEntity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        public string Description { get; set; }
    }
}
