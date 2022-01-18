using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VeriSorgulari;

namespace OgrYurt
{
    public partial class Yurt : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Kullanici"] != null)
            {
                VeriSorgulari.Personel aktifkullanici = Session["Kullanici"] as VeriSorgulari.Personel;
                lblKullaniciAd.Text = "Hoşgeldiniz    " + aktifkullanici.Ad + " " + aktifkullanici.Soyad;
            }
        }

        protected void lkOda_Click(object sender, EventArgs e)
        {
            Response.Redirect("Oda.aspx");
        }

        protected void lkPersonel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Personel.aspx");
        }

        protected void lkGelir_Click(object sender, EventArgs e)
        {
            Response.Redirect("Gelir.aspx");
        }

        protected void lkOgrenci_Click(object sender, EventArgs e)
        {
            Response.Redirect("Ogrenci.aspx");
        }

        protected void lkOdeme_Click(object sender, EventArgs e)
        {
            Session["OdemeUcretId"] = null;
            Response.Redirect("Odeme.aspx");
        }

        protected void lkUcret_Click(object sender, EventArgs e)
        {
            Session["UcretOgrenciId"] = null;
            Response.Redirect("Ucret.aspx");
        }

        protected void lkVeli_Click(object sender, EventArgs e)
        {
            Session["VeliOgrenciId"] = null;
            Response.Redirect("Veli.aspx");
        }
    }
}