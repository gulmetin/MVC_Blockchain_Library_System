using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DataAccessLayer;
using DataAccessLayer.Model.Entity;

namespace MvcKutuphane.Controllers
{
    public class PanelimController : Controller
    {
        // GET: Panelim
        //DBKUTUPHANEEntities1 db = new DBKUTUPHANEEntities1();
        UyeDBIslemleri uyeDb = new UyeDBIslemleri();
        HareketDBIslemleri hrktDb = new HareketDBIslemleri();

        [HttpGet]
        [Authorize]
        public ActionResult Index()
        {
           
            return View();
        }
        [HttpPost]
        public ActionResult Index2(tblUyeler p)
        {
            p.ID = Convert.ToInt32(Session["ID"]);
            uyeDb.updateUye(p);
            return RedirectToAction("Index");
        }

        public ActionResult Kitaplarim()
        {
            var uyeId = Convert.ToInt32(Session["ID"]);
            return View(hrktDb.getHareketByUyeId(uyeId).ToList());
        }

       

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }


    }
}

   