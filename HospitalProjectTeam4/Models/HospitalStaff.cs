using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//Install  entity framework 6 on Tools > Manage Nuget Packages > Microsoft Entity Framework (ver 6.4)
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using HospitalProjectTeam4.Data;

namespace HospitalProjectTeam4.Models
{
    public class HospitalStaff
    {
        [Key, ForeignKey("ApplicationUser")]
        public string StaffID { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public string StaffFName { get; set; }
        public string StaffMNmae { get; set; }
        public string StaffNmae { get; set; }
        public string StaffBirthDate { get; set; }
        public string StaffEmail { get; set; }
        public string StaffPhone { get; set; }

        public ICollection<Booking> Booking { get; set; }
    }
}