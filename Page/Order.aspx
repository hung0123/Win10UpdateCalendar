<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Order.aspx.cs" Inherits="Web1.Page.Order" %>

<%@ Register TagPrefix="acc" Namespace="Accudata.Web.Control" Assembly="Accudata.Web.Control" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server">

    <script src="../Main.js"></script>

    <style>
        .tdDay
        {
            color: Red;
            height: 20px;
        }
        .orderTable td
        {
            text-align: center;
            vertical-align: middle;
            width:14%;
            border:1px solid black;
        }
        .modalTable td 
        {
            text-align: center;
            vertical-align: middle;
            width:14%;
        }
    </style>
    <style>
        body
        {
            font-family: Arial, Helvetica, sans-serif;
        }
        /* The Modal (background) */
        .modal
        {
            display: none; /* Hidden by default */
            position: fixed; /* Stay in place */
            z-index: 1; /* Sit on top */
            left: 0;
            top: 0;
            width: 100%; /* Full width */
            height: 100%; /* Full height */
            overflow: auto; /* Enable scroll if needed */
            background-color: rgb(0,0,0); /* Fallback color */
            background-color: rgba(0,0,0,0.4); /* Black w/ opacity */
            -webkit-animation-name: fadeIn; /* Fade in the background */
            -webkit-animation-duration: 0.4s;
            animation-name: fadeIn;
            animation-duration: 0.4s;
        }
        /* Modal Content */
        .modal-content
        {
            position: fixed;
            bottom: 50%;
            background-color: #fefefe;
            width: 100%;
            -webkit-animation-name: slideIn;
            -webkit-animation-duration: 0.4s;
            animation-name: slideIn;
            animation-duration: 0.4s;
        }
        /* The Close Button */
        .close
        {
            color: white;
            float: right;
            font-size: 28px;
            font-weight: bold;
        }
        .close:hover, .close:focus
        {
            color: #000;
            text-decoration: none;
            cursor: pointer;
        }
        .modal-header
        {
            padding: 2px 16px;
            background-color: #5cb85c;
            color: white;
        }
        .modal-body
        {
            padding: 2px 16px;
        }
        .modal-footer
        {
            padding: 2px 16px;
            background-color: #5cb85c;
            color: white;
        }
        /* Add Animation */
        @-webkit-keyframes@-webkit-keyframesslideIn{from{bottom:-300px;opacity:0}
        to
        {
            bottom: 0;
            opacity: 1;
        }
        }
        @keyframesslideIn{from{bottom:-300px;opacity:0}
        to
        {
            bottom: 0;
            opacity: 1;
        }
        }
        @-webkit-keyframesfadeIn{from{opacity:0}
        to
        {
            opacity: 1;
        }
        }
        @keyframesfadeIn{from{opacity:0}
        to
        {
            opacity: 1;
        }
        }</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpContent" runat="server">
    <h1>總部個人電腦系統升級登記系統</h1>
    <h1>請選擇您可升級的時間</h1>
    <div id="divMain">
    </div>
    <div id="myModal" class="modal">
        <!-- Modal content -->
        <div class="modal-content">
            <div class="modal-header">
                <span class="close">&times;</span>
                <h2 style="text-align:center;">
                    登記表單</h2>
            </div>
            <div class="modal-body">
                <table style="margin: 0 auto; width:17%" class="modalTable">
                    <tr>
                        <td>
                            梯次
                        </td>
                        <td id="step">
                            
                        </td>
                    </tr>
                    <tr>
                        <td>
                            單位
                        </td>
                        <td>
                            <select id="organ">
　                            <option value="總經理室">總經理室</option>
　                            <option value="稽核室">稽核室</option>
　                            <option value="加盟諮商室">加盟諮商室</option>
　                            <option value="經營企劃部">經營企劃部</option>
　                            <option value="新商機開發部">新商機開發部</option>
　                            <option value="管理企劃Team">管理企劃Team</option>
　                            <option value="財會部">財會部</option>
　                            <option value="法遵室">法遵室</option>
　                            <option value="人力資源部">人力資源部</option>
　                            <option value="總務部">總務部</option>
　                            <option value="系統企劃 Team">系統企劃 Team</option>
　                            <option value="系統開發部">系統開發部</option>
　                            <option value="系統運用部">系統運用部</option>
　                            <option value="數位創研部">數位創研部</option>
　                            <option value="商品部">商品部</option>
　                            <option value="鮮食部">鮮食部</option>
　                            <option value="整合行銷暨企劃部">整合行銷暨企劃部</option>
　                            <option value="會員暨電商推進部">會員暨電商推進部</option>
　                            <option value="E-R事業部">E-R事業部</option>
　                            <option value="物流品保本部企劃team">物流品保本部企劃team</option>
　                            <option value="物流部">物流部</option>
　                            <option value="品保部">品保部</option>
　                            <option value="開發業務部">開發業務部</option>
　                            <option value="設備工程部">設備工程部</option>
　                            <option value="商場管理部">商場管理部</option>
　                            <option value="營業業務部">營業業務部</option>
　                            <option value="營業企劃Team">營業企劃Team</option>
　                            <option value="營業訓練部">營業訓練部</option>
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            姓名
                        </td>
                        <td>
                            <input id="name">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            分機
                        </td>
                        <td>
                            <input id="phone">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <input type="button" value="確定" onclick="SaveData();">
                            <input type="button" value="取消" onclick="Cancel();">
                        </td>
                        
                            
                        
                    </tr>
                </table>
            </div>
            <div class="modal-footer">
                <h3></h3>
            </div>
        </div>
    </div>
</asp:Content>
