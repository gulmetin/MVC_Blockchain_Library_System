using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using DataAccessLayer;
using DataAccessLayer.Model.Entity;
using BusinessLayer;

namespace MVCKutuphane.Controllers
{
    public class UyeController : Controller
    {
        // GET: Uye
        //DBKUTUPHANEEntities1 db = new DBKUTUPHANEEntities1();
        UyeDBIslemleri uyeDb = new UyeDBIslemleri();
        UyeBsnsIslemleri uyeBsns = new UyeBsnsIslemleri();
        public ActionResult Index(int sayfa=1)
        {
            return View(uyeDb.getAllUyeler().ToList().ToPagedList(sayfa, 3));
        }
        [HttpGet]
        public ActionResult UyeEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UyeEkle(tblUyeler u)
        {

            if (!ModelState.IsValid)
            {
                return View("UyeEkle");
            }
            bool sonuc = false;
            using (ServiceReference2.KPSPublicSoapClient tc = new ServiceReference2.KPSPublicSoapClient())
            {
                sonuc = tc.TCKimlikNoDogrula(Convert.ToInt64(u.TC), u.AD, u.SOYAD, Convert.ToInt32(u.DOGUMYILI));
            }
            if (!sonuc)
            {
                return View("UyeEkle");
            }
            uyeDb.addUye(u);
            return View();

        }

        public ActionResult UyeSil(int id)
        {
            uyeDb.deleteUye(id);
            return RedirectToAction("Index");
        }

        public ActionResult UyeGetir(int id)
        {
            return View("UyeGetir", uyeDb.getUyeByUyeId(id));
        }

        public ActionResult UyeGuncelle(tblUyeler u)
        {
            uyeDb.updateUye(u);
            return RedirectToAction("Index");
        }
    }
}