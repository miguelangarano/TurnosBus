using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TurnosBus.Models;

namespace TurnosBus.Controllers
{
    public class CancelarController : Controller
    {
        private TurnosBusEntities db = new TurnosBusEntities();

        // GET: Cancelar
        public ActionResult Index()
        {
            return View();
        }


    }
}