using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VeriSorgulari;

namespace OgrYurt
{
    public partial class Veli : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Veli_Doldur();
                Ogrenci_Doldur();
            }
            if (Session["VeliOgrenciId"] != null)
            {
                try
                {
                    ddlOgrenci.Items.FindByValue(Session["VeliOgrenciId"].ToString()).Selected = true;
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
        private void Veli_Doldur()
        {
            using (YurtDataContext db = Yardimci.Baglan())
            {
                vsVeli veliIslemleri = new vsVeli(db);

                if (Session["VeliOgrenciId"] == null)
                {
                    grdVeli.DataSource = veliIslemleri.HepsiniGetir();
                }
                else
                {
                    grdVeli.DataSource = veliIslemleri.OgrenciIdFiltresiyleVeliListele(int.Parse(Session["VeliOgrenciId"].ToString()));

                }
                grdVeli.DataBind();
            }
        }

        protected void btnKaydet_Click(object sender, EventArgs e)
        {
            using (YurtDataContext db = Yardimci.Baglan())
            {
                vsVeli veriIsle = new vsVeli(db);

                VeriSorgulari.Veli veli = null;
                bool guncelleme = false;

                if (Session["GuncelenenVeliId"] != null)
                {
                    veli = veriIsle.VeliIdFiltresiyleVeliGetir(Convert.ToInt32(Session["GuncelenenVeliId"]));
                    guncelleme = true;
                }
                else
                {
                    veli = new VeriSorgulari.Veli();
                }

                veli.Telefon = txtTelefon.Text;
                veli.Ad = txtAd.Text;
                veli.Soyad = txtSoyad.Text;
                veli.YakinlikDerecesi = txtYakinlikDerecesi.Text;
                if (!string.IsNullOrWhiteSpace(ddlOgrenci.SelectedValue))
                {
                    veli.OgrenciID = int.Parse(ddlOgrenci.SelectedValue);
                }
                try
                {

                    if (guncelleme)
                    {
                        veriIsle.Guncelle(veli);
                        Session["GuncelenenVeliId"] = null;
                    }
                    else
                    {
                        veriIsle.Ekle(veli);
                    }
                }
                catch (SqlException ex)
                {
                    if (ex.Number.Equals(2627))
                    {
                        Response.Write("<script>alert('" + ddlOgrenci.Text + " adlı öğrencinin " + txtYakinlikDerecesi.Text + " derecesinde velisi bulunmaktadır. ')</script>");
                    }
                    else
                    {
                        Response.Write("<script>alert('" + ex.Message + "');</script>");
                    }
                    return;
                }
                catch (Exception ex)
                {
                    throw;
                }

                Response.Write("<script>alert('Veli başarılı bir şekilde kaydedilmiştir. ')</script>");
                alanlariBosalt();

                Veli_Doldur();
            }
        }

        private void alanlariBosalt()
        {
            txtYakinlikDerecesi.Text = "";
            if (Session["VeliOgrenciId"] == null)
            {
                ddlOgrenci.ClearSelection();
            }
            txtAd.Text = "";
            txtSoyad.Text = "";
            txtTelefon.Text = "";
        }

        protected void grdVeli_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int veliId = 0;

            switch (e.CommandName)
            {
                case "Güncelle":
                    veliId = Convert.ToInt32(e.CommandArgument);
                    Veli_Guncelle(veliId);
                    break;
                case "Sil":
                    veliId = Convert.ToInt32(e.CommandArgument);
                    using (YurtDataContext db = Yardimci.Baglan())
                    {
                        vsVeli veriIsle = new vsVeli(db);
                        VeriSorgulari.Veli silinecekVeli = veriIsle.VeliIdFiltresiyleVeliGetir(veliId);
                        if (silinecekVeli != null)
                        {
                            veriIsle.Sil(silinecekVeli);
                        }
                        Response.Write("<script>alert('Veli başarılı bir şekilde silinmiştir. ')</script>");
                    }
                    Veli_Doldur();

                    break;

            }
        }

        protected void Veli_Guncelle(int veliId)
        {
            using (YurtDataContext db = Yardimci.Baglan())
            {
                vsVeli veriIsle = new vsVeli(db);


                VeriSorgulari.Veli guncellenecekVeli = veriIsle.VeliIdFiltresiyleVeliGetir(veliId);
                if (guncellenecekVeli != null)
                {
                    Session["GuncelenenVeliId"] = veliId;
                    if (Session["VeliOgrenciId"] == null)
                    {
                        try
                        {
                            ddlOgrenci.ClearSelection();
                            ddlOgrenci.Items.FindByValue(guncellenecekVeli.OgrenciID.ToString()).Selected = true;
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    txtSoyad.Text = guncellenecekVeli.Soyad;
                    txtYakinlikDerecesi.Text = guncellenecekVeli.YakinlikDerecesi;
                    txtAd.Text = guncellenecekVeli.Ad;
                    txtTelefon.Text = guncellenecekVeli.Telefon;
                }
            }
        }

        protected void btnTemizle_Click(object sender, EventArgs e)
        {
            Session["GuncelenenVeliId"] = null;
            alanlariBosalt();
        }
    }
}