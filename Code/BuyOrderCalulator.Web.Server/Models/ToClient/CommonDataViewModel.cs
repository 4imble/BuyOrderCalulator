using BuyOrderCalc.Domain;
using System.Collections.Generic;

namespace BuyOrderCalc.Web.Server.Models.ToClient
{
    public class CommonDataViewModel
    {
        public List<ItemType> ItemTypes { get; set; }
        public List<SupplyType> SupplyTypes { get; set; }
    }
}