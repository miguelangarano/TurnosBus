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

        public JsonResult GetTurn(String date, String hour, String id_cliente)
        {
            return Json((date));
            /*var turn = db.turns.Where(t => t.date.Equals(DateTime.Parse(date)) && t.hour.Equals(TimeSpan.Parse(hour)) && t.id_client.Equals(id_cliente)).ToList<turn>();
            if (turn != null)
            {
                return Json(turn);
            }
            else
            {
                return Json("Holaaa");
            }*/
            
        }
    }
}