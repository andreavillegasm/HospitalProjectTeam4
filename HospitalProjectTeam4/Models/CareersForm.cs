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
    public class CareersForm
    {

        // A career form can be filled from any user. No need to log in. 
        // A career form can be read by an admin, a registered user or guest.
        // A career form must reference to a job posting.



        [Key]
        public int PostFormId { get; set; }
        public string FormFName { get; set; }
        public string FormLName { get; set; }
        public int FormPhone { get; set; }
        public string FormEmail { get; set; }
        public DateTime FormPostedDate { get; set; }
        public string FormCoverLetter { get; set; }
        public string FormResume { get; set; }
        public int JobPostId { get; set; }

        //Representing the "Many" to (One Career Form to Many Job Postings )

        public ICollection<JobPosting> JobPosting { get; set; }
    }
}