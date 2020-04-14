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
using Microsoft.AspNet.Identity;
using System.IO;

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

        //This add function will simply retrieve all patients and doctor data and pass it to view
        public ActionResult Add()
        {
            
            AddBooking viewmodel = new AddBooking();
            //Doctor details
            viewmodel.Doctors = db.Doctors.ToList();
            //Patient details
            viewmodel.Patients = db.Patients.ToList();
            
            return View(viewmodel);
        }
        [HttpPost]
        //Add function will will capture data from the view page and add them to BookingAppointment table
        public ActionResult Add(string doctorid,string datebooking,string patientid)
        {
            //Debug.WriteLine(doctorid + datebooking + patientid);
            
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
            return RedirectToAction("ListMyBooking");
        }

        //List will fetch all booking details from table and send it to view
        public ActionResult List()
        {
            List<Booking> Bookings;
            Bookings = db.Bookings.ToList();
            return View(Bookings);
        }
        //This function will capture who is logged in and only show his/her bookings
        public ActionResult ListMyBooking()
        {
            string id = User.Identity.GetUserId();
            List<Booking> Bookings;
            Bookings = db.Bookings.Where(bb=>bb.PatientID.Contains(id) ||
            bb.DoctorID.Contains(id)).ToList();
            return View(Bookings);
        }


        //Deatil information about appointment
        public ActionResult Show(int id)
        {

            Debug.WriteLine(id);

            //Information of specific booking
            Booking booking = db.Bookings.FirstOrDefault(b => b.BookingID == id);

            //Records list associated with that booking
            string query = "Select * from Records where BookingID =@id";

            //Checks to see if the query is being sent
            Debug.WriteLine(query);

            var first_parameter = new SqlParameter("@id", id);


            //Grabs all the records
            List<Record> allrecords = db.Records.SqlQuery(query, first_parameter).ToList();

            //Getting it into the view models
            BookingDetails viewmodel = new BookingDetails();

            viewmodel.bookinginfo = booking;
            viewmodel.records = allrecords;

            return View(viewmodel);
        }

        //Update booking appointment
        public ActionResult Update(int id)
        {
            //This different model to fetch data from different tables
            UpdateBooking viewmodel = new UpdateBooking();

            viewmodel.Booking = db.Bookings.FirstOrDefault(booking => booking.BookingID == id);
            viewmodel.Doctors = db.Doctors.ToList();
            viewmodel.Patients = db.Patients.ToList();

            return View(viewmodel);
        }
        [HttpPost]
        public ActionResult Update(int id, string doctorid, string datebooking, string patientid)
        {
            
            //Update the booking details
            Booking booking = db.Bookings.FirstOrDefault(b => b.BookingID == id);

            booking.DoctorID = doctorid;
            booking.BookingDate = datebooking;
            booking.PatientID = patientid;

            db.SaveChanges();

            return RedirectToAction("ListMyBooking");
        }
        //To delete booking appointment
        public ActionResult Delete(int id)
        {
            string query = "delete from Bookings where BookingID = @id";
            SqlParameter param = new SqlParameter("@id", id);
            db.Database.ExecuteSqlCommand(query, param);
            return RedirectToAction("ListMyBooking");
        }

        //Displaying details of a record
        public ActionResult ShowRecord(int? id)
        {
            Debug.WriteLine(id);

            //Get the information regarding one record id 
            var first_query = "select * from Records where RecordID= @id";
            var first_parameter = new SqlParameter("@id", id);
            Record recordinfo = db.Records.SqlQuery(first_query, first_parameter).FirstOrDefault();
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
        //Add a record to an appointment
        [HttpPost]
        public ActionResult AttachRecord(int id, string recordName, string recordType, string recordContent, HttpPostedFileBase recordFile, int hasFile)
        {


            //CHECK IF THE VALUES ARE BEING PASSED INTO THE METHOD
            Debug.WriteLine("The values passed into the method are: " + recordName + ", " + recordType + ", " + recordContent + ", " + id);

            //CREATE THE INSERT INTO QUERY
            string query = "insert into Records (RecordName, RecordType, RecordContent, BookingID, HasFile) values (@recordName, @recordType, @recordContent, @id , @hasFile)";

            //Binding the variables to the parameters
            SqlParameter[] sqlparams = new SqlParameter[5]; //0,1,2,3 pieces of information to add
            //each piece of information is a key and value pair
            sqlparams[0] = new SqlParameter("@recordName", recordName);
            sqlparams[1] = new SqlParameter("@recordType", recordType);
            sqlparams[2] = new SqlParameter("@recordContent", recordContent);
            sqlparams[3] = new SqlParameter("@id", id);
            sqlparams[4] = new SqlParameter("@hasFile", hasFile);

            //RUN THE QUERY WITH THE PARAMETERS 
            db.Database.ExecuteSqlCommand(query, sqlparams);

            //GRABING THE ID OF THE LAST RECORD WE JUST INSERTED
            //var idquery = "select RecordID from Records desc limit 1";

            return RedirectToAction("Show/"+id );
        }

        //Showing the Update Record Page
        //UPDATE 
        //Update contorller that pulls information for the page
        public ActionResult UpdateRecord(int id)
        {
            string query = "select * from Records where RecordID = @id";
            var parameter = new SqlParameter("@id", id);
            Record selectedrecord = db.Records.SqlQuery(query, parameter).FirstOrDefault();

            return View(selectedrecord);
        }

        //UPDATE that actually changes the query
        [HttpPost]
        public ActionResult UpdateRec(int id, string recordName, string recordType, string recordContent, int bookingID, HttpPostedFileBase recordFile, string fileExtension, string fileDelete)
        {
            //We assume at the beggining that there is no document
            int hasfile = 0;
            string fileextension = "";

            //checking to see if some information is there
            //if they did input the pdf
            if (recordFile != null)
            {
                Debug.WriteLine("Something identified...");

                //checking to see if the file size is greater than 0 (bytes)
                //If it is it means that the extension is one of a picture
                if (recordFile.ContentLength > 0)
                {
                    Debug.WriteLine("Successfully Identified PDF");
                    //file extensioncheck taken from https://www.c-sharpcorner.com/article/file-upload-extension-validation-in-asp-net-mvc-and-javascript/
                    var valtypes = new[] { "pdf" };

                    //Identifies the exension at the end of the picture
                    var extension = Path.GetExtension(recordFile.FileName).Substring(1);

                    //if the extension is one of the valid types
                    if (valtypes.Contains(extension))
                    {
                        try
                        {
                            //file name is the id of the image
                            string fn = id + "." + extension;

                            //get a direct file path to ~/Content/Records/{id}.{extension}
                            string path = Path.Combine(Server.MapPath("~/Content/Records/"), fn);

                            //save the file
                            recordFile.SaveAs(path);
                            //if these are all successful then we can set these fields
                            hasfile = 1;
                            fileextension = extension;

                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine("Record Attachement was not saved successfully.");
                            Debug.WriteLine("Exception:" + ex);
                        }



                    }
                }
                //if a record was not sent make sure there isnt one in store already
            }
            else
            {
                //Check if there is a record
                if (fileExtension != null)
                {
                    //If there is a file do they wish to delete it?
                    if (fileDelete == "no")
                    {
                        //If they didn't choose to delete it, we assign these values so it pulls the exisitng record
                        hasfile = 1;
                        fileextension = fileExtension;
                    } // If they choose to delete it that means we keep the has record assigned at the beginning at 0 and assume  there is no record on the update

                }

            }


            Debug.WriteLine("I am trying to edit the follwoing values: " + recordName + ", " + recordType + ", " + recordContent + " " + recordFile);

            string query = "update Records set RecordName=@recordName, RecordType=@recordType, RecordContent=@recordContent, BookingID=@bookingID, HasFile=@hasfile, FileExtension=@fileExtension where RecordID=@id";
            SqlParameter[] sqlparams = new SqlParameter[7];
            sqlparams[0] = new SqlParameter("@recordName", recordName);
            sqlparams[1] = new SqlParameter("@recordType", recordType);
            sqlparams[2] = new SqlParameter("@recordContent", recordContent);
            sqlparams[3] = new SqlParameter("@bookingID", bookingID);
            sqlparams[4] = new SqlParameter("@hasfile", hasfile);
            sqlparams[5] = new SqlParameter("@fileextension", fileextension);
            sqlparams[6] = new SqlParameter("@id", id);

            db.Database.ExecuteSqlCommand(query, sqlparams);


            return RedirectToAction("ShowRecord/" + id);
        }

        //DELETE CONFIRM PAGE
        //Sends the view of the delete confirmation with the info of the Record
        public ActionResult DeleteRecord(int id)
        {
            string query = "select * from Records where RecordID=@id";
            SqlParameter param = new SqlParameter("@id", id);
            Record selectedrecord = db.Records.SqlQuery(query, param).FirstOrDefault();
            return View(selectedrecord);
        }

        //DELETING THE RECORDS FROM THE DATABASE
        [HttpPost]
        public ActionResult DeleteRec(int id, int bookingID)
        {
            string query = "delete from Records where RecordID=@id";
            SqlParameter param = new SqlParameter("@id", id);
            db.Database.ExecuteSqlCommand(query, param);


            return RedirectToAction("Show/" + bookingID);
        }



    }
}