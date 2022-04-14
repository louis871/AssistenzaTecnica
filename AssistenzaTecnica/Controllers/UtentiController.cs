using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AssistenzaTecnica.Models;

namespace AssistenzaTecnica.Controllers
{
    public class UtentiController : Controller
    {
        public ActionResult Index()
        {
            if (Utente.UtenteConnesso == null)
                return RedirectToAction("Login", "Home");

            Dictionary<int, Utente> utente = Utente.getAllUtenti();
            return View(utente);
        }

        public ActionResult EditUtente(int? idUtente)
        {
            if (Utente.UtenteConnesso == null)
                return RedirectToAction("Login", "Home");

            if (!idUtente.HasValue || idUtente.Value == 0)
                return RedirectToAction("Index");

            return View(new Utente(idUtente.Value));
        }

        public ActionResult SalvaUtente(Utente u)
        {
            if( u.NuovaPassword != null && u.NuovaPassword != "" && u.NuovaPassword != u.RipetiPassword )
                return RedirectToAction("EditUtente", new { idUtente = u.Id });

            if (u.Id == 0 && (u.NuovaPassword == null || u.NuovaPassword == ""))
                return RedirectToAction("EditUtente", new { idUtente = u.Id });

            u.SalvaSuDb();
            if( u.Id == Utente.UtenteConnesso.Id )
            {
                Utente.UtenteConnesso = u;
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index");
        }

        public ActionResult NuovoUtente()
        {
            if (Utente.UtenteConnesso == null)
                return RedirectToAction("Login", "Home");

            return View("EditUtente", new Utente());
        }

        public ActionResult EliminaUtente(int idUtente)
        {
            if (Utente.UtenteConnesso == null)
                return RedirectToAction("Login", "Home");

            Utente.EliminaDaDb(idUtente);
            return RedirectToAction("Index");
        }
    }
}