using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace VotingSystem.Models
{
    public class VoterDbGateway:DbGateway
    {
        public void Save(Voter aVoter)
        {

            string query = "INSERT INTO tbl_voter VALUES ('" + aVoter.Name + "','" + aVoter.Code + "')";
            aSqlConnection.Open();
            aSqlCommand = new SqlCommand(query, aSqlConnection);
            aSqlCommand.ExecuteNonQuery();
            aSqlConnection.Close();
        }
        public List<Cast> GetAll()
        {

            string query = "SELECT *FROM tbl_result";
            aSqlConnection.Open();
            aSqlCommand = new SqlCommand(query, aSqlConnection);
            SqlDataReader aSqlDataReader = aSqlCommand.ExecuteReader();
            List<Cast> candidates = new List<Cast>();
            while (aSqlDataReader.Read())
            {
                Cast aCast=new Cast();
                aCast.Id = Convert.ToInt32(aSqlDataReader["id"]);
                aCast.CandidateId = Convert.ToInt32(aSqlDataReader["candidateId"]);
                
                candidates.Add(aCast);

            }
            aSqlDataReader.Close();
            aSqlConnection.Close();
            return candidates;

        }
        public Voter UniqueCheker(string id)
        {
            string query = "SELECT *FROM tbl_voter WHERE voterId='" + id + "'";
            aSqlConnection.Open();
            aSqlCommand = new SqlCommand(query, aSqlConnection);
            SqlDataReader aSqlDataReader = aSqlCommand.ExecuteReader();

            if (aSqlDataReader.HasRows)
            {
                aSqlDataReader.Read();
                Voter aVoter=new Voter();
                aVoter.Id = Convert.ToInt16(aSqlDataReader["id"]);
                aVoter.Code = aSqlDataReader["voterId"].ToString();

                aSqlDataReader.Close();
                aSqlConnection.Close();

                return aVoter;
            }
            else
            {
                aSqlDataReader.Close();
                aSqlConnection.Close();
                return null;
            }
        }
        public Cast Uniquevoter(int id)
        {
            string query = "SELECT *FROM tbl_cast_vote WHERE voterId='" + id + "'";
            aSqlConnection.Open();
            aSqlCommand = new SqlCommand(query, aSqlConnection);
            SqlDataReader aSqlDataReader = aSqlCommand.ExecuteReader();

            if (aSqlDataReader.HasRows)
            {
                aSqlDataReader.Read();
                Cast aCast=new Cast();
                aCast.Id = Convert.ToInt16(aSqlDataReader["id"]);
                aCast.VoterId = Convert.ToInt16(aSqlDataReader["voterId"]);

                aSqlDataReader.Close();
                aSqlConnection.Close();

                return aCast;
            }
            else
            {
                aSqlDataReader.Close();
                aSqlConnection.Close();
                return null;
            }
        }
        
        public void Cast(Cast aCast)
        {
            foreach (Cast bCast in GetAll())
            {
                if (bCast.CandidateId == aCast.CandidateId)
                {
                    string upquery = "INSERT INTO tbl_cast_vote VALUES ('" + aCast.CandidateId + "','" + aCast.VoterId + "')";
                    aSqlConnection.Open();
                    aSqlCommand = new SqlCommand(upquery, aSqlConnection);
                    aSqlCommand.ExecuteNonQuery();
                    aSqlConnection.Close();
                    string query = "update tbl_result set noOfVote+=1 where candidateId=" + aCast.CandidateId;
                    aSqlConnection.Open();
                    aSqlCommand = new SqlCommand(query, aSqlConnection);
                    aSqlCommand.ExecuteNonQuery();
                    aSqlConnection.Close();
                }
                else
                {
                    string query = "INSERT INTO tbl_cast_vote VALUES ('" + aCast.CandidateId + "','" + aCast.VoterId + "')";
                    aSqlConnection.Open();
                    aSqlCommand = new SqlCommand(query, aSqlConnection);
                    aSqlCommand.ExecuteNonQuery();
                    aSqlConnection.Close();
                    string upquery = "INSERT INTO tbl_result VALUES ('" + aCast.CandidateId + "','1')";
                    aSqlConnection.Open();
                    aSqlCommand = new SqlCommand(upquery, aSqlConnection);
                    aSqlCommand.ExecuteNonQuery();
                    aSqlConnection.Close();
                }
            }
            
        }
    }
}