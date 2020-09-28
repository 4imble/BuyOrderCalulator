using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using BuyOrderCalc.Domain;
using BuyOrderCalc.EntityFramework;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;

namespace BuyOrderCalc.Web.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImportController : ControllerBase
    {
        readonly DataContext dataContext;

        public ImportController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public void ImportFromCsv()
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create("https://api.eve-echoes-market.com/market-stats/stats.csv");
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

            using StreamReader sr = new StreamReader(resp.GetResponseStream());
            using (var csv = new CsvReader(sr, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<ApiItem>();
                var allItems = dataContext.Items.ToList();

                foreach (var record in records)
                {
                    if (!record.sell.HasValue || !record.buy.HasValue)
                        continue;

                    var item = allItems.SingleOrDefault(x => x.ApiId.ToString() == record.item_id) ?? new Item();
                    item.Name = record.name;
                    item.MarketPrice = (int)((record.sell + record.buy) / 2);

                    if (item.Id == 0)
                    {
                        item.TypeId = 1; //unclassified
                        item.SupplyTypeId = 4; //unwanted
                        dataContext.Items.Add(item);
                    }
                }
            }
        }

        public class ApiItem
        {
            public string item_id { get; set; }
            public string name { get; set; }
            public DateTime time { get; set; }
            public double? sell { get; set; }
            public double? buy { get; set; }
            public double? lowest_sell { get; set; }
            public double? highest_buy { get; set; }
        }
    }
}
