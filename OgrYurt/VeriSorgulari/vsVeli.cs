using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeriSorgulari
{
    public class vsVeli
    {
        public YurtDataContext DB { get; set; }
        public vsVeli(YurtDataContext db)
        {
            DB = db;
        }

        public List<Veli> HepsiniGetir()
        {
            List<Veli> veliListesi = null;

            var query = from veli in DB.Velis
                        select veli;

            veliListesi = query.ToList();
            return veliListesi;
        }

        public Veli VeliIdFiltresiyleVeliGetir(int veliID)
        {
            Veli veliOBJ = null;

            var query = from veli in DB.Velis
                        where veli.Id.Equals(veliID)
                        select veli;

            veliOBJ = query.SingleOrDefault();
            return veliOBJ;
        }

        public List<Veli> OgrenciIdFiltresiyleVeliListele(int ogrenciId)
        {
            var query = from veli in DB.Velis
                        where veli.OgrenciID.Equals(ogrenciId)
                        select veli;

            return query.ToList();
        }

        public void Ekle(Veli veli)
        {
            DB.Velis.InsertOnSubmit(veli);
            DB.SubmitChanges();
        }

        public void Guncelle(Veli veli)
        {
            DB.SubmitChanges();
        }

        public void Sil(Veli veli)
        {
            DB.Velis.DeleteOnSubmit(veli);
            DB.SubmitChanges();
        }
    }
}
