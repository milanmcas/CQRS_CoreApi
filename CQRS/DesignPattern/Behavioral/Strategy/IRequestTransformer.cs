namespace CQRS.DesignPattern.Behavioral.Strategy
{
    public delegate string EligibilityAction(string factor);
    public interface IRequestTransformer1
    {
        string CreateLFRequest(string factor);
        string CreateFQRequest(string factor);
        string CreateIQRequest(string factor);

    }
    public class RequestTransformer1 : IRequestTransformer1
    {
        string IRequestTransformer1.CreateFQRequest(string factor)
        {
            return factor;
        }

        string IRequestTransformer1.CreateIQRequest(string factor)
        {
            return factor;
        }

        string IRequestTransformer1.CreateLFRequest(string factor)
        {
            return factor;
        }
    }
    public interface IRequestTransformer
    {
        string CreateRequest(string factor);
        
    }
    public class LFRequestTransformer : IRequestTransformer
    {
        string IRequestTransformer.CreateRequest(string factor)
        {
            return factor;
        }
    }
    public class FQRequestTransformer : IRequestTransformer
    {
        string IRequestTransformer.CreateRequest(string factor)
        {
            return factor;
        }
    }
    public class IQRequestTransformer : IRequestTransformer
    {
        string IRequestTransformer.CreateRequest(string factor)
        {
            return factor;
        }
    }
    public interface IRequestProcessor1
    {
        void ProcessRequest(int requestType);
    }
    public class RequestProcessor1: IRequestProcessor1
    {
        Dictionary<int, IRequestTransformer> aniaProcess = new Dictionary<int, IRequestTransformer>();
        public RequestProcessor1()
        {
            aniaProcess = new Dictionary<int, IRequestTransformer>()
            {
                {1,new LFRequestTransformer() },
                {2,new FQRequestTransformer() },
                {3,new IQRequestTransformer() }
            };
        }        

        void IRequestProcessor1.ProcessRequest(int requestType)
        {
            aniaProcess[requestType].CreateRequest("factor");
        }
    }
    public interface IAniaEligibility
    {
        int AniaEligibility(string aniaRequest);
    }
    public class AniaEligibility : IAniaEligibility
    {
        int IAniaEligibility.AniaEligibility(string aniaRequest)
        {
            return 2;
        }
    }
    public interface IRequestProcessor2
    {
        string ProcessRequest(int requestType,string factors);
    }
    public class RequestProcessor2 : IRequestProcessor2
    {
        Dictionary<int, Func<string, string>> aniaProcess;
        private readonly IRequestTransformer1 _requestTransformer1;
        private readonly IAniaEligibility _aniaEligibility;
        public RequestProcessor2(IRequestTransformer1 requestTransformer1, IAniaEligibility aniaEligibility)
        {
            _requestTransformer1 = requestTransformer1;
            aniaProcess = new Dictionary<int, Func<string, string>>()
            {
                {1,_requestTransformer1.CreateLFRequest },
                {2,_requestTransformer1.CreateFQRequest },
                {3,_requestTransformer1.CreateIQRequest }
            };
            this._aniaEligibility = aniaEligibility;
        }
        string IRequestProcessor2.ProcessRequest(int requestType, string factors)
        {
            return aniaProcess[_aniaEligibility.AniaEligibility("request")](factors);
        }
    }
}
