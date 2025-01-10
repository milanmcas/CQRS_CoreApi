namespace CQRS.ServiceLife
{
    public interface IUserService
    {
        string Name { get; set; }
        int Age { get; set; }
        void Print();
    }
    public class UserService: IUserService
    {
        public Guid a = Guid.Empty;
        public string Name {  get; set; }
        public int Age { get; set; }
        public UserService() {
            a = Guid.NewGuid();
            
        }
        public void Print() {
            Console.WriteLine($"UserService - {a} - {Name}");
        }
    }
}
