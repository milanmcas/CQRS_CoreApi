using SolrNet.Attributes;

namespace CQRS.Models
{
    public class NewProduct
    {
        [SolrUniqueKey("id")]
        public string Id { get; set; } = null!;
        [SolrField("name")]
        public string Name { get; set; } = null!;
        [SolrField("manu")]
        public string? Manufacturer { get; set; }
        [SolrField("desc")]
        public string? Description { get; set; }
        [SolrField("cat")]
        
        public ICollection<string>? Categories { get; set; }
        [SolrField("price")]
        public decimal Price { get; set; }
        [SolrField("inStock")]
        public bool InStock { get; set; }
    }
}
