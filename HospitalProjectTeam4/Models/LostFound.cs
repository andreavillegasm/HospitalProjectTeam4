using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using HospitalProjectTeam4.Data;

namespace HospitalProjectTeam4.Models
{
    public class LostFound
    {
        [Key]
        public int LostFoundID { get; set; }
        public string LostorFound { get; set; }
        public string LostFoundItem { get; set; }
        public string LostFoundDate { get; set; }
        public string LostFoundCategory { get; set; }
        public string LostFoundColor { get; set; }
        public string LostFoundPerson { get; set; }
        public string LostFoundNote { get; set; }
        public string picextension { get; set; }
       
        public string PatientID { get; set; }
        [ForeignKey("PatientID")]
        public virtual Patient Patient { get; set; }
    }
}