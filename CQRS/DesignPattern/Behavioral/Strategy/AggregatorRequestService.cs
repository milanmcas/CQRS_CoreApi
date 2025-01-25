namespace CQRS.DesignPattern.Behavioral.Strategy
{
    /// <summary>
    /// List<ptfDanniFattore> - AggregatorRequest
    /// Strategy Pattern + Liskov Substitution Principle
    /// </summary>
    public class AggregatorRequest
    {

    }
    public enum AggregatorRequestType
    {
        LongFlow,
        FastQuote,
        InstantQuote
    }
    public abstract class AggregatorRequestService
    {
        public abstract AggregatorRequest CreateRequest();
    }
    public class LongFlowRequestService : AggregatorRequestService
    {
        public override AggregatorRequest CreateRequest()
        {
            return new AggregatorRequest();
        }
    }
    public class FastQuoteRequestService : AggregatorRequestService
    {
        public override AggregatorRequest CreateRequest()
        {
            return new AggregatorRequest();
        }
    }
    public class InstantQuoteRequestService : AggregatorRequestService
    {
        public override AggregatorRequest CreateRequest()
        {
            return new AggregatorRequest();
        }
    }
    public interface IAggregatorRequestServiceFactory
    {
        AggregatorRequestService GetRequestService(AggregatorRequestType requestType);
    }
    public class AggregatorRequestServiceFactory : IAggregatorRequestServiceFactory
    {
        public AggregatorRequestService GetRequestService(AggregatorRequestType requestType)
        {
            switch (requestType)
            {
                case AggregatorRequestType.LongFlow:
                    return new LongFlowRequestService();
                    case AggregatorRequestType.FastQuote:
                    return new FastQuoteRequestService();
                    case AggregatorRequestType.InstantQuote:
                    return new InstantQuoteRequestService();
                    default:
                    return new FastQuoteRequestService();
            }
        }
    }
    public interface IRequestProcessor
    {
        void ProcessRequest(AggregatorRequestType requestType);
    }
    public class RequestProcessor : IRequestProcessor
    {
        private readonly IAggregatorRequestServiceFactory _aggregatorRequestServiceFactory;
        public RequestProcessor(IAggregatorRequestServiceFactory aggregatorRequestServiceFactory)
        {
            _aggregatorRequestServiceFactory= aggregatorRequestServiceFactory;
        }
        public void ProcessRequest(AggregatorRequestType requestType)
        {
            var request=_aggregatorRequestServiceFactory.GetRequestService(requestType).CreateRequest();
        }
    }
}
