using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AssistenzaTecnica.DataAccessLayer;

namespace AssistenzaTecnica.Models
{
    public class Riferimento
    {
        private RiferimentiDalc _riferimentiDalc = new RiferimentiDalc();
        
        public static Dictionary<int, Riferimento> getAllRiferimenti()
        {
            return new RiferimentiDalc().getAllRiferimenti();
        }

        public int Id { get; set; }
        public string Descrizione { get; set; }
        public int? IdUtente { get; set; }
        public Utente Utente { get; set; }

        public Riferimento() { }

        public Riferimento(int idRiferimento)
        {
            _riferimentiDalc.getRiferimento(idRiferimento, this);
        }

        public void SalvaSuDb()
        {
            _riferimentiDalc.salvaRiferimento(this);
        }

        public static void EliminaDaDb(int idRiferimento)
        {
            new RiferimentiDalc().eliminaRiferimento(idRiferimento);
        }
    }
}