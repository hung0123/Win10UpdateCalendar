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
    <h1>�`���ӤH�q���t�Τɯŵn�O�t��</h1>
    <h1>�п�ܱz�i�ɯŪ��ɶ�</h1>
    <div id="divMain">
    </div>
    <div id="myModal" class="modal">
        <!-- Modal content -->
        <div class="modal-content">
            <div class="modal-header">
                <span class="close">&times;</span>
                <h2 style="text-align:center;">
                    �n�O���</h2>
            </div>
            <div class="modal-body">
                <table style="margin: 0 auto; width:17%" class="modalTable">
                    <tr>
                        <td>
                            �覸
                        </td>
                        <td id="step">
                            
                        </td>
                    </tr>
                    <tr>
                        <td>
                            ���
                        </td>
                        <td>
                            <select id="organ">
�@                            <option value="�`�g�z��">�`�g�z��</option>
�@                            <option value="�]�֫�">�]�֫�</option>
�@                            <option value="�[���԰ӫ�">�[���԰ӫ�</option>
�@                            <option value="�g�������">�g�������</option>
�@                            <option value="�s�Ӿ��}�o��">�s�Ӿ��}�o��</option>
�@                            <option value="�޲z����Team">�޲z����Team</option>
�@                            <option value="�]�|��">�]�|��</option>
�@                            <option value="�k���">�k���</option>
�@                            <option value="�H�O�귽��">�H�O�귽��</option>
�@                            <option value="�`�ȳ�">�`�ȳ�</option>
�@                            <option value="�t�Υ��� Team">�t�Υ��� Team</option>
�@                            <option value="�t�ζ}�o��">�t�ζ}�o��</option>
�@                            <option value="�t�ιB�γ�">�t�ιB�γ�</option>
�@                            <option value="�Ʀ�Ь㳡">�Ʀ�Ь㳡</option>
�@                            <option value="�ӫ~��">�ӫ~��</option>
�@                            <option value="�A����">�A����</option>
�@                            <option value="��X��P�[������">��X��P�[������</option>
�@                            <option value="�|���[�q�ӱ��i��">�|���[�q�ӱ��i��</option>
�@                            <option value="E-R�Ʒ~��">E-R�Ʒ~��</option>
�@                            <option value="���y�~�O��������team">���y�~�O��������team</option>
�@                            <option value="���y��">���y��</option>
�@                            <option value="�~�O��">�~�O��</option>
�@                            <option value="�}�o�~�ȳ�">�}�o�~�ȳ�</option>
�@                            <option value="�]�Ƥu�{��">�]�Ƥu�{��</option>
�@                            <option value="�ӳ��޲z��">�ӳ��޲z��</option>
�@                            <option value="��~�~�ȳ�">��~�~�ȳ�</option>
�@                            <option value="��~����Team">��~����Team</option>
�@                            <option value="��~�V�m��">��~�V�m��</option>
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            �m�W
                        </td>
                        <td>
                            <input id="name">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            ����
                        </td>
                        <td>
                            <input id="phone">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <input type="button" value="�T�w" onclick="SaveData();">
                            <input type="button" value="����" onclick="Cancel();">
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
