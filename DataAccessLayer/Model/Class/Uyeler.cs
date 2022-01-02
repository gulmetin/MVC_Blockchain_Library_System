using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model.Class
{
    public class Uyeler
    {
        public int ID { get; set; }
        public string AD { get; set; }
        public string SOYAD { get; set; }
        public string MAIL { get; set; }
        public string KULLANICIADI { get; set; }
        public string SIFRE { get; set; }
        public string TELEFON { get; set; }
        public string OKUL { get; set; }
        public Nullable<long> TC { get; set; }
        public string DOGUMYILI { get; set; }
        public int iadesizKitapSayisi { get; set; }
    }
}
