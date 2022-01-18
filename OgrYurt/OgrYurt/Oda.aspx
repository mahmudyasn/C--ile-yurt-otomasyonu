<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Oda.aspx.cs" Inherits="OgrYurt.Oda" MasterPageFile="~/Yurt.Master" %>

<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <h3>Oda İşlemleri</h3>
    <fieldset>
        <table>
            <tr>
                <td>
                    <label>Oda No</label>
                </td>
                <td>
                    <asp:TextBox ID="txtOdaNo" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ValidationGroup="OdaKaydet" ID="RequiredFieldValidator1" ControlToValidate="txtOdaNo" ErrorMessage="Oda No Giriniz!" />
                </td>
            </tr>
            <tr>
                <td>
                    <label>Kapasite</label>
                </td>
                <td>
                    <asp:TextBox ID="txtKapasite" runat="server" type="number">
                    </asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ValidationGroup="OdaKaydet" ID="reqKapasite" ControlToValidate="txtKapasite" ErrorMessage="Kapasite Giriniz!" />
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
            </tr>
        </table>
        <asp:Button ID="btnKaydet" runat="server" OnClick="btnKaydet_Click" Text="Kaydet" ValidationGroup="OdaKaydet" />
        <asp:Button ID="btnTemizle" runat="server" OnClick="btnTemizle_Click" Text="Temizle" />
    </fieldset>
    <fieldset>
        
        <table style="width: 100%;">
            <tr>
                <th>Sıra No</th>
                <th>Oda No</th>
                <th>Kapasite</th>
                <th>Öğrenci Sayısı</th>
                <th>Durum</th>
                <th>İşlemler</th>
            </tr>
            <asp:Repeater runat="server" ID="rptData" OnItemCommand="rptData_ItemCommand">
                <ItemTemplate>
                    <tr style="text-align: center;">
                        <td><%# Container.ItemIndex+1 %></td>
                        <td><%# Eval("OdaNo") %></td>
                        <td><%# Eval("KisiSayisi") %></td>
                        <td><%#  (((VeriSorgulari.Oda)Container.DataItem).Ogrencis.Count()) %></td>
                        <td><%#  (((VeriSorgulari.Oda)Container.DataItem).Durum?"Aktif":"Pasif") %></td>
                        <td>
                            <asp:Button ID="ButtonGuncelle" runat="server" Text="Güncelle" CommandName="Güncelle"
                                CommandArgument="<%# ((VeriSorgulari.Oda)Container.DataItem).Id %>" />
                            <asp:Button ID="ButtonSil" runat="server" Text="Sil" CommandName="Sil"
                                CommandArgument="<%# ((VeriSorgulari.Oda)Container.DataItem).Id %>" />
                        </td>
                    </tr>
                </ItemTemplate>

            </asp:Repeater>
        </table>

    </fieldset>
</asp:Content>
