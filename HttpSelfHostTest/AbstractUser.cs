using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpSelfHostTest
{
    public abstract class AbstractUser
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual int Age { get; set; }
        //public abstract AbstractUser GetUser(int id);
    }
}
