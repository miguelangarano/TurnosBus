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
using System.Text;

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

        public ActionResult CodigoQr()
        {
            return View("CodigoQr");
        }

        public JsonResult getFrecsTable()
        {
            try
            {
                var frecuencias = db.frequencies.ToList<frequency>();
                List<FrequenTurn> lista = new List<FrequenTurn>();
                foreach (frequency l in frecuencias)
                {
                    FrequenTurn dummy = new FrequenTurn() { id = l.id, day = l.day, hour = l.hour.ToString(), place = l.place.name, capacity = (int)l.bus.capacity };
                    lista.Add(dummy);
                }
                return Json(lista);
            }
            catch (Exception error)
            {
                return Json(error);
            }
        }

        public JsonResult saveTurn(int id_frecuency, string id_client)
        {
            try
            {
                var frecuencias = db.frequencies.Where(x=>x.id==id_frecuency).ToList<frequency>();

                if (checkTurn(id_client, (TimeSpan)frecuencias[0].hour))
                {
                    turn turn = new turn() { code = GetRandomAlphaNumeric(50), date = DateTime.Today, hour = frecuencias[0].hour, last = true, id_place = frecuencias[0].id_place, id_bus = frecuencias[0].id_bus, id_client = id_client };

                    db.turns.Add(turn);
                    db.SaveChanges();

                    code code = new code() { id_turn = turn.id, turncode = turn.code, assisted = false };
                    db.codes.Add(code);
                    db.SaveChanges();
                    QrTurn qr = new QrTurn() { code = turn.code, saved = true };
                    return Json(qr);
                }
                else
                {
                    QrTurn qr = new QrTurn() { code = "", saved = false };
                    return Json(false);
                }
            }
            catch(Exception e)
            {
                return Json(false+""+e);
            }
        }

        public string GetRandomAlphaNumeric(int length)
        {
            char[] chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789".ToCharArray();
            StringBuilder result = new StringBuilder();
            Random rn = new Random();
            for(int i = 0; i < length; i++)
            {
                result.Append(chars[rn.Next(chars.Length)]);
            }
            return result.ToString();
        }

        public bool checkTurn(string id_client, TimeSpan time)
        {
       
            var turns = db.turns.Where(x => x.date == DateTime.Today && x.id_client == id_client && x.hour == time).ToList<turn>();
            if (turns.Count==0)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }
    }
}

class FrequenTurn
{
    public int id { get; set; }
    public string day { get; set; }
    public string hour { get; set; }
    public string place { get; set; }
    public int capacity { get; set; }
}

class QrTurn
{
    public string code { get; set; }
    public bool saved { get; set; }
}
