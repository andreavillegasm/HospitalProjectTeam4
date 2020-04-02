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
    public class Booking
    {
        [Key]
        public int BookingID { get; set; }
        public DateTime CurrentDate { get; set; }
        public DateTime BookingDate { get; set; }

        //Representing the "One" in (Many Bookings to one Patient)
        public int PatientID { get; set; }
        [ForeignKey("PatientID")]

        public virtual Patient Patient { get; set; }

        //Representing the "One" in (Many Bookings to one Doctor)
        public int DoctorID { get; set; }
        [ForeignKey("DoctorID")]

        public virtual Doctor Doctor { get; set; }


        //Representing the "Many" in (One Booking to many Records)
        public ICollection<Record> Record { get; set; }
    }
}