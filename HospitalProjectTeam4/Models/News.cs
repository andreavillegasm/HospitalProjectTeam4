using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//Install  entity framework 6 on Tools > Manage Nuget Packages > Microsoft Entity Framework (ver 6.4)
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using HospitalProjectTeam4.Data;

namespace HospitalProjectTeam4.Models
{
    public class News
    {
        /*
            A news belongs to one category. But, one news category can have many news associated to it.
            Some things that describe a news:
                - Name
                - Date
                - Category
                - Publish
                - Description

            A News must reference a Category.
        */
        [Key]
        public int NewsID { get; set; }
        public string NewsName { get; set; }
        //weight is in kilograms (kg)
        public DateTime NewsDate { get; set; }
        public string NewsPublish { get; set; }
        public string NewsDescription { get; set; }

        public int HasPic { get; set; }
        public string PicExtension { get; set; }


        //Representing the Many in (One Category to Many News)        
        public int CategoryID { get; set; }
        [ForeignKey("CategoryID")]
        public virtual Category Category { get; set; }

    }
}