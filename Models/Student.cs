using Microsoft.AspNetCore.Authentication;

namespace StudentCourseManagment.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime EnrollmentDate { get; set; }
        public bool IsActive { get; set; } = true;

        public string FullName => $"{FirstName} {LastName}";  

    }
}
