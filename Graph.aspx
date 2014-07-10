<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Graph.aspx.cs" Inherits="Graph" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
     <style>
      body {
        padding-top: 60px; /* 60px to make the container go all the way to the bottom of the topbar */
      }
    </style>


    <!-- HTML5 shim, for IE6-8 support of HTML5 elements -->
    <!--[if lt IE 9]>
      <script src="/Scripts/bootstrap/html5shiv.js"></script>
    <![endif]-->


    <!--擺的位置有差,注意注意,先bootstrap CSS再jquery再bootstrap JS,否則IE報錯,但不影響執行-->
    <link href="Bootstrap/css/bootstrap.css" rel="stylesheet" />
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.9.1.js"></script>
    <script type="text/javascript" src="Bootstrap/js/bootstrap.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <p>
       請選擇月份:
        </p>
    <div class="navbar navbar-inverse navbar-fixed-top">
        <div class="navbar-inner">
            <div class="container">
                <button type="button" class="btn btn-navbar" data-toggle="collapse" data-target=".nav-collapse">
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                <a class="brand" href="#">Account System</a>
                <div class="nav-collapse collapse">
                    <ul class="nav">
                        <li ><a href="Default.aspx" >首頁</a></li>
                        <li ><a href="AddAccount.aspx">新增科目</a></li>
                        <li ><a href="AddJournal.aspx">新增資料</a></li>
                        <li ><a href="UpdateJournal.aspx">更新資料</a></li>
                        <li ><a href="Search.aspx">搜尋資料</a></li>
                        <li ><a href="Report.aspx">資料明細</a></li>
                        <li class="active"><a href="Graph.aspx">圖表資料</a></li>
                        <li><a href="SaveCluod.aspx">儲存資料</a></li>
                        <li><a href="print.aspx">列印資料</a></li>
                    </ul>
                </div>
                <!--/.nav-collapse -->
            </div>
        </div>
    </div>

    <div class="control-group">
         <asp:DropDownList ID="DropDownList1" runat="server" DataTextField="Expr1" 
            DataValueField="Expr1" >
        </asp:DropDownList>
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click1" 
            Text="Go (輸出資料)" class="btn btn-success" />
        <br />

        <asp:Chart ID="Chart1" runat="server" Width="500px" Height="400px">
            <series>
                <asp:Series Name="Series1" XValueMember="Account_name" YValueMembers="COUNT" 
                    Legend="Legend1" IsValueShownAsLabel="True" ChartType="Pyramid">
                </asp:Series>
            </series>
            <chartareas>
                <asp:ChartArea Name="ChartArea1">
                </asp:ChartArea>
            </chartareas>
                        <Legends>
                <asp:Legend Font="新細明體, 9.75pt" IsTextAutoFit="False" Name="Legend1" 
                    Title="科目說明" TitleBackColor="White" TitleFont="新細明體, 12pt, style=Bold">
                </asp:Legend>
            </Legends>
        </asp:Chart>
    </div>
    </form>
</body>
</html>
