using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagementTask.Models
{
    public partial class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Cnic { get; set; }
        public string Subject { get; set; }
        public string Mobile { get; set; }
        [DataType(DataType.Date)]
        public DateTime? JoiningDate { get; set; }
    }
}
