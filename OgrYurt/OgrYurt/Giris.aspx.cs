using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using VeriSorgulari;

namespace OgrYurt
{
    public partial class Giris : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lgn_Authenticate(object sender, AuthenticateEventArgs e)
        {
            using(YurtDataContext db = Yardimci.Baglan())
            {
                vsPersonel personelIsleri = new vsPersonel(db);
                VeriSorgulari.Personel kullanici = personelIsleri.kullaniciAdiSorgula(lgn.UserName);
                if (kullanici != null && kullanici.Sifre.Equals(lgn.Password))
                {
                    Session["Kullanici"] = kullanici;

                    if (!string.IsNullOrEmpty(Request.QueryString["ReturnUrl"]))
                    {
                        FormsAuthentication.SetAuthCookie(lgn.UserName, false);
                        Response.Redirect("~/AnaSayfa.aspx");
                    }
                    else
                    {
                        FormsAuthentication.RedirectFromLoginPage(lgn.UserName, false);
                    }
                }
            }
        }
    }
}