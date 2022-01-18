using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeriSorgulari
{
    public class vsOdeme
    {
        public YurtDataContext DB { get; set; }
        public vsOdeme(YurtDataContext db)
        {
            DB = db;
        }

        public List<Odeme> HepsiniGetir()
        {
            List<Odeme> odemeListesi = null;

            var query = from odeme in DB.Odemes
                        select odeme;

            odemeListesi = query.ToList();
            return odemeListesi;
        }

        public Odeme OdemeIdFiltresiyleOdemeGetir(int odemeID)
        {
            Odeme odemeOBJ = null;

            var query = from odeme in DB.Odemes
                        where odeme.Id.Equals(odemeID)
                        select odeme;

            odemeOBJ = query.SingleOrDefault();
            return odemeOBJ;
        }

        public List<Odeme> UcretIdFiltresiyleOdemeListele(int ucretId)
        {
            var query = from ucret in DB.Odemes
                        where ucret.UcretID.Equals(ucretId)
                        select ucret;

            return query.ToList();
        }

        public List<Odeme> TarihAraligiFiltresiyleOdemeListele(DateTime baslangic, DateTime bitisTarihi)
        {
            var query = from ucret in DB.Odemes
                        where ucret.IslemTarihi >= baslangic && ucret.IslemTarihi <= bitisTarihi
                        select ucret;

            return query.ToList();
        }

        public List<Odeme> PersonelIdveTarihAraligiFiltresiyleOdemeListele(int personelID, DateTime baslangic, DateTime bitisTarihi)
        {
            var query = from ucret in DB.Odemes
                        where ucret.PersonelID.Equals(personelID) && ucret.IslemTarihi >= baslangic && ucret.IslemTarihi <= bitisTarihi
                        select ucret;

            return query.ToList();
        }

        public void Ekle(Odeme odeme)
        {
            DB.Odemes.InsertOnSubmit(odeme);
            DB.SubmitChanges();
        }

        public void Guncelle(Odeme odeme)
        {
            DB.SubmitChanges();
        }

        public void Sil(Odeme odeme)
        {
            DB.Odemes.DeleteOnSubmit(odeme);
            DB.SubmitChanges();
        }
    }
}
