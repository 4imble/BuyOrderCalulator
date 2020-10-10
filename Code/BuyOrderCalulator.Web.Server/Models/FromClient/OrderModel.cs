using System.Collections.Generic;

namespace BuyOrderCalc.Web.Server.Models.FromClient
{
    public class OrderModel: UserCredModel
    {
        public List<SaleItem> SaleItems { get; set; }
    }
}
