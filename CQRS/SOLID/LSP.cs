namespace CQRS.SOLID
{
    public class LSP
    {
    }
    public class Brand
    {
        public string BrandCode { get; set; }
        public string BrandName { get; set; }
        public Brand(string BrandCode, string BrandName)
        {
            this.BrandCode = BrandCode;
            this.BrandName = BrandName;
        }
    }
    public class Model: Brand
    {
        public string ModelName { get; set; }
        public string ModelCode { get; set; }
        public string ModelDesc { get; set; }
        public Model(string BrandCode, string BrandName, string ModelName, string ModelCode):base(BrandCode, BrandName)
        {

        }
    }
    public class Version : Model
    {
        public string VersionCode { get; set; }
        public string VersionName { get; set; }
        public Version(string VersionCode, string VersionName, string BrandCode, string BrandName, string ModelName, string ModelCode) : 
            base(BrandCode, BrandName, ModelName, ModelCode)
        {

        }

    }
}
