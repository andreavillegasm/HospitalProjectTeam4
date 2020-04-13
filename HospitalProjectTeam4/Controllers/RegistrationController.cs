using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
//needed for other sign in feature classes
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using HospitalProjectTeam4.Data;
using HospitalProjectTeam4.Models;
using System.Globalization;
using System.Diagnostics;

namespace HospitalProjectTeam4.Controllers
{
    public class RegistrationController : Controller
    {
        // GET: Registration
        public ActionResult Index()
        {
            return View();
        }

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public RegistrationController() { }

        private HospitalProjectContext db = new HospitalProjectContext();
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Add(string user,string emailaddress,string password,string confirmpassword, string firstname,string middlename,string lastname,string dob,string phonenumber,string altphonenumber)
        {
            ApplicationUser NewUser = new ApplicationUser();
            NewUser.UserName = emailaddress;
            NewUser.Email = emailaddress;
            IdentityResult result = await UserManager.CreateAsync(NewUser,password);
            if(result.Succeeded)
            {
                if(user == "Patient")
                {
                    Patient newpatient = new Patient();
                    string id = NewUser.Id;
                    newpatient.PatientID = id;
                    newpatient.PatientFName = firstname;
                    newpatient.PatientMNmae = middlename;
                    newpatient.PatientLNmae = lastname;
                    newpatient.PatientBirthDate = DateTime.ParseExact(dob, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    newpatient.PatientPhone = phonenumber;
                    newpatient.PatientAltPhone = altphonenumber;
                    db.Patients.Add(newpatient);
                    db.SaveChanges();
                }
                else
                {
                    Debug.WriteLine(user);
                }
            }

            return View();
        }
        public RegistrationController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
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