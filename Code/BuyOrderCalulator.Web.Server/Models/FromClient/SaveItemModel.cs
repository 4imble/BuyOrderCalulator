namespace BuyOrderCalc.Web.Server.Models.FromClient
{
    public class SaveItemModel: UserCredModel
    {
        public int ItemId { get; set; }
        public int ItemTypeId { get; set; }
        public int SupplyTypeId { get; set; }
    }
}
