<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Ucret.aspx.cs" Inherits="OgrYurt.Ucret" MasterPageFile="~/Yurt.Master" %>

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
    <h3>Ücret İşlemleri</h3>
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
                    <asp:CustomValidator runat="server" ID="cvOgrenci" ValidationGroup="UcretKaydet" ControlToValidate="ddlOgrenci" ClientValidationFunction="OgrenciKontrol" ErrorMessage="Öğrenci Seçiniz!" ForeColor="Red" ValidateRequestMode="Enabled" ValidateEmptyText="true"></asp:CustomValidator>
                </td>

                <td>
                    <label>
                        <asp:Label ID="Label5" runat="server" Text="Yıl"></asp:Label></label>
                </td>
                <td>
                    <asp:TextBox ID="txtYil" runat="server"  TextMode="Number"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtYil" ValidationGroup="UcretKaydet" ErrorMessage="Yıl Giriniz!" ForeColor="Red" />
                </td>
            </tr>
            <tr>
                <td>
                    <label>
                        <asp:Label ID="Label3" runat="server" Text="Başlangıç Tarihi" ></asp:Label></label>
                </td>
                <td>
                    <asp:TextBox ID="txtBaslangic" runat="server" TextMode="Date"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ValidationGroup="UcretKaydet" ControlToValidate="txtBaslangic" ErrorMessage="Başlangıç Tarihi Giriniz!" ForeColor="Red" />
                </td>

                <td>
                    <label>
                        <asp:Label ID="Label1" runat="server" Text="Bitiş Tarihi"></asp:Label></label>
                </td>
                <td>
                    <asp:TextBox ID="txtBitis" runat="server" TextMode="Date"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtBitis" ValidationGroup="UcretKaydet" ErrorMessage="Bitiş Tarihi Giriniz!" ForeColor="Red" />
                </td>
            </tr>
            
            <tr>
                <td>
                    <label>
                        <asp:Label ID="Label2" runat="server" Text="Taksit Sayısı"></asp:Label></label>
                </td>
                <td>
                    <asp:TextBox ID="txtTaksitSayisi" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="txtTaksitSayisi" ValidationGroup="UcretKaydet" ErrorMessage="Taksit Sayısı Giriniz!" ForeColor="Red" />
                </td>

                <td>
                    <label>
                        <asp:Label ID="Label4" runat="server" Text="Ücret"></asp:Label></label>
                </td>
                <td>
                    <asp:TextBox ID="txtUcret" runat="server" TextMode="Number" step="0.5"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ControlToValidate="txtUcret" ValidationGroup="UcretKaydet" ErrorMessage="Ücret Giriniz!" ForeColor="Red" />
                </td>
            </tr>
            
        </table>
        <asp:Button ID="btnKaydet" runat="server" OnClick="btnKaydet_Click" ValidationGroup="UcretKaydet" Text="Kaydet" />
        <asp:Button ID="btnTemizle" runat="server" OnClick="btnTemizle_Click" Text="Temizle" />
    </fieldset>
    <fieldset>
     
         <table style="width: 100%;">
            <tr>
                <th>Sıra No</th>
                <th>Yıl</th>
                <th>Kayıt Başlangıç</th>
                <th>Kayıt Bitiş</th>
                <th>Taksit Sayısı</th>
                <th>Ücret</th>
                <th>Yapılan Ödeme Sayısı</th>
                <th>Yapılan Ödeme Toplamı</th>
                
            </tr>
            <asp:Repeater runat="server" ID="rptData" OnItemCommand="rptData_ItemCommand">
                <ItemTemplate>
                    <tr style="text-align: center;">
                        <td><%# Container.ItemIndex+1 %></td>
                        <td><%# Eval("Yil") %></td>
                        <td><%# string.Format("{0:dd.MM.yyyy}", Eval("KayitBaslangicTarihi")) %></td>
                        <td><%# string.Format("{0:dd.MM.yyyy}", Eval("KayitBitisTarihi")) %></td>
                        <td><%# Eval("TaksitSayisi") %></td>
                        <td><%# Eval("ToplamUcret") %></td>
                        <td> <%# string.Format("{0:0}", ((VeriSorgulari.Ucret)Container.DataItem).Odemes.Count())%></td>
                        <td><%# string.Format("{0:c}", ((VeriSorgulari.Ucret)Container.DataItem).Odemes.Sum(odeme=>odeme.OdemeTutari))%></td>
                        
                        <td>
                           <asp:Button ID="btnOdeme" runat="server" Text="Ödeme Yönetimi" CommandName="Odeme"
                            CommandArgument="<%# ((VeriSorgulari.Ucret)Container.DataItem).Id %>" />
                        <asp:Button ID="ButtonGuncelle" runat="server" Text="Güncelle" CommandName="Güncelle"
                            CommandArgument="<%# ((VeriSorgulari.Ucret)Container.DataItem).Id %>" />
                        <asp:Button ID="ButtonSil" runat="server" Text="Sil" CommandName="Sil"
                            CommandArgument="<%# ((VeriSorgulari.Ucret)Container.DataItem).Id %>" />
                        </td>
                    </tr>
                </ItemTemplate>

            </asp:Repeater>
        </table>

    </fieldset>
</asp:Content>