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
            //Debug.WriteLine(doctorid + datebooking + patientid);
            //string BookingTime = "1200";
            DateTime now = DateTime.Now;
            Debug.WriteLine(now);
            Booking book = new Booking();
            book.DoctorID = doctorid;
            book.CurrentDate = now.ToString();
            book.BookingDate = datebooking;
            book.PatientID = patientid;
            db.Bookings.Add(book);
            db.SaveChanges();
            //return View();
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            List<Booking> Bookings;
            Bookings = db.Bookings.ToList();
            return View(Bookings);
        }
        
        public ActionResult Show(int id)
        {


            Booking booking = db.Bookings.FirstOrDefault(b => b.BookingID == id);
            Debug.WriteLine(id);
            return View(booking);
        }
        public ActionResult Update(int id)
        {
            UpdateBooking viewmodel = new UpdateBooking();

            viewmodel.Booking = db.Bookings.FirstOrDefault(booking => booking.BookingID == id);
            viewmodel.Doctors = db.Doctors.ToList();
            viewmodel.Patients = db.Patients.ToList();

            return View(viewmodel);
        }
        [HttpPost]
        public ActionResult Update(int id, int doctorid, string datebooking, int patientid)
        {
            

            Booking booking = db.Bookings.FirstOrDefault(b => b.BookingID == id);

            booking.DoctorID = doctorid;
            booking.BookingDate = datebooking;
            booking.PatientID = patientid;

            db.SaveChanges();

            return RedirectToAction("List");
        }
        public ActionResult Delete(int id)
        {
            string query = "delete from Bookings where BookingID = @id";
            SqlParameter param = new SqlParameter("@id", id);
            db.Database.ExecuteSqlCommand(query, param);
            return RedirectToAction("List");
        }
    }
}