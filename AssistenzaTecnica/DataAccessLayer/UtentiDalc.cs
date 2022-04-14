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
                SqlCommand comm = new SqlCommand("SELECT id, nome, username, profilo FROM utenti", conn);
                conn.Open();
                SqlDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    Utente utente = new Utente();
                    utente.Id = reader.GetInt32(0);
                    utente.Nome = reader.GetString(1);
                    utente.Username = reader.GetString(2);
                    utente.Profilo = reader.GetInt32(3);
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
                SqlCommand comm = new SqlCommand("SELECT nome, username, profilo FROM utenti WHERE id = " + idUtente, conn);
                conn.Open();
                SqlDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    u.Id = idUtente;
                    u.Nome = reader.GetString(0);
                    u.Username = reader.GetString(1);
                    u.Profilo = reader.GetInt32(2);
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
                    comm = new SqlCommand("INSERT INTO utenti (nome, username, password, profilo) VALUES (@nome, @username, @password, @profilo)", conn);
                    comm.Parameters.AddWithValue("@nome", u.Nome);
                    comm.Parameters.AddWithValue("@username", u.Username);
                    comm.Parameters.AddWithValue("@password", u.NuovaPassword);
                    comm.Parameters.AddWithValue("@profilo", u.Profilo);
                }
                else if (u.NuovaPassword != null && u.NuovaPassword != "")
                {
                    comm = new SqlCommand("UPDATE utenti SET nome = @nome, username = @username, password = @password, profilo = @profilo WHERE id = @id", conn);
                    comm.Parameters.AddWithValue("@nome", u.Nome);
                    comm.Parameters.AddWithValue("@username", u.Username);
                    comm.Parameters.AddWithValue("@password", u.NuovaPassword);
                    comm.Parameters.AddWithValue("@profilo", u.Profilo);
                    comm.Parameters.AddWithValue("@id", u.Id);
                }
                else
                { 
                    comm = new SqlCommand("UPDATE utenti SET nome = @nome, username = @username, profilo = @profilo WHERE id = @id", conn);
                    comm.Parameters.AddWithValue("@nome", u.Nome);
                    comm.Parameters.AddWithValue("@username", u.Username);
                    comm.Parameters.AddWithValue("@profilo", u.Profilo);
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

        public Utente checkLogin(string username, string password)
        {
            Utente utente = null;
            string connectionString = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand comm = new SqlCommand("SELECT id, nome, profilo FROM utenti WHERE username = @username AND password = @password", conn);
                comm.Parameters.AddWithValue("@username", username);
                comm.Parameters.AddWithValue("@password", password);
                conn.Open();
                SqlDataReader reader = comm.ExecuteReader();
                if (reader.Read())
                {
                    utente = new Utente();
                    utente.Id = reader.GetInt32(0);
                    utente.Nome = reader.GetString(1);
                    utente.Profilo = reader.GetInt32(2);
                    utente.Username = username;
                }
            }
            return utente;
        }
    }
}