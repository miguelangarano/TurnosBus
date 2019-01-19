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
    public class TurnoController : Controller
    {
        private TurnosBusEntities db = new TurnosBusEntities();


        // GET: Turno
        public ActionResult Index()
        {
            var turns = db.turns.Include(t => t.bus).Include(t => t.client).Include(t => t.place);
            return View(turns.ToList());
        }

        // GET: Turno/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            turn turn = db.turns.Find(id);
            if (turn == null)
            {
                return HttpNotFound();
            }
            return View(turn);
        }

        // GET: Turno/Create
        public ActionResult Create()
        {
            ViewBag.id_bus = new SelectList(db.buses, "id", "plate");
            ViewBag.id_client = new SelectList(db.clients, "id", "name");
            ViewBag.id_place = new SelectList(db.places, "id", "name");
            return View();
        }

        // POST: Turno/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,code,date,hour,id_place,id_bus,id_client")] turn turn)
        {
            if (ModelState.IsValid)
            {
                db.turns.Add(turn);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_bus = new SelectList(db.buses, "id", "plate", turn.id_bus);
            ViewBag.id_client = new SelectList(db.clients, "id", "name", turn.id_client);
            ViewBag.id_place = new SelectList(db.places, "id", "name", turn.id_place);
            return View(turn);
        }
    }
}
