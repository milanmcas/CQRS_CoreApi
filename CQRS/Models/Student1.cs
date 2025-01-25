namespace CQRS.Models
{
    public class Student1
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Teacher> Teacher { get; } = []; //collection navigation property
    }
    public class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Student1> Student { get; } = []; //collection navigation property
    }

    public class TeacherStudent
    {
        public int StudentId { get; set; }  //foreign key property
        public int TeacherId { get; set; }  //foreign key property

        public Student Student_R { get; set; } = null!; //Reference navigation property
        public Teacher Teacher_R { get; set; } = null!; //Reference navigation property
    }
}
