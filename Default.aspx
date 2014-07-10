<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
   <link href="StyleSheet.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style1 {
            width: 900px;
        }
        .auto-style2 {
            width: 900px;
            height: 200px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
   
        
        &nbsp;<table align="center" cellpadding="0" cellspacing="0" class="auto-style1">
            <tr>
                <td colspan="3">
   
        
        <img src="無標題-1.png" /></td>
            </tr>
            <tr>
                <td><a href="AddAccount.aspx" class="btn-two blue block" >新增科目</a></td>
                <td><a href="AddJournal.aspx" class="btn-two blue block">新增資料</a></td>
                <td><a href="updateJournal.aspx" class="btn-two blue block" >更新資料</a></td>
            </tr>
            <tr>
                <td><a href="Search.aspx" class="btn-two blue block" >搜尋資料</a></td>
                <td><a href="Report.aspx" class="btn-two blue block">明細資料</a></td>
                <td><a href="Graph.aspx" class="btn-two blue block">圖表資料</a></td>                
            </tr>
            <tr>
                <td><a href="print.aspx" class="btn-two blue block" >列印資料</a></td>
                <td></td>               
            </tr>
            <tr>              
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
