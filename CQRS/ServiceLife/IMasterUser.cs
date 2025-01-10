namespace CQRS.ServiceLife
{
    public interface IMasterUser
    {
        string Name { get; }
        void Print();
    }
    public class MasterUser : IMasterUser
    {
        public Guid a = Guid.Empty;
        private readonly IUserService _userService;
        public MasterUser(IUserService userService) {
            _userService = userService;
            a = Guid.NewGuid();
        }
        public string Name { get; set; }
        public void Print() {
            Name = "Milan";
            Console.WriteLine($"MasterUser - {a} - {_userService.Name}");

            _userService.Print();
        }

    }
}
