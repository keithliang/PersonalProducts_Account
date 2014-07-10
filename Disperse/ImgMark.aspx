<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ImgMark.aspx.cs" Inherits="Disperse_ImgMark" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
    
    </div>
        <asp:Image ID="Image1" runat="server" />
        <asp:FileUpload ID="FileUpload1" runat="server" />
    </form>
</body>
</html>
