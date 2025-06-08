using CQRS.Models;

namespace CQRS.Data.Repository
{
    public interface IProductSolrRepository
    {
        Task Add(NewProduct product);
        Task Delete(NewProduct product);
        Task<NewProduct> GetById(string Id);
        Task<IEnumerable<NewProduct>> Search(string searchNameString);
    }
}
