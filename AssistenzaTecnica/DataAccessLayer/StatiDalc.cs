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
                SqlCommand comm = new SqlCommand("SELECT id, descrizione, profilo_minimo FROM stati", conn);
                conn.Open();
                SqlDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    Stato stato = new Stato();
                    stato.Id = reader.GetInt32(0);
                    stato.Descrizione = reader.GetString(1);
                    stato.ProfiloMinimo = reader.GetInt32(2);
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
                SqlCommand comm = new SqlCommand("SELECT descrizione, profilo_minimo FROM stati WHERE id = " + idStato, conn);
                conn.Open();
                SqlDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    s.Id = idStato;
                    s.Descrizione = reader.GetString(0);
                    s.ProfiloMinimo = reader.GetInt32(1);   
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
                    comm = new SqlCommand("INSERT INTO stati (descrizione, profilo_minimo) VALUES (@descrizione, @profilo)", conn);
                    comm.Parameters.AddWithValue("@descrizione", s.Descrizione);
                    comm.Parameters.AddWithValue("@profilo", s.ProfiloMinimo);
                }
                else
                {
                    comm = new SqlCommand("UPDATE stati SET descrizione = @descrizione, profilo_minimo = @profilo WHERE id = @id", conn);
                    comm.Parameters.AddWithValue("@descrizione", s.Descrizione);
                    comm.Parameters.AddWithValue("@profilo", s.ProfiloMinimo);
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