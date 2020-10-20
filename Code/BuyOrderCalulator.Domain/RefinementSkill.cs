namespace BuyOrderCalc.Domain
{
    public class RefinementSkill : Entity
    {
        public OreQuality Quality { get; set; }
        public double Efficiency { get; set; }
    }

    public enum OreQuality
    {
        Common,
        Uncommon,
        Special,
        Rare,
        Precious
    }
}
