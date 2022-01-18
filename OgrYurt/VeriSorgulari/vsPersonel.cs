using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeriSorgulari
{
    public class vsPersonel
    {
        public YurtDataContext DB { get; set; }
        public vsPersonel(YurtDataContext db)
        {
            DB = db;
        }

        public List<Personel> HepsiniGetir()
        {
            List<Personel> personelListesi = null;

            var query = from personel in DB.Personels
                        select personel;

            personelListesi = query.ToList();
            return personelListesi;
        }

        public Personel PersonelIdFiltresiylePersonelGetir(int personelID)
        {
            Personel personelOBJ = null;

            var query = from personel in DB.Personels
                        where personel.Id.Equals(personelID)
                        select personel;

            personelOBJ = query.SingleOrDefault();
            return personelOBJ;
        }

        //kullanıcı adı sorgulama
        public Personel kullaniciAdiSorgula(string kullaniciAdi)
        {
            var sorgu = from personel in DB.Personels
                        where personel.KullaniciAdi.Equals(kullaniciAdi)
                        select personel;
            return sorgu.FirstOrDefault();
        }

        public void Ekle(Personel personel)
        {
            DB.Personels.InsertOnSubmit(personel);
            DB.SubmitChanges();
        }

        public void Guncelle(Personel personel)
        {
            DB.SubmitChanges();
        }

        public void Sil(Personel personel)
        {
            DB.Personels.DeleteOnSubmit(personel);
            DB.SubmitChanges();
        }
    }
}
