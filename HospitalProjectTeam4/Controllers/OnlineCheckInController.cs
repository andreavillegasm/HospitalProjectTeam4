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
using System.IO;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
namespace HospitalProjectTeam4.Controllers
{
    public class OnlineCheckInConroller : Controller
    {
        private HospitalProjectContext db = new HospitalProjectContext();

        // GET: OnlineCheckIn
        public ActionResult Index()
        {

            return View();
        }

        //List Online CheckIn
        public ActionResult List()
        {

            string query = "Select * from OnlineCheckIns join Bookings on OnlineCheckIns.BookingID = Bookings.BookingID order by BookingDate Desc";

            Debug.WriteLine(query);

            //List<OnlineCheckIn> OnlineCheckIns = db.OnlineCheckIns(query, sqlparams.ToArray()).ToList();
            List<OnlineCheckIn> allonlinecheckins = db.OnlineCheckIns.SqlQuery(query).ToList();

            ListOnlineCheckIns viewmodel = new ListOnlineCheckIns();
            viewmodel.OnlineCheckIns = allonlinecheckins;


            return View(viewmodel);
        }

        //pagination   (resource Christine Bittle: Pet Grooming Project)

        //   int perpage = 5;
        //  int OnlineCheckIncount = OnlineCheckIn.Count();
        //  int maxpage = (int)Math.Ceiling((decimal)OnlineCheckIncount / perpage) - 1;
        //      if (maxpage < 0) maxpage = 0;
        //      if (pagenum < 0) pagenum = 0;
        //      if (pagenum > maxpage) pagenum = maxpage;
        //      int start = (int)(perpage * pagenum);
        //      ViewData["pagenum"] = pagenum;
        //      ViewData["pagesummary"] = "";
        //      if (maxpage > 0)
        //      {
        //          ViewData["pagesummary"] = (pagenum + 1) + " of " + (maxpage + 1);
        //          List<SqlParameter> newparams = new List<SqlParameter>();

        //if (petsearchkey != "")
        //{
        //   newparams.Add(new SqlParameter("@searchkey", "%" + onlinecheckinsearchkey + "%"));
        //   ViewData["onlinecheckinsearchkey"] = onlinecheckinsearchkey;
        // }
        //      newparams.Add( new SqlParameter("@start", start));
        //       newparams.Add( new SqlParameter("@perpage", perpage));
        //       string pagedquery = query + " order by OnlineCheckInID offset @start rows fetch first @perpage rows only ";
        //       Debug.WriteLine(pagedquery);
        //       Debug.WriteLine("offset "+start);
        //       Debug.WriteLine("fetch first "+perpage);
        //       OnlineCheckIn = db.OnlineCheckIn.SqlQuery(pagedquery, newparams.ToArray()).ToList();
        //         }
        //End of Pagination Algorithm

        //Show Online CheckIn
        public ActionResult Show(int? id)
        {
            Debug.WriteLine(id);

            //Get the information regarding one record id 
            var first_query = "select * from OnlineCheckIns where CheckInID= @id";
            var first_parameter = new SqlParameter("@id", id);
            OnlineCheckIn OnlineCheckIninfo = db.OnlineCheckIns.SqlQuery(first_query, first_parameter).FirstOrDefault();
            if (OnlineCheckIninfo == null)
            {
                return HttpNotFound();
            }
            var second_parameter = new SqlParameter("@id", id);
            //Find information about the booking related to that  record
            var second_query = "select * from Bookings join CheckIns on Bookings.BookingID = CheckIns.BookingID where CheckInID= @id";
            Booking bookinginfo = db.Bookings.SqlQuery(second_query, second_parameter).FirstOrDefault();
            if (bookinginfo == null)
            {
                return HttpNotFound();
            }


            ListOnlineCheckIns viewmodel = new ListOnlineCheckIns();
            viewmodel.OnlineCheckIninfo = OnlineCheckIninfo;
            viewmodel.bookinginfo = bookinginfo;

            return View(viewmodel);
        }

        // Add OnlineCheckIns 
        public ActionResult Add()
        {
            return View();
        }

        //Adds a new OnlineCheckIn in database with onlinecheckin form parameters
        [HttpPost]
        public ActionResult New(string patientFName, string patientLName, DateTime Date, int patientID, int bookingID, HttpPostedFileBase OnlineCheckInFile)
        {


            Debug.WriteLine("The values passed into the method are: " + patientFName + ", " + patientLName + ", " + Date + ", " + patientID + ", " + bookingID);

            //query
            string query = "insert into OnlineCheckIns (patientFName, patientLName, Date, patientID, bookingID) values (@patientFName, @patientLName, @Date, @patientID, @bookingID)";


            SqlParameter[] sqlparams = new SqlParameter[5]; //0,1,2,3,4 pieces of information to add

            //Key and value pairs
            sqlparams[0] = new SqlParameter("@patientFName", patientFName);
            sqlparams[1] = new SqlParameter("@patientLName", patientLName);
            sqlparams[2] = new SqlParameter("@Date", Date);
            sqlparams[3] = new SqlParameter("@patientID", patientID);
            sqlparams[4] = new SqlParameter("@bookingID", bookingID);


            db.Database.ExecuteSqlCommand(query, sqlparams);



            return RedirectToAction("List");
        }

        //Shows the UPDATE online check in page
        [HttpPost]
        public ActionResult Update(int CheckInID, string patientFName, string patientLName, DateTime Date, int patientID, int bookingID)
        {

            Debug.WriteLine("Trying to edit the values: " + patientFName + ", " + patientLName + ", " + patientID + " " + bookingID);

            string query = "update OnlineCheckIns set patientFName=@patientFName, patientLName=@patientLName,  bookingID=@bookingID where OnlineCheckInID=@id";
            SqlParameter[] sqlparams = new SqlParameter[4];
            sqlparams[0] = new SqlParameter("@patientFName", patientFName);
            sqlparams[1] = new SqlParameter("@patientLName", patientLName);
            sqlparams[2] = new SqlParameter("@bookingID", bookingID);
            sqlparams[3] = new SqlParameter("@id", CheckInID);

            db.Database.ExecuteSqlCommand(query, sqlparams);


            return RedirectToAction("Show/" + CheckInID);
        }


        //Shows the DELETE Online CheckIns from the database
        [HttpPost]
        public ActionResult Delete(int CheckInID)
        {
            string query = "Delete from OnlineCheckIn where OnlineCheckInID=@id";
            SqlParameter param = new SqlParameter("@id", CheckInID);
            db.Database.ExecuteSqlCommand(query, param);


            return RedirectToAction("List");
        }
    }
    
}