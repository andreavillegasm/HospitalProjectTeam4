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
    public class Patient
    {
        [Key, ForeignKey("ApplicationUser")]
        public string PatientID { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public string PatientFName { get; set; }
        public string PatientMName { get; set; }
        public string PatientLName { get; set; }
        public string PatientBirthDate { get; set; }
        public string PatientEmail { get; set; }
        public string PatientPhone { get; set; }

        public string PatientAltPhone { get; set; }

        //Representing the "Many" in (Many Bookings to one Patient)
        public ICollection<Booking> Booking { get; set; }

        //Representing the "Many" in (Many Posts to one Patient)
        public ICollection<ForumPost> ForumPost { get; set; }
    }
}