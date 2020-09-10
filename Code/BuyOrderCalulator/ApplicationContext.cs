using SimpleInjector;

namespace BuyOrderCalc
{
    public class ApplicationContext
    {
        public static readonly Container Container;

        static ApplicationContext()
        {
            Container = new Container();
        }

        public static T Resolve<T>() where T : class
        {
            return Container.GetInstance<T>();
        }
    }
}
