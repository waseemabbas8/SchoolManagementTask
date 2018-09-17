using System;
using System.Collections.Generic;

namespace SchoolManagementTask.Models
{
    public partial class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Class { get; set; }
        public DateTime? Dob { get; set; }
        public string Cnic { get; set; }
        public string Gender { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
    }
}
