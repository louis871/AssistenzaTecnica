using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AssistenzaTecnica.DataAccessLayer;

namespace AssistenzaTecnica.Models
{
    public class Utente
    {
        private UtentiDalc _utentiDalc = new UtentiDalc();

        public int Id { get; set; }
        public string Nome { get; set; }

        public static Dictionary<int, Utente> getAllUtenti()
        {
            return new UtentiDalc().getAllUtenti();
        }

        public Utente() { }

        public Utente(int idUtente)
        {
            _utentiDalc.getUtente(idUtente, this);
        }

        public void SalvaSuDb()
        {
            _utentiDalc.salvaUtente(this);
        }

        public static void EliminaDaDb(int idUtente)
        {
            new UtentiDalc().eliminaUtente(idUtente);
        }
    }
}