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
    public class CategoryController : Controller
    {
        private HospitalProjectContext db = new HospitalProjectContext();
        // GET: Categories
        public ActionResult Index()
        {
            return View();
        }

        //TODO: Each line should be a separate method in this class
        // List
        public ActionResult List(string categorysearchkey)
        {
            Debug.WriteLine("The parameter is " + categorysearchkey);

            string query = "Select * from categories";
            if (categorysearchkey != "")
            {
                query = query + " where Name like '%" + categorysearchkey + "%'";
            }

            //what data do we need?
            List<Category> mycategory = db.Categories.SqlQuery(query).ToList();

            return View(mycategory);
        }

        public ActionResult Add()
        {
            //I don't need any information to do add of category.
            return View();
        }
        [HttpPost]
        public ActionResult Add(string CategoryName)
        {
            string query = "insert into categories (Name) values (@CategoryName)";
            var parameter = new SqlParameter("@CategoryName", CategoryName);

            db.Database.ExecuteSqlCommand(query, parameter);
            return RedirectToAction("List");
        }

        public ActionResult Show(int id)
        {
            string query = "select * from categories where categoryid = @id";
            var parameter = new SqlParameter("@id", id);
            Category selectedcategory = db.Categories.SqlQuery(query, parameter).FirstOrDefault();

            return View(selectedcategory);
        }

        public ActionResult Update(int id)
        {
            string query = "select * from categories where categoryid = @id";
            var parameter = new SqlParameter("@id", id);
            Category selectedcategory = db.Categories.SqlQuery(query, parameter).FirstOrDefault();

            return View(selectedcategory);
        }
        [HttpPost]
        public ActionResult Update(int id, string CategoryName)
        {
            string query = "update categories set name = @CategoryName where categoryid = @id";
            SqlParameter[] sqlparams = new SqlParameter[2];
            sqlparams[0] = new SqlParameter("@CategoryName", CategoryName);
            sqlparams[1] = new SqlParameter("@id", id);
            db.Database.ExecuteSqlCommand(query, sqlparams);

            return RedirectToAction("List");
        }

        public ActionResult DeleteConfirm(int id)
        {
            string query = "select * from categories where CategoryID=@id";
            SqlParameter param = new SqlParameter("@id", id);
            Category selectedcategory = db.Categories.SqlQuery(query, param).FirstOrDefault();
            return View(selectedcategory);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string query = "delete from categories where categoryid=@id";
            SqlParameter param = new SqlParameter("@id", id);
            db.Database.ExecuteSqlCommand(query, param);


            //for the sake of referential integrity, unset the categories for all news
            string refquery = "update news set CategoryID = '' where CategoryID=@id";
            db.Database.ExecuteSqlCommand(refquery, param); //same param as before

            return RedirectToAction("List");
        }

    }
}