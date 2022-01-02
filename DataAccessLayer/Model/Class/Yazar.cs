using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model.Class
{
    public class Yazar
    {
        public int ID { get; set; }
        public string AD { get; set; }
        public string SOYAD { get; set; }
        public string DETAY { get; set; }
        public int kitapSayisi { get; set; }
    }
}
