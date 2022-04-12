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

        public void getUtente(int idUtente, Utente u)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand comm = new SqlCommand("SELECT nome FROM utenti WHERE id = " + idUtente, conn);
                conn.Open();
                SqlDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    u.Id = idUtente;
                    u.Nome = reader.GetString(0);
                }
            }
        }

        public void salvaUtente(Utente u)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand comm;
                if (u.Id == 0)
                {
                    comm = new SqlCommand("INSERT INTO utenti (nome) VALUES (@nome)", conn);
                    comm.Parameters.AddWithValue("@nome", u.Nome);
                }
                else
                {
                    comm = new SqlCommand("UPDATE utenti SET nome = @nome WHERE id = @id", conn);
                    comm.Parameters.AddWithValue("@nome", u.Nome);
                    comm.Parameters.AddWithValue("@id", u.Id);
                }
                conn.Open();
                comm.ExecuteNonQuery();
            }
        }

        public void eliminaUtente(int idUtente)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand comm = new SqlCommand("DELETE FROM utenti WHERE id = " + idUtente, conn);
                conn.Open();
                comm.ExecuteNonQuery();
            }
        }
    }
}