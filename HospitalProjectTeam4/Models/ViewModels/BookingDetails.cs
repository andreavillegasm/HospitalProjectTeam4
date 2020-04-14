using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalProjectTeam4.Models.ViewModels
{
    public class BookingDetails
    {
        //Details of a specific booking
        public virtual Booking bookinginfo { get; set; }

        //List of Records
        public virtual List<Record> records { get; set; }
    }
}