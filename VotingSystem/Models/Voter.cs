using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Services.Description;

namespace VotingSystem.Models
{
    public class Voter
    {
        public int Id { set; get; }
        
        public string Name { set; get; }
       
        [DisplayName("Voter Id")]
        public string Code { set; get; }
    }
}