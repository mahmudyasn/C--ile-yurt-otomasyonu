using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VeriSorgulari;

namespace OgrYurt
{
    public partial class Gelir : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Personel_Doldur();
            }
        }
        private void Personel_Doldur()
        {
            using (YurtDataContext db = Yardimci.Baglan())
            {
                vsPersonel personelIslemleri = new vsPersonel(db);

                List<VeriSorgulari.Personel> personelListesi = personelIslemleri.HepsiniGetir();

                ddlPersonel.DataSource = personelIslemleri.HepsiniGetir().Select(personel=> new { personel.Id, AdSoyad= string.Format("{0} {1}", personel.Ad, personel.Soyad) });
                ddlPersonel.DataBind();
                ddlPersonel.Items.Insert(0, new ListItem("Lütfen Personel Seçiniz", ""));
            }
        }

        private void Odeme_Doldur()
        {
            using (YurtDataContext db = Yardimci.Baglan())
            {
                vsOdeme veriIsle = new vsOdeme(db);

                List<VeriSorgulari.Odeme> siparisListesi = null;
                DateTime baslangicTarihi = Convert.ToDateTime(txtBaslangicTarih.Text);
                DateTime bitisTarihi = Convert.ToDateTime(txtBitisTarih.Text);

                if (string.IsNullOrWhiteSpace(ddlPersonel.SelectedValue))
                {
                    siparisListesi = veriIsle.TarihAraligiFiltresiyleOdemeListele(baslangicTarihi, bitisTarihi);
                }
                else
                {
                    siparisListesi = veriIsle.PersonelIdveTarihAraligiFiltresiyleOdemeListele(Convert.ToInt32(ddlPersonel.SelectedValue), baslangicTarihi, bitisTarihi);
                }
                grdOdeme.DataSource = siparisListesi;
                grdOdeme.DataBind();
            }
        }

        protected void ButtonRaporla_Click(object sender, EventArgs e)
        {
            Odeme_Doldur();
            ButtonAktar.Enabled = true;
        }
        double toplamGelir = 0;
        protected void grdOdeme_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                VeriSorgulari.Odeme odeme = (VeriSorgulari.Odeme)e.Row.DataItem;
                toplamGelir = toplamGelir + odeme.OdemeTutari;

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[4].Text = "Toplam: " + toplamGelir;
            }
        }

        protected void ButtonAktar_Click(object sender, EventArgs e)
        {
            //https://stackoverflow.com/questions/15832339/gridview-data-export-to-excel-in-asp-net

            string filename = String.Format("GelirTablosu_{0}_{1}.xls", DateTime.Today.Month.ToString(), DateTime.Today.Year.ToString());
            if (!string.IsNullOrEmpty(grdOdeme.Page.Title))
                filename = grdOdeme.Page.Title + ".xls";

            HttpContext.Current.Response.Clear();

            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + filename);


            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.Charset = "";

            System.IO.StringWriter stringWriter = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWriter = new HtmlTextWriter(stringWriter);



            System.Web.UI.HtmlControls.HtmlForm form = new System.Web.UI.HtmlControls.HtmlForm();
            grdOdeme.Parent.Controls.Add(form);
            form.Controls.Add(grdOdeme);
            form.RenderControl(htmlWriter);

            HttpContext.Current.Response.Write("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />");
            HttpContext.Current.Response.Write(stringWriter.ToString());
            HttpContext.Current.Response.End();
        }
    }
}