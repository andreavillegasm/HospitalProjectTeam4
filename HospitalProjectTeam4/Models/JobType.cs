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
    public class JobType
    {
        [Key]
        public int JobTypeID { get; set; }
        public string JobTypeName { get; set; }

        //representing the "Many" in (One Job Type to Many Job Postings)
        public ICollection<JobPosting> JobPostings { get; set; }
    }
}