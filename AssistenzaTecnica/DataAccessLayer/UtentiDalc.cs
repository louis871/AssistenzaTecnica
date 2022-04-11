using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AssistenzaTecnica.Models;
using System.Configuration;
using System.Data.SqlClient;

namespace AssistenzaTecnica.DataAccessLayer
{
    public class UtentiDalc
    {
        public Dictionary<int, Utente> getAllUtenti()
        {
            Dictionary<int, Utente> listaUtenti = new Dictionary<int, Utente>();
            string connectionString = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand comm = new SqlCommand("SELECT id, nome FROM utenti", conn);
                conn.Open();
                SqlDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    Utente utente = new Utente();
                    utente.Id = reader.GetInt32(0);
                    utente.Nome = reader.GetString(1);
                    listaUtenti.Add(utente.Id, utente);
                }
            }

            return listaUtenti;
        }
    }
}