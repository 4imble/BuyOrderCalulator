namespace BuyOrderCalc.Web.Server.Models.ToClient
{
    public class ItemModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UnitPrice { get; set; }
        public int Quantity { get; set; }
        public int ReorderLevel { get; set; }

        public int TypeId { get; set; }
        public string TypeName { get; set; }

        public int SupplyTypeId { get; set; }
        public string SupplyTypeName { get; set; }
        public int PricePercentModifier { get; set; }
        public double CorpCreditPercent { get; set; }
    }
}