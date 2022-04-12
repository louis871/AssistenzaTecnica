using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AssistenzaTecnica.Models;

namespace AssistenzaTecnica.Controllers
{
    public class StatiController : Controller
    {
        public ActionResult Index()
        {
            Dictionary<int, Stato> stati = Stato.getAllStati();
            return View(stati);
        }

        public ActionResult EditStato(int? idStato)
        {
            if (!idStato.HasValue || idStato.Value == 0)
                return RedirectToAction("Index");

            return View(new Stato(idStato.Value));
        }

        public ActionResult SalvaStato(Stato s)
        {
            s.SalvaSuDb();
            return RedirectToAction("Index");
        }

        public ActionResult NuovoStato()
        {
            return View("EditStato", new Stato());
        }

        public ActionResult EliminaStato(int idStato)
        {
            Stato.EliminaDaDb(idStato);
            return RedirectToAction("Index");
        }
    }
}