<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Lottery.aspx.cs" Inherits="Lottery" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
        <script type="text/javascript" src="jquery-1.2.6.pack.js"></script>
<script type="text/javascript" src="jquery.autocomplete.js"></script>
<link rel="stylesheet" type="text/css" href="jquery.autocomplete.css" />
<script type="text/javascript">
    $(function () {
        $("#Text").autocomplete('Lottery.aspx');
    });
</script> 
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
        <asp:TextBox ID="TextBox1" runat="server" Width="483px"></asp:TextBox>
    
        <br />
        <br />
             <table runat="server" id="myTable"  width="100%" 
         cellpadding="5" cellspacing="0" />
    </div>
    </form>
</body>
</html>
