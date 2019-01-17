using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TurnosBus.Models;

namespace TurnosBus.Controllers
{
    public class HorarioController : Controller
    {

        TurnosBusEntities db = new TurnosBusEntities();

        // GET: Horario
        public ActionResult Index()
        {
            return View();
        }

        //[HttpPost]
        //[AllowAnonymous]
        public JsonResult getHorarioTable()
        {
            try
            {
                var fechaList = db.frequencies.Where(f => f.available == 1).ToList<frequency>();
                List<int> freq=new List<int>();
                foreach(frequency fecha in fechaList)
                {
                    freq.Add((int)fecha.bus.capacity);
                }
                return Json(fechaList);
               
            }
            catch (Exception error)
            {
                return Json(error);
            }

        }
    }
}