<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="OutputExcel.aspx.cs" Inherits="OutputExcel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <div>
    
        <asp:DropDownList ID="DropDownList1" runat="server" DataTextField="Expr1" 
            DataValueField="Expr1">
        </asp:DropDownList>
        <asp:Button ID="Button1" runat="server" Text="Button (選取月份)" onclick="Button1_Click" />
        <br />

   </div>
</asp:Content>

