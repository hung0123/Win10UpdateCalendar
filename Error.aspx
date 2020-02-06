<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="Web1.Error" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Content/Style.Standard.Blue.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" style="height: 500px; width: 100%;">
        <tr>
            <td valign="middle" align="center" >
                <table class="Pattern" width="500px">
                    <tr>
                        <td colspan="2" class="Head">
                            系統發生內部錯誤，請再試一次或向系統負責人報修！
                        </td>
                    </tr>
                    <tr>
                        <td class="Label" width="25%">
                            登入人員：
                        </td>
                        <td class="Context">
                            <asp:Label ID="lbUserID" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="Label">
                            錯誤時間：
                        </td>
                        <td class="Context">
                            <asp:Label ID="lbErrTime" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="Foot">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
