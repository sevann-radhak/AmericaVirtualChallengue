namespace AmericaVirtualChallengue.Web.Models.ModelsView
{
    using AmericaVirtualChallengue.Web.Models.Data.Entities;
    using System.Collections.Generic;

    public class OrderViewModel : Order
    {
        public List<OrderDetail> OrderDetails { get; set; }
    }
}
