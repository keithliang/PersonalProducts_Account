<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AutocompleteAjaxToolkit.aspx.cs" Inherits="AutocompleteAjaxToolkit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <asp:TextBox ID="TextBox1" runat="server" />
        
    <asp:AutoCompleteExtender
           ID="AutoCompleteExtender1"
           runat="server"
           TargetControlID="TextBox1"
           MinimumPrefixLength="1"
           ServiceMethod="GetCompletionList"
           CompletionSetCount="15"
    />
 
    </form>
</body>
</html>
