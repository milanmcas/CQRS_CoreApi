using Microsoft.EntityFrameworkCore.Diagnostics;

namespace CQRS.Interceptor
{
    public class DemoDbCommandInterceptor:IDbCommandInterceptor
    {

    }
    public class DemoDbConnectionInterceptor : IDbConnectionInterceptor
    {

    }
    public class DemoDbTransactionInterceptor : IDbTransactionInterceptor
    {

    }
    public class DemoSaveChangesInterceptor : ISaveChangesInterceptor
    {

    }
}
