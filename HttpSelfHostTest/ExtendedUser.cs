using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpSelfHostTest
{
    public class ExtendedUser : SimpleUser
    {
        private string language { get; set; }

        public string Language { get { return language; } set { language = value; } }


    }


    public class ExtendedUserRepository : IAbstractUserRepository<ExtendedUser>
    {
        List<ExtendedUser> list = new List<ExtendedUser>()
        {
            new ExtendedUser() {Id = 1, Age =  30, Name =  "Vasya", Language = "ru"},
            new ExtendedUser() {Id = 2, Age =  31, Name =  "Petya", Language = "idish"}
        };

        public ExtendedUser GetAbstractUser(int id)
        {
            return list.Where(i => i.Id == id).FirstOrDefault();
        }

        public SimpleUser GetUser(int id)
        {
            return list.Where(i => i.Id == id).FirstOrDefault();
        }

        //public void Show(int id)
        //{
        //    Console.WriteLine(String.Format(" {0} - {1} -{2}", GetUser(id).Age, GetUser(id).Name, GetUser(id).Language));
        //}
    }
}
