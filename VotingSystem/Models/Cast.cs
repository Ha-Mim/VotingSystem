using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VotingSystem.Models
{
    public class Cast
    {
        public int Id { set; get; }
        public int VoterId { set; get; }
        public int CandidateId { set; get; }
    }
}