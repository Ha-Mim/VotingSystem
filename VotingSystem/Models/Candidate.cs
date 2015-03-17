using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VotingSystem.Models
{
    public class Candidate
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string Symbol { set; get; }
        public string Status { set; get; }
        public int NoOfVotes { set; get; }
    }
}