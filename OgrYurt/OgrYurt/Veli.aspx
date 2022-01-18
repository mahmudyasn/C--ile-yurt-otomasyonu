<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Veli.aspx.cs" Inherits="OgrYurt.Veli" MasterPageFile="~/Yurt.Master" %>

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
    <h3>Veli İşlemleri</h3>
    <fieldset>
        <table>
            <tr>
                <td>
                    <label>
                        <asp:Label ID="Label6" runat="server" Text="Öğrenci"></asp:Label></label>
                </td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlOgrenci" DataTextField="AdSoyad" DataValueField="Id">
                        
                    </asp:DropDownList>
                    <asp:CustomValidator runat="server" ID="cvOgrenci" ValidationGroup="VeliKaydet" ControlToValidate="ddlOgrenci" ClientValidationFunction="OgrenciKontrol" ErrorMessage="Öğrenci Seçiniz!" ForeColor="Red" ValidateRequestMode="Enabled" ValidateEmptyText="true"></asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <label>
                        <asp:Label ID="Label3" runat="server" Text="Ad"></asp:Label></label>
                </td>
                <td>
                    <asp:TextBox ID="txtAd" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ValidationGroup="VeliKaydet" ControlToValidate="txtAd" ErrorMessage="Adınızı Giriniz!" ForeColor="Red" />
                </td>
            </tr>
            <tr>
                <td>
                    <label>
                        <asp:Label ID="Label1" runat="server" Text="Soyad"></asp:Label></label>
                </td>
                <td>
                    <asp:TextBox ID="txtSoyad" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtSoyad" ValidationGroup="VeliKaydet" ErrorMessage="Soyad Giriniz!" ForeColor="Red" />
                </td>
            </tr>
            <tr>
                <td>
                    <label>
                        <asp:Label ID="Label5" runat="server" Text="Yakınlık Derecesi"></asp:Label></label>
                </td>
                <td>
                    <asp:TextBox ID="txtYakinlikDerecesi" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtYakinlikDerecesi" ValidationGroup="VeliKaydet" ErrorMessage="Yakınlık Derecesi Giriniz!" ForeColor="Red" />
                </td>
            </tr>
            <tr>
                <td>
                    <label>
                        <asp:Label ID="Label2" runat="server" Text="Telefon"></asp:Label></label>
                </td>
                <td>
                    <asp:TextBox ID="txtTelefon" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="txtTelefon" ValidationGroup="VeliKaydet" ErrorMessage="Telefon Giriniz!" ForeColor="Red" />
                </td>
            </tr>
            
        </table>
        <asp:Button ID="btnKaydet" runat="server" OnClick="btnKaydet_Click" ValidationGroup="VeliKaydet" Text="Kaydet" />
        <asp:Button ID="btnTemizle" runat="server" OnClick="btnTemizle_Click" Text="Temizle" />
    </fieldset>
    <fieldset>
        <asp:GridView ID="grdVeli" runat="server"
            Width="100%" CellPadding="4"
            GridLines="Vertical"
            HeaderStyle-HorizontalAlign="Left"
            AllowPaging="false"
            AutoGenerateColumns="false"
            DataKeyNames="id" OnRowCommand="grdVeli_RowCommand">
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
                        <%# string.Format("{0} {1}", ((VeriSorgulari.Veli)Container.DataItem).Ogrenci.Ad,  ((VeriSorgulari.Veli)Container.DataItem).Ogrenci.Soyad)%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="Ad" DataField="Ad" />
                <asp:BoundField HeaderText="Soyad" DataField="Soyad" />
                <asp:BoundField HeaderText="Yakınlık Derecesi" DataField="YakinlikDerecesi" />
                <asp:BoundField HeaderText="Telefon" DataField="Telefon" />

                

                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:Button ID="ButtonGuncelle" runat="server" Text="Güncelle" CommandName="Güncelle"
                            CommandArgument="<%# ((VeriSorgulari.Veli)Container.DataItem).Id %>" />
                        <asp:Button ID="ButtonSil" runat="server" Text="Sil" CommandName="Sil"
                            CommandArgument="<%# ((VeriSorgulari.Veli)Container.DataItem).Id %>" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>

        </asp:GridView>

    </fieldset>
</asp:Content>
