using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TurnosBus.Models;

namespace TurnosBus.Controllers
{
    public class VerificarController : Controller
    {
        private TurnosBusEntities db = new TurnosBusEntities();
        // GET: Verificar
        public ActionResult Index()
        {
            return View(db.frequencies.ToList());
        }
    }
}