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
        public async Task<ActionResult> Add(string user, string emailaddress, string password, string confirmpassword, string firstname, string middlename, string lastname, string dob, string phonenumber, string altphonenumber)
        {
            //Stores data in master user table
            ApplicationUser NewUser = new ApplicationUser();
            NewUser.UserName = emailaddress;
            NewUser.Email = emailaddress;
            //Pass data to table
            IdentityResult result = await UserManager.CreateAsync(NewUser, password);
            //If it succeed
            if (result.Succeeded)
            {
                //If registered user is patient
                if (user == "Patient")
                {
                    Patient newpatient = new Patient();
                    string id = NewUser.Id;
                    newpatient.PatientID = id;
                    newpatient.PatientFName = firstname;
                    newpatient.PatientMName = middlename;
                    newpatient.PatientLName = lastname;
                    newpatient.PatientBirthDate = dob;
                    newpatient.PatientEmail = emailaddress;
                    newpatient.PatientPhone = phonenumber;
                    newpatient.PatientAltPhone = altphonenumber;
                    db.Patients.Add(newpatient);
                    db.SaveChanges();
                }
                //registered user is doctor
                else if(user == "Doctor")
                {
                    Doctor newdoctor = new Doctor();
                    string id = NewUser.Id;
                    newdoctor.DoctorID = id;
                    newdoctor.DoctorFName = firstname;
                    newdoctor.DoctorMName = middlename;
                    newdoctor.DoctorLName = lastname;
                    newdoctor.DoctorBirthDate = dob;
                    newdoctor.DoctorEmail = emailaddress;
                    newdoctor.DoctorPhone = phonenumber;
                    newdoctor.DoctorAltPhone = altphonenumber;
                    db.Doctors.Add(newdoctor);
                    db.SaveChanges();

                }
                //If registered user is hspital staff
                else if(user == "Hospitalstaff")
                {
                    HospitalStaff newstaff = new HospitalStaff();
                    string id = NewUser.Id;
                    newstaff.StaffID = id;
                    newstaff.StaffFName = firstname;
                    newstaff.StaffMNmae = middlename;
                    newstaff.StaffNmae = lastname;
                    newstaff.StaffBirthDate = dob;
                    newstaff.StaffEmail = emailaddress;
                    newstaff.StaffPhone = phonenumber;
                    db.hospitalStaffs.Add(newstaff);
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