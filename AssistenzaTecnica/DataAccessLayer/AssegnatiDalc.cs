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
        public Dictionary<int, Assegnato> getAllAssegnati()
        {
            Dictionary<int, Assegnato> listaAssegnati = new Dictionary<int, Assegnato>();
            string connectionString = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand comm = new SqlCommand("SELECT id, nome FROM assegnati", conn);
                conn.Open();
                SqlDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    Assegnato a = new Assegnato();
                    a.Id = reader.GetInt32(0);
                    a.Nome = reader.GetString(1);
                    listaAssegnati.Add(a.Id, a);
                }
            }

            return listaAssegnati;
        }

        public void getAssegnato(int idAssegnato, Assegnato a)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand comm = new SqlCommand("SELECT nome FROM assegnati WHERE id = " + idAssegnato, conn);
                conn.Open();
                SqlDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    a.Id = idAssegnato;
                    a.Nome = reader.GetString(0);
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
                    comm = new SqlCommand("INSERT INTO assegnati (nome) VALUES (@nome)", conn);
                    comm.Parameters.AddWithValue("@nome", a.Nome);
                }
                else
                {
                    comm = new SqlCommand("UPDATE assegnati SET nome = @nome WHERE id = @id", conn);
                    comm.Parameters.AddWithValue("@nome", a.Nome);
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