using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalProjectTeam4.Models.ViewModels
{
    public class ListOnlineCheckIns
    {

        //List of all the CheckIns
        public virtual List<OnlineCheckIn> OnlineCheckIns { get; set; }

        //Checkin info info based on booking id
        public virtual OnlineCheckIn OnlineCheckIninfo { get; set; }

        //CheckIn info based on booking id
        public virtual Booking bookinginfo { get; set; }
    }
}