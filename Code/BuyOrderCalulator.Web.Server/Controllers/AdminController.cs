using BuyOrderCalc.EntityFramework;
using BuyOrderCalc.Web.Server.Models.FromClient;
using BuyOrderCalc.Web.Server.Models.ToClient;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BuyOrderCalc.Web.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class AdminController : ControllerBase
    {
        private readonly DataContext dataContext;

        public AdminController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        [HttpPost]
        [Route("ToggleActive")]
        public void ToggleActive(ToggleActiveModel model)
        {
            var item = dataContext.Items.SingleOrDefault(x => x.Id == model.ItemId);
            item.IsActive = !item.IsActive;
            dataContext.SaveChanges();
        }
        
        [HttpPost]
        [Route("SaveItem")]
        public void SaveItem(SaveItemModel model)
        {
            var item = dataContext.Items.SingleOrDefault(x => x.Id == model.ItemId);
            item.TypeId = model.ItemTypeId;
            item.SupplyTypeId = model.SupplyTypeId;
            dataContext.SaveChanges();
        }

        [HttpGet]
        [Route("CommonData")]
        public CommonDataModel CommonData()
        {
            return new CommonDataModel
            {
                ItemTypes = dataContext.ItemTypes.ToList(),
                SupplyTypes = dataContext.SupplyTypes.ToList()
            };
        }
    }
}
