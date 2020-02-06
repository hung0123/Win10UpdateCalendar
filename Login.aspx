<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Web1.Login" %>
<%@ Register TagPrefix="acc" Namespace="Accudata.Web.Control" Assembly="Accudata.Web.Control" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>帳號:
            <asp:TextBox runat="server" ID="txtAcc"></asp:TextBox>
            <br />
            密碼:
            <asp:TextBox runat="server"  id="txtPsw" TextMode="Password"></asp:TextBox>
            <br />
            <asp:Button runat="server" Text="確定" id="btnConfirm" OnClick="btnConfirm_Click"/>
        </div>
    </form>
</body>
</html>
