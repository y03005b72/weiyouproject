<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShopOperaTrip.aspx.cs" Inherits="web.ShopOperaTrip" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <!--#include file="include/headercon.html"-->
    <script src="JS/ShopOperaTrip.js"></script>
    <link href="CSS/TripList.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <!--#include file="include/header.html"-->
        <!--#include file="include/nav1.html"-->
        <div id="newsmain">
            <div class="crumbs"><a href="index.aspx">首页</a><a href="ShopList.aspx"> > 旅社查看</a><a href="GuideList.aspx"> > 导游查看</a> > 旅程查看</div>
            <div class="tripcard">
                <%=GetTripList() %>
            </div>    
        <div class="dgv">
            <table>
                <tr>
                    <td>旅途名称：</td>
                    <td>
                        <input type="text" id="tripname" /></td>
                </tr>
                <tr>
                    <td>旅途价格：</td>
                    <td>
                        <input type="text" id="tripprice" /></td>
                </tr>
                <tr>
                    <td>旅涂简介：</td>
                    <td>
                        <input type="text" id="tripcontent" /></td>
                </tr>
                <tr>
                    <td>旅途图片：</td>
                    <td>
                        <input type="text" id="trippicture" /></td>
                </tr>
                <tr>
                    <td>
                        <input type="button" id="addbtn" value="提交" onclick="AddClick()" /></td>
                </tr>
            </table>
        </div>
        <div class="dgv">
            <table>
                <tr>
                    <td>旅途ID：</td>
                    <td>
                        <input type="text" id="tripDid" /></td>
                    <td>
                        <input type="button" id="delbtn" value="删除" onclick="DelClick()" /></td>
                </tr>
            </table>
        </div> 
        <div class="dgv">
            <table>
                <tr>
                    <td>旅途ID：</td>
                    <td>
                        <input type="text" id="UtripId" /></td>
                </tr>
                <tr>
                    <td>旅途名称：</td>
                    <td>
                        <input type="text" id="Utripname" /></td>
                </tr>
                <tr>
                    <td>旅途价格：</td>
                    <td>
                        <input type="text" id="Utripprice" /></td>
                </tr>
                <tr>
                    <td>旅涂简介：</td>
                    <td>
                        <input type="text" id="Utripcontent" /></td>
                </tr>
                <tr>
                    <td>旅途图片：</td>
                    <td>
                        <input type="text" id="Utrippicture" /></td>
                </tr>
                <tr>
                    <td>
                        <input type="button" id="updatebtn" value="提交" onclick="UpDateClick()" /></td>
                </tr>
            </table>
        </div> </div>
        <!--#include file="include/footer.html"-->
    </form>
</body>
</html>
