using System;
using System.Collections.Generic;
using System.Data;
//required for SqlParameter class
using System.Data.SqlClient;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HospitalProjectTeam4.Data;
using HospitalProjectTeam4.Models;
using HospitalProjectTeam4.Models.ViewModels;
using System.Diagnostics;
using System.Globalization; //for cultureinfo.invariantculture
//needed for await
using System.Threading.Tasks;
//needed for other sign in feature classes
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace HospitalProjectTeam4.Controllers
{
    public class DoctorController : Controller
    {
        //need this to work with the login functionalities
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        //reference how the Account Controller instantiates the controller class with SignInManager and UserManager
        // GET: Doctor
        private HospitalProjectContext db = new HospitalProjectContext();
        //parameterless constructor function
        public DoctorController() { }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Add(string UserEmail, string UserPassword, string DoctorFName, string DoctorMName, string DoctorLName, string DoctorDOB, string DoctorPhone, string DoctorAltPhone)
        {
            //before creating a doctor, we would like to create a user.
            //this user will be linked with a doctor.
            ApplicationUser NewUser = new ApplicationUser();
            NewUser.UserName = UserEmail;
            NewUser.Email = UserEmail;
            //code interpreted from AccountController.cs Register Method
            IdentityResult result = await UserManager.CreateAsync(NewUser, UserPassword);

            if (result.Succeeded)
            {
                //need to find the user we just created -- get the ID
                string Id = NewUser.Id; //what was the id of the new account?
                //link this id to the Owner
                string DoctorID = Id;



                Doctor NewDoctor = new Doctor();
                NewDoctor.DoctorID = Id;
                NewDoctor.DoctorFName = DoctorFName;
                NewDoctor.DoctorMName = DoctorMName;
                NewDoctor.DoctorLName = DoctorLName;
                NewDoctor.DoctorBirthDate = DoctorDOB;
                NewDoctor.DoctorPhone = DoctorPhone;
                NewDoctor.DoctorAltPhone = DoctorAltPhone;

                //SQL equivalent : INSERT INTO GROOMERS (GroomerFname, .. ) VALUES (@GroomerFname..)
                db.Doctors.Add(NewDoctor);

                db.SaveChanges();
            }
            else
            {
                //Simple way of displaying errors
                ViewBag.ErrorMessage = "Something Went Wrong!";
                ViewBag.Errors = new List<string>();
                foreach (var error in result.Errors)
                {
                    ViewBag.Errors.Add(error);
                }
            }


            return View();
        }

        /////////////
        //how to get the UserManager and SignInManager from the server
        /////////////
        public DoctorController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
    }
}