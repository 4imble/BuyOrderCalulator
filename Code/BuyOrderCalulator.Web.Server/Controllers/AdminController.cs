using BuyOrderCalc.EntityFramework;
using BuyOrderCalc.Web.Server.Helpers;
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
        private readonly AuthHelper authHelper;

        public AdminController(DataContext dataContext, AuthHelper authHelper)
        {
            this.dataContext = dataContext;
            this.authHelper = authHelper;
        }

        [HttpPost]
        [Route("ToggleActive")]
        public void ToggleActive(ToggleActiveModel model)
        {
            authHelper.EnsureAdmin(model);

            var item = dataContext.Items.SingleOrDefault(x => x.Id == model.ItemId);
            item.IsActive = !item.IsActive;
            dataContext.SaveChanges();
        }
        
        [HttpPost]
        [Route("SaveItem")]
        public void SaveItem(SaveItemModel model)
        {
            authHelper.EnsureAdmin(model);

            var item = dataContext.Items.SingleOrDefault(x => x.Id == model.ItemId);
            item.TypeId = model.ItemTypeId;
            item.SupplyTypeId = model.SupplyTypeId;
            dataContext.SaveChanges();
        }

        [HttpGet]
        [Route("CommonData")]
        public CommonDataViewModel CommonData()
        {
            return new CommonDataViewModel
            {
                ItemTypes = dataContext.ItemTypes.ToList(),
                SupplyTypes = dataContext.SupplyTypes.ToList()
            };
        }
    }
}
