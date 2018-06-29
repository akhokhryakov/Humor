using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpSelfHostTest
{
    public class SimpleEvent<T> where T: class
    {
        public Guid EventId { get; set; }
        public Guid StreamId { get; set; }
        public T Obj { get; set; }
    }


    public class EventFactory<T> where T :class
    {
        public static SimpleEvent<T> CreatEvent<T>(T obj) where  T : class
        {
            return new SimpleEvent<T>()
            {
                EventId = Guid.NewGuid(),
                StreamId = Guid.NewGuid(),
                Obj = obj
            };
        }
    }
}
