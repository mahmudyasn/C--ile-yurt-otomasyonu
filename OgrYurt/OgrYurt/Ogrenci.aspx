<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Ogrenci.aspx.cs" Inherits="OgrYurt.Ogrenci" MasterPageFile="~/Yurt.Master"%>

<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
        <h3>Öğrenci İşlemleri</h3>
    <fieldset>
        <table>
            <tr>
                <th colspan="2" style="width:50%;"></th>
                <th colspan="2" style="width:50%;"></th>
            </tr>
            <tr>
                <td>
                    <label>TC Kimlik No</label>
                </td>
                <td>
                    <asp:TextBox ID="txtTC" runat="server" type="number"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ValidationGroup="OgrenciKaydet" ID="RequiredFieldValidator3" ControlToValidate="txtTC" ErrorMessage="TC Kimlik No Giriniz!" />
                </td>

                <td>
                    <label>Öğrenci No</label>
                </td>
                <td>
                    <asp:TextBox ID="txtOgrenciNo" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ValidationGroup="OgrenciKaydet" ID="RequiredFieldValidator1" ControlToValidate="txtOgrenciNo" ErrorMessage="Ogrenci No Giriniz!" />
                </td>
            </tr>
            <tr>
                <td>
                    <label>Ad</label>
                </td>
                <td>
                    <asp:TextBox ID="txtAd" runat="server" >
                    </asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ValidationGroup="OgrenciKaydet" ID="reqKapasite" ControlToValidate="txtAd" ErrorMessage="Ad Giriniz!" />
                </td>
                <td>
                    <label>Soyad</label>
                </td>
                <td>
                    <asp:TextBox ID="txtSoyad" runat="server" >
                    </asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ValidationGroup="OgrenciKaydet" ID="RequiredFieldValidator2" ControlToValidate="txtSoyad" ErrorMessage="Soyad Giriniz!" />
                </td>
            </tr>
            <tr>
                <td>
                    <label>Telefon</label>
                </td>
                <td>
                    <asp:TextBox ID="txtTelefon" runat="server" type="number">
                    </asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ValidationGroup="OgrenciKaydet" ID="RequiredFieldValidator4" ControlToValidate="txtTelefon" ErrorMessage="Telefon Giriniz!" />
                </td>
                <td>
                    <label>Sınıf</label>
                </td>
                <td>
                    <asp:TextBox ID="txtSinif" runat="server" type="number">
                    </asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ValidationGroup="OgrenciKaydet" ID="RequiredFieldValidator5" ControlToValidate="txtSinif" ErrorMessage="Sınıf Giriniz!" />
                </td>
            </tr>
            <tr>
                <td>
                    <label>
                        <asp:Label ID="Label4" runat="server" Text="Durum"></asp:Label></label>
                </td>
                <td>
                    <asp:CheckBox ID="chkDurum" runat="server" Text="Aktif"></asp:CheckBox>
                </td>

                <td>
                    <label>
                        <asp:Label ID="Label1" runat="server" Text="Oda"></asp:Label></label>
                </td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlOda" DataTextField="OdaNo" DataValueField="Id"></asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server" ValidationGroup="OgrenciKaydet" ID="RequiredFieldValidator6" ControlToValidate="ddlOda" ErrorMessage="Oda Seçiniz!" />
                </td>
            </tr>
        </table>
        <asp:Button ID="btnKaydet" runat="server" OnClick="btnKaydet_Click" Text="Kaydet" ValidationGroup="OgrenciKaydet" />
        <asp:Button ID="btnTemizle" runat="server" OnClick="btnTemizle_Click" Text="Temizle" />
    </fieldset>
    <fieldset>
      

           <table style="width: 100%;">
            <tr>
                <th>Sıra No</th>
                <th>TC</th>
                <th>Öğrenci Numara</th>
                <th>Ad</th>
                <th>Soyad</th>
                <th>Sınıf</th>
                
                <th>Oda</th>
                
            </tr>
            <asp:Repeater runat="server" ID="rptData" OnItemCommand="rptData_ItemCommand">
                <ItemTemplate>
                    <tr style="text-align: center;">
                        <td><%# Container.ItemIndex+1 %></td>
                        <td><%# Eval("TC") %></td>
                        <td><%# Eval("OgrenciNo") %></td>
                        <td><%# Eval("Ad") %></td>
                        <td><%# Eval("Soyad") %></td>
                        <td><%# Eval("OgrSinif") %></td>
                        <td><%# Eval("Oda.OdaNo") %></td>
                          <%#  (((VeriSorgulari.Ogrenci)Container.DataItem).Durum?"Aktif":"Pasif") %>
                                            
                        <td>
                           <asp:Button ID="btnUcret" runat="server" Text="Ücret Yönetimi" CommandName="Ucret"
                            CommandArgument="<%# ((VeriSorgulari.Ogrenci)Container.DataItem).Id %>" />
                       
                        <asp:Button ID="ButtonGuncelle" runat="server" Text="Güncelle" CommandName="Güncelle"
                            CommandArgument="<%# ((VeriSorgulari.Ogrenci)Container.DataItem).Id %>" />

                        <asp:Button ID="ButtonSil" runat="server" Text="Sil" CommandName="Sil"
                            CommandArgument="<%# ((VeriSorgulari.Ogrenci)Container.DataItem).Id %>" />
                        </td>
                    </tr>
                </ItemTemplate>

            </asp:Repeater>
        </table>

    </fieldset>
</asp:Content>
