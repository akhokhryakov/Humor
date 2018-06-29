using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Http.SelfHost;
using Castle.MicroKernel;
using Castle.Windsor;
using NEventStore;

namespace HttpSelfHostTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new WindsorContainer();
            container.Install(new AppInstaller());

            HttpSelfHostConfiguration config = new HttpSelfHostConfiguration("http://localhost:56785");
            config.Routes.MapHttpRoute(
                name: "Default",
                routeTemplate: "api/{controller}/{id}",
                constraints: new
                {
                    id = "^[12]$"
                },
                defaults: new {Id = RouteParameter.Optional }
            );

            config.Services.Replace(typeof(IHttpControllerActivator), new WindsorHttpControllerActivator(container));

            var server = new HttpSelfHostServer(config);
            server.OpenAsync().Wait();
            Console.WriteLine("For close press any key");
            Console.ReadKey();
            server.CloseAsync();

        }
    }



    public class SimpleController : ApiController
    {
        //private readonly IUserRepository m_Repository;
        private readonly IAbstractUserRepository<SimpleUser> m_Repository;

        public SimpleController(IAbstractUserRepository<SimpleUser> repository)
        {
            this.m_Repository = repository;
        }

        public static IStoreEvents CreateMemoryConnection()
        {
            return Wireup.Init()
                .UsingInMemoryPersistence()
                .InitializeStorageEngine()
                .Build();
        }

        public SimpleUser Get(int id)
        {
            SimpleEvent<HttpRequestMessage> ev = EventFactory<HttpContext>.CreatEvent(base.Request);

            using (var store = CreateMemoryConnection())
            {
                using (var stream = store.OpenStream(ev.StreamId, 0))
                {
                    stream.Add(new EventMessage {Body = ev.Obj});
                    stream.CommitChanges(ev.EventId);
                }

                using (var stream = store.OpenStream(ev.StreamId, 0))
                {
                    foreach (var commited in stream.CommittedEvents)
                    {
                        Console.WriteLine(((HttpRequestMessage)commited.Body).RequestUri);
                    }
                }
            }

            return m_Repository.GetAbstractUser(id);
        }
    }


    public class ExtendedController : ApiController
    {
        private readonly IAbstractUserRepository<ExtendedUser> m_Repository;

        public ExtendedController(IAbstractUserRepository<ExtendedUser> repository)
        {
            this.m_Repository = repository;
        }


        public ExtendedUser Get(int id)
        {
            return m_Repository.GetAbstractUser(id);
        }
    }
    }
