using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data.Entity;
using HospitalProjectTeam4.Data;
using HospitalProjectTeam4.Models;
using PagedList;
using PagedList.Mvc;
using Microsoft.AspNet.Identity;
using System.Diagnostics;
using System.IO;

namespace HospitalProjectTeam4.Controllers
{
    public class LostFoundController : Controller
    {
        
        // GET: LostFound
        private HospitalProjectContext db = new HospitalProjectContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        //This function will add lost and found reports 
        public ActionResult Add(string lostorfound,HttpPostedFileBase itempic, string item, string category, string color, string contactno, string note)
        {

            int haspic = 0;
            string picextension = "";
            string id = User.Identity.GetUserId();

            if (itempic != null)
            {
                Debug.WriteLine("Something identified...");
                //checking to see if the file size is greater than 0 (bytes)
                if (itempic.ContentLength > 0)
                {
                    Debug.WriteLine("Successfully Identified Image");
                    
                    var valtypes = new[] { "jpeg", "jpg", "png", "gif" };
                    var extension = Path.GetExtension(itempic.FileName).Substring(1);

                    if (valtypes.Contains(extension))
                    {
                        try
                        {
                            //file name is the id of the image
                            string fn = id + "." + extension;

                            //get a direct file path to ~/Content/Pets/{id}.{extension}
                            string path = Path.Combine(Server.MapPath("~/Content/LostFoundImages/"), fn);

                            //save the file
                            itempic.SaveAs(path);
                            //if these are all successful then we can set these fields
                            haspic = 1;
                            //This will be in database to fetch and used to display photo on show page
                            picextension = fn;

                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine("News Image was not saved successfully.");
                            Debug.WriteLine("Exception:" + ex);
                        }



                    }
                }
            }
            //Store details in lost and found table
            LostFound lostandfound = new LostFound();
            DateTime now = DateTime.Now;
            
            lostandfound.LostorFound = lostorfound;
            lostandfound.LostFoundItem = item;
            lostandfound.LostFoundDate = now.ToString();
            lostandfound.LostFoundCategory = category;
            lostandfound.LostFoundColor = color;
            lostandfound.LostFoundPerson = contactno;
            lostandfound.LostFoundNote = note;
            lostandfound.picextension = picextension;
            //Stores patientID to associate with patient
            lostandfound.PatientID = id;
            db.lostFounds.Add(lostandfound);
            db.SaveChanges();
            return RedirectToAction("List");
            //return View();
        }
        public ActionResult List(string sel,int? page)
        {
            //List all reports in Pagination format
            List<LostFound> lostFounds;
            if (sel != "" && sel != null)
            {
                lostFounds = db.lostFounds.Where(search => search.LostorFound.Contains(sel)).ToList();
            }
            else
            {
                lostFounds = db.lostFounds.ToList();
            }
            //returns list of reports in pagination 5 at a time.
            return View(lostFounds.ToPagedList(page ?? 1,5));
        }
        //Fetch report detail and pass it to view to see what values are in database already
        public ActionResult Update(int id)
        {
            LostFound item = db.lostFounds.FirstOrDefault(b => b.LostFoundID == id);
            return View(item);
        }
        [HttpPost]
        //Update the report
        public ActionResult Update(int id, string lostorfound, string item, string category, string color, string contactno, string note)
        {
            LostFound items = db.lostFounds.FirstOrDefault(b => b.LostFoundID == id);

            items.LostorFound = lostorfound;
            items.LostFoundItem = item;
            items.LostFoundCategory = category;
            items.LostFoundColor = color;
            items.LostFoundPerson = contactno;
            items.LostFoundNote = note;

            db.SaveChanges();
            return RedirectToAction("List");
        }

        //Show details of report
        public ActionResult show(int id)
        {
            LostFound item = db.lostFounds.FirstOrDefault(b => b.LostFoundID == id);
            return View(item);
        }
        //Delete report
        public ActionResult Delete(int id)
        {
            string query = "delete from LostFounds where LostFoundID = @id";
            SqlParameter param = new SqlParameter("@id", id);
            db.Database.ExecuteSqlCommand(query, param);
            return RedirectToAction("List");
        }
    }
}