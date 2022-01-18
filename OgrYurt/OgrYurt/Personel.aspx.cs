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
    public partial class Personel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Kullanici_Doldur();
            }
        }

        private void Kullanici_Doldur()
        {
            using (YurtDataContext db = Yardimci.Baglan())
            {
                vsPersonel personelIslemleri = new vsPersonel(db);

                List<VeriSorgulari.Personel> personelListesi = personelIslemleri.HepsiniGetir();

                grdPersonel.DataSource = personelListesi;
                grdPersonel.DataBind();
            }
        }

        protected void btnKaydet_Click(object sender, EventArgs e)
        {
            using (YurtDataContext db = Yardimci.Baglan())
            {
                vsPersonel veriIsle = new vsPersonel(db);

                VeriSorgulari.Personel personel = null;
                bool guncelleme = false;

                if (Session["GuncelenenPersonelId"] != null)
                {
                    personel = veriIsle.PersonelIdFiltresiylePersonelGetir(Convert.ToInt32(Session["GuncelenenPersonelId"]));
                    guncelleme = true;
                }
                else
                {
                    personel = new VeriSorgulari.Personel();
                }

                personel.KullaniciAdi = txtKullaniciAdi.Text;
                if (string.IsNullOrWhiteSpace(txtPersonelSifre.Text))
                {
                    if (!guncelleme)
                    {
                        Response.Write("<script>alert('Şifre boş geçilemez. ')</script>");
                        return;
                    }
                }
                else
                {
                    personel.Sifre = txtPersonelSifre.Text;
                }
                personel.Telefon = txtTelefon.Text;
                personel.Ad = txtAd.Text;
                personel.Soyad = txtSoyad.Text;
                personel.KayitTarihi = DateTime.Now;
                personel.Durum = chkDurum.Checked;
                try
                {

                    if (guncelleme)
                    {
                        veriIsle.Guncelle(personel);
                        Session["GuncelenenPersonelId"] = null;
                    }
                    else
                    {
                        veriIsle.Ekle(personel);
                    }
                }
                catch (SqlException ex)
                {
                    if (ex.Number.Equals(2627))
                    {
                        Response.Write("<script>alert('" + txtKullaniciAdi.Text + " adlı bir kullanıcı bulunmaktadır. ')</script>");
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

                Response.Write("<script>alert('Personel başarılı bir şekilde kaydedilmiştir. ')</script>");
                alanlariBosalt();

                Kullanici_Doldur();
            }
        }

        private void alanlariBosalt()
        {
            txtKullaniciAdi.Text = "";
            txtPersonelSifre.Text = "";
            txtAd.Text = "";
            txtSoyad.Text = "";
            txtTelefon.Text = "";
            chkDurum.Checked = false;
        }

        protected void grdPersonel_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int personelId = 0;

            switch (e.CommandName)
            {
                case "Güncelle":
                    personelId = Convert.ToInt32(e.CommandArgument);
                    Personel_Guncelle(personelId);
                    break;
                case "Sil":
                    personelId = Convert.ToInt32(e.CommandArgument);
                    using (YurtDataContext db = Yardimci.Baglan())
                    {
                        vsPersonel veriIsle = new vsPersonel(db);
                        VeriSorgulari.Personel silinecekPersonel = veriIsle.PersonelIdFiltresiylePersonelGetir(personelId);
                        if (silinecekPersonel != null)
                        {
                            veriIsle.Sil(silinecekPersonel);
                        }
                        Response.Write("<script>alert('Personel başarılı bir şekilde silinmiştir. ')</script>");
                    }
                    Kullanici_Doldur();

                    break;

            }
        }

        protected void Personel_Guncelle(int personelId)
        {
            using (YurtDataContext db = Yardimci.Baglan())
            {
                vsPersonel veriIsle = new vsPersonel(db);


                VeriSorgulari.Personel guncellenecekPersonel = veriIsle.PersonelIdFiltresiylePersonelGetir(personelId);
                if (guncellenecekPersonel != null)
                {
                    Session["GuncelenenPersonelId"] = personelId;
                    txtSoyad.Text = guncellenecekPersonel.Soyad;
                    txtKullaniciAdi.Text = guncellenecekPersonel.KullaniciAdi;
                    txtPersonelSifre.Text = "";
                    txtAd.Text = guncellenecekPersonel.Ad;
                    txtTelefon.Text = guncellenecekPersonel.Telefon;

                    chkDurum.Checked = guncellenecekPersonel.Durum;
                }
            }
        }

        protected void btnTemizle_Click(object sender, EventArgs e)
        {
            Session["GuncelenenPersonelId"] = null;
            alanlariBosalt();
        }
    }
}