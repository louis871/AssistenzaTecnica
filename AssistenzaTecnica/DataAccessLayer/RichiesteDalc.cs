using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AssistenzaTecnica.Models;
using System.Configuration;
using System.Data.SqlClient;

namespace AssistenzaTecnica.DataAccessLayer
{
    public class RichiesteDalc
    {
        private Dictionary<int, Stato> _stati = new Dictionary<int, Stato>();
        private void riempiStatiDaDB()
        {
            StatiDalc statiDalc = new StatiDalc();
            _stati = statiDalc.getAllStati();
        }

        private Dictionary<int, Utente> _utenti = new Dictionary<int, Utente>();
        private void riempiUtentiDaDB()
        {
            UtentiDalc utentiDalc = new UtentiDalc();
            _utenti = utentiDalc.getAllUtenti();
        }

        public Dictionary<int, Richiesta> getAllRichieste()
        {
            riempiStatiDaDB();
            riempiUtentiDaDB();
            
            Dictionary<int, Richiesta> listaRichieste = new Dictionary<int, Richiesta>();
            string connectionString = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand comm = new SqlCommand("SELECT id, testo, riferimento FROM richieste", conn);
                conn.Open();
                SqlDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    Richiesta richiesta = new Richiesta();
                    richiesta.Id = reader.GetInt32(0);
                    richiesta.Testo = reader.GetString(1);
                    richiesta.Riferimento = reader[2] == DBNull.Value ? null : reader.GetString(2);
                    richiesta.StoricoStati = getStoricoStatiRichiesta(richiesta.Id);
                    listaRichieste.Add(richiesta.Id, richiesta);
                }
            }

