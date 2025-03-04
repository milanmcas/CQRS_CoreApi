using CQRS.Resolution.Generic;
using CQRS.Resolution;
using CQRS.ServiceLife;
using CQRS.Services;
using System.Configuration;

namespace CQRS.OOPS
{
    public static class StaticDIClass
    {
        private static ISingletonService1? _singletonService1;
        private static IGenericService<Service1>? _masterUser;
        internal static volatile int s_curr = 0;
        public static void Initialize(ISingletonService1 singletonService1, IGenericService<Service1> masterUser)
        {
            _singletonService1 = singletonService1;
            _masterUser = masterUser;
        }        
        internal static int GetNext()
        {
            return Interlocked.Increment(ref s_curr);
        }
        public static void Print()
        {
            _singletonService1?.Print();
            _masterUser?.DoWork();
        }

    }
}
