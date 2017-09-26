<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShopList.aspx.cs" Inherits="web.ShopList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <!--#include file="include/headercon.html"-->
    <link href="CSS/ShopList.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <!--#include file="include/header.html"-->
        <!--#include file="include/nav.html"-->
        <div id="newsmain">
            <div class="crumbs"><a href="index.aspx">首页</a> > 旅社列表</div>
            <div class="shopcard">
                <%=GetShopList() %>
            </div>
           <%=GetPagerHtml() %>
        </div>
        <!--#include file="include/footer.html"-->
    </form>
</body>
</html>
