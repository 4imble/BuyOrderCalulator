using Microsoft.AspNetCore.Mvc;
using BuyOrderCalc.EntityFramework;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using BuyOrderCalc.Web.Server.Builders;
using BuyOrderCalc.Web.Server.Models.ToClient;

namespace BuyOrderCalc.Web.Server.Controllers
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
        public List<ItemViewModel> Get()
        {
            var items = dataContext.Items
                //.Where(x => x.IsActive)
                .Include(x => x.Type)
                .Include(x => x.SupplyType);

            return items.Select(x => x.BuildForView()).ToList();
        }
    }
}
