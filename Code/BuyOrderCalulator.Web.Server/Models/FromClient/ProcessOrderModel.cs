using BuyOrderCalc.Domain;
using System;
using System.Collections.Generic;

namespace BuyOrderCalc.Web.Server.Models.FromClient
{
    public class ProcessOrderModel: UserCredModel
    {
        public Guid OrderGuid { get; set; }
        public OrderStatus State { get; set; }
    }
}
