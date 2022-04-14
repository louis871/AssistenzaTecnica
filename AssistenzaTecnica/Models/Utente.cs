using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AssistenzaTecnica.DataAccessLayer;

namespace AssistenzaTecnica.Models
{
    public class Utente
    {
        public static Utente UtenteConnesso = null;

        private UtentiDalc _utentiDalc = new UtentiDalc();

        public enum ProfiliUtente { TecnicoPrimoLivello, TecnicoOperativo, Organizzatore, Amministratore };

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Username { get; set; }
        public int Profilo { get; set; }
        public string NuovaPassword { get; set; }
        public string RipetiPassword { get; set; }

        public Utente() { }

        public Utente(int idUtente)
        {
            _utentiDalc.getUtente(idUtente, this);
        }

        public static Dictionary<int, Utente> getAllUtenti()
        {
            return new UtentiDalc().getAllUtenti();
        }

        public void SalvaSuDb()
        {
            _utentiDalc.salvaUtente(this);
        }

        public static void EliminaDaDb(int idUtente)
        {
            new UtentiDalc().eliminaUtente(idUtente);
        }

        public static void checkAndConnettiUtente(string username, string password)
        {
            UtenteConnesso = new UtentiDalc().checkLogin(username, password);
        }
    }
}