using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AssistenzaTecnica.Models;

namespace AssistenzaTecnica.Controllers
{
    public class RiferimentiController : Controller
    {
        public ActionResult Index()
        {
            if (Utente.UtenteConnesso == null)
                return RedirectToAction("Login", "Home");

            Dictionary<int, Riferimento> riferimenti = Riferimento.getAllRiferimenti();
            return View(riferimenti);
        }

        public ActionResult EditRiferimento(int? idRiferimento)
        {
            if (Utente.UtenteConnesso == null)
                return RedirectToAction("Login", "Home");

            if (!idRiferimento.HasValue || idRiferimento.Value == 0)
                return RedirectToAction("Index");

            return View(new Riferimento(idRiferimento.Value));
        }

        public ActionResult SalvaRiferimento(Riferimento r)
        {
            r.SalvaSuDb();
            return RedirectToAction("Index");
        }

        public ActionResult NuovoRiferimento()
        {
            if (Utente.UtenteConnesso == null)
                return RedirectToAction("Login", "Home");

            return View("EditRiferimento", new Riferimento());
        }

        public ActionResult EliminaRiferimento(int idRiferimento)
        {
            if (Utente.UtenteConnesso == null)
                return RedirectToAction("Login", "Home");

            Riferimento.EliminaDaDb(idRiferimento);
            return RedirectToAction("Index");
        }
    }
}