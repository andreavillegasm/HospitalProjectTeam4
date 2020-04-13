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
        //to use database
        // GET: News
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult List(string newssearchkey, int pagenum = 0)
        {
            Debug.WriteLine("The parameter is " + newssearchkey);
            //for searchkey
            string query = "Select * from news";
            if (newssearchkey != "")
            {
                query = query + " where NewsName like '%" + newssearchkey + "%'";
            }

            //query to get the list of all the news in the system.
            List<News> news = db.News.SqlQuery(query).ToList();

            int perpage = 3;
            int newscount = news.Count();
            int maxpage = (int)Math.Ceiling((decimal)newscount / perpage) - 1;
            if (maxpage < 0) maxpage = 0;
            if (pagenum < 0) pagenum = 0;
            if (pagenum > maxpage) pagenum = maxpage;
            int start = (int)(perpage * pagenum);
            ViewData["pagenum"] = pagenum;
            ViewData["pagesummary"] = "";
            if (maxpage > 0)
            {
                ViewData["pagesummary"] = (pagenum + 1) + " of " + (maxpage + 1);
                List<SqlParameter> newparams = new List<SqlParameter>();

                if (newssearchkey != "")
                {
                    newparams.Add(new SqlParameter("@searchkey", "%" + newssearchkey + "%"));
                    ViewData["newssearchkey"] = newssearchkey;
                }
                newparams.Add(new SqlParameter("@start", start));
                newparams.Add(new SqlParameter("@perpage", perpage));
                string pagedquery = query + " order by NEWSID offset @start rows fetch first @perpage rows only ";
                Debug.WriteLine(pagedquery);
                Debug.WriteLine("offset " + start);
                Debug.WriteLine("fetch first " + perpage);
                news = db.News.SqlQuery(pagedquery, newparams.ToArray()).ToList();
            }
            //End of Pagination Algorithm

            return View(news);
        }

        // GET: details of the news with id =id
        //news show method
        public ActionResult Show(int? id)
        {
            //if the id is null, then return this message of the bad request.
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // News news = db.News.Find(id); 
            //if id exists, but the news does not exist, then return this message.
            News News = db.News.SqlQuery("select * from news where newsid=@NewsID", new SqlParameter("@NewsID", id)).FirstOrDefault();
            if (News == null)
            {
                return HttpNotFound();
            }

            return View(News);

        }

        //URL: /News/Add
        [HttpPost]
        public ActionResult Add(string NewsName, DateTime NewsDate, string NewsPublish, string NewsDescription, int CategoryID, HttpPostedFileBase NewsPic, int HasPic)
        {
            //the above are taken from the addnews form and should alwasys match.
            //otherwise the following method will not work.


            //Debug.WriteLine("Want to create a news with name " + NewsName + " and description " + NewsDescription) ;
            //the debug writeline is for testing, we don't need all the parameters.Some of them are fine.

            //query to add a new news into the database.

            //STEP 2: FORMAT QUERY! the query will look something like "insert into () values ()"...
            string query = "insert into news (NewsName, NewsDate, NewsPublish, NewsDescription, CategoryID, HasPic) values (@NewsName,@NewsDate,@NewsPublish,@NewsDescription,@CategoryID, @HasPic)";
            SqlParameter[] sqlparams = new SqlParameter[6]; //0,1,2,3,4 pieces of information to add
            //each piece of information is a key and value pair
            sqlparams[0] = new SqlParameter("@NewsName", NewsName);
            sqlparams[1] = new SqlParameter("@NewsDate", NewsDate);
            sqlparams[2] = new SqlParameter("@NewsPublish", NewsPublish);
            sqlparams[3] = new SqlParameter("@NewsDescription", NewsDescription);
            sqlparams[4] = new SqlParameter("@CategoryId", CategoryID);
            sqlparams[5] = new SqlParameter("@HasPic", HasPic);

            //db.Database.ExecuteSqlCommand will run insert, update, delete statements
            //db.News.SqlCommand will run a select statement, for example.
            db.Database.ExecuteSqlCommand(query, sqlparams);


            //run the list method to return to a list of news so we can see our new one!
            return RedirectToAction("List");
        }


        public ActionResult New()
        {
            //this is get the information that we want to provide the user in order
            //for the addition of the news into the system.We need categorynames in the 
            //dropdown list.

            List<Category> categories = db.Categories.SqlQuery("select * from categories").ToList();

            return View(categories);
        }

        public ActionResult Update(int id)
        {
            //need information about a particular news
            News selectednews = db.News.SqlQuery("select * from news where newsid = @id", new SqlParameter("@id", id)).FirstOrDefault();
            List<Category> categories = db.Categories.SqlQuery("select * from categories").ToList();

            UpdateNews UpdateNewsViewModel = new UpdateNews();
            UpdateNewsViewModel.News = selectednews;
            UpdateNewsViewModel.category = categories;
            //using viewmodel to get the list of all the categories in order to perform updation of the news.

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

            //Debug.WriteLine("I am trying to edit a news's name to "+NewsName+" and change the Publish to "+NewsPublish);

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

            //logic for updating the news in the database goes here
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