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
        //I was confused here how to take the userfname, userlname, DOB, email from
        //different users like pateints, doctors, visitors and from registered user models.
        // I know that we were to link the donation to the identity model. But, was not sure 
        //about how to do that. The pagination lab was clear to me i did that for news list page. 
        //The simple CRUD is now very clear to me.
        //I tried to understand your code of the updated petgrooming repo but the code
        //seemed totally confusing to me. I tried to understand those terms. 
        // the terms ApplicationSignInManager 
        // ApplicationUserManager what these are doing i don't know about them. That is why
        //this code i tried to look into the whole repository but i was unable to understand
        //the whole flow of the code behind the admin functionality.
        //In the groomcontroller, how you have used the how to get the UserManager and 
        //SignInManager from the server
        //Also, in the starting of the controller you have written some terms that 
        // you said are for using the above two.
        //In the GroomServiceController, you have used nothing of this type.Why?
        //I understand all the other code. But, this ApplicationUserManager and ApplicationSignInManager
        //making me confused. How this little code on the end of the controller is affecting 
        // create, read, update and delte pages?
        // I clearly understand which part of the CRUD is visible to whom??
        //But, in code how you are making those restrictions??

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