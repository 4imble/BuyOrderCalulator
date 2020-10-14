namespace BuyOrderCalc.Domain
{
    public class CondensedOre : Entity
    {
        public string Name { get; set; }
        public string ApiId { get; set; }
        public Ore Ore { get; set; }
    }
}