            return listaRichieste;
        }

        private SortedDictionary<int, Richiesta.StatoRichiesta> getStoricoStatiRichiesta(int idRichiesta)
        {
            SortedDictionary<int, Richiesta.StatoRichiesta> listaStatiRichiesta = new SortedDictionary<int, Richiesta.StatoRichiesta>();
            string connectionString = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString; 
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand comm = new SqlCommand("SELECT id, id_stati, id_utenti, data_aggiunta, assegnazione, ore_lavorate FROM stati_richieste WHERE id_richieste = " + idRichiesta, conn);
                conn.Open();
                SqlDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    Richiesta.StatoRichiesta sr = new Richiesta.StatoRichiesta();
                    sr.Id = reader.GetInt32(0);
                    sr.IdRichiesta = idRichiesta;
                    sr.Stato = _stati[reader.GetInt32(1)];
                    sr.Utente = _utenti[reader.GetInt32(2)];
                    sr.DataAggiunta = reader.GetDateTime(3);
                    sr.Assegnazione = reader.IsDBNull(4) ? null : reader.GetString(4);
                    sr.OreLavorate = reader.IsDBNull(5) ? 0 : reader.GetDouble(5);
                    listaStatiRichiesta.Add(sr.Id, sr);
                }
            }

            return listaStatiRichiesta;
        }

        public void getRichiesta(int idRichiesta, Richiesta richiesta)
        {
            riempiStatiDaDB();
            riempiUtentiDaDB();

            string connectionString = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand comm = new SqlCommand("SELECT id, testo, riferimento FROM richieste WHERE id = " + idRichiesta, conn);
                conn.Open();
                SqlDataReader reader = comm.ExecuteReader();
                if (reader.Read())
                {
                    richiesta.Id = reader.GetInt32(0);
                    richiesta.Testo = reader.GetString(1);
                    richiesta.Riferimento = reader[2] == DBNull.Value ? null : reader.GetString(2);
                    richiesta.StoricoStati = getStoricoStatiRichiesta(richiesta.Id);
                }
            }
        }

        public void SalvaRichiesta(Richiesta richiesta)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand comm;
                conn.Open();
                if ( richiesta.Id == 0 )
                {
                    comm = new SqlCommand("INSERT INTO richieste (testo, riferimento) OUTPUT Inserted.ID VALUES (@testo, @riferimento)", conn);
                    comm.Parameters.AddWithValue("@testo", richiesta.Testo);
                    if( richiesta.Riferimento == null )
                        comm.Parameters.AddWithValue("@riferimento", DBNull.Value);
                    else
                        comm.Parameters.AddWithValue("@riferimento", richiesta.Riferimento);
                    object idInserito = comm.ExecuteScalar();
                    comm = new SqlCommand("INSERT INTO stati_richieste (id_richieste, id_stati, id_utenti, data_aggiunta) VALUES (@id_richieste, @id_stati, @id_utenti, @data_aggiunta)", conn);
                    comm.Parameters.AddWithValue("@id_richieste", (int)idInserito);
                    comm.Parameters.AddWithValue("@id_stati", Stato.ID_STATO_INSERITO);
                    comm.Parameters.AddWithValue("@id_utenti", 1);
                    comm.Parameters.AddWithValue("@data_aggiunta", DateTime.Now);
                    comm.ExecuteNonQuery();
                }
                else
                {
                    comm = new SqlCommand("UPDATE richieste SET testo = @testo, riferimento = @riferimento WHERE id = @id", conn);
                    comm.Parameters.AddWithValue("@testo", richiesta.Testo);
                    if (richiesta.Riferimento == null)
                        comm.Parameters.AddWithValue("@riferimento", DBNull.Value);
                    else
                        comm.Parameters.AddWithValue("@riferimento", richiesta.Riferimento);
                    comm.Parameters.AddWithValue("@id", richiesta.Id);
                    comm.ExecuteNonQuery();
                }
            }
        }

        public void getStatoRichiesta(int idStatoRichiesta, Richiesta.StatoRichiesta sr)
        {
            riempiStatiDaDB();
            riempiUtentiDaDB();
            
            string connectionString = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand comm = new SqlCommand("SELECT id, id_richieste, id_stati, id_utenti, data_aggiunta, assegnazione, ore_lavorate FROM stati_richieste WHERE id = " + idStatoRichiesta, conn);
                conn.Open();
                SqlDataReader reader = comm.ExecuteReader();
                if (reader.Read())
                {
                    sr.Id = reader.GetInt32(0);
                    sr.IdRichiesta = reader.GetInt32(1);
                    sr.Stato = _stati[reader.GetInt32(2)];
                    sr.Utente = _utenti[reader.GetInt32(3)];
                    sr.DataAggiunta = reader.GetDateTime(4);
                    sr.Assegnazione = reader.IsDBNull(5) ? null : reader.GetString(5);
                    sr.OreLavorate = reader.IsDBNull(6) ? 0 : reader.GetDouble(6);
                }
            }
        }

        public void SalvaStatoRichiesta(Richiesta.StatoRichiesta sr)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand comm;
                if (sr.Id == 0)
                {
                    comm = new SqlCommand("INSERT INTO stati_richieste (id_richieste, id_stati, id_utenti, data_aggiunta, assegnazione, ore_lavorate) VALUES (@id_richieste, @id_stati, @id_utenti, @data_aggiunta, @assegnazione, @ore_lavorate)", conn);
                    comm.Parameters.AddWithValue("@id_richieste", sr.IdRichiesta);
                    comm.Parameters.AddWithValue("@id_stati", sr.IdStato);
                    comm.Parameters.AddWithValue("@id_utenti", sr.IdUtente);
                    comm.Parameters.AddWithValue("@data_aggiunta", sr.DataAggiunta);
                    if (sr.Assegnazione == null)
                        comm.Parameters.AddWithValue("@assegnazione", DBNull.Value);
                    else
                        comm.Parameters.AddWithValue("@assegnazione", sr.Assegnazione);
                   comm.Parameters.AddWithValue("@ore_lavorate", sr.OreLavorate);

                }
                else
                {
                    comm = new SqlCommand("UPDATE stati_richieste SET id_richieste = @id_richieste, id_stati = @id_stati, id_utenti = @id_utenti, data_aggiunta = @data_aggiunta, assegnazione = @assegnazione, ore_lavorate = @ore_lavorate WHERE id = @id", conn);
                    comm.Parameters.AddWithValue("@id_richieste", sr.IdRichiesta);
                    comm.Parameters.AddWithValue("@id_stati", sr.IdStato);
                    comm.Parameters.AddWithValue("@id_utenti", sr.IdUtente);
                    comm.Parameters.AddWithValue("@data_aggiunta", sr.DataAggiunta);
                    if( sr.Assegnazione == null )
                        comm.Parameters.AddWithValue("@assegnazione", DBNull.Value);
                    else
                        comm.Parameters.AddWithValue("@assegnazione", sr.Assegnazione);
                    comm.Parameters.AddWithValue("@ore_lavorate", sr.OreLavorate);
                    comm.Parameters.AddWithValue("@id", sr.Id);
                }
                conn.Open();
                comm.ExecuteNonQuery();
            }
        }

        public void eliminaRichiesta(int idRichiesta)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand comm = new SqlCommand("DELETE FROM richieste WHERE id = " + idRichiesta, conn);
                conn.Open();
                comm.ExecuteNonQuery();
            }
        }

        public void eliminaStatoRichiesta(int idStatoRichiesta)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand comm = new SqlCommand("DELETE FROM stati_richieste WHERE id = " + idStatoRichiesta, conn);
                conn.Open();
                comm.ExecuteNonQuery();
            }
        }
    }
}