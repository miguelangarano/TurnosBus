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
    public class clientsController : Controller
    {
        private TurnosBusEntities db = new TurnosBusEntities();

        // GET: clients
        public ActionResult Index()
        {
            return View(db.clients.ToList());
        }

        // GET: clients/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            client client = db.clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // GET: clients/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: clients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,mail,password")] client client)
        {
            if (ModelState.IsValid)
            {
                db.clients.Add(client);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(client);
        }




        public string SaveClients(string id, string name, string mail, string password)
        { 

            client client = new client() { id = id, name = name, mail = mail, password = password };
            try
            {
                db.clients.Add(client);
                db.SaveChanges();
                return "1";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        // GET: clients/Edit/5
    }
}
