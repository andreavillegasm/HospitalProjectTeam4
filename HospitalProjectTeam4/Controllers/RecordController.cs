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
//using HospitalProjectTeam4.Models.ViewModels;
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
            List<Record> records = db.Records.SqlQuery(query).ToList();
            return View(records);
        }
    }
}