using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeriSorgulari
{
    public class vsLog
    {
        public YurtDataContext DB { get; set; }
        public vsLog(YurtDataContext db)
        {
            DB = db;
        }

        public List<Log> HepsiniGetir()
        {
            List<Log> logListesi = null;

            var query = from log in DB.Logs
                        select log;

            logListesi = query.ToList();
            return logListesi;
        }

        public Log LogIdFiltresiyleLogGetir(int logID)
        {
            Log logOBJ = null;

            var query = from log in DB.Logs
                        where log.Id.Equals(logID)
                        select log;

            logOBJ = query.SingleOrDefault();
            return logOBJ;
        }

        public void Ekle(Log log)
        {
            DB.Logs.InsertOnSubmit(log);
            DB.SubmitChanges();
        }

        public void Guncelle(Log log)
        {
            DB.SubmitChanges();
        }

        public void Sil(Log log)
        {
            DB.Logs.DeleteOnSubmit(log);
            DB.SubmitChanges();
        }

    }
}
