using BuyOrderCalc.Domain;
using BuyOrderCalc.Web.Server.Helpers;
using BuyOrderCalc.Web.Server.Models.ToClient;
using System;

namespace BuyOrderCalc.Web.Server.Builders
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
                UnitPrice = Helper.GetPercentage(item.MarketPrice, item.SupplyType.PricePercentModifier),
                MarketPrice = item.MarketPrice,
                TypeId = item.TypeId,
                TypeName = item.Type.Name,
                SupplyTypeId = item.SupplyType.Id,
                SupplyTypeName = item.SupplyType.Name,
                PricePercentModifier = item.SupplyType.PricePercentModifier,
                CorpCreditPercent = item.SupplyType.CorpCreditPercent,
                IsActive = item.IsActive
            };
        }
    }
}