using Newtonsoft.Json;
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

        [HttpPost]
        [AllowAnonymous]
        public JsonResult getHorarioTable()
        {
            try
            {
                var fechaList = db.frequencies.Where(f => f.available == true).ToList<frequency>();
                List<Frecuen> lista = new List<Frecuen>();
                foreach (frequency l in fechaList)
                {
                    Frecuen dummy = new Frecuen() { id = l.id, day = l.day, hour = l.hour.ToString(), available = (bool)l.available, place = l.place.name, capacity = (int)l.bus.capacity };
                    lista.Add(dummy);
                }
                return Json(lista);
            }
            catch (Exception error)
            {
                return Json(error);
            }

        }
    }

    class Frecuen
    {
        public int id { get; set; }
        public string day { get; set; }
        public string hour { get; set; }
        public bool available { get; set; }
        public string place { get; set; }
        public int capacity { get; set; }
    }
}