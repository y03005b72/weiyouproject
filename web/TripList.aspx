<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TripList.aspx.cs" Inherits="web.TripList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <!--#include file="include/headercon.html"-->
    <link href="CSS/TripList.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <!--#include file="include/header.html"-->
        <!--#include file="include/nav.html"-->
        <div id="newsmain">
            <div class="crumbs"><a href="index.aspx">首页</a><a href="ShopList.aspx"> > 旅社查看</a><a href="GuideList.aspx"> > 导游查看</a> > 旅程查看</div>
            <div class="tripcard">
                <%=GetTripList() %>
            </div>
            <%=GetPagerHtml() %>
        </div>
        <!--#include file="include/footer.html"-->
    </form>
</body>
</html>
