using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TurnosBus.Models;

namespace TurnosBus.Controllers
{
    public class FrequenciesController : Controller
    {
        private TurnosBusEntities db = new TurnosBusEntities();

        // GET: Frequencies
        public ActionResult Manejar()
        {
            var frequencies = db.frequencies.Include(f => f.bus).Include(f => f.place);
            return View(frequencies.ToList());
        }

        public JsonResult getFrequenciesTable()
        {
            try
            {
                var frecuencias = db.frequencies.Include(f => f.bus).Include(f => f.place).ToList<frequency>();
                List<Frequen> lista = new List<Frequen>();
                foreach (frequency l in frecuencias)
                {
                    Frequen dummy = new Frequen() { id = l.id, day = l.day, hour = l.hour.ToString(), available = (bool)l.available, place_nombre = l.place.name, bus_placa = l.bus.plate };
                    lista.Add(dummy);
                }
                return Json(lista);
            }
            catch (Exception error)
            {
                return Json(error);
            }

        }

        public JsonResult cancelarFrecuencia(int id_frecuen)
        {
            return Json("");
        }

        public JsonResult activarFrecuencia(int id_frecuen)
        {
            return Json("");
        }


    }

    class Frequen
    {
        public int id { get; set; }
        public string day { get; set; }
        public string hour { get; set; }
        public bool available { get; set; }
        public string place_nombre { get; set; }
        public string bus_placa { get; set; }
    }

}
