using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalProjectTeam4.Models.ViewModels
{
    public class UpdateBooking
    {
        public virtual Booking Booking { get; set; }
        public virtual List<Doctor> Doctors { get; set; }
        //provide a list of patients

       public virtual List<Patient> Patients { get; set; }
    }
}