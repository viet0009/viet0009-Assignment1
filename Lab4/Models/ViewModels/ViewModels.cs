using Lab4.Models;
using System.Collections.Generic;

namespace Lab4.ViewModels
{
    public class StudentsViewModel
    {
        public IList<Student> Students { get; set; }
        public Student Selected { get; set; }
    }

    public class CommunitiesViewModel
    {
        public IList<Community> Communities { get; set; }
        public Community Selected { get; set; }
    }

    public class StudentMembershipsViewModel
    {
        public Student Student { get; set; }
        public IList<Community> Communities { get; set; }
    }

}
