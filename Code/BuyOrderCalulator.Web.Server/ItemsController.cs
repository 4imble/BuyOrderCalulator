using Microsoft.AspNetCore.Mvc;
using BuyOrderCalc.Domain;
using BuyOrderCalc.EntityFramework;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using BuyOrderCalc.Web.Server.Buillders;

namespace BuyOrderCalc.Web.Server
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : Controller
    {
        private readonly DataContext dataContext;

        public ItemsController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        [HttpGet]
        public List<ItemModel> Get()
        {
            var items = dataContext.Items
                .Include(x => x.Type)
                .Include(x => x.SupplyType);

            return items.Select(x => x.BuildForView()).ToList();
        }
    }
}
