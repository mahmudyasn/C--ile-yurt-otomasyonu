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
    public partial class Ucret : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Ucret_Doldur();
                Ogrenci_Doldur();
                alanlariBosalt();
            }
            if (Session["UcretOgrenciId"] != null)
            {
                try
                {
                    ddlOgrenci.Items.FindByValue(Session["UcretOgrenciId"].ToString()).Selected = true;
                    ddlOgrenci.Enabled = false;
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
        private void Ucret_Doldur()
        {
            using (YurtDataContext db = Yardimci.Baglan())
            {
                vsUcret ucretIslemleri = new vsUcret(db);

                if (Session["UcretOgrenciId"] == null)
                {
                    
                    rptData.DataSource = ucretIslemleri.HepsiniGetir();
                }
                else
                {
                   
                    rptData.DataSource = ucretIslemleri.OgrenciIdFiltresiyleUcretListele(int.Parse(Session["UcretOgrenciId"].ToString()));

                }
                
                rptData.DataBind();
            }
        }

        protected void btnKaydet_Click(object sender, EventArgs e)
        {
            using (YurtDataContext db = Yardimci.Baglan())
            {
                vsUcret veriIsle = new vsUcret(db);

                VeriSorgulari.Ucret ucret = null;
                bool guncelleme = false;

                if (Session["GuncelenenUcretId"] != null)
                {
                    ucret = veriIsle.UcretIdFiltresiyleUcretGetir(Convert.ToInt32(Session["GuncelenenUcretId"]));
                    guncelleme = true;
                }
                else
                {
                    ucret = new VeriSorgulari.Ucret();
                }

                ucret.Yil = int.Parse(txtYil.Text);
                ucret.KayitBaslangicTarihi = DateTime.ParseExact(txtBaslangic.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                ucret.KayitBitisTarihi = DateTime.ParseExact(txtBitis.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                ucret.TaksitSayisi = short.Parse(txtTaksitSayisi.Text);
                ucret.ToplamUcret = Double.Parse(txtUcret.Text, CultureInfo.InvariantCulture);
                ucret.PersonelID = (Session["Kullanici"] as VeriSorgulari.Personel).Id;
                if (!string.IsNullOrWhiteSpace(ddlOgrenci.SelectedValue))
                {
                    ucret.OgrenciID = int.Parse(ddlOgrenci.SelectedValue);
                }
                try
                {

                    if (guncelleme)
                    {
                        veriIsle.Guncelle(ucret);
                        Session["GuncelenenUcretId"] = null;
                    }
                    else
                    {
                        veriIsle.Ekle(ucret);
                    }
                }
                catch (SqlException ex)
                {
                    if (ex.Number.Equals(2627))
                    {
                        Response.Write("<script>alert('" + ddlOgrenci.Text + " adlı öğrencinin " + txtYil.Text + " yılı için ücret tanımı bulunmaktadır. ')</script>");
                    }
                    else
                    {
                        Response.Write("<script>alert('" + ex.Message + "')</script>");
                    }
                    return;
                }
                catch (Exception ex)
                {
                    throw;
                }

                Response.Write("<script>alert('Ücret başarılı bir şekilde kaydedilmiştir. ')</script>");
                alanlariBosalt();

                Ucret_Doldur();
            }
        }

        private void alanlariBosalt()
        {
            txtYil.Text = DateTime.Now.Year.ToString();
            if (Session["UcretOgrenciId"] == null)
            {
                ddlOgrenci.ClearSelection();
            }
            txtBaslangic.Text = "";
            txtBitis.Text = "";
            txtTaksitSayisi.Text = "";
            txtUcret.Text = "";
        }

       

        protected void Ucret_Guncelle(int ucretId)
        {
            using (YurtDataContext db = Yardimci.Baglan())
            {
                vsUcret veriIsle = new vsUcret(db);


                VeriSorgulari.Ucret guncellenecekUcret = veriIsle.UcretIdFiltresiyleUcretGetir(ucretId);
                if (guncellenecekUcret != null)
                {
                    Session["GuncelenenUcretId"] = ucretId;
                    if (Session["UcretOgrenciId"] == null)
                    {
                        try
                        {
                            ddlOgrenci.ClearSelection();
                            ddlOgrenci.Items.FindByValue(guncellenecekUcret.OgrenciID.ToString()).Selected = true;
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    txtYil.Text = guncellenecekUcret.Yil.ToString();
                    txtBaslangic.Text = guncellenecekUcret.KayitBaslangicTarihi.ToString("yyyy-MM-dd");
                    txtBitis.Text = guncellenecekUcret.KayitBitisTarihi.ToString("yyyy-MM-dd");
                    txtTaksitSayisi.Text = guncellenecekUcret.TaksitSayisi.ToString();
                    txtUcret.Text = guncellenecekUcret.ToplamUcret.ToString(CultureInfo.InvariantCulture);
                }
            }
        }

        protected void btnTemizle_Click(object sender, EventArgs e)
        {
            Session["GuncelenenUcretId"] = null;
            alanlariBosalt();
        }

        protected void rptData_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int ucretId = 0;

            switch (e.CommandName)
            {
                case "Güncelle":
                    ucretId = Convert.ToInt32(e.CommandArgument);
                    Ucret_Guncelle(ucretId);
                    break;
                case "Sil":
                    ucretId = Convert.ToInt32(e.CommandArgument);
                    using (YurtDataContext db = Yardimci.Baglan())
                    {
                        vsUcret veriIsle = new vsUcret(db);
                        VeriSorgulari.Ucret silinecekUcret = veriIsle.UcretIdFiltresiyleUcretGetir(ucretId);
                        if (silinecekUcret != null)
                        {
                            veriIsle.Sil(silinecekUcret);
                        }
                        Response.Write("<script>alert('Ücret başarılı bir şekilde silinmiştir. ')</script>");
                    }
                    alanlariBosalt();
                    Ucret_Doldur();

                    break;
                case "Odeme":
                    ucretId = Convert.ToInt32(e.CommandArgument);

                    Session["OdemeUcretId"] = ucretId;
                    Response.Redirect("Odeme.aspx");
                    break;
            }
        }
    }
}