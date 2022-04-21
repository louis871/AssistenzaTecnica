using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AssistenzaTecnica.DataAccessLayer;

namespace AssistenzaTecnica.Models
{
    public class Stato
    {
        private StatiDalc _statiDalc = new StatiDalc();
        public static int ID_STATO_INSERITO = 1;

        public int Id { get; set; }
        public string Descrizione { get; set; }
        public int ProfiloMinimo { get; set; }

        public static Dictionary<int, Stato> getAllStati()
        {
            return new StatiDalc().getAllStati();
        }

        public Stato() { }

        public Stato(int idStato)
        {
            _statiDalc.getStato(idStato, this);
        }

        public void SalvaSuDb()
        {
            _statiDalc.salvaStato(this);
        }

        public static void EliminaDaDb(int idStato)
        {
            new StatiDalc().eliminaStato(idStato);
        }
    }
}