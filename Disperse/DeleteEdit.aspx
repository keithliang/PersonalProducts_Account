<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="DeleteEdit.aspx.cs" Inherits="DeleteEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

        日　　期：<asp:Label ID="Label2" runat="server"></asp:Label>
        <br />
        <br />
        發票編號：<asp:Label ID="Label3" runat="server"></asp:Label>
        <br />
        <br />
        會計科目：<asp:Label ID="Label4" runat="server"></asp:Label>
        <br />
        <br />
        摘　　要：<asp:Label ID="Label5" runat="server"></asp:Label>
        <br />
        <br />
        支　　出：<asp:Label ID="Label6" runat="server"></asp:Label>
        <br />
        <br />
        收　　入：<asp:Label ID="Label7" runat="server"></asp:Label>
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
            style="height: 21px" Text="delete (刪除資料)" />
        <asp:Label ID="Label1" runat="server"></asp:Label>
        <br />
</asp:Content>

