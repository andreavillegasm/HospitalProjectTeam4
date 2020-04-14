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
    public class Donation
    {
        /*
          Many donations can be made by one user.
          User can be of many types like doctor, patient, visitor or a registered user.
        */
        [Key]
        public int DonationID { get; set; }
        public string UserfName { get; set; }
        
        public string Userlname { get; set; }
        public DateTime UserDOB { get; set; }
        public string Useremail { get; set; }

        public DateTime Date { get; set; }
        public int Amount { get; set; }
        //Amount is established as Cents rather than dollars(i.e. 2000c = $20.00)
        //currency is CANADIAN (cad)
        public string CardType { get; set; }
        public string CardNumber { get; set; }
        public string DonationStatus { get; set; }

    }
}

// i wanted to this feature but i was getting confused about linking 
//donation to different users(doctors, patients,visitors) through identity user.