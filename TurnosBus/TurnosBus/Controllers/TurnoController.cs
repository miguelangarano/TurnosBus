using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TurnosBus.Models;
using QRCoder;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace TurnosBus.Controllers
{
    public class TurnoController : Controller
    {
        private TurnosBusEntities db = new TurnosBusEntities();
        // GET: Turno/Create
        public ActionResult Create()
        {
            return View();
        }
        // POST: Turno/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,code,date,hour,id_place,id_bus,id_client")] turn turn)
        {
            if (ModelState.IsValid)
            {
                turn.date = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
                turn.hour = TimeSpan.Parse(DateTime.Now.ToString("hh:mm"));
                //db.turns.Add(turn);
                //db.SaveChanges();
                return RedirectToAction("CodigoQr", "Turno", turn);
            }
            ViewBag.id_bus = new SelectList(db.buses, "id", "plate", turn.id_bus);
            ViewBag.id_client = new SelectList(db.clients, "id", "name", turn.id_client);
            ViewBag.id_place = new SelectList(db.places, "id", "name", turn.id_place);
            return View(turn);
        }

        public ActionResult CodigoQr(turn turn)
        {
            string codigoQr = turn.client + turn.date.ToString();
            turn.code = turn.client + turn.date.ToString();
            using (MemoryStream ms = new MemoryStream())
            {
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode(codigoQr, QRCodeGenerator.ECCLevel.Q);
                using (Bitmap bitMap = qrCode.GetGraphic(20))
                {
                    bitMap.Save(ms, ImageFormat.Png);
                    ViewBag.QRCodeImage = "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
                }
            }
            if (ModelState.IsValid)
            {
                //db.turns.Add(turn);
                //db.SaveChanges();
            }
            return View(turn);
        }

        public JsonResult getFrecsTable()
        {
            try
            {
                var frecuencias = db.frequencies.Where(x=>x.id==1).ToList<frequency>();
                List<FrequenTurn> lista = new List<FrequenTurn>();
                foreach (frequency l in frecuencias)
                {
                    FrequenTurn dummy = new FrequenTurn() { id = l.id, hour = DateTime.Now.TimeOfDay.ToString(), place_nombre = l.place.name, capacity = (int)l.bus.capacity };
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
}

class FrequenTurn
{
    public int id { get; set; }
    public string hour { get; set; }
    public string place_nombre { get; set; }
    public int capacity { get; set; }
}
