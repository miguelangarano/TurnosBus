using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TurnosBus.Models;

namespace TurnosBus.Controllers
{
    public class VerficarController : Controller
    {
        private TurnosBusEntities db = new TurnosBusEntities();
        // GET: Verficar
        public ActionResult Index()
        {
            ViewBag.id_bus = new SelectList(db.buses, "id", "plate");
            ViewBag.id_place = new SelectList(db.places, "id", "name");

            return View();
        }

        // GET: Verficar/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Verficar/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Verficar/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Verficar/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Verficar/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Verficar/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Verficar/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
