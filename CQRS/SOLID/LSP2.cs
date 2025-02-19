namespace CQRS.SOLID
{
    public class BaseSubscriber
    {
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password {  get; set; } = null!;

    }
    public class StandardSubscriber : BaseSubscriber
    {
        public void AccessToLimitTitles()
        {

        }
    }
    public class PremiumSubscriber : BaseSubscriber
    {
        public void AccessToUnLimitTitles()
        {

        }
        public void GiveAccessToFamilyMembers()
        {

        }
    }
}
