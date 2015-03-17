using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace VotingSystem.Models
{
    public class DbGateway
    {
        public string connectionString = ConfigurationManager.ConnectionStrings["VoterDbConnectionString"].ConnectionString;
        public SqlConnection aSqlConnection;
        public SqlCommand aSqlCommand;
        public DbGateway()
        {
            aSqlConnection = new SqlConnection(connectionString);
        }
    }
}