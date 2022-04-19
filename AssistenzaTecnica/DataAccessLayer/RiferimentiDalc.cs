using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AssistenzaTecnica.Models;
using System.Configuration;
using System.Data.SqlClient;

namespace AssistenzaTecnica.DataAccessLayer
{
    public class RiferimentiDalc
    {
        public Dictionary<int, Riferimento> getAllRiferimenti()
        {
            Dictionary<int, Riferimento> listaRiferimenti = new Dictionary<int, Riferimento>();
            string connectionString = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand comm = new SqlCommand("SELECT id, descrizione FROM riferimenti", conn);
                conn.Open();
                SqlDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    Riferimento riferimento = new Riferimento();
                    riferimento.Id = reader.GetInt32(0);
                    riferimento.Descrizione = reader.GetString(1);
                    listaRiferimenti.Add(riferimento.Id, riferimento);
                }
            }

            return listaRiferimenti;
        }

        public void getRiferimento(int idRiferimento, Riferimento r)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand comm = new SqlCommand("SELECT descrizione FROM riferimenti WHERE id = " + idRiferimento, conn);
                conn.Open();
                SqlDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    r.Id = idRiferimento;
                    r.Descrizione = reader.GetString(0);
                }
            }
        }

        public void salvaRiferimento(Riferimento r)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand comm;
                if (r.Id == 0)
                {
                    comm = new SqlCommand("INSERT INTO riferimenti (descrizione) VALUES (@descrizione)", conn);
                    comm.Parameters.AddWithValue("@descrizione", r.Descrizione);
                }
                else
                {
                    comm = new SqlCommand("UPDATE riferimenti SET descrizione = @descrizione WHERE id = @id", conn);
                    comm.Parameters.AddWithValue("@descrizione", r.Descrizione);
                    comm.Parameters.AddWithValue("@id", r.Id);
                }
                conn.Open();
                comm.ExecuteNonQuery();
            }
        }

        public void eliminaRiferimento(int idRiferimento)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand comm = new SqlCommand("DELETE FROM riferimenti WHERE id = " + idRiferimento, conn);
                conn.Open();
                comm.ExecuteNonQuery();
            }
        }
    }
}