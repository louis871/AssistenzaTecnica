using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AssistenzaTecnica.Models;
using System.Configuration;
using System.Data.SqlClient;

namespace AssistenzaTecnica.DataAccessLayer
{
    public class AssegnatiDalc
    {
        private Dictionary<int, Utente> _utenti = new Dictionary<int, Utente>();
        private void riempiUtentiDaDB()
        {
            UtentiDalc utentiDalc = new UtentiDalc();
            _utenti = utentiDalc.getAllUtenti();
        }

        public Dictionary<int, Assegnato> getAllAssegnati()
        {
            riempiUtentiDaDB();

            Dictionary<int, Assegnato> listaAssegnati = new Dictionary<int, Assegnato>();
            string connectionString = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand comm = new SqlCommand("SELECT id, nome, id_utenti FROM assegnati", conn);
                conn.Open();
                SqlDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    Assegnato a = new Assegnato();
                    a.Id = reader.GetInt32(0);
                    a.Nome = reader.GetString(1);
                    a.IdUtente = reader[2] == DBNull.Value ? null : (int?)reader.GetInt32(2);
                    a.Utente = a.IdUtente.HasValue ? _utenti[a.IdUtente.Value] : null;
                    listaAssegnati.Add(a.Id, a);
                }
            }

            return listaAssegnati;
        }

        public void getAssegnato(int idAssegnato, Assegnato a)
        {
            riempiUtentiDaDB();
            string connectionString = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand comm = new SqlCommand("SELECT nome, id_utenti FROM assegnati WHERE id = " + idAssegnato, conn);
                conn.Open();
                SqlDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    a.Id = idAssegnato;
                    a.Nome = reader.GetString(0);
                    a.IdUtente = reader[2] == DBNull.Value ? null : (int?)reader.GetInt32(2);
                    a.Utente = a.IdUtente.HasValue ? _utenti[a.IdUtente.Value] : null;
                }
            }
        }

        public void salvaAssegnato(Assegnato a)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand comm;
                if (a.Id == 0)
                {
                    comm = new SqlCommand("INSERT INTO assegnati (nome, id_utenti) VALUES (@nome, @id_utenti)", conn);
                    comm.Parameters.AddWithValue("@nome", a.Nome);
                    if( a.IdUtente.HasValue )
                        comm.Parameters.AddWithValue("@id_utenti", a.IdUtente.Value);
                    else
                        comm.Parameters.AddWithValue("@id_utenti", DBNull.Value);
                }
                else
                {
                    comm = new SqlCommand("UPDATE assegnati SET nome = @nome, id_utenti = @id_utenti WHERE id = @id", conn);
                    comm.Parameters.AddWithValue("@nome", a.Nome);
                    if (a.IdUtente.HasValue)
                        comm.Parameters.AddWithValue("@id_utenti", a.IdUtente.Value);
                    else
                        comm.Parameters.AddWithValue("@id_utenti", DBNull.Value);
                    comm.Parameters.AddWithValue("@id", a.Id);
                }
                conn.Open();
                comm.ExecuteNonQuery();
            }
        }

        public void eliminaAssegnato(int idAssegnato)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand comm = new SqlCommand("DELETE FROM assegnati WHERE id = " + idAssegnato, conn);
                conn.Open();
                comm.ExecuteNonQuery();
            }
        }
    }
}