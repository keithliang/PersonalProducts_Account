<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="View.aspx.cs" Inherits="View" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
    
<asp:DropDownList ID="DropDownList1" runat="server" DataTextField="Expr1" 
            DataValueField="Expr1">
        </asp:DropDownList>
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
            Text="Button  (選取月份)" />
            <br />
            <br />
                   <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
        
        <br />
        <br />
                   <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
        
        <br /><br />
         <table runat="server" id="myTable"  width="100%" 
         cellpadding="5" cellspacing="0" />
        
    </div>
</asp:Content>

