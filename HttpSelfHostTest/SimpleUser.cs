using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpSelfHostTest
{
    public class SimpleUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }


    public class UserRepository : IAbstractUserRepository<SimpleUser>
    {
        List<SimpleUser> list = new List<SimpleUser>()
        {
            new SimpleUser() {Id = 1, Age =  30, Name =  "Vasya"},
            new SimpleUser() {Id = 2, Age =  31, Name =  "Petya"},
        };

        public SimpleUser GetAbstractUser(int id)
        {
            return list.Where(i => i.Id == id).FirstOrDefault();
        }

        public SimpleUser GetUser(int id)
        {
            return list.Where(i => i.Id == id).FirstOrDefault();
        }
        
    }
}
