using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AssistenzaTecnica.DataAccessLayer;

namespace AssistenzaTecnica.Models
{
    public class Stato
    {
        //private StatiDalc _statiDalc = new StatiDalc();
        public static int ID_STATO_INSERITO = 1;

        public int Id { get; set; }
        public string Descrizione { get; set; }

        public static Dictionary<int, Stato> getAllStati()
        {
            return new StatiDalc().getAllStati();
        }
    }
}