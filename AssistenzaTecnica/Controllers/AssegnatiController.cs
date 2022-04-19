using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AssistenzaTecnica.Models;

namespace AssistenzaTecnica.Controllers
{
    public class AssegnatiController : Controller
    {
        public ActionResult Index()
        {
            if (Utente.UtenteConnesso == null)
                return RedirectToAction("Login", "Home");

            Dictionary<int, Assegnato> assegnati = Assegnato.getAllAssegnati();
            return View(assegnati);
        }

        public ActionResult EditAssegnato(int? idAssegnato)
        {
            if (Utente.UtenteConnesso == null)
                return RedirectToAction("Login", "Home");

            if (!idAssegnato.HasValue || idAssegnato.Value == 0)
                return RedirectToAction("Index");

            return View(new Assegnato(idAssegnato.Value));
        }

        public ActionResult SalvaAssegnato(Assegnato a)
        {
            a.SalvaSuDb();
            return RedirectToAction("Index");
        }

        public ActionResult NuovoAssegnato()
        {
            if (Utente.UtenteConnesso == null)
                return RedirectToAction("Login", "Home");

            return View("EditAssegnato", new Assegnato());
        }

        public ActionResult EliminaAssegnato(int idAssegnato)
        {
            if (Utente.UtenteConnesso == null)
                return RedirectToAction("Login", "Home");

            Assegnato.EliminaDaDb(idAssegnato);
            return RedirectToAction("Index");
        }
    }
}