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
    public class HospitalProjectContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx

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

    }
}