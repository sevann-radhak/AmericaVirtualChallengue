namespace AmericaVirtualChallengue.Web.Models.ModelsView
{
    using System.Collections.Generic;
    using Data.Entities;

    public class ProductViewAPI : Product
    {
        public List<Topic> Topics { get; set; }
    }
}
