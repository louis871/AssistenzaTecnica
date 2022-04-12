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

        public void getStato(int idStato, Stato s)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand comm = new SqlCommand("SELECT descrizione FROM stati WHERE id = " + idStato, conn);
                conn.Open();
                SqlDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    s.Id = idStato;
                    s.Descrizione = reader.GetString(0);
                }
            }
        }

        public void salvaStato(Stato s)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand comm;
                if (s.Id == 0)
                {
                    comm = new SqlCommand("INSERT INTO stati (descrizione) VALUES (@descrizione)", conn);
                    comm.Parameters.AddWithValue("@descrizione", s.Descrizione);
                }
                else
                {
                    comm = new SqlCommand("UPDATE stati SET descrizione = @descrizione WHERE id = @id", conn);
                    comm.Parameters.AddWithValue("@descrizione", s.Descrizione);
                    comm.Parameters.AddWithValue("@id", s.Id);
                }
                conn.Open();
                comm.ExecuteNonQuery();
            }
        }

        public void eliminaStato(int idStato)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand comm = new SqlCommand("DELETE FROM stati WHERE id = " + idStato, conn);
                conn.Open();
                comm.ExecuteNonQuery();
            }
        }
    }
}