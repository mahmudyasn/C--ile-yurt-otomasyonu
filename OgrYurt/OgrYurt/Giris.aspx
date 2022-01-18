<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Giris.aspx.cs" Inherits="OgrYurt.Giris" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style>
        #login {
            background-repeat: no-repeat;
            background-size: cover;
            height: 600px;
        }

        .auto-style1 {
            width: 100%;
        }
        
    </style>
</head>
<body style="height:100%;">
    <form id="login" runat="server">
        <div style="text-align: center; height:100%;" >
            <div style="width: 20%;  height:30%; margin-left: auto; margin-right: auto; top:35%; position: absolute; left: 40%;">
                <h1><strong>Ankara Üniversitesi</strong></h1>
                <h1><strong>Final Ödevi</strong></h1>
                <h2><strong>M. Yasin ÖZCAN</strong></h2>
                <table class="auto-style1">
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>
                <h3><strong>Yurt Otomasyonu</strong></h3>
                            <asp:Login ID="lgn" runat="server" OnAuthenticate="lgn_Authenticate"></asp:Login>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
