using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using Castle.Facilities.Startable;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;


namespace HttpSelfHostTest
{
    public class AppInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.AddFacility<TypedFactoryFacility>();
            container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel, true));
            container.AddFacility<StartableFacility>(f => f.DeferredStart());

            // WebApi
            container.Register(
                Classes.FromThisAssembly()
                    .IncludeNonPublicTypes()
                    .BasedOn<ApiController>().LifestyleTransient());


           container.Register(
               //Component.For<IUserRepository>().ImplementedBy(typeof(UserRepository)).LifestyleTransient(),
                              Component.For<IAbstractUserRepository<ExtendedUser>>().ImplementedBy(typeof(ExtendedUserRepository)).LifestyleTransient(),
                              Component.For<IAbstractUserRepository<SimpleUser>>().ImplementedBy(typeof(UserRepository)).LifestyleTransient());

            //container.Register(
            //    Component.For<IHttpController>().ImplementedBy<SimpleController>()
            //        .DependsOn(()=> container.Resolve<IUserRepository>()).LifestylePerWebRequest());
            //.LifestylePerWebRequest());
           // container.Register(Component.For<IHttpControllerActivator>().ImplementedBy<WindsorHttpControllerActivator>());
            //new DependencyConventions();

            //throw new NotImplementedException();
        }
    }
}
