using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TurnosBus.Models;

namespace TurnosBus.Controllers
{
    public class ReportController : Controller
    {
        TurnosBusEntities db = new TurnosBusEntities();
        // GET: Report
        public ActionResult Index()
        {
            return View();
        }
      

        [HttpPost]
        [AllowAnonymous]
        public JsonResult getFrequencies()
        {
            try
            {
                var fechaList = db.frequencies.ToList<frequency>();
                List<FrequenciesTableDetail> lista = new List<FrequenciesTableDetail>();
                foreach (frequency l in fechaList)
                {
                    FrequenciesTableDetail dummy = new FrequenciesTableDetail() { day = l.day, hour = l.hour.ToString(), available = (bool)l.available, plate = l.bus.plate,capacity = (int)l.bus.capacity, place = l.place.name  };
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

    class FrequenciesTableDetail
    {
        public string day { get; set; }
        public string hour { get; set; }
        public bool available { get; set; }
        public string plate { get; set; }
        public int capacity { get; set; }
        public string place { get; set; }
        
    }
}