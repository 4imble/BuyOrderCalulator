using System;

namespace BuyOrderCalc.Web.Server.Helpers
{
    public static class Helper
    {
        public static int GetPercentage(int number, double percent)
        {
            var value = (percent / 100) * number;
            return (int)Math.Ceiling(value);
        }
    }
}
