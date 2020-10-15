using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using BuyOrderCalc.Domain;
using BuyOrderCalc.EntityFramework;
using BuyOrderCalc.Web.Server.Helpers;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;

namespace BuyOrderCalc.Web.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImportController : ControllerBase
    {
        readonly DataContext dataContext;
        List<RefinementSkill> refinementSkills;

        public ImportController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public void ImportFromCsv()
        {
            ImportEvePrices();
            RecalculateTrueOrePrices();
        }

        private void RecalculateTrueOrePrices()
        {
            var localPath = Directory.GetCurrentDirectory();
            using StreamReader sr = new StreamReader(localPath + "\\orevalues.csv");
            using (var csv = new CsvReader(sr, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<OreItem>();
                var allItems = dataContext.Items.Include(x => x.SupplyType).ToList();

                var TritaniumPrice = GetPrice(allItems.Single(x => x.Name == "Tritanium"));
                var PyeritePrice = GetPrice(allItems.Single(x => x.Name == "Pyerite"));
                var MexallonPrice = GetPrice(allItems.Single(x => x.Name == "Mexallon"));
                var IsogenPrice = GetPrice(allItems.Single(x => x.Name == "Isogen"));
                var NocxiumPrice = GetPrice(allItems.Single(x => x.Name == "Nocxium"));
                var ZydrinePrice = GetPrice(allItems.Single(x => x.Name == "Zydrine"));
                var MegacytePrice = GetPrice(allItems.Single(x => x.Name == "Megacyte"));
                var MorphitePrice = GetPrice(allItems.Single(x => x.Name == "Morphite"));

                refinementSkills = dataContext.RefinementSkills.ToList();

                foreach (var record in records)
                {
                    var ore = allItems.Single(x => x.Name == record.ORETYPE);
                    ore.MarketPrice = 0;
                    ore.MarketPrice += GetYieldForOneOre(record.Tritanium, record.RARITY) * TritaniumPrice;
                    ore.MarketPrice += GetYieldForOneOre(record.Pyerite, record.RARITY) * PyeritePrice;
                    ore.MarketPrice += GetYieldForOneOre(record.Mexallon, record.RARITY) * MexallonPrice;
                    ore.MarketPrice += GetYieldForOneOre(record.Isogen, record.RARITY) * IsogenPrice;
                    ore.MarketPrice += GetYieldForOneOre(record.Nocxium, record.RARITY) * NocxiumPrice;
                    ore.MarketPrice += GetYieldForOneOre(record.Zydrine, record.RARITY) * ZydrinePrice;
                    ore.MarketPrice += GetYieldForOneOre(record.Megacyte, record.RARITY) * MegacytePrice;
                    ore.MarketPrice += GetYieldForOneOre(record.Morphite, record.RARITY) * MorphitePrice;

                    var compressedOre = allItems.Single(x => x.Name == "Compressed " + record.ORETYPE);
                    compressedOre.MarketPrice = ore.MarketPrice * 10;

                    ore.SupplyTypeId = 5;
                    compressedOre.SupplyTypeId = 5;
                }

                dataContext.SaveChanges();
            }
        }

        private double GetYieldForOneOre(string amount, string oreQuality)
        {
            var quality = (OreQuality)Enum.Parse(typeof(OreQuality), oreQuality);
            var hundredPercentBasedOn30 = double.Parse(amount) * 100 / 30;
            var skillEfficiency = refinementSkills.Single(x => x.Quality == quality).Efficiency;
            return Helper.GetPercentage(hundredPercentBasedOn30, skillEfficiency) / 100;
        }

        private double GetPrice(Item item)
        {
            return Helper.GetPercentage(item.MarketPrice, item.SupplyType.PricePercentModifier);
        }

        private void ImportEvePrices()
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

                    var item = allItems.SingleOrDefault(x => x.ApiId == record.item_id) ?? new Item();
                    item.Name = record.name;
                    item.MarketPrice = (double)((record.sell + record.buy) / 2);

                    if (item.Id == 0)
                    {
                        item.ApiId = record.item_id;
                        item.TypeId = 1; //unclassified
                        item.SupplyTypeId = 4; //unwanted
                        dataContext.Items.Add(item);
                    }
                }
            }
            dataContext.SaveChanges();
        }

        public class OreItem
        {
            public string ORETYPE { get; set; }
            public string RARITY { get; set; }
            public string Volume { get; set; }
            public string Tritanium { get; set; }
            public string Pyerite { get; set; }
            public string Mexallon { get; set; }
            public string Isogen { get; set; }
            public string Nocxium { get; set; }
            public string Zydrine { get; set; }
            public string Megacyte { get; set; }
            public string Morphite { get; set; }

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
