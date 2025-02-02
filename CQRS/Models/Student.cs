using CQRS.DesignPattern.Builder;

namespace CQRS.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public List<StudentCourses> StudentCourses { get; } = [];
        public List<Book> Books { get; set; } = []; // Navigation property for many-to-many
    }
    public class Book
    {
        public int Id { get; set; }
        public string CourseName { get; set; }
        //public List<StudentCourses> StudentCourses { get; } = [];
        public List<Student> Students { get; set; } = []; // Navigation property for many-to-many
    }
    public class StudentCourses
    {
        public int StudentId { get; set; }
        public int BookId { get; set; }
        public Student Student { get; set; } = null!;
        public Book Book { get; set; } = null!;
    }
}
