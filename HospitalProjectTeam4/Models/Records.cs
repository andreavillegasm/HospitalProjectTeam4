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
    public class Records
    {
        [Key]
        public int RecordID { get; set; }
        public string RecordType { get; set; }
        public string RecordContent { get; set; }



        //Attachment Available 
        public int HasFile { get; set; }
        public string FileExtension { get; set; }



        //Representing the many records to one appointment relationship       
        //public int BookingID { get; set; }
        //[ForeignKey("BookingID")]

        //public virtual Bookings Bookings { get; set; }

    }
}