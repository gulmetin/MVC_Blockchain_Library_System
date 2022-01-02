using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DataAccessLayer;
using DataAccessLayer.Model.Entity;

namespace MVCKutuphane.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        //DBKUTUPHANEEntities1 db = new DBKUTUPHANEEntities1();
        UyeDBIslemleri uyeDb = new UyeDBIslemleri();
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(tblUyeler p)
        {
            var bilgiler = uyeDb.getUyeByMailAndSifre(p);
            if (bilgiler != null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.MAIL, false);
                Session["MAIL"] = bilgiler.MAIL.ToString();
                Session["ID"] = bilgiler.ID.ToString();
                Session["AD"] = bilgiler.AD.ToString();
                Session["SOYAD"] = bilgiler.SOYAD.ToString();
                Session["KULLANICIADI"] = bilgiler.KULLANICIADI.ToString();
                Session["SIFRE"] = bilgiler.SIFRE.ToString();
                Session["TELEFON"] = bilgiler.TELEFON.ToString();
                Session["OKUL"] = bilgiler.OKUL.ToString();
                Session["TC"] = bilgiler.TC.ToString();
                Session["DOGUMYILI"] = bilgiler.DOGUMYILI.ToString();
                return RedirectToAction("Index", "Panelim");
            }
            else
            {
                return View();
            }
            
        }
    }
}