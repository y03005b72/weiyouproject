<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserSearch.aspx.cs" Inherits="web.UserSearch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <!--#include file="include/headercon.html"-->
    <link href="CSS/UserSearch.css" rel="stylesheet" />
    <script src="JS/UserSearch.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <!--#include file="include/header.html"-->
    <!--#include file="include/nav.html"-->
        <div id="dtitle">
            <input type="text" id="txtTitle" placeholder="请输入关键字" />
        </div>
        <div id="dcon">
            <ul>
            </ul>
        </div>
        <!--#include file="include/footer.html"-->
    </form>
</body>
</html>
