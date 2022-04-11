using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AssistenzaTecnica.Models;
using System.Configuration;
using System.Data.SqlClient;


namespace AssistenzaTecnica.DataAccessLayer
{
    public class StatiDalc
    {
        public Dictionary<int, Stato> getAllStati()
        {
            Dictionary<int, Stato> listaStati = new Dictionary<int, Stato>();
            string connectionString = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand comm = new SqlCommand("SELECT id, descrizione FROM stati", conn);
                conn.Open();
                SqlDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    Stato stato = new Stato();
                    stato.Id = reader.GetInt32(0);
                    stato.Descrizione = reader.GetString(1);
                    listaStati.Add(stato.Id, stato);
                }
            }

            return listaStati;
        }
    }
}