<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GuideList.aspx.cs" Inherits="web.GuideList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <!--#include file="include/headercon.html"-->
    <link href="CSS/GuideList.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <!--#include file="include/header.html"-->
        <!--#include file="include/nav.html"-->
        <div id="newsmain">
            <div class="crumbs"><a href="index.aspx">首页</a><a href="ShopList.aspx"> > 旅社查看</a> > 导游查看</div>
            <div class="guidecard">
                <%=GetGuideList() %>
            </div>
            <%=GetPagerHtml() %>
        </div>
        <!--#include file="include/footer.html"-->
    </form>
</body>
</html>
