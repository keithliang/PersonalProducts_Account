<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JqoueyAutocomplete.aspx.cs" Inherits="JqoueyAutocomplete" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>

    <script type="text/javascript" src="Jquery/jquery-1.3.2.js"></script>
    <script type="text/javascript" src="Jquery/jquery.autocomplete.js"></script>
    <link href="Jquery/jquery.autocomplete.css" rel="stylesheet" />

    <script type="text/javascript">
        $(function () {
            //選項說明: http://docs.jquery.com/Plugins/Autocomplete/autocomplete#url_or_dataoptions
            $("#TextBox1").autocomplete("Default3.aspx",
            {
                minChars: 1, //至少輸入幾個字元才開始給提示?
                matchSubset: false,
                matchContains: false,
                cacheLength:0,
            });

        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
        <asp:TextBox ID="TextBox1" runat="server" Width="428px"></asp:TextBox>
    
        <br />
        <br />
        <br />
         <table runat="server" id="myTable"  width="100%" 
         cellpadding="5" cellspacing="0" /></div>
    </form>
</body>
</html>
