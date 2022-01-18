using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeriSorgulari
{
    public class vsOgrenci
    {
        public YurtDataContext DB { get; set; }
        public vsOgrenci(YurtDataContext db)
        {
            DB = db;
        }

        public List<Ogrenci> HepsiniGetir()
        {
            List<Ogrenci> ogrenciListesi = null;

            var query = from ogrenci in DB.Ogrencis
                        select ogrenci;

            ogrenciListesi = query.ToList();
            return ogrenciListesi;
        }

        public Ogrenci OgrenciIdFiltresiyleOgrenciGetir(int ogrenciID)
        {
            Ogrenci ogrenciOBJ = null;

            var query = from ogrenci in DB.Ogrencis
                        where ogrenci.Id.Equals(ogrenciID)
                        select ogrenci;

            ogrenciOBJ = query.SingleOrDefault();
            return ogrenciOBJ;
        }

        public void Ekle(Ogrenci ogrenci)
        {
            DB.Ogrencis.InsertOnSubmit(ogrenci);
            DB.SubmitChanges();
        }

        public void Guncelle(Ogrenci ogrenci)
        {
            DB.SubmitChanges();
        }

        public void Sil(Ogrenci ogrenci)
        {
            DB.Ogrencis.DeleteOnSubmit(ogrenci);
            DB.SubmitChanges();
        }
    }
}
