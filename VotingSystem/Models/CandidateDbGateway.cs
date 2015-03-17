using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace VotingSystem.Models
{
    public class CandidateDbGateway:DbGateway
    {
        public void Save(Candidate aCandidate)
        {

            string query = "INSERT INTO tbl_candidate VALUES ('" + aCandidate.Name + "','" + aCandidate.Symbol + "')";
            aSqlConnection.Open();
            aSqlCommand = new SqlCommand(query, aSqlConnection);
            aSqlCommand.ExecuteNonQuery();
            aSqlConnection.Close();
        }
        public void Result(Candidate aCandidate)
        {

            string query = "INSERT INTO tbl_result VALUES ('" + aCandidate.Id + "','" + aCandidate.Status + "','"+aCandidate.NoOfVotes+"')";
            aSqlConnection.Open();
            aSqlCommand = new SqlCommand(query, aSqlConnection);
            aSqlCommand.ExecuteNonQuery();
            aSqlConnection.Close();
        }
        public Candidate UniqueCheker(string symbol)
        {
            string query = "SELECT *FROM tbl_candidate WHERE symbol='" + symbol + "'";
            aSqlConnection.Open();
            aSqlCommand = new SqlCommand(query, aSqlConnection);
            SqlDataReader aSqlDataReader = aSqlCommand.ExecuteReader();

            if (aSqlDataReader.HasRows)
            {
                aSqlDataReader.Read();
                Candidate aCandidate=new Candidate();

                aCandidate.Symbol = aSqlDataReader["symbol"].ToString();

                aSqlDataReader.Close();
                aSqlConnection.Close();

                return aCandidate;
            }
            else
            {
                aSqlDataReader.Close();
                aSqlConnection.Close();
                return null;
            }
        }
        public Candidate GetOne(int id)
        {
            string query = "SELECT *FROM tbl_candidate WHERE id='" + id + "'";
            aSqlConnection.Open();
            aSqlCommand = new SqlCommand(query, aSqlConnection);
            SqlDataReader aSqlDataReader = aSqlCommand.ExecuteReader();

            if (aSqlDataReader.HasRows)
            {
                aSqlDataReader.Read();
                Candidate aCandidate = new Candidate();
                aCandidate.Name = aSqlDataReader["name"].ToString();
                aCandidate.Symbol = aSqlDataReader["symbol"].ToString();

                aSqlDataReader.Close();
                aSqlConnection.Close();

                return aCandidate;
            }
            else
            {
                aSqlDataReader.Close();
                aSqlConnection.Close();
                return null;
            }
        }
     
        public List<Candidate> GetAll()
        {

            string query = "SELECT *FROM tbl_candidate";
            aSqlConnection.Open();
            aSqlCommand = new SqlCommand(query, aSqlConnection);
            SqlDataReader aSqlDataReader = aSqlCommand.ExecuteReader();
            List<Candidate> candidates = new List<Candidate>();
            while (aSqlDataReader.Read())
            {
                Candidate candidate = new Candidate();
                candidate.Id = Convert.ToInt32(aSqlDataReader["id"]);
                candidate.Name = aSqlDataReader["name"].ToString();
                candidate.Symbol = aSqlDataReader["symbol"].ToString();
                candidates.Add(candidate);

            }
            aSqlDataReader.Close();
            aSqlConnection.Close();
            return candidates;

        }
        public List<Candidate> GetResult()
        {

            string query = "SELECT * FROM tbl_result ORDER BY noOfVote DESC;";
            aSqlConnection.Open();
            aSqlCommand = new SqlCommand(query, aSqlConnection);
            SqlDataReader aSqlDataReader = aSqlCommand.ExecuteReader();
            List<Candidate> candidates = new List<Candidate>();
            while (aSqlDataReader.Read())
            {
                Candidate candidate = new Candidate();
                candidate.Id = Convert.ToInt32(aSqlDataReader["candidateId"]);
                candidate.NoOfVotes = Convert.ToInt32(aSqlDataReader["noOfVote"]);
                candidates.Add(candidate);

            }
            aSqlDataReader.Close();
            aSqlConnection.Close();
            return candidates;

        }

        public int Count(int candidateId)
        {
            string query="SELECT COUNT(candidateId) As NoOfVotes FROM tbl_cast_vote where candidateId="+candidateId;
            aSqlConnection.Open();
            aSqlCommand = new SqlCommand(query, aSqlConnection);
            SqlDataReader aSqlDataReader = aSqlCommand.ExecuteReader();
            int count = 0;
            while (aSqlDataReader.Read())
            {
                count = Convert.ToInt16(aSqlDataReader["NoOfVotes"]);
               
            }
             aSqlDataReader.Close();
                aSqlConnection.Close();
            return count;
           
            
        }

    }
}