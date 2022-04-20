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
        private Dictionary<int, Utente> _utenti = new Dictionary<int, Utente>();
        private void riempiUtentiDaDB()
        {
            UtentiDalc utentiDalc = new UtentiDalc();
            _utenti = utentiDalc.getAllUtenti();
        }

        public Dictionary<int, Riferimento> getAllRiferimenti()
        {
            riempiUtentiDaDB();

            Dictionary<int, Riferimento> listaRiferimenti = new Dictionary<int, Riferimento>();
            string connectionString = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand comm = new SqlCommand("SELECT id, descrizione, id_utenti FROM riferimenti", conn);
                conn.Open();
                SqlDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    Riferimento riferimento = new Riferimento();
                    riferimento.Id = reader.GetInt32(0);
                    riferimento.Descrizione = reader.GetString(1);
                    riferimento.IdUtente = reader[2] == DBNull.Value ? null : (int?)reader.GetInt32(2);
                    riferimento.Utente = riferimento.IdUtente.HasValue ? _utenti[riferimento.IdUtente.Value] : null;
                    listaRiferimenti.Add(riferimento.Id, riferimento);
                }
            }

            return listaRiferimenti;
        }

        public void getRiferimento(int idRiferimento, Riferimento r)
        {
            riempiUtentiDaDB();

            string connectionString = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand comm = new SqlCommand("SELECT descrizione, id_utenti FROM riferimenti WHERE id = " + idRiferimento, conn);
                conn.Open();
                SqlDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    r.Id = idRiferimento;
                    r.Descrizione = reader.GetString(0);
                    r.IdUtente = reader[2] == DBNull.Value ? null : (int?)reader.GetInt32(2);
                    r.Utente = r.IdUtente.HasValue ? _utenti[r.IdUtente.Value] : null;
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
                    comm = new SqlCommand("INSERT INTO riferimenti (descrizione, id_utenti) VALUES (@descrizione, @id_utenti)", conn);
                    comm.Parameters.AddWithValue("@descrizione", r.Descrizione);
                    if (r.IdUtente.HasValue)
                        comm.Parameters.AddWithValue("@id_utenti", r.IdUtente.Value);
                    else
                        comm.Parameters.AddWithValue("@id_utenti", DBNull.Value);
                }
                else
                {
                    comm = new SqlCommand("UPDATE riferimenti SET descrizione = @descrizione, id_utenti = @id_utenti WHERE id = @id", conn);
                    comm.Parameters.AddWithValue("@descrizione", r.Descrizione);
                    if (r.IdUtente.HasValue)
                        comm.Parameters.AddWithValue("@id_utenti", r.IdUtente.Value);
                    else
                        comm.Parameters.AddWithValue("@id_utenti", DBNull.Value);
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