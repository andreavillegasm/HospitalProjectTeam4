using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using HospitalProjectTeam4.Models;
using HospitalProjectTeam4.Data;
using System.Diagnostics;

namespace HospitalProjectTeam4
{
   
        //I've split the "ApplicationUserManager" class defined in "IdentityConfig.cs"
        //into this file. My plan is to create extra methods that help
        //authenticate the different kinds of users associated with an account
        //NOTE: you also have to modify the "ApplicationUserManager" class so
        //that it is partial as well.
        public partial class ApplicationUserManager : UserManager<ApplicationUser>
        {
            //You could argue having a database reference in this class is a
            //"separation of concerns" issue
            //The other AplicationUserManager class has a static reference to the db
            //static just means we can access the db without "instantiating" the class
            //like we're doing below.
            private HospitalProjectContext db = new HospitalProjectContext();
            private string userid
            {
                get { return HttpContext.Current.User.Identity.GetUserId() != null ? HttpContext.Current.User.Identity.GetUserId() : ""; }
            }
            private bool isLoggedIn
            {
                get { return HttpContext.Current.User.Identity.IsAuthenticated ? true : false; }
            }
            //public property accessors
            public bool IsLoggedIn
            {
                get { return isLoggedIn; }
            }


            //I only want to use this field inside this class, so it is private
            private ApplicationUser GetUser()
            {
                //it will hurt your brain to think about
                //instantiating a class within the class definition itself
                //although, it is possible.
                //I need to do this to easily grab the user.
                //This is considered better practice than using the db to grab the user
                ApplicationUserManager manager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
                if (userid == "") return null;
                return (manager.FindById(userid));
            }
            
            //Is the user an admin?
            public bool IsUserAdmin()
            {
                ApplicationUser user = GetUser();
                if (user == null) return false;
                if (user.IsAdmin) return true;
                return false;
            }

        //Who is the user?

            public bool IsUserDoctor()
            {
                ApplicationUser user = GetUser();
                if (user == null) return false;
                if (user.Doctor != null) return true;
                return false;
            }

            public bool IsUserPatient()
            {
                ApplicationUser user = GetUser();
                if (user == null) return false;
                if (user.Patient != null) return true;
                return false;
            }

            public bool IsUserHospitalStaff()
            {
            ApplicationUser user = GetUser();
            if (user == null) return false;
            if (user.HospitalStaff != null) return true;
            return false;
            }


            public string TestMethod()
            {

                return ("Test Successful");
            }
        }
}
