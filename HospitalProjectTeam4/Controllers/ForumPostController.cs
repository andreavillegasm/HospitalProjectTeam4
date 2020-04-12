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
        private HospitalProjectContext db = new HospitalProjectContext();

        // GET: ForumPost
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {

            //Put the newest postings at the top
            string query = "Select * from ForumPosts  order by PostingDate Desc";

            //Checks to see if the query is being sent
            Debug.WriteLine(query);

            //Grabs all the records plus the associated information regarding that specific record
            List<ForumPost> allposts = db.ForumPosts.SqlQuery(query).ToList();


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
            return View();
        }

        //ADD A NEW POST TO THE DATABASE
        //Method is only called when it comes from a form submission
        //Parameters are all the values from the form
        [HttpPost]
        public ActionResult New(int patientID, string postingTitle, string postingCategory, string postingContent)
        {


            //Getting the current time
            DateTime currentTime = DateTime.Now;

            //CHECK IF THE VALUES ARE BEING PASSED INTO THE METHOD
            Debug.WriteLine("The values passed into the method are: " + patientID + ", " + postingTitle + ", " + postingCategory + ", " + postingContent + ", " + currentTime);

            //CREATE THE INSERT INTO QUERY
            string query = "insert into ForumPosts (PatientID, PostingDate, PostingTitle, PostingCategory, PostingContent) values (@patientID, @currentTime, @postingTitle, @postingCategory, @postingContent)";

            //Binding the variables to the parameters
            SqlParameter[] sqlparams = new SqlParameter[5];
            //each piece of information is a key and value pair
            sqlparams[0] = new SqlParameter("@patientID", patientID);
            sqlparams[1] = new SqlParameter("@currentTime", currentTime);
            sqlparams[2] = new SqlParameter("@postingTitle", postingTitle);
            sqlparams[3] = new SqlParameter("@postingCategory", postingCategory);
            sqlparams[4] = new SqlParameter("@postingContent", postingContent);

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
            Debug.WriteLine("forum post id is" + id);

            //Get the date for the comment
            DateTime currentTime = DateTime.Now;

            //Insert references into the bridging table
            string query = "insert into ForumReplies (PostID, ReplyDate, ReplyContent) values (@id, @currentTime, @replyContent)";
            SqlParameter[] sqlparams = new SqlParameter[3];
            sqlparams[0] = new SqlParameter("@id", id);
            sqlparams[1] = new SqlParameter("@currentTime", currentTime);
            sqlparams[2] = new SqlParameter("@replyContent", replyContent);


            db.Database.ExecuteSqlCommand(query, sqlparams);
   

            return RedirectToAction("Show/" + id);


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



    }
}