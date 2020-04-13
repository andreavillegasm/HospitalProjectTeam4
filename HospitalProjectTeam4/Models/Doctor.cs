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
    public class Doctor
    {
        [Key, ForeignKey("ApplicationUser")]
        public string DoctorID { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public string DoctorFName { get; set; }
        public string DoctorMName { get; set; }
        public string DoctorLName { get; set; }
        public string DoctorBirthDate { get; set; }
        public string DoctorEmail { get; set; }
        public string DoctorPhone { get; set; }
        public string DoctorAltPhone { get; set; }

        //Representing the "Many" in (Many Bookings to one Doctor)
        public ICollection<Booking> Booking { get; set; }

        //Representing the "Many" in (Many Replies to one Doctor)
        public ICollection<ForumReply> ForumReply { get; set; }
    }
}