<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CityWeather.aspx.cs" Inherits="web.CityWeather" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <!--#include file="include/headercon.html"-->
</head>
<body>
    <!--#include file="include/header.html"-->
    <!--#include file="include/nav.html"-->
    <div><%=GetWeather()%></div>
    <!--#include file="include/footer.html"-->
</body>
</html>
