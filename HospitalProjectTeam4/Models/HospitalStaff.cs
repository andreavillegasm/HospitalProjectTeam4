using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using HospitalProjectTeam4.Data;

namespace HospitalProjectTeam4.Models
{
    public class HospitalStaff
    {
        [Key]
        public int StaffID { get; set; }
        public string StaffFName { get; set; }
        public string StaffMNmae { get; set; }
        public string StaffNmae { get; set; }
        public string StaffBirthDate { get; set; }
        public string StaffEmail { get; set; }
        public string StaffPhone { get; set; }

        public ICollection<Booking> Booking { get; set; }
    }
}