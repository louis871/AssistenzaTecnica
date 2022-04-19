using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AssistenzaTecnica.DataAccessLayer;

namespace AssistenzaTecnica.Models
{
    public class Assegnato
    {
        private AssegnatiDalc _assegnatiDalc = new AssegnatiDalc();

        public static Dictionary<int, Assegnato> getAllAssegnati()
        {
            return new AssegnatiDalc().getAllAssegnati();
        }

        public int Id { get; set; } 
        public string Nome { get; set; }

        public Assegnato() { }

        public Assegnato(int idAssegnato)
        {
            _assegnatiDalc.getAssegnato(idAssegnato, this);
        }

        public void SalvaSuDb()
        {
            _assegnatiDalc.salvaAssegnato(this);
        }

        public static void EliminaDaDb(int idAssegnato)
        {
            new AssegnatiDalc().eliminaAssegnato(idAssegnato);
        }
    }
}