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
    public class Record
    {
        [Key]
        public int RecordID { get; set; }
        public string RecordName { get; set; }
        public string RecordType { get; set; }
        public string RecordContent { get; set; }



        //Attachment Available
        public int HasFile { get; set; }
        public string FileExtension { get; set; }



        //Representing the "One" in (Many Records to one Booking)
        public int BookingID { get; set; }
        [ForeignKey("BookingID")]

        public virtual Booking Booking { get; set; }

    }
}