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

        public JsonResult GetTurn(String id_cliente)
        {
            //return Json((date));
            var turn = db.turns.Where(t=>t.id_client.Equals(id_cliente)).ToList<turn>();
            if (turn.Count>0)
            {
                CancelClass cancelClass = new CancelClass() { id = turn[turn.Count - 1].id, date = turn[turn.Count - 1].date.ToString(), hour = turn[turn.Count - 1].hour.ToString(), place= turn[turn.Count - 1].place.name };
                //turno = turn[turn.Count - 1];
                return Json(cancelClass);
            }
            else
            {
                return Json(id_cliente);
            }
            
        }

        public bool CancelarTurno(String id_turno)
        {
            return true;
        }
    }

    public class CancelClass
    {
        public int id { get; set; }
        public String date { get; set; }
        public String hour { get; set; }
        public String place { get; set; }
    }
}

