<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Manage.aspx.cs" Inherits="Web1.Page.Manage" %>
<%@ Register TagPrefix="acc" Namespace="Accudata.Web.Control" Assembly="Accudata.Web.Control" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpContent" runat="server">
    <asp:UpdatePanel ID="upCondition" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
        <ContentTemplate>
            <table width="100%" class="Pattern" border="0" cellpadding="0" cellspacing="1">
                <tr>
                    <td colspan="2">
                        管理介面
                    </td>
                </tr>
                <tr>
                    <td class="Label">
                        時間區間
                    </td>
                    <td class="Context">
                        <asp:TextBox ID="txtSTIME" runat="server" Width="100px"></asp:TextBox>
                        <asp:ImageButton ID="btnSTIME" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                        <asp:CalendarExtender ID="calSTIME" runat="server" DateYear="CommonYear" TargetControlID="txtSTIME"
                            PopupButtonID="btnSTIME" Format="yyyy/MM/dd">
                        </asp:CalendarExtender>
                        ~
                        <asp:TextBox ID="txtETIME" runat="server" Width="100px"></asp:TextBox>
                        <asp:ImageButton ID="btnETIME" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                        <asp:CalendarExtender ID="calETIME" runat="server" DateYear="CommonYear" TargetControlID="txtETIME"
                            PopupButtonID="btnETIME" Format="yyyy/MM/dd">
                        </asp:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td class="Label">
                        每天升級人數
                    </td>
                    <td class="Context">
                        <asp:TextBox ID="txtLimit" runat="server" Width="100px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="Label">
                        六日是否開課
                    </td>
                    <td class="Context">
                        <asp:RadioButton ID="rbY" runat="server" GroupName="W" Text="是"/>
                        <asp:RadioButton ID="rbN" runat="server" GroupName="W" Text="否" Checked="true"/>
                        
                    </td>
                </tr>
                <tr align="center">
                    <td colspan="2">
                        <asp:Button ID="btnSaveShow" runat="server" Text="確定" CssClass="btn" 
                            onclick="btnSaveShow_Click"/>
                        <asp:Button ID="btnSave" runat="server" Text="確定" CssClass="btn" 
                            onclick="btnSave_Click" style="display:none"/>
                        <asp:Button ID="btnExport" runat="server" Text="輸出報表" CssClass="btn" 
                            onclick="btnExport_Click"/>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

