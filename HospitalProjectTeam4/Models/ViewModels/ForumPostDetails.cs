using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace HospitalProjectTeam4.Models.ViewModels
{
    public class ForumPostDetails
    {
        //Information about a specfic posting
        public virtual ForumPost forumPost { get; set; }

        //List of all the comments associated with that post
        public List<ForumReply> forumReplies { get; set; }

    }
}