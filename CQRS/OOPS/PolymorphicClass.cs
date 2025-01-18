using System.Transactions;

namespace CQRS.OOPS
{
    public class Brand
    {
        public string BrandCode {  get; set; }
        public string BrandName { get; set; }
        public virtual void GetData(string BrandName)
        {
            BrandCode = "A";
            BrandCode = "Fuel";
            //fetching data
            //assigning values to properties
            Console.WriteLine("Brand GetData()");
        }
    }
    public class Model : Brand
    {
        public string ModelCode { get; set; }
        public string ModelName { get; set; }
        public override void GetData(string BrandName)
        {
            base.GetData(BrandName);
            Console.WriteLine("Model GetData()");
            
            //fetching Model data
            //assigning values to properties
            ModelCode = "A";
            ModelName = "mode";
        }
    }
    public class Version : Model {
        public string VersionCode { get; set; }
        public string versionName { get; set; }
        public override void GetData(string BrandName)
        {
            base.GetData(BrandName);
            //fetching version data
            //assigning values to properties
            Console.WriteLine("Version GetData()");
        }
    }
}
