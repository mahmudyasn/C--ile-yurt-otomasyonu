using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeriSorgulari
{
    public class vsUcret
    {
        public YurtDataContext DB { get; set; }
        public vsUcret(YurtDataContext db)
        {
            DB = db;
        }

        public List<Ucret> HepsiniGetir()
        {
            List<Ucret> ucretListesi = null;

            var query = from ucret in DB.Ucrets
                        select ucret;

            ucretListesi = query.ToList();
            return ucretListesi;
        }

        public Ucret UcretIdFiltresiyleUcretGetir(int ucretID)
        {
            Ucret ucretOBJ = null;

            var query = from ucret in DB.Ucrets
                        where ucret.Id.Equals(ucretID)
                        select ucret;

            ucretOBJ = query.SingleOrDefault();
            return ucretOBJ;
        }

        public List<Ucret> OgrenciIdFiltresiyleUcretListele(int ogrenciId)
        {
            var query = from ucret in DB.Ucrets
                        where ucret.OgrenciID.Equals(ogrenciId)
                        select ucret;

            return query.ToList();
        }

        public void Ekle(Ucret ucret)
        {
            DB.Ucrets.InsertOnSubmit(ucret);
            DB.SubmitChanges();
        }

        public void Guncelle(Ucret ucret)
        {
            DB.SubmitChanges();
        }

        public void Sil(Ucret ucret)
        {
            DB.Ucrets.DeleteOnSubmit(ucret);
            DB.SubmitChanges();
        }
    }
}
