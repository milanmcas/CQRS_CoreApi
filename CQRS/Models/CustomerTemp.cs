using System.ComponentModel.DataAnnotations.Schema;

namespace CQRS.Models
{
    public class CustomerTemp
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }

        // This property will not be mapped to the database
        [NotMapped]
        public DateTime DateOfBirth { get; set; }

        // This property will not be mapped to the database using Ignore in dbcontext
        public string TemporaryData { get; set; }
    }
    public class AuditLog
    {
        public int Id { get; set; }
    }

    [NotMapped]
    public class AuditLog1
    {
        public int AuditLogId { get; set; }
        public string Action { get; set; }
        public DateTime Timestamp { get; set; }
    }

}
