<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserBuyCar.aspx.cs" Inherits="web.UserBuyCar" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <!--#include file="include/headercon.html"-->
    <link href="CSS/TripList.css" rel="stylesheet" />
    <link href="CSS/UserBuyCar.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <!--#include file="include/header.html"-->
        <!--#include file="include/nav.html"-->
        <div>
            <%=GetBuycarList() %>
        </div>
        <!--#include file="include/footer.html"-->
    </form>
</body>
</html>
