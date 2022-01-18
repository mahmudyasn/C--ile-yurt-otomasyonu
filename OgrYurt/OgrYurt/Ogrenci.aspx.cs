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
    public partial class Ogrenci : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Ogrenci_Doldur();
                Oda_Doldur();
            }
        }
        private void Ogrenci_Doldur()
        {
            using (YurtDataContext db = Yardimci.Baglan())
            {
                vsOgrenci veriIsle = new vsOgrenci(db);

                List<VeriSorgulari.Ogrenci> ogrenciListesi = veriIsle.HepsiniGetir();

                rptData.DataSource = ogrenciListesi;
                rptData.DataBind();
                
            }
        }

        private void Oda_Doldur()
        {
            using (YurtDataContext db = Yardimci.Baglan())
            {
                vsOda veriIsle = new vsOda(db);
                ddlOda.DataSource = veriIsle.DurumFiltresiyleOdaGetir(true);
                ddlOda.DataBind();
                ddlOda.Items.Insert(0, new ListItem("Oda Seçiniz", ""));
            }
        }

        protected void btnKaydet_Click(object sender, EventArgs e)
        {
            using (YurtDataContext db = Yardimci.Baglan())
            {
                vsOgrenci veriIsle = new vsOgrenci(db);

                VeriSorgulari.Ogrenci ogrenciObj = null;
                bool guncelleme = false;

                if (Session["GuncelenenOgrenciId"] != null)
                {
                    ogrenciObj = veriIsle.OgrenciIdFiltresiyleOgrenciGetir(Convert.ToInt32(Session["GuncelenenOgrenciId"]));
                    guncelleme = true;
                }
                else
                {
                    ogrenciObj = new VeriSorgulari.Ogrenci();
                }

                ogrenciObj.OgrenciNo = txtOgrenciNo.Text;
                ogrenciObj.TC = txtTC.Text;
                ogrenciObj.Ad = txtAd.Text;
                ogrenciObj.Soyad = txtSoyad.Text;
                ogrenciObj.Telefon = txtTelefon.Text;
                ogrenciObj.KayitTarihi = DateTime.Now;
                if (!string.IsNullOrWhiteSpace(ddlOda.SelectedValue))
                {
                    ogrenciObj.OdaID = int.Parse(ddlOda.SelectedValue);
                }
                ogrenciObj.OgrSinif = int.Parse(txtSinif.Text);

                ogrenciObj.Durum = chkDurum.Checked;


                try
                {

                    if (guncelleme)
                    {
                        veriIsle.Guncelle(ogrenciObj);
                        Session["GuncelenenOgrenciId"] = null;
                    }
                    else
                    {
                        veriIsle.Ekle(ogrenciObj);
                    }
                }
                catch (SqlException ex)
                {
                    if (ex.Number.Equals(2627))
                    {
                        Response.Write("<script>alert('" + txtOgrenciNo.Text + " nolu ogrenci bulunmaktadır. ')</script>");
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

                Response.Write("<script>alert('Ogrenci başarılı bir şekilde kaydedilmiştir. ')</script>");
                alanlariBosalt();


                Ogrenci_Doldur();
            }
        }

        private void alanlariBosalt()
        {
            txtTC.Text = "";
            txtAd.Text = "";
            txtSoyad.Text = "";
            txtTelefon.Text = "";
            txtSinif.Text = "";
            ddlOda.ClearSelection();
            txtOgrenciNo.Text = "";
            chkDurum.Checked = false;
        }

        protected void btnTemizle_Click(object sender, EventArgs e)
        {
            alanlariBosalt();
            Session["GuncelenenOgrenciId"] = null;
        }

       


        protected void rptData_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int ogrenciId = 0;

            switch (e.CommandName)
            {
                case "Güncelle":
                    ogrenciId = Convert.ToInt32(e.CommandArgument);
                    Ogrenci_Guncelle(ogrenciId);
                    break;
                case "Sil":
                    ogrenciId = Convert.ToInt32(e.CommandArgument);
                    using (YurtDataContext db = Yardimci.Baglan())
                    {
                        vsOgrenci veriIsle = new vsOgrenci(db);
                        VeriSorgulari.Ogrenci silinecekOgrenci = veriIsle.OgrenciIdFiltresiyleOgrenciGetir(ogrenciId);
                        if (silinecekOgrenci != null)
                        {
                            veriIsle.Sil(silinecekOgrenci);
                            Ogrenci_Doldur();
                            Response.Write("<script>alert('Öğrenci başarılı bir şekilde silinmiştir. ')</script>");
                        }
                    }

                    break;
                case "Ucret":
                    ogrenciId = Convert.ToInt32(e.CommandArgument);
                    Session["UcretOgrenciId"] = ogrenciId;
                    Response.Redirect("Ucret.aspx");
                    break;
                case "Veli":
                    ogrenciId = Convert.ToInt32(e.CommandArgument);
                    Session["VeliOgrenciId"] = ogrenciId;
                    Response.Redirect("Veli.aspx");
                    break;
            }
        }

        protected void Ogrenci_Guncelle(int ogrenciId)
        {
            using (YurtDataContext db = Yardimci.Baglan())
            {
                vsOgrenci veriIsle = new vsOgrenci(db);


                VeriSorgulari.Ogrenci guncellenecekOgrenci = veriIsle.OgrenciIdFiltresiyleOgrenciGetir(ogrenciId);
                if (guncellenecekOgrenci != null)
                {
                    Session["GuncelenenOgrenciId"] = ogrenciId;
                    alanlariBosalt();
                    ddlOda.Items.FindByValue(guncellenecekOgrenci.OdaID.ToString()).Selected = true;
                    txtAd.Text = guncellenecekOgrenci.Ad;
                    txtSoyad.Text = guncellenecekOgrenci.Soyad;
                    txtTelefon.Text = guncellenecekOgrenci.Telefon;
                    txtSinif.Text = guncellenecekOgrenci.OgrSinif.ToString();
                    txtOgrenciNo.Text = guncellenecekOgrenci.OgrenciNo.ToString();
                    chkDurum.Checked = guncellenecekOgrenci.Durum;
                }
            }
        }
    }
}