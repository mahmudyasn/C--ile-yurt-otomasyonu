<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Gelir.aspx.cs" Inherits="OgrYurt.Gelir" MasterPageFile="~/Yurt.Master"%>

<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <h3>Gelir Raporlama</h3>
    <fieldset>
        <table>
            <tr>
                <td>
                    <label>Başlangıç Tarihi</label>
                </td>
                <td>
                    <asp:TextBox ID="txtBaslangicTarih" runat="server" TextMode="Date"></asp:TextBox>
                   
                    <asp:RequiredFieldValidator runat="server" ValidationGroup="Rapor" ID="RequiredFieldValidator1" ControlToValidate="txtBaslangicTarih" ErrorMessage="Rapor Başlangıç Tarihi Giriniz!" />
                </td>
            </tr>
            <tr>
                <td>
                    <label>Bitiş Tarihi</label>
                </td>
                <td>
                    <asp:TextBox ID="txtBitisTarih" runat="server" TextMode="Date"></asp:TextBox>
                    
                    <asp:RequiredFieldValidator runat="server" ValidationGroup="Rapor" ID="RequiredFieldValidator4" ControlToValidate="txtBitisTarih" ErrorMessage="Rapor Bitiş Tarihini Giriniz!" />
                </td>
            </tr>
            <tr>
                <td>
                    <label>Personel</label>
                </td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlPersonel" DataTextField="AdSoyad" DataValueField="Id"></asp:DropDownList>
                </td>
            </tr>
        </table>
        <asp:Button ID="ButtonRaporla" runat="server" OnClick="ButtonRaporla_Click" ValidationGroup="Rapor" Text="Raporla" />
        <asp:Button ID="ButtonAktar" runat="server" OnClick="ButtonAktar_Click" Text="Excele Aktar" Enabled="false"/>
    </fieldset>
    <fieldset>
        <asp:GridView ID="grdOdeme" runat="server"
            Width="100%" CellPadding="4"
            GridLines="None"
            HeaderStyle-HorizontalAlign="Left"
            AllowPaging="false"
            AutoGenerateColumns="false"
            DataKeyNames="id" ShowFooter="true" OnRowDataBound="grdOdeme_RowDataBound">
            <AlternatingRowStyle BackColor="#BFE4FF" ForeColor="Black" />
            <HeaderStyle BackColor="White" Font-Bold="True" ForeColor="Black" />
            <RowStyle ForeColor="Black" BackColor="White" />
            <Columns>
                <asp:BoundField HeaderText="Id" DataField="id" Visible="false" />
                <asp:TemplateField HeaderText="Sıra No">
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Öğrenci">
                    <ItemTemplate>
                        <%# string.Format("{0} {1}", ((VeriSorgulari.Odeme)Container.DataItem).Ucret.Ogrenci.Ad,  ((VeriSorgulari.Odeme)Container.DataItem).Ucret.Ogrenci.Soyad)%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Ücret">
                    <ItemTemplate>
                        <%# string.Format("{0} {1}", ((VeriSorgulari.Odeme)Container.DataItem).Ucret.Ogrenci.Ad,  ((VeriSorgulari.Odeme)Container.DataItem).Ucret.Ogrenci.Soyad)%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="Tutar" DataField="OdemeTutari" />
                <asp:BoundField HeaderText="İşlem Tarihi" DataField="IslemTarihi" DataFormatString="{0:dd.MM.yyyy HH:mm:ss}" />
                <asp:TemplateField HeaderText="Personel">
                    <ItemTemplate>
                        <%# string.Format("{0} {1}", ((VeriSorgulari.Odeme)Container.DataItem).Personel.Ad,  ((VeriSorgulari.Odeme)Container.DataItem).Personel.Soyad)%>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </fieldset>
</asp:Content>
