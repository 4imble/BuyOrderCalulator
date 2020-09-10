using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BuyOrderCalc.Web.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
#if DEBUG
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("  -------------------------------------------------");
            Console.WriteLine($"    To debug - attach to 'BuyOrderCalc' process '{Process.GetCurrentProcess().Id}'");
            Console.WriteLine("  -------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.White;
#endif

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
