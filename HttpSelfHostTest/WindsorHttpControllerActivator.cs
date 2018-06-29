using Castle.MicroKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using Castle.MicroKernel.Lifestyle;
using Castle.Windsor;

namespace HttpSelfHostTest
{
    internal class WindsorHttpControllerActivator : IHttpControllerActivator
    {
        private readonly IWindsorContainer _container;
        //private readonly IKernel m_Kernel;
        //private readonly IHttpControllerActivator _activator;

        public WindsorHttpControllerActivator(IWindsorContainer container)
        {
            if (container == null) throw new ArgumentNullException("container");
            this._container = container;
            //if (kernel == null) throw new ArgumentNullException("kernel");
            //m_Kernel = kernel;
        }

        public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
        {
            var controller = (IHttpController) this._container.Resolve(controllerType);
            request.RegisterForDispose(
                new Release(
                    () => this._container.Release(controller)));

            return controller;

            //if (controllerType == typeof(SimpleController)) return new SimpleController(new UserRepository());
            //else return null;
            //var scope = m_Kernel.BeginScope();
            //request.RegisterForDispose(scope);
            //var controller = (IHttpController)m_Kernel.Resolve(controllerType);
            //return (IHttpController)m_Kernel.Resolve(controllerType);
        }

        public class Release : IDisposable
        {
            private readonly Action _release;

            public Release(Action release)
            {
                this._release = release;
            }

            public void Dispose()
            {
                this._release();
            }
        }
    }
}
