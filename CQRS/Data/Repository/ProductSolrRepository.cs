using CQRS.Models;
using SolrNet;

namespace CQRS.Data.Repository
{
    public class ProductSolrRepository : IProductSolrRepository
    {
        private readonly ISolrOperations<NewProduct> _solr;
        public ProductSolrRepository(ISolrOperations<NewProduct> solr)
        {
            _solr = solr;
        }
        async Task IProductSolrRepository.Add(NewProduct product)
        {
            await _solr.AddAsync(product??new NewProduct());
            await _solr.CommitAsync();
        }

        async Task IProductSolrRepository.Delete(NewProduct product)
        {
            await _solr.DeleteAsync(product);
            await _solr.CommitAsync();
        }

        async Task<NewProduct> IProductSolrRepository.GetById(string Id)
        {
            var solrResult =await _solr.QueryAsync(new SolrQueryByField("id", Id));
            if (solrResult != null)
                return solrResult.FirstOrDefault()??new NewProduct();
            return new NewProduct();
        }

        async Task<IEnumerable<NewProduct>> IProductSolrRepository.Search(string searchNameString)
        {
            if (!string.IsNullOrEmpty(searchNameString))
                return await _solr.QueryAsync(new SolrQueryByField("name_str", $"name_str:*{searchNameString.Replace(' ', '*')}*"));
            else
                return await _solr.QueryAsync(SolrQuery.All);
        }
    }
}
