using Microsoft.AspNetCore.Mvc;
using Swarm.Domain;
using Swarm.EntityFramework;
using System.Linq;

namespace Swarm.Web.Server
{
    public class HomeController : Controller
    {
        private readonly DataContext dataContext;

        public HomeController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public IActionResult Index()
        {
            dataContext.Add(new Game());
            return Content(dataContext.Games.Count().ToString());
        }
    }
}
