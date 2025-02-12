namespace CQRS.Resolution.Generic
{
    public interface IGenericService<T>:IService where T : IService
    {

    }
    public class GenericService<T> : IGenericService<T> where T : IService
    {
        private readonly T _implementation;
        public GenericService(T implementation)
        {
            _implementation = implementation;
        }
        void IService.DoWork()
        {
            _implementation.DoWork();
        }
    }
}
