﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AssistenzaTecnica.DataAccessLayer;

namespace AssistenzaTecnica.Models
{
    public class Richiesta
    {
        private RichiesteDalc _richiesteDalc = new RichiesteDalc();

        public int Id { get; set; }
        public string Testo { get; set; }
        public int? IdRiferimento { get; set; }
        public Riferimento Riferimento { get; set; }
        public SortedDictionary<int, StatoRichiesta> StoricoStati { get; set; }
        public StatoRichiesta StatoCorrente
        {
            get
            {
                return StoricoStati == null || StoricoStati.Values.Count == 0 ? null : StoricoStati.Values.Last();
            }
        }
        public double OreLavorateTotali
        {
            get
            {
                double oreTotali = 0;
                if (StoricoStati != null)
                {
                    foreach (StatoRichiesta stato in StoricoStati.Values)
                    {
                        oreTotali += stato.OreLavorate;
                    }
                }
                return oreTotali;
            }
        }

        public static Dictionary<int, Richiesta> getAllFromDB()
        {
            RichiesteDalc richiesteDalc = new RichiesteDalc();
            return richiesteDalc.getAllRichieste();
        }

        public Richiesta() {}

        public Richiesta(int idRichiesta)
        {
            _richiesteDalc.getRichiesta(idRichiesta, this);
        }

        public void SalvaSuDb()
        {
            _richiesteDalc.SalvaRichiesta(this);
        }

        public static void EliminaDaDb(int idRichiesta)
        {
            RichiesteDalc richiesteDalc = new RichiesteDalc(); 
            richiesteDalc.eliminaRichiesta(idRichiesta);
        }

        public class StatoRichiesta
        {
            private RichiesteDalc _richiesteDalc = new RichiesteDalc();
            
            public int Id { get; set; }
            public int IdRichiesta { get; set; }
            public Stato Stato { get; set; }
            public int IdStato { get; set; }
            public Utente Utente { get; set; }
            public int IdUtente { get; set; }
            public DateTime DataAggiunta { get; set; }
            public int? IdAssegnazione { get; set; }
            public Assegnato Assegnazione { get; set; }
            public double OreLavorate { get; set; }

            public StatoRichiesta() { }

            public StatoRichiesta(int idStatoRichiesta)
            {
                _richiesteDalc.getStatoRichiesta(idStatoRichiesta, this);
            }

            public void SalvaSuDb()
            {
                _richiesteDalc.SalvaStatoRichiesta(this);
            }

            public static void EliminaDaDb(int idStatoRichiesta)
            {
                RichiesteDalc richiesteDalc = new RichiesteDalc(); 
                richiesteDalc.eliminaStatoRichiesta(idStatoRichiesta);
            }
        }
    }
}