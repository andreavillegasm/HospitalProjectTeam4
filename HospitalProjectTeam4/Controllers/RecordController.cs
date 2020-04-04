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
//needed for other sign in feature classes
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace HospitalProjectTeam4.Controllers
{
    public class RecordController : Controller

    {
        private HospitalProjectContext db = new HospitalProjectContext();
        // GET: Record
        public ActionResult Index()
        {

            return View();
        }
        public ActionResult List()
        {
            
            string query = "Select * from Records join Bookings on Records.BookingID = Bookings.BookingID order by BookingDate Desc";

            //Checks to see if the query is being sent
            Debug.WriteLine(query);

            //Grabs all the records plus the associated information regarding that specific record
            List<Record> allrecords = db.Records.SqlQuery(query).ToList();

            ListRecords viewmodel = new ListRecords();
            viewmodel.records = allrecords;
          

            return View(viewmodel);
        }

        public ActionResult Show(int? id)
        {
            Debug.WriteLine(id);

            //Get the information regarding one record id 
            var first_query = "select * from Records where RecordID= @id";
            var first_parameter = new SqlParameter("@id", id);
            Record recordinfo = db.Records.SqlQuery(first_query,first_parameter ).FirstOrDefault();
            if (recordinfo == null)
            {
                return HttpNotFound();
            }
            var second_parameter = new SqlParameter("@id", id);
            //Find information about the booking related to that  record
            var second_query = "select * from Bookings join Records on Bookings.BookingID = Records.BookingID where RecordID= @id";
            Booking bookinginfo = db.Bookings.SqlQuery(second_query, second_parameter).FirstOrDefault();
            if (bookinginfo == null)
            {
                return HttpNotFound();
            }


            ListRecords viewmodel = new ListRecords();
            viewmodel.recordinfo = recordinfo;
            viewmodel.bookinginfo = bookinginfo;

            return View(viewmodel);
        }
        
        //Display the Add page
        public ActionResult Add()
        {
            return View();
        }

        //ADD A NEW RECORD TO THE DATABASE
        //Method is only called when it comes from a form submission
        //Parameters are all the values from the form
        [HttpPost]
        public ActionResult New(string recordName, string recordType, string recordContent, int bookingID)
        {
            

            //CHECK IF THE VALUES ARE BEING PASSED INTO THE METHOD
            Debug.WriteLine("The values passed into the method are: " + recordName + ", " + recordType + ", " + recordContent + ", " + bookingID);

            //CREATE THE INSERT INTO QUERY
            string query = "insert into Records (RecordName, RecordType, RecordContent, BookingID) values (@recordName, @recordType, @recordContent, @bookingID)";

            //Binding the variables to the parameters
            SqlParameter[] sqlparams = new SqlParameter[4]; //0,1,2,3 pieces of information to add
            //each piece of information is a key and value pair
            sqlparams[0] = new SqlParameter("@recordName", recordName);
            sqlparams[1] = new SqlParameter("@recordType", recordType);
            sqlparams[2] = new SqlParameter("@recordContent", recordContent);
            sqlparams[3] = new SqlParameter("@bookingID", bookingID);

            //RUN THE QUERY WITH THE PARAMETERS 
            db.Database.ExecuteSqlCommand(query, sqlparams);

            return RedirectToAction("List");
        }
    }
}