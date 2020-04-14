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
    public class JobPosting
    {

        // A job posting can be written by an admin or authorized user. 
        // A job posting can be read by an admin, a registered user or guest.
        // A job posting must reference to a department.
        // A job posting must reference to a job type


        [Key]
        public int JobPostID { get; set; }
        public string JobPostTitle { get; set; }
        public string JobPostDescription { get; set; }
        public DateTime JobPostDate { get; set; }

        //Representing the "One" in (One Job Type in one Job Posting)
        public int JobTypeID { get; set; }
        [ForeignKey("JobTypeID")]

        public virtual JobType JobType { get; set; }

        //Representing the "One" in (One Job Department in one Job Posting)
        public int JobDeptID { get; set; }
        [ForeignKey("JobDeptID")]

        public virtual JobDepartment JobDepartment { get; set; }


        //A Job Posting must reference to a User
        //public string UserId { get; set; }
        //[ForeignKey("UserId")]
        // public virtual ApplicationUser User { get; set; }



    }
}