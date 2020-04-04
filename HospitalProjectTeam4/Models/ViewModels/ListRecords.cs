using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalProjectTeam4.Models.ViewModels
{
    public class ListRecords
    {
        //List of all the records
        public virtual List<Record> records { get; set; }

        //Record info based on booking id
        public virtual Record recordinfo { get; set; }

        //Booking info based on booking id
        public virtual Booking bookinginfo { get; set; }

       

    }
}