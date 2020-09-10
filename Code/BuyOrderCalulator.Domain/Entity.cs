using System;

namespace BuyOrderCalc.Domain
{
    public abstract class Entity
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
    }
}
