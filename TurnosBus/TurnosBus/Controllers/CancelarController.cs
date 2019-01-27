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
            var turn = db.turns.Where(t=>t.id_client.Equals(id_cliente) && t.last == true && t.date==DateTime.Today).ToList<turn>();
            if (turn.Count>0)
            {
                CancelClass cancelClass = new CancelClass() { id = turn[turn.Count - 1].id, date = turn[turn.Count - 1].date.ToString(), hour = turn[turn.Count - 1].hour.ToString(), place= turn[turn.Count - 1].place.name };
                return Json(cancelClass);
            }
            else
            {
                return Json(id_cliente);
            }
            
        }

        public JsonResult CancelarTurno(int id_turno)
        {
            try
            {
                code code = db.codes.Where(x => x.id_turn == id_turno).ToList<code>()[0];
                db.codes.Remove(code);
                db.SaveChanges();
                turn turn = db.turns.Where(x => x.id == id_turno).ToList<turn>()[0];
                turn.last = false;
                db.SaveChanges();
                return Json(true);
            }catch(Exception e)
            {
                return Json(false+"  "+e);
            }
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

