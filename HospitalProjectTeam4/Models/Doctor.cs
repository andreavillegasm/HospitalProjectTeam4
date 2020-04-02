using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//Install  entity framework 6 on Tools > Manage Nuget Packages > Microsoft Entity Framework (ver 6.4)
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace HospitalProjectTeam4.Models
{
    public class Doctor
    {
        [Key]
        public int DoctorID { get; set; }
        public string DoctorFName { get; set; }
        public string DoctorMNmae { get; set; }
        public string DoctorLNmae { get; set; }
        public DateTime DoctorBirthDate { get; set; }
        public DateTime DoctorEmail { get; set; }
        public string DoctorPhone { get; set; }
        public string DoctorAltPhone { get; set; }

        //Representing the "Many" in (Many Bookings to one Doctor)
        public ICollection<Booking> Booking { get; set; }

    }
}