using Microsoft.AspNetCore.Mvc.Filters;
using Swarm.EntityFramework;

namespace Swarm.Web.Server
{
    public class UnitOfWork : ActionFilterAttribute
    {
        readonly DataContext dataContext;

        public UnitOfWork(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public override void OnActionExecuted(ActionExecutedContext actionContext)
        {
            if (actionContext != null && actionContext.Exception == null)
            {
                dataContext.SaveChanges();
            }
        }
    }
}
