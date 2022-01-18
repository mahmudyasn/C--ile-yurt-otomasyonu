<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Odeme.aspx.cs" Inherits="OgrYurt.Odeme" MasterPageFile="~/Yurt.Master" %>

<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <script type="text/javascript">
        function OgrenciKontrol(source, arguments) {
            if (arguments.Value == null || arguments.Value == '') {
                arguments.IsValid = false;
            } else {
                arguments.IsValid = true;
            }
        }
    </script>
    <h3>Ödeme İşlemleri</h3>
    <fieldset>
        <table>
            <tr>
                <td>
                    <label>
                        <asp:Label ID="Label6" runat="server" Text="Öğrenci"></asp:Label></label>
                </td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlOgrenci" DataTextField="AdSoyad" DataValueField="Id" OnSelectedIndexChanged="ddlOgrenci_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                    <asp:CustomValidator runat="server" ID="cvOgrenci" ValidationGroup="OdemeKaydet" ControlToValidate="ddlOgrenci" ClientValidationFunction="OgrenciKontrol" ErrorMessage="Öğrenci Seçiniz!" ForeColor="Red" ValidateRequestMode="Enabled" ValidateEmptyText="true"></asp:CustomValidator>
                </td>
            </tr>
            <tr>

                <td>
                    <label>
                        <asp:Label ID="Label5" runat="server" Text="Ücret"></asp:Label></label>
                </td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlUcret" DataTextField="Yil" DataValueField="Id">
                    </asp:DropDownList>
                    <asp:CustomValidator runat="server" ID="CustomValidator1" ValidationGroup="OdemeKaydet" ControlToValidate="ddlUcret" ClientValidationFunction="OgrenciKontrol" ErrorMessage="Ücret Seçiniz!" ForeColor="Red" ValidateRequestMode="Enabled" ValidateEmptyText="true"></asp:CustomValidator>
                </td>
            </tr>
            <tr>

                <td>
                    <label>
                        <asp:Label ID="Label1" runat="server" Text="Tutar"></asp:Label></label>
                </td>
                <td>
                    <asp:TextBox ID="txtTutar" runat="server" TextMode="Number" step="0.5"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtTutar" ValidationGroup="OdemeKaydet" ErrorMessage="Tutar Giriniz!" ForeColor="Red" />
                </td>
            </tr>



        </table>
        <asp:Button ID="btnKaydet" runat="server" OnClick="btnKaydet_Click" ValidationGroup="OdemeKaydet" Text="Kaydet" />
        <asp:Button ID="btnTemizle" runat="server" OnClick="btnTemizle_Click" Text="Temizle" />
    </fieldset>
    <fieldset>
        <asp:GridView ID="grdOdeme" runat="server"
            Width="100%" CellPadding="4"
            GridLines="Vertical"
            HeaderStyle-HorizontalAlign="Left"
            AllowPaging="false"
            AutoGenerateColumns="false"
            DataKeyNames="id" OnRowCommand="grdOdeme_RowCommand">
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
                        <%# string.Format("{0}", ((VeriSorgulari.Odeme)Container.DataItem).Ucret.Yil)%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="Tutar" DataField="OdemeTutari" />
                <asp:BoundField HeaderText="İşlem Tarihi" DataField="IslemTarihi" DataFormatString="{0:dd.MM.yyyy HH:mm:ss}" />
                <asp:TemplateField HeaderText="Personel">
                    <ItemTemplate>
                        <%# string.Format("{0} {1}", ((VeriSorgulari.Odeme)Container.DataItem).Personel.Ad,  ((VeriSorgulari.Odeme)Container.DataItem).Personel.Soyad)%>
                    </ItemTemplate>
                </asp:TemplateField>


                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:Button ID="ButtonGuncelle" runat="server" Text="Güncelle" CommandName="Güncelle"
                            CommandArgument="<%# ((VeriSorgulari.Odeme)Container.DataItem).Id %>" />
                        <asp:Button ID="ButtonSil" runat="server" Text="Sil" CommandName="Sil"
                            CommandArgument="<%# ((VeriSorgulari.Odeme)Container.DataItem).Id %>" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>

        </asp:GridView>

    </fieldset>
</asp:Content>
