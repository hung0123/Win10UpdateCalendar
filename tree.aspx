<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="tree.aspx.cs" Inherits="Web1.tree" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="smMaster" runat="server" EnablePageMethods="true" CombineScripts="true">
    </asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="upMaster" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
        <ContentTemplate>
            <table style="width: 100%;" border="0" cellspacing="0">
                <tr>
                    <td align="right" width="100">
                        Func：
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="drpFunc" Style="width: 100%" AutoPostBack="true" runat="server"
                            OnSelectedIndexChanged="drpFunc_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        User：
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="drpUser" Style="width: 100%" AutoPostBack="true" runat="server"
                            OnSelectedIndexChanged="drpUser_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <asp:UpdatePanel ID="upParam" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:GridView ID="gvParam" runat="server" Style="width: 100%" AutoGenerateColumns="False"
                        CellPadding="0" CellSpacing="0" BorderWidth="0" AllowSorting="True" ShowHeader="false">
                        <PagerSettings Visible="False" />
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <table border="0" cellpadding="0" cellspacing="0" width=100%>
                                        <tr>
                                            <td align="right" width="100">
                                                <asp:Label ID="lbParamID" runat="server" Text='<%# Eval("ParamID")%>'></asp:Label>：
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtParamValue" Width="100%" Text='<%# Eval("ParamDefault")%>' runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td align="left" style="font-size: small">
                                                <asp:Label ID="lbParamDesc" runat="server" Text='<%# Eval("ParamDesc")%>'></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="center" BorderWidth="0" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
            <table style="width: 100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td align="right" width="100">
                        WindowOpen：
                    </td>
                    <td align="left">
                        <asp:CheckBox ID="ckOpenWindow" runat="server" />
                    </td>
                </tr>
            </table>
            <hr />
            <center>
                <asp:Button ID="btnLogin" runat="server" Text="登入" OnClick="btnLogin_Click" /></center>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
