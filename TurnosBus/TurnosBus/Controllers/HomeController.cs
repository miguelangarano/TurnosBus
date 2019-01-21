using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TurnosBus.Models;

namespace TurnosBus.Controllers
{
    public class HomeController : Controller
    {

        private TurnosBusEntities db = new TurnosBusEntities();


        public ActionResult Index()
        {
            return View("Login");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
    
        public JsonResult LoginGet(string mail, string password)
        {
            try
            {
                var usuario = db.clients.Where(x => x.mail == mail).ToList<client>();
                Clnt cl;
                if (password.Equals(usuario[0].password))
                {
                     cl = new Clnt() { id = usuario[0].id, banned = usuario[0].banned.ToString() };
                }
                else
                {
                    cl = new Clnt() { id = null, banned = null };
                }
                
                if (cl.id != null)
                {
                    return Json(cl);
                }
                else
                {
                    return Json("0");
                }
            }catch(Exception e)
            {
                return Json(e.ToString());
            }
        }

        public JsonResult ValidarUsuario(String date, String hour, String id_cliente)
        {
            return Json(date);
        }
    }

    class Clnt
    {
        public string id { get; set; }
        public string banned { get; set; }
    }
}