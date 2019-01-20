﻿using System;
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
          string  CodigoQR2 = turn.id_client + DateTime.Now.ToString("yyyy-MM-dd");
            if (ModelState.IsValid)
            {
                turn.date= DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
                turn.hour = TimeSpan.Parse(DateTime.Now.ToString("hh:mm"));
                db.turns.Add(turn);
                db.SaveChanges();
                return RedirectToAction("CodigoQr","Turno");
            }

            ViewBag.id_bus = new SelectList(db.buses, "id", "plate", turn.id_bus);
            ViewBag.id_client = new SelectList(db.clients, "id", "name", turn.id_client);
            ViewBag.id_place = new SelectList(db.places, "id", "name", turn.id_place);
            return View(turn);
        }

        public ActionResult CodigoQr()
        {
            string codigoQr = "CODIGO QR RESULTANTE";
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
            return View();
        }
    }
}
