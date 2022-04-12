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
            Dictionary<int, Utente> utente = Utente.getAllUtenti();
            return View(utente);
        }

        public ActionResult EditUtente(int? idUtente)
        {
            if (!idUtente.HasValue || idUtente.Value == 0)
                return RedirectToAction("Index");

            return View(new Utente(idUtente.Value));
        }

        public ActionResult SalvaUtente(Utente u)
        {
            u.SalvaSuDb();
            return RedirectToAction("Index");
        }

        public ActionResult NuovoUtente()
        {
            return View("EditUtente", new Utente());
        }

        public ActionResult EliminaUtente(int idUtente)
        {
            Utente.EliminaDaDb(idUtente);
            return RedirectToAction("Index");
        }
    }
}