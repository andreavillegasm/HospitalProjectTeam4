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
    public class ForumPostController : Controller
    {
        //need this to work with the login functionalities
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        //reference how the Account Controller instantiates the controller class with SignInManager and UserManager


        private HospitalProjectContext db = new HospitalProjectContext();
        //paramaterless constructor
        public ForumPostController() { }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List(int pagenum = 0)
        {

            //Put the newest postings at the top
            string query = "Select * from ForumPosts";

            //Checks to see if the query is being sent
            Debug.WriteLine(query);

            //Grabs all the records plus the associated information regarding that specific record
            List<ForumPost> allposts = db.ForumPosts.SqlQuery(query).ToList();

            //Start of Pagination Algorithm (Raw MSSQL)

            //How many  posts do I want per page
            int perpage = 5;

            //Count how many posts on the list
            int postcount = allposts.Count();
            int maxpage = (int)Math.Ceiling((decimal)postcount / perpage) - 1;
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

                newparams.Add(new SqlParameter("@start", start));
                newparams.Add(new SqlParameter("@perpage", perpage));
                string pagedquery = query + " order by PostingDate Desc offset @start rows fetch first @perpage rows only ";
                Debug.WriteLine(pagedquery);
                Debug.WriteLine("offset " + start);
                Debug.WriteLine("fetch first " + perpage);
                allposts = db.ForumPosts.SqlQuery(pagedquery, newparams.ToArray()).ToList();
            }
            //End of Pagination Algorithm


            return View(allposts);
        }

        public ActionResult Show(int? id)
        {
            Debug.WriteLine(id);

            //Get the information regarding the posting id
            var first_query = "select * from ForumPosts where PostID= @id";
            var first_parameter = new SqlParameter("@id", id);
            ForumPost postinfo = db.ForumPosts.SqlQuery(first_query, first_parameter).FirstOrDefault();
            if (postinfo == null)
            {
                return HttpNotFound();
            }
            var second_parameter = new SqlParameter("@id", id);
            //Find information about the booking related to that  record
            var second_query = "select * from ForumReplies where PostID= @id order by ReplyDate Desc";
            List<ForumReply> replies = db.ForumReplies.SqlQuery(second_query, second_parameter).ToList();
            if (replies == null)
            {
                return HttpNotFound();
            }


            ForumPostDetails viewmodel = new ForumPostDetails();
            viewmodel.forumPost = postinfo;
            viewmodel.forumReplies = replies;

            return View(viewmodel);
        }

        //Display the Add page
        public ActionResult Add()
        {
            //Show this page only if user is a patient
            if (UserManager.IsUserPatient())
            {
                return View();

            }
            else
            {
                return View("AccessDenied");
            }

                
        }
        //Displaying the Access denied page
        public ActionResult AccessDenied()
        {
            return View();


        }


        //ADD A NEW POST TO THE DATABASE
        //Method is only called when it comes from a form submission
        //Parameters are all the values from the form
        [HttpPost]
        public ActionResult New(string postingTitle, string postingCategory, string postingContent, int postingState)
        {

            //Only user can post on the page
            string id = User.Identity.GetUserId();

            //Only Patient can post

                //Getting the current time
                DateTime currentTime = DateTime.Now;

                //CHECK IF THE VALUES ARE BEING PASSED INTO THE METHOD
                Debug.WriteLine("The values passed into the method are: " + id + ", " + postingTitle + ", " + postingCategory + ", " + postingContent + ", " + currentTime,", "+ postingState );

                //CREATE THE INSERT INTO QUERY
                string query = "insert into ForumPosts (PatientID, PostingDate, PostingTitle, PostingCategory, PostingContent, PostingState) values (@patientID, @currentTime, @postingTitle, @postingCategory, @postingContent, @postingState)";

                //Binding the variables to the parameters
                SqlParameter[] sqlparams = new SqlParameter[6];
                //each piece of information is a key and value pair
                sqlparams[0] = new SqlParameter("@patientID", id);
                sqlparams[1] = new SqlParameter("@currentTime", currentTime);
                sqlparams[2] = new SqlParameter("@postingTitle", postingTitle);
                sqlparams[3] = new SqlParameter("@postingCategory", postingCategory);
                sqlparams[4] = new SqlParameter("@postingContent", postingContent);
                sqlparams[5] = new SqlParameter("@postingState", postingState);

            //RUN THE QUERY WITH THE PARAMETERS 
            db.Database.ExecuteSqlCommand(query, sqlparams);


                return RedirectToAction("List");


        }

        //UPDATE 
        //Update contorller that pulls information for the page
        public ActionResult Update(int id)
        {
            string query = "select * from ForumPosts where PostID = @id";
            var parameter = new SqlParameter("@id", id);
            ForumPost selectedrecord = db.ForumPosts.SqlQuery(query, parameter).FirstOrDefault();

            return View(selectedrecord);
        }

        //UPDATE that actually changes the query
        [HttpPost]
        public ActionResult Update(int id, string postingTitle, string postingCategory, string postingContent)
        {

            //Getting the current time of update
            DateTime currentTime = DateTime.Now;

            Debug.WriteLine("I am trying to edit the follwoing values: " + postingTitle + ", " + postingCategory + ", " + postingContent + ", " + currentTime);

            string query = "update ForumPosts set PostingTitle=@postingTitle, PostingCategory=@postingCategory, PostingContent=@postingContent, PostingDate=@currentTime where PostID=@id";
            SqlParameter[] sqlparams = new SqlParameter[5];
            sqlparams[0] = new SqlParameter("@postingTitle", postingTitle);
            sqlparams[1] = new SqlParameter("@postingCategory", postingCategory);
            sqlparams[2] = new SqlParameter("@postingContent", postingContent);
            sqlparams[3] = new SqlParameter("@currentTime", currentTime);
            sqlparams[4] = new SqlParameter("@id", id);


            db.Database.ExecuteSqlCommand(query, sqlparams);


            return RedirectToAction("Show/" + id);
        }
        //DELETE CONFIRM PAGE
        //Sends the view of the delete confirmation with the info of the ForumPost
        public ActionResult DeleteConfirm(int id)
        {
            string query = "select * from ForumPosts where PostID=@id";
            SqlParameter param = new SqlParameter("@id", id);
            ForumPost selectedpost = db.ForumPosts.SqlQuery(query, param).FirstOrDefault();
            return View(selectedpost);
        }

        //DELETING THE POST FROM THE DATABASE
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string query = "delete from ForumPosts where PostID=@id";
            SqlParameter param = new SqlParameter("@id", id);
            db.Database.ExecuteSqlCommand(query, param);


            return RedirectToAction("List");
        }

        //ADDING A COMMENT TO A SPECIFIC POST
        [HttpPost]
        public ActionResult AddComment(int id, string replyContent)
        {
            if (UserManager.IsUserDoctor())
            {
                string doctorid = User.Identity.GetUserId();

                Debug.WriteLine("forum post id is" + id);

                //Get the date for the comment
                DateTime currentTime = DateTime.Now;

                //Insert references into the bridging table
                string query = "insert into ForumReplies (PostID, ReplyDate, ReplyContent, DoctorID) values (@id, @currentTime, @replyContent, @doctorid)";
                SqlParameter[] sqlparams = new SqlParameter[4];
                sqlparams[0] = new SqlParameter("@id", id);
                sqlparams[1] = new SqlParameter("@currentTime", currentTime);
                sqlparams[2] = new SqlParameter("@replyContent", replyContent);
                sqlparams[3] = new SqlParameter("@doctorid", doctorid);


                db.Database.ExecuteSqlCommand(query, sqlparams);


                return RedirectToAction("Show/" + id);

            } else
            {
                return View("AccessDeniedComment");
            }
               


        }
        public ActionResult DeleteComment(int id, int PostID)
        {
            //For this one we are receiving the comment id rather than the post id
            Debug.WriteLine("forum post id is" + id);


            //Delete comment
            string query = "delete from ForumReplies where ReplyID=@id";
            SqlParameter param = new SqlParameter("@id", id);
            db.Database.ExecuteSqlCommand(query, param);


            return RedirectToAction("Show/" + PostID);


        }

        public ActionResult EditComment(int id)
        {
            string query = "select * from ForumReplies where ReplyID = @id";
            var parameter = new SqlParameter("@id", id);
            ForumReply selectedreply = db.ForumReplies.SqlQuery(query, parameter).FirstOrDefault();

            return View(selectedreply);


        }

        //UPDATE that actually changes the query
        [HttpPost]
        public ActionResult UpdateComment(int id, int PostID, string replyContent)
        {

            //Getting the current time of update
            DateTime currentTime = DateTime.Now;

            Debug.WriteLine("I am trying to edit the follwoing values: " + replyContent + ", " + currentTime + ", "+ PostID);

            string query = "update ForumReplies set ReplyContent=@replyContent, ReplyDate=@currentTime where ReplyID=@id";
            SqlParameter[] sqlparams = new SqlParameter[3];
            sqlparams[0] = new SqlParameter("@replyContent", replyContent);
            sqlparams[1] = new SqlParameter("@currentTime", currentTime);
            sqlparams[2] = new SqlParameter("@id", id);


            db.Database.ExecuteSqlCommand(query, sqlparams);


            return RedirectToAction("Show/" + PostID);
        }
        //how to get the UserManager and SignInManager from the server
        public ForumPostController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }



    }
}