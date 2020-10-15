using System;

namespace BuyOrderCalc.Web.Server.Helpers
{
    public static class Helper
    {
        public static double GetPercentage(double number, double percent)
        {
            return (percent / 100) * number;
        }
    }
}
