using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Core
{
    public interface ILocatorFactory
    {
        Locator GetLocator();
    }

    public class LocatorFactory : ILocatorFactory
    {
        private readonly TinyIoC.TinyIoCContainer Container;

        public LocatorFactory(IIoCContainerFactory iocContainerFactory)
        {
            this.Container = iocContainerFactory.GetContainer();
        }

        public Locator GetLocator()
        {
            Func<string, object> resolver =
              property =>
              {
                  string name = string.Format("TestApp.ViewModels.{0}", property);
                  var type = Type.GetType(name);
                  return this.Container.Resolve(type);
              };
            return new Locator(resolver);
        }
    }
}
