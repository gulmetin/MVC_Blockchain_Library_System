using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer;
using DataAccessLayer.Model.Entity;
using DataAccessLayer.Model.Class;
using BlockChainLayer;

namespace MVCKutuphane.Controllers
{
    public class BlockChainController : Controller
    {
        //DBKUTUPHANEEntities1 db = new DBKUTUPHANEEntities1();
        BlockChainDBIslemleri blckDb = new BlockChainDBIslemleri();
        HareketDBIslemleri hrktDb = new HareketDBIslemleri();
        BlockchainIslemleri blckIslem = new BlockchainIslemleri();
        // GET: BlockChain
        public ActionResult Index()
        {
            var blocks = blckDb.getAllBlock();
            Blockchain blockchain = blckIslem.loadChain();
            if (blockchain.isChainValid())
            {
                ViewBag.kontrol = "zinciri tam";
            }
            else
            {
                ViewBag.kontrol = "zinciri kopuk";
            }
            return View(blocks);
        }

        [HttpGet]
        public ActionResult Index2()
        {
            List<tblHareket> transactions = hrktDb.getAllHareketByDay(DateTime.Now);
            string transactionString = "";
            for (int i = 0; i < transactions.Count; i++)
            {
                transactionString += (transactions[i].UYE.ToString() + transactions[i].KITAP.ToString() + transactions[i].PERSONEL.ToString() + transactions[i].ISLEMTARIHI.ToString() + transactions[i].ISLEMTURU);
            }
            blckDb.addBlock(transactionString);
            return RedirectToAction("Index");
        }


    }
}