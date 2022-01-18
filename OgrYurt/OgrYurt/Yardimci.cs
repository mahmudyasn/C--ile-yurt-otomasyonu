using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VeriSorgulari;

namespace OgrYurt
{
    public static class Yardimci
    {
        public static YurtDataContext Baglan()
        {
            return new YurtDataContext(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["vtBaglanti"].ConnectionString);
        }
    }
}