<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TripLiskItem.aspx.cs" Inherits="web.TripLiskItem" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <!--#include file="include/headercon.html"-->
    <link href="CSS/TripListItem.css" rel="stylesheet" />
    <script src="JS/TripListItem.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <!--#include file="include/header.html"-->
        <!--#include file="include/nav.html"-->
        <div>
            <%=GetTriplist() %>
        </div>
        <!--#include file="include/footer.html"-->
    </form>
</body>
</html>
