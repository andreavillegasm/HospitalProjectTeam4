using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalProjectTeam4.Models;
using System.ComponentModel.DataAnnotations;

namespace HospitalProjectTeam4.Data
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    // The application user class is the class that is used to describe someone who is logged in
    

    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        //A logged in user could be a Doctor
        public virtual Doctor Doctor { get; set; }

        //A logged in user could be a Patient
        public virtual Patient Patient { get; set; }

        //A logged in user could be Hospital Staff
        public virtual HospitalStaff HospitalStaff { get; set; }


        //A logged in user could be an Admin
        //note: this could be a separate admin entity
        //a separate admin entity would be suitable if there was
        //more information about an admin we would need to store
        //or relationships to admins that we would need to represent
        //that's not the case here, so this column is fine
        public bool IsAdmin { get; set; }

       

    }

    //Becomes subclass of IdentityDBContenxt
    public class HospitalProjectContext : IdentityDbContext<ApplicationUser>
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx


        //LOCAL CONNECTION STRING
        public HospitalProjectContext() : base("name=HospitalProjectContext")
        {
        }
        public static HospitalProjectContext Create()
        {
            return new HospitalProjectContext();
        }

        public System.Data.Entity.DbSet<HospitalProjectTeam4.Models.Record> Records { get; set; }
        public System.Data.Entity.DbSet<HospitalProjectTeam4.Models.Booking> Bookings { get; set; }
        public System.Data.Entity.DbSet<HospitalProjectTeam4.Models.Patient> Patients { get; set; }
        public System.Data.Entity.DbSet<HospitalProjectTeam4.Models.Doctor> Doctors { get; set; }

        public System.Data.Entity.DbSet<HospitalProjectTeam4.Models.ForumPost> ForumPosts { get; set; }
        public System.Data.Entity.DbSet<HospitalProjectTeam4.Models.ForumReply> ForumReplies { get; set; }
        public System.Data.Entity.DbSet<HospitalProjectTeam4.Models.HospitalStaff> hospitalStaffs { get; set; }
        public System.Data.Entity.DbSet<HospitalProjectTeam4.Models.LostFound> lostFounds { get; set; }

        public System.Data.Entity.DbSet<HospitalProjectTeam4.Models.News> News { get; set; }
        public System.Data.Entity.DbSet<HospitalProjectTeam4.Models.Category> Categories { get; set; }

        public System.Data.Entity.DbSet<HospitalProjectTeam4.Models.OnlineCheckIn> OnlineCheckIns { get; set; }
        public System.Data.Entity.DbSet<HospitalProjectTeam4.Models.JobPosting> JobPostings { get; set; }
        public System.Data.Entity.DbSet<HospitalProjectTeam4.Models.CareersForm> CareersForms { get; set; }
        public System.Data.Entity.DbSet<HospitalProjectTeam4.Models.JobDepartment> JobDepartments { get; set; }
        public System.Data.Entity.DbSet<HospitalProjectTeam4.Models.JobType> JobTypes { get; set; }


    }
}