using Microsoft.AspNetCore.Mvc;
using StudentCourseManagment.Models;

namespace StudentCourseManagment.Controllers
{
    public class StudentController : Controller
    {
        private static readonly List<Student> _students = new List<Student>
        {
            new Student { Id = 1, FirstName = "John", LastName = "Doe", Email = "John@example.com", EnrollmentDate = new DateTime(2020, 1, 15) },
            new Student { Id = 2, FirstName = "Jane", LastName = "Smith", Email = "Jane@example.com", EnrollmentDate = new DateTime(2021, 3, 10) },
            new Student { Id = 3, FirstName = "Michael", LastName = "Johnson", Email = "Michael@example.com", EnrollmentDate = new DateTime(2019, 8, 25),IsActive=false }
        };

        public IActionResult Index()
        {
            var students = _students.OrderBy(s => s.LastName).ThenBy(s => s.FirstName).ToList();
            return View(students);
        }

        public IActionResult ActiveOnly()
        {
            var activeStudents = _students.Where(s => s.IsActive).OrderBy(s => s.LastName).ThenBy(s => s.FirstName).ToList();
            return View("Index", activeStudents);
        }
    }
}
