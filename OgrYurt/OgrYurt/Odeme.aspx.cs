using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VeriSorgulari;

namespace OgrYurt
{
    public partial class Odeme : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Odeme_Doldur();
                Ogrenci_Doldur();
                alanlariBosalt();
            }
            if (Session["OdemeUcretId"] != null)
            {
                try
                {
                    int ucretId = int.Parse(Session["OdemeUcretId"].ToString());
                    using (YurtDataContext db = Yardimci.Baglan())
                    {
                        vsUcret veriIsle = new vsUcret(db);
                        VeriSorgulari.Ucret ucret = veriIsle.UcretIdFiltresiyleUcretGetir(ucretId);
                        if (ucret != null)
                        {
                            ddlOgrenci.Items.FindByValue(ucret.Ogrenci.Id.ToString()).Selected = true;
                            ddlOgrenci.Enabled = false;

                            Ucret_Doldur(ucret.Ogrenci.Id);
                            ddlUcret.Items.FindByValue(ucret.Id.ToString()).Selected = true;
                            ddlUcret.Enabled = false;
                        }
                    }
         
                }
                catch (Exception ex)
                {

                }
            }
        }

        private void Ogrenci_Doldur()
        {
            using (YurtDataContext db = Yardimci.Baglan())
            {
                vsOgrenci ogrenciIslemleri = new vsOgrenci(db);

                ddlOgrenci.DataSource = ogrenciIslemleri.HepsiniGetir().Select(ogr => new { Id = ogr.Id, AdSoyad = string.Format("{0} {1}", ogr.Ad, ogr.Soyad) });
                ddlOgrenci.DataBind();
                ddlOgrenci.Items.Insert(0, new ListItem("Lütfen Öğrenci Seçiniz", ""));
            }
        }

        private void Ucret_Doldur(int? ogrenciID=null)
        {
            using (YurtDataContext db = Yardimci.Baglan())
            {
                vsUcret ucretIslemleri = new vsUcret(db);
                if (ogrenciID.HasValue)
                {
                    ddlUcret.DataSource = ucretIslemleri.OgrenciIdFiltresiyleUcretListele(ogrenciID.Value).Select(ucret => new { Id = ucret.Id, Yil = ucret.Yil });

                }
                else
                {
                    ddlUcret.DataSource = null;
                }
                ddlUcret.DataBind();
                ddlUcret.Items.Insert(0, new ListItem("Lütfen Ücret Seçiniz", ""));
            }
        }
        private void Odeme_Doldur()
        {
            using (YurtDataContext db = Yardimci.Baglan())
            {
                vsOdeme odemeIslemleri = new vsOdeme(db);

                if (Session["OdemeUcretId"] == null)
                {
                    grdOdeme.DataSource = odemeIslemleri.HepsiniGetir();
                }
                else
                {
                    grdOdeme.DataSource = odemeIslemleri.UcretIdFiltresiyleOdemeListele(int.Parse(Session["OdemeUcretId"].ToString()));

                }
                grdOdeme.DataBind();
            }
        }

        protected void btnKaydet_Click(object sender, EventArgs e)
        {
            using (YurtDataContext db = Yardimci.Baglan())
            {
                vsOdeme veriIsle = new vsOdeme(db);

                VeriSorgulari.Odeme odeme = null;
                bool guncelleme = false;

                if (Session["GuncelenenOdemeId"] != null)
                {
                    odeme = veriIsle.OdemeIdFiltresiyleOdemeGetir(Convert.ToInt32(Session["GuncelenenOdemeId"]));
                    guncelleme = true;
                }
                else
                {
                    odeme = new VeriSorgulari.Odeme();
                }

                odeme.OdemeTutari = int.Parse(txtTutar.Text);
                odeme.IslemTarihi = DateTime.Now;
                odeme.PersonelID = (Session["Kullanici"] as VeriSorgulari.Personel).Id;
                if (!string.IsNullOrWhiteSpace(ddlUcret.SelectedValue))
                {
                    odeme.UcretID = int.Parse(ddlUcret.SelectedValue);
                }
                try
                {

                    if (guncelleme)
                    {
                        veriIsle.Guncelle(odeme);
                        Session["GuncelenenOdemeId"] = null;
                    }
                    else
                    {
                        veriIsle.Ekle(odeme);
                    }
                }
                catch (SqlException ex)
                {
                    Response.Write("<script>alert('" + ex.Message + "');</script>");
                    
                    return;
                }
                catch (Exception ex)
                {
                    throw;
                }

                Response.Write("<script>alert('Ücret başarılı bir şekilde kaydedilmiştir. ')</script>");
                alanlariBosalt();

                Odeme_Doldur();
            }
        }

        private void alanlariBosalt()
        {
            txtTutar.Text = "";
            if (Session["OdemeUcretId"] == null)
            {
                ddlUcret.ClearSelection();
            }
        }

        protected void grdOdeme_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int odemeId = 0;

            switch (e.CommandName)
            {
                case "Güncelle":
                    odemeId = Convert.ToInt32(e.CommandArgument);
                    Odeme_Guncelle(odemeId);
                    break;
                case "Sil":
                    odemeId = Convert.ToInt32(e.CommandArgument);
                    using (YurtDataContext db = Yardimci.Baglan())
                    {
                        vsOdeme veriIsle = new vsOdeme(db);
                        VeriSorgulari.Odeme silinecekOdeme = veriIsle.OdemeIdFiltresiyleOdemeGetir(odemeId);
                        if (silinecekOdeme != null)
                        {
                            veriIsle.Sil(silinecekOdeme);
                        }
                        Response.Write("<script>alert('Ücret başarılı bir şekilde silinmiştir. ')</script>");
                    }
                    alanlariBosalt();
                    Odeme_Doldur();

                    break;
            }
        }

        protected void Odeme_Guncelle(int odemeId)
        {
            using (YurtDataContext db = Yardimci.Baglan())
            {
                vsOdeme veriIsle = new vsOdeme(db);


                VeriSorgulari.Odeme guncellenecekOdeme = veriIsle.OdemeIdFiltresiyleOdemeGetir(odemeId);
                if (guncellenecekOdeme != null)
                {
                    Session["GuncelenenOdemeId"] = odemeId;
                    if (Session["OdemeUcretId"] == null)
                    {
                        try
                        {

                            ddlOgrenci.ClearSelection();
                            ddlOgrenci.Items.FindByValue(guncellenecekOdeme.Ucret.Ogrenci.Id.ToString()).Selected = true;

                            ddlUcret.ClearSelection();
                            ddlUcret.Items.FindByValue(guncellenecekOdeme.Ucret.Id.ToString()).Selected = true;
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    txtTutar.Text = guncellenecekOdeme.OdemeTutari.ToString(CultureInfo.InvariantCulture);
                }
            }
        }

        protected void btnTemizle_Click(object sender, EventArgs e)
        {
            Session["GuncelenenOdemeId"] = null;
            alanlariBosalt();
        }

        protected void ddlOgrenci_SelectedIndexChanged(object sender, EventArgs e)
        {
            int? OgrenciId = null;
            if (!string.IsNullOrWhiteSpace(ddlOgrenci.SelectedValue))
            {
                OgrenciId = int.Parse(ddlOgrenci.SelectedValue);
            }
            Ucret_Doldur(OgrenciId);
        }
    }
}