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
    public class OnlineCheckIn
    {

        // An online check-in can be done by an admin or registered user. 
        // An online check-in can be read by an admin or a registered user.
        // An online check-in must reference to a specific patient.
        // An online check-in must reference to a specific booking appointment.

        [Key]
        public int CheckInID { get; set; }
        public string PatientFName { get; set; }
        public string PatientLName { get; set; }
        public DateTime Date { get; set; }

        //Representing the "One" in (One check-in to one- booking)
        public int BookingID { get; set; }
        //[ForeignKey("BookingID")]

        public virtual Booking Booking { get; set; }

        //Representing the "One" in (One patient to one check-in)

        public int PatientID { get; set; }
        //[ForeignKey("PatientID")]

        public virtual Patient Patient { get; set; }

    }
}