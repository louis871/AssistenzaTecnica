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
        /* richieste */
        public ActionResult Index()
        {
            if (Utente.UtenteConnesso == null)
                return RedirectToAction("Login");

            Dictionary<int, Richiesta> richieste = Richiesta.getAllFromDB();
            return View(richieste);
        }

        public ActionResult EditRichiesta(int? idRichiesta)
        {
            if (Utente.UtenteConnesso == null)
                return RedirectToAction("Login");

            if ( !idRichiesta.HasValue || idRichiesta.Value == 0 )
                return RedirectToAction("Index");

            return View(new Richiesta(idRichiesta.Value));
        }

        public ActionResult NuovaRichiesta()
        {
            if (Utente.UtenteConnesso == null)
                return RedirectToAction("Login");

            Richiesta richiesta = new Richiesta();
            richiesta.Id = 0;
            richiesta.StoricoStati = new SortedDictionary<int, Richiesta.StatoRichiesta>();

            return View("EditRichiesta", richiesta);
        }

        public ActionResult EliminaRichiesta(int idRichiesta)
        {
            if (Utente.UtenteConnesso == null)
                return RedirectToAction("Login");

            Richiesta.EliminaDaDb(idRichiesta);
            return RedirectToAction("Index");
        }

        public ActionResult SalvaDatiBaseRichiesta(Richiesta r)
        {
            r.SalvaSuDb();
            return RedirectToAction("Index");
        }

        /* storico stati richiesta */
        public ActionResult EditStoricoRichiesta(int? idStatoRichiesta)
        {
            if (Utente.UtenteConnesso == null)
                return RedirectToAction("Login");

            if (!idStatoRichiesta.HasValue || idStatoRichiesta.Value == 0)
                return RedirectToAction("Index");

            ViewBag.AllStati = Stato.getAllStati();
            ViewBag.AllUtenti = Utente.getAllUtenti();

            return View(new Richiesta.StatoRichiesta(idStatoRichiesta.Value));
        }

        public ActionResult NuovoStoricoRichiesta(int? idRichiesta)
        {
            if (Utente.UtenteConnesso == null)
                return RedirectToAction("Login");

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

        public ActionResult EliminaStoricoRichiesta(int idStatoRichiesta, int idRichiesta)
        {
            if (Utente.UtenteConnesso == null)
                return RedirectToAction("Login");

            Richiesta.StatoRichiesta.EliminaDaDb(idStatoRichiesta);
            return RedirectToAction("EditRichiesta", new { idRichiesta = idRichiesta });
        }

        public ActionResult SalvaStatoRichiesta(Richiesta.StatoRichiesta sr)
        {
            sr.SalvaSuDb();
            return RedirectToAction("EditRichiesta", new { idRichiesta = sr.IdRichiesta });
        }

        /* login */
        public ActionResult Login()
        { 
            if( Utente.UtenteConnesso != null )
                return RedirectToAction("Index");

            return View();
        }
        
        public ActionResult CheckLogin(string username, string password)
        {
            Utente.checkAndConnettiUtente(username, password);
            return RedirectToAction("Index");
        }

        public ActionResult Logout()
        {
            Utente.UtenteConnesso = null;
            return RedirectToAction("Login");
        }
    }
}