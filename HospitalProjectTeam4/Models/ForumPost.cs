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
    public class ForumPost
    {
        [Key]
        public int PostID { get; set; }

        //Representing the "One" in (One Patient to Many Posts)
        public string PatientID { get; set; }
        [ForeignKey("PatientID")]

        public virtual Patient Patient { get; set; }

        public DateTime PostingDate { get; set; }
        public string PostingTitle { get; set; }
        public string PostingContent { get; set; }
        public int PostingState { get; set; }

        public string PostingCategory { get; set; }

        //Representing the "Many" in (Many Replies to one ForumPost)
        public ICollection<ForumReply> ForumReply { get; set; }





    }
}