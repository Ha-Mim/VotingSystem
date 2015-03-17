using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VotingSystem.Models;

namespace VotingSystem.Controllers
{
    public class VoterController : Controller
    {
        private VoterDbGateway aVoterDbGateway = new VoterDbGateway();
        private CandidateDbGateway aCandidateDbGateway = new CandidateDbGateway();
        //
        // GET: /Voter/
        public ActionResult Save()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Save(Voter aVoter)
        {
            if (aVoter.Name != null & aVoter.Code != null)
            {
                if (aVoterDbGateway.UniqueCheker(aVoter.Code) == null)
                {
                    if (aVoter.Code.Length >= 13)
                    {
                        aVoterDbGateway.Save(aVoter);
                        ViewBag.Msg = "Successfully Saved";
                    }
                    else
                    {
                        ViewBag.error = "Id must be 13 Char long";
                    }
                }
                else
                {
                    ViewBag.error = "Id must be unique";
                }
            }
            else
            {
                ViewBag.error = "Name and Id is required";
            }
            return View("Save");
        }

        public ActionResult Candidate()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Candidate(Candidate aCandidate)
        {
            if (aCandidate.Name != null & aCandidate.Symbol != null)
            {
                if (aCandidateDbGateway.UniqueCheker(aCandidate.Symbol) == null)
                {
                    aCandidateDbGateway.Save(aCandidate);
                    ViewBag.Msg = "Successfully Saved";

                }
                else
                {
                    ViewBag.error = "Symbol must be unique";
                }
            }
            else
            {
                ViewBag.error = "Name and Symbol is required";
            }
            return View("Candidate");
        }

        public ActionResult CastVote()
        {
            ViewBag.Candidate = aCandidateDbGateway.GetAll();
            return View();
        }

        [HttpPost]
        public ActionResult CastVote(string voterId, int candidateId)
        {
            var voter = aVoterDbGateway.UniqueCheker(voterId);
            ViewBag.Candidate = aCandidateDbGateway.GetAll();
            if (voter != null)
            {
                if (aVoterDbGateway.Uniquevoter(voter.Id) == null)
                {
                    Cast aCast = new Cast() {CandidateId = candidateId, VoterId = voter.Id};
                    aVoterDbGateway.Cast(aCast);
                    ViewBag.Msg = "Successfully casted";
                }
                else
                {
                    ViewBag.error = "This voter already casted his/her vote, so he/she can't cast any more vote";
                }
            }
            else
            {
                ViewBag.error = "This voter doesn't exist into the system";
            }
            return View("CastVote");
        }

        public ActionResult ViewResult()
        {
            int max = 0;

            var candidates = aCandidateDbGateway.GetResult();
            foreach (var candidate in candidates)
            {
                candidate.Name = aCandidateDbGateway.GetOne(candidate.Id).Name;
                candidate.Symbol = aCandidateDbGateway.GetOne(candidate.Id).Symbol;

                if (candidate.NoOfVotes > max)
                {
                    max = candidate.NoOfVotes;
                    candidate.Status = "Winner";
                }
                else
                {
                    if (candidate.NoOfVotes == max)
                    {
                        foreach (var acandidate in candidates)
                        {
                            if (acandidate.NoOfVotes == max)
                            {
                                acandidate.Status = "Tie";
                            }
                        }
                    }
                    else
                    {
                        candidate.Status = "Losser";
                    }

                }
            }

            return View(candidates);
            }
        }
    }
