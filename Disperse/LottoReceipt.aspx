﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LottoReceipt.aspx.cs" Inherits="LottoReceipt" %>

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

    <script type="text/javascript" src="Jquery/jquery-1.3.2.js"></script>
    <script type="text/javascript" src="Jquery/jquery.autocomplete.js"></script>
    <link href="Jquery/jquery.autocomplete.css" rel="stylesheet" />
        <script type="text/javascript">
            $(function () {
                $("#TextBox1").autocomplete("LottoReceipt.aspx",
                {
                    minChars: 1,
                    matchSubset: false,
                    matchContains: false,
                    cacheLength: 0,
                });

            });
    </script>
</head>
<body>
    <form id="form1" runat="server">
 <p>
        Please input data:
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
                        <li><a href="AddAccount.aspx">新增科目</a></li>
                        <li ><a href="AddJournal.aspx">新增日記帳</a></li>
                        <li ><a href="Search.aspx">搜尋日記帳</a></li>
                        <li ><a href="Report.aspx">展示日記帳</a></li>
                        <li ><a href="Graph.aspx">日記帳圖表</a></li>
                        <li><a href="SaveCluod.aspx">儲存日記帳</a></li>
                        <li><a href="print.aspx">列印日記帳</a></li>
                        <li class="active"><a href="LottoReceipt.aspx" >發票對獎</a></li>
                    </ul>
                </div>
                <!--/.nav-collapse -->
            </div>
        </div>
    </div>
    <div>
        <asp:TextBox ID="TextBox1" runat="server" Width="300px"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" Text="Go (搜尋資料)" OnClick="Button1_Click" class="btn btn-success" />
    
        <br />
         <table runat="server" id="myTable"  class="table table-bordered"/>
    </div>
    </form>

</body>
</html>
