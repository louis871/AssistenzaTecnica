using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AssistenzaTecnica.Models;

namespace AssistenzaTecnica.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Dictionary<int, Richiesta> richieste = Richiesta.getAllFromDB();
            return View(richieste);
        }

        public ActionResult EditRichiesta(int? idRichiesta)
        {
            if( !idRichiesta.HasValue || idRichiesta.Value == 0 )
                return RedirectToAction("Index");

            return View(new Richiesta(idRichiesta.Value));
        }

        public ActionResult SalvaDatiBaseRichiesta(Richiesta r)
        {
            r.SalvaSuDb();
            return RedirectToAction("Index");
        }

        public ActionResult EditStoricoRichiesta(int? idStatoRichiesta)
        {
            if (!idStatoRichiesta.HasValue || idStatoRichiesta.Value == 0)
                return RedirectToAction("Index");

            ViewBag.AllStati = Stato.getAllStati();
            ViewBag.AllUtenti = Utente.getAllUtenti();

            return View(new Richiesta.StatoRichiesta(idStatoRichiesta.Value));
        }

        public ActionResult SalvaStatoRichiesta(Richiesta.StatoRichiesta sr)
        {
            sr.SalvaSuDb();
            return RedirectToAction("EditRichiesta", new { idRichiesta = sr.IdRichiesta });
        }

        public ActionResult NuovoStoricoRichiesta(int? idRichiesta)
        {
            if (!idRichiesta.HasValue || idRichiesta.Value == 0)
                return RedirectToAction("Index");

            ViewBag.AllStati = Stato.getAllStati();
            ViewBag.AllUtenti = Utente.getAllUtenti();

            Richiesta.StatoRichiesta sr = new Richiesta.StatoRichiesta();
            sr.IdRichiesta = idRichiesta.Value;
            sr.Stato = new Stato();
            sr.Utente = new Utente();
            sr.DataAggiunta = DateTime.Now;

            return View("EditStoricoRichiesta", sr);
        }

        public ActionResult NuovaRichiesta()
        {
            Richiesta richiesta = new Richiesta();
            richiesta.Id = 0;
            richiesta.StoricoStati = new SortedDictionary<int, Richiesta.StatoRichiesta>();

            return View("EditRichiesta", richiesta);
        }

        public ActionResult EliminaRichiesta(int idRichiesta)
        {
            Richiesta.EliminaDaDb(idRichiesta);
            return RedirectToAction("Index");
        }

        public ActionResult EliminaStoricoRichiesta(int idStatoRichiesta, int idRichiesta)
        {
            Richiesta.StatoRichiesta.EliminaDaDb(idStatoRichiesta);
            return RedirectToAction("EditRichiesta", new { idRichiesta = idRichiesta });
        }
    }
}