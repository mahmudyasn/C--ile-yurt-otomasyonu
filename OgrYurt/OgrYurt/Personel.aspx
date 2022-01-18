<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Personel.aspx.cs" Inherits="OgrYurt.Personel" MasterPageFile="~/Yurt.Master" %>

<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <h3>Personel İşlemleri</h3>
    <fieldset>
        <table>
            <tr>
                <td>
                    <label>Kullanıcı Adı</label>
                </td>
                <td>
                    <asp:TextBox ID="txtKullaniciAdi" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ValidationGroup="PersonelKaydet" ControlToValidate="txtKullaniciAdi" ErrorMessage="Kullanıcı Adı Giriniz!" ForeColor="Red" />
                </td>

                <td>
                    <label>Şifre</label>
                </td>
                <td>
                    <asp:TextBox TextMode="Password" AutoCompleteType="None" ID="txtPersonelSifre" runat="server">
                    </asp:TextBox>
                    <%--<asp:RequiredFieldValidator runat="server" ID="reqSifre" ControlToValidate="txtPersonelSifre" ValidationGroup="PersonelKaydet" ErrorMessage="Şifre Giriniz!" ForeColor="Red" />--%>
                </td>
            </tr>

            <tr>
                <td>
                    <label>
                        <asp:Label ID="Label3" runat="server" Text="Ad"></asp:Label></label>
                </td>
                <td>
                    <asp:TextBox ID="txtAd" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ValidationGroup="PersonelKaydet" ControlToValidate="txtAd" ErrorMessage="Adınızı Giriniz!" ForeColor="Red" />
                </td>

                <td>
                    <label>
                        <asp:Label ID="Label1" runat="server" Text="Soyad"></asp:Label></label>
                </td>
                <td>
                    <asp:TextBox ID="txtSoyad" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtSoyad" ValidationGroup="PersonelKaydet" ErrorMessage="Soyad Giriniz!" ForeColor="Red" />
                </td>
            </tr>
            <tr>
                <td>
                    <label>
                        <asp:Label ID="Label2" runat="server" Text="Telefon"></asp:Label></label>
                </td>
                <td>
                    <asp:TextBox ID="txtTelefon" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="txtTelefon" ValidationGroup="PersonelKaydet" ErrorMessage="Telefon Giriniz!" ForeColor="Red" />
                </td>

                <td>
                    <label>
                        <asp:Label ID="Label4" runat="server" Text="Durum"></asp:Label></label>
                </td>
                <td>
                    <asp:CheckBox ID="chkDurum" runat="server" Text="Aktif"></asp:CheckBox>
                </td>
            </tr>
        </table>
        <asp:Button ID="btnKaydet" runat="server" OnClick="btnKaydet_Click" ValidationGroup="PersonelKaydet" Text="Kaydet" />
        <asp:Button ID="btnTemizle" runat="server" OnClick="btnTemizle_Click" Text="Temizle" />
    </fieldset>
    <fieldset>
        <asp:GridView ID="grdPersonel" runat="server"
            Width="100%" CellPadding="4"
            GridLines="Vertical"
            HeaderStyle-HorizontalAlign="Left"
            AllowPaging="false"
            AutoGenerateColumns="false"
            DataKeyNames="id" OnRowCommand="grdPersonel_RowCommand">
            
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

                <asp:BoundField HeaderText="Kullanıcı Adı" DataField="KullaniciAdi" />
                <asp:BoundField HeaderText="Ad" DataField="Ad" />
                <asp:BoundField HeaderText="Soyad" DataField="Soyad" />
                <asp:BoundField HeaderText="Telefon" DataField="Telefon" />
                <asp:BoundField HeaderText="Tarih" DataField="KayitTarihi" DataFormatString="{0:dd.MM.yyyy HH:mm:ss}" />

                <asp:TemplateField HeaderText="Durum">
                    <ItemTemplate>
                        <%#  (((VeriSorgulari.Personel)Container.DataItem).Durum?"Aktif":"Pasif") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:Button ID="ButtonGuncelle" runat="server" Text="Güncelle" CommandName="Güncelle"
                            CommandArgument="<%# ((VeriSorgulari.Personel)Container.DataItem).Id %>" />
                        <asp:Button ID="ButtonSil" runat="server" Text="Sil" CommandName="Sil"
                            CommandArgument="<%# ((VeriSorgulari.Personel)Container.DataItem).Id %>" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>

        </asp:GridView>

    </fieldset>
</asp:Content>
