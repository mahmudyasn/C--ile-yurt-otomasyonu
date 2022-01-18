using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Data.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VeriSorgulari;

namespace OgrYurt
{
    public partial class Oda : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Oda_Doldur();
            }
        }
        private void Oda_Doldur()
        {
            using (YurtDataContext db = Yardimci.Baglan())
            {
                vsOda veriIsle = new vsOda(db);

                List<VeriSorgulari.Oda> odaListesi = veriIsle.HepsiniGetir();

                rptData.DataSource = odaListesi;
                rptData.DataBind();
            }
        }

        protected void btnKaydet_Click(object sender, EventArgs e)
        {
            using (YurtDataContext db = Yardimci.Baglan())
            {
                vsOda veriIsle = new vsOda(db);

                VeriSorgulari.Oda odaObj = null;
                bool guncelleme = false;

                if (Session["GuncelenenOdaId"] != null)
                {
                    odaObj = veriIsle.OdaIdFiltresiyleOdaGetir(Convert.ToInt32(Session["GuncelenenOdaId"]));
                    guncelleme = true;
                }
                else
                {
                    odaObj = new VeriSorgulari.Oda();
                }

                odaObj.OdaNo = txtOdaNo.Text;
                odaObj.KisiSayisi = int.Parse(txtKapasite.Text);
                odaObj.Durum = chkDurum.Checked;


                try
                {

                    if (guncelleme)
                    {
                        veriIsle.Guncelle(odaObj);
                        Session["GuncelenenOdaId"] = null;
                    }
                    else
                    {
                        veriIsle.Ekle(odaObj);
                    }
                }
                catch (SqlException ex)
                {
                    if (ex.Number.Equals(2627))
                    {
                        Response.Write("<script>alert('" + txtOdaNo.Text + " nolu oda bulunmaktadır. ')</script>");
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

                Response.Write("<script>alert('Oda başarılı bir şekilde kaydedilmiştir. ')</script>");
                alanlariBosalt();


                Oda_Doldur();
            }
        }

        private void alanlariBosalt()
        {
            txtKapasite.Text = "";
            txtOdaNo.Text = "";
            chkDurum.Checked = false;
        }

        protected void btnTemizle_Click(object sender, EventArgs e)
        {
            alanlariBosalt();
            Session["GuncelenenOdaId"] = null;
        }

        protected void Oda_Guncelle(int odaId)
        {
            using (YurtDataContext db = Yardimci.Baglan())
            {
                vsOda veriIsle = new vsOda(db);


                VeriSorgulari.Oda guncellenecekOda = veriIsle.OdaIdFiltresiyleOdaGetir(odaId);
                if (guncellenecekOda != null)
                {
                    Session["GuncelenenOdaId"] = odaId;
                    txtKapasite.Text = guncellenecekOda.KisiSayisi.ToString();
                    txtOdaNo.Text = guncellenecekOda.OdaNo.ToString();
                    chkDurum.Checked = guncellenecekOda.Durum;
                }
            }
        }

        protected void rptData_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int odaId = 0;

            switch (e.CommandName)
            {
                case "Güncelle":
                    odaId = Convert.ToInt32(e.CommandArgument);
                    Oda_Guncelle(odaId);
                    break;
                case "Sil":
                    odaId = Convert.ToInt32(e.CommandArgument);
                    using (YurtDataContext db = Yardimci.Baglan())
                    {
                        vsOda veriIsle = new vsOda(db);
                        VeriSorgulari.Oda silinecekOda = veriIsle.OdaIdFiltresiyleOdaGetir(odaId);
                        if (silinecekOda != null)
                        {
                            veriIsle.Sil(silinecekOda);
                            Oda_Doldur();
                            Response.Write("<script>alert('Oda başarılı bir şekilde silinmiştir. ')</script>");
                        }
                    }

                    break;

            }
        }

    }
}