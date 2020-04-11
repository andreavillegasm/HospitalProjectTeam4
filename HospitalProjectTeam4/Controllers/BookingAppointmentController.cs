using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data.Entity;
using HospitalProjectTeam4.Data;
using HospitalProjectTeam4.Models;
using HospitalProjectTeam4.Models.ViewModels;
using System.Globalization;

namespace HospitalProjectTeam4.Controllers
{
    public class BookingAppointmentController : Controller
    {
        // GET: BookingAppointment

        private HospitalProjectContext db = new HospitalProjectContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            
            AddBooking viewmodel = new AddBooking();

            viewmodel.Doctors = db.Doctors.ToList();
            viewmodel.Patients = db.Patients.ToList();
            
            return View(viewmodel);
        }
        [HttpPost]
        public ActionResult Add(int doctorid,string datebooking,int patientid)
        {
            Debug.WriteLine(doctorid + datebooking + patientid);
            //string BookingTime = "1200";
            Booking book = new Booking();
            book.DoctorID = doctorid;
            book.BookingDate = datebooking;
            book.PatientID = patientid;
            db.Bookings.Add(book);
            db.SaveChanges();

            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            List<Booking> Bookings;
            Bookings = db.Bookings.ToList();
            return View(Bookings);
        }
    }
}