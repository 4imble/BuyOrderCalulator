using System;
using System.Collections.Generic;
using System.Text;

namespace BuyOrderCalc.Domain
{
    public class Ore : Entity
    {
        public string Name { get; set; }
        public string ApiId { get; set; }
        public List<OreMineral> Items { get; set; } = new List<OreMineral>();
    }
}
