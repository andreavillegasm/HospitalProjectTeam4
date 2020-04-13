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
    public class NewsController : Controller
    {
        private HospitalProjectContext db = new HospitalProjectContext();

        // GET: News
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List()
        {
            List<News> News;
            News = db.News.ToList();
            return View(News);
        }

        // GET: News/Details/5
        public ActionResult Show(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // News news = db.News.Find(id); //EF 6 technique
            News News = db.News.SqlQuery("select * from news where newsid=@NewsID", new SqlParameter("@NewsID", id)).FirstOrDefault();
            if (News == null)
            {
                return HttpNotFound();
            }

            return View(News);

        }

        //THE [HttpPost] Means that this method will only be activated on a POST form submit to the following URL
        //URL: /News/Add
        [HttpPost]
        public ActionResult Add(string NewsName, DateTime NewsDate, string NewsPublish, string NewsDescription, int CategoryID)
        {
            //STEP 1: PULL DATA! The data is access as arguments to the method. Make sure the datatype is correct!
            //The variable name  MUST match the name attribute described in Views/Pet/Add.cshtml

            //Tests are very useul to determining if you are pulling data correctly!
            //Debug.WriteLine("Want to create a pet with name " + PetName + " and weight " + PetWeight.ToString()) ;

            //STEP 2: FORMAT QUERY! the query will look something like "insert into () values ()"...
            string query = "insert into news (NewsName, NewsDate, NewsPublish, NewsDescription, CategoryID) values (@NewsName,@NewsDate,@NewsPublish,@NewsDescription,@CategoryID)";
            SqlParameter[] sqlparams = new SqlParameter[5]; //0,1,2,3,4 pieces of information to add
            //each piece of information is a key and value pair
            sqlparams[0] = new SqlParameter("@NewsName", NewsName);
            sqlparams[1] = new SqlParameter("@NewsDate", NewsDate);
            sqlparams[2] = new SqlParameter("@NewsPublish", NewsPublish);
            sqlparams[3] = new SqlParameter("@NewsDescription", NewsDescription);
            sqlparams[4] = new SqlParameter("@CategoryId", CategoryID);

            //db.Database.ExecuteSqlCommand will run insert, update, delete statements
            //db.Pets.SqlCommand will run a select statement, for example.
            db.Database.ExecuteSqlCommand(query, sqlparams);


            //run the list method to return to a list of pets so we can see our new one!
            return RedirectToAction("List");
        }


        public ActionResult New()
        {
            //STEP 1: PUSH DATA!
            //What data does the Add.cshtml page need to display the interface?
            //A list of species to choose for a pet

            //alternative way of writing SQL -- will learn more about this week 4
            //List<Species> Species = db.Species.ToList();

            List<Category> categories = db.Categories.SqlQuery("select * from categories").ToList();

            return View(categories);
        }

        public ActionResult Update(int id)
        {
            //need information about a particular pet
            News selectednews = db.News.SqlQuery("select * from news where newsid = @id", new SqlParameter("@id", id)).FirstOrDefault();
            List<Category> categories = db.Categories.SqlQuery("select * from categories").ToList();

            UpdateNews UpdateNewsViewModel = new UpdateNews();
            UpdateNewsViewModel.News = selectednews;
            UpdateNewsViewModel.category = categories;

            return View(UpdateNewsViewModel);
        }

        [HttpPost]
        public ActionResult Update(int id, string NewsName, DateTime NewsDate, String NewsPublish, string NewsDescription, int CategoryID, HttpPostedFileBase NewsPic)
        {
            //start off with assuming there is no picture

            int haspic = 0;
            string newspicextension = "";
            //checking to see if some information is there
            if (NewsPic != null)
            {
                Debug.WriteLine("Something identified...");
                //checking to see if the file size is greater than 0 (bytes)
                if (NewsPic.ContentLength > 0)
                {
                    Debug.WriteLine("Successfully Identified Image");
                    //file extensioncheck taken from https://www.c-sharpcorner.com/article/file-upload-extension-validation-in-asp-net-mvc-and-javascript/
                    var valtypes = new[] { "jpeg", "jpg", "png", "gif" };
                    var extension = Path.GetExtension(NewsPic.FileName).Substring(1);

                    if (valtypes.Contains(extension))
                    {
                        try
                        {
                            //file name is the id of the image
                            string fn = id + "." + extension;

                            //get a direct file path to ~/Content/Pets/{id}.{extension}
                            string path = Path.Combine(Server.MapPath("~/Content/News/"), fn);

                            //save the file
                            NewsPic.SaveAs(path);
                            //if these are all successful then we can set these fields
                            haspic = 1;
                            newspicextension = extension;

                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine("News Image was not saved successfully.");
                            Debug.WriteLine("Exception:" + ex);
                        }



                    }
                }
            }

            //Debug.WriteLine("I am trying to edit a pet's name to "+PetName+" and change the weight to "+PetWeight.ToString());

            string query = "update news set NewsName=@NewsName, CategoryID=@CategoryID, NewsDate=@NewsDate, NewsPublish=@NewsPublish, NewsDescription=@NewsDescription, HasPic=@haspic, PicExtension=@newspicextension where NewsID=@id";
            SqlParameter[] sqlparams = new SqlParameter[8];
            sqlparams[0] = new SqlParameter("@NewsName", NewsName);
            sqlparams[1] = new SqlParameter("@NewsDate", NewsDate);
            sqlparams[2] = new SqlParameter("@NewsPublish", NewsPublish);
            sqlparams[3] = new SqlParameter("@CategoryID", CategoryID);
            sqlparams[4] = new SqlParameter("@NewsDescription", NewsDescription);
            sqlparams[5] = new SqlParameter("@id", id);
            sqlparams[6] = new SqlParameter("@HasPic", haspic);
            sqlparams[7] = new SqlParameter("@newspicextension", newspicextension);

            db.Database.ExecuteSqlCommand(query, sqlparams);

            //logic for updating the pet in the database goes here
            return RedirectToAction("List");
        }

        public ActionResult DeleteConfirm(int id)
        {
            string query = "select * from news where newsid = @id";
            SqlParameter param = new SqlParameter("@id", id);
            News selectednews = db.News.SqlQuery(query, param).FirstOrDefault();

            return View(selectednews);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string query = "delete from news where newsid = @id";
            SqlParameter param = new SqlParameter("@id", id);
            db.Database.ExecuteSqlCommand(query, param);

            return RedirectToAction("List");
        }

        


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


    }
}