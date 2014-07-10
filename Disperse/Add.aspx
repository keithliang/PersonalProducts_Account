<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Add.aspx.cs" Inherits="Add" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
        日　　期：<asp:TextBox ID="TextBox1" runat="server" ReadOnly="True"></asp:TextBox>
        <br />
        <br />
        <asp:Calendar ID="Calendar1" runat="server" 
            onselectionchanged="Calendar1_SelectionChanged"></asp:Calendar>
        <br />
        <br />
        發票編號：<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        <br />
        <br />
        會計科目：<asp:DropDownList ID="DropDownList1" runat="server">
        </asp:DropDownList>
        <br />
        <br />
        摘　　要：<asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
        <br />
        <br />
        支　　出：<asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
        <br />
        <br />
        收　　入：<asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
    
        <br />
    
    </div>
    <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
        Text="Button (新增資料)" />
    <asp:Label ID="Label1" runat="server"></asp:Label>
</asp:Content>

