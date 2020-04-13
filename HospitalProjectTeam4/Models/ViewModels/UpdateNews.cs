using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalProjectTeam4.Models.ViewModels
{
    public class UpdateNews
    {
       
            //when we need to update a news
            //we need the pet info as well as a list of species

            public News News { get; set; }
            public List<Category> category { get; set; }
        
    }
}