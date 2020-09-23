using BuyOrderCalc.Domain;
using System;

namespace BuyOrderCalc.Web.Server.Buillders
{
    public static class ItemBuilder
    {
        public static ItemModel BuildForView(this Item item)
        {
            return new ItemModel
            {
                Id = item.Id,
                Name = item.Name,
                Quantity = item.Quantity,
                ReorderLevel = item.ReorderLevel,
                UnitPrice = GetPercentage(item.MarketPrice, item.SupplyType.PricePercentModifier),
                TypeId = item.TypeId,
                TypeName = item.Type.Name,
                SupplyTypeId = item.SupplyType.Id,
                SupplyTypeName = item.SupplyType.Name,
                PricePercentModifier = item.SupplyType.PricePercentModifier,
                CorpCreditPercent = item.SupplyType.CorpCreditPercent
            };

            int GetPercentage(int number, double percent)
            {
                var value = (percent / 100) * number;
                return (int)Math.Ceiling(value);
            }
        }
    }
}