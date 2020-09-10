using BuyOrderCalc.Domain;

namespace BuyOrderCalc.Web.Server.Buillders
{
    public static class ItemBuilder
    {
        public static ItemModel BuildForView(this Item item)
        {
            return new ItemModel
            {
                Name = item.Name,
                Quantity = item.Quantity,
                ReorderCreditValue = item.ReorderCreditValue,
                ReorderLevel = item.ReorderLevel,
                TakingOrders = item.TakingOrders,
                UnitPrice = item.UnitPrice,
                TypeId = item.TypeId,
                TypeName = item.Type.Name
            };
        }
    }
}