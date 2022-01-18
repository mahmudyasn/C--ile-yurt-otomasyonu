using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeriSorgulari
{
    public class vsOda
    {
        public YurtDataContext DB { get; set; }
        public vsOda(YurtDataContext db)
        {
            DB = db;
        }

        public List<Oda> HepsiniGetir()
        {
            List<Oda> odaListesi = null;

            var query = from oda in DB.Odas
                        select oda;

            odaListesi = query.ToList();
            return odaListesi;
        }

        public Oda OdaIdFiltresiyleOdaGetir(int odaID)
        {
            Oda odaOBJ = null;

            var query = from oda in DB.Odas
                        where oda.Id.Equals(odaID)
                        select oda;

            odaOBJ = query.SingleOrDefault();
            return odaOBJ;
        }

        public IQueryable<Oda> DurumFiltresiyleOdaGetir(bool durum)
        {
            var query = from oda in DB.Odas
                        where oda.Durum.Equals(durum)
                        select oda;
            return query;
        }

        public void Ekle(Oda oda)
        {
            DB.Odas.InsertOnSubmit(oda);
            DB.SubmitChanges();
        }

        public void Guncelle(Oda oda)
        {
            DB.SubmitChanges();
        }

        public void Sil(Oda oda)
        {
            DB.Odas.DeleteOnSubmit(oda);
            DB.SubmitChanges();
        }
    }
}
