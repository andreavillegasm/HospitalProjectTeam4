using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data.Entity;
using HospitalProjectTeam4.Data;
using HospitalProjectTeam4.Models;
using Microsoft.AspNet.Identity;
using System.Diagnostics;

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
        public ActionResult Add(string lostorfound,string item, string category, string color, string contactno, string note)
        {
            LostFound lostandfound = new LostFound();
            DateTime now = DateTime.Now;
            string userid = User.Identity.GetUserId();
            //Debug.WriteLine(userid);
            lostandfound.LostorFound = lostorfound;
            lostandfound.LostFoundItem = item;
            lostandfound.LostFoundDate = now.ToString();
            lostandfound.LostFoundCategory = category;
            lostandfound.LostFoundColor = color;
            lostandfound.LostFoundPerson = contactno;
            lostandfound.LostFoundNote = note;
            lostandfound.PatientID = userid;
            db.lostFounds.Add(lostandfound);
            db.SaveChanges();
            return RedirectToAction("List");
            //return View();
        }
        public ActionResult List(string sel)
        {
            List<LostFound> lostFounds;
            if (sel != "" && sel != null)
            {
                lostFounds = db.lostFounds.Where(search => search.LostorFound.Contains(sel)).ToList();
            }
            else
            {
                lostFounds = db.lostFounds.ToList();
            }
            return View(lostFounds);
        }
        public ActionResult Update(int id)
        {
            LostFound item = db.lostFounds.FirstOrDefault(b => b.LostFoundID == id);
            return View(item);
        }
        [HttpPost]
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
        public ActionResult Delete(int id)
        {
            string query = "delete from LostFounds where LostFoundID = @id";
            SqlParameter param = new SqlParameter("@id", id);
            db.Database.ExecuteSqlCommand(query, param);
            return RedirectToAction("List");
        }
    }
}