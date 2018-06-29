using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpSelfHostTest
{
    public interface IUserRepository
    {
        SimpleUser GetUser(int id);
    }


    public interface IAbstractUserRepository<T> where T: class
    {
        T GetAbstractUser(int id);
    }
}
