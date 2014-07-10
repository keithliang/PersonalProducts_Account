<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JqueryBootstrap.aspx.cs" Inherits="JqueryBootstrap" %>

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
                        <li class="active"><a title="首頁" href="#">Home</a></li>
                        <li><a href="#about" title="新增科目">Add acount</a></li>
                        <li><a href="#about" title="新增日記帳">Add Journal</a></li>
                        <li><a href="#about" title="搜尋日記帳">Search</a></li>
                        <li><a href="#about" title="展示日記帳">Report</a></li>
                        <li><a href="#about" title="日記帳圖表">Graph</a></li>
                        <li><a href="#about" title="儲存日記帳至....">Save to files</a></li>
                        <li><a href="#about" title="列印日記帳">Print</a></li>
                        <li><a href="#about" title="發票對獎">Lotto receipt</a></li>
                    </ul>
                </div>
                <!--/.nav-collapse -->
            </div>
        </div>
    </div>
    </form>
</body>
</html>
