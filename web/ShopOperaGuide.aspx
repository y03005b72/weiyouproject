<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShopOperaGuide.aspx.cs" Inherits="web.ShopOpera" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <!--#include file="include/headercon.html"-->
    <script src="JS/ShopOperaGuide.js"></script>
    <link href="CSS/GuideList.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <!--#include file="include/header.html"-->
        <!--#include file="include/nav1.html"-->
        <div id="newsmain">
        <div class="guidecard">
                <%=GetGuideList() %>
            </div>  
        <div class="dgv">
            <table>
                <tr>
                    <td>导游证号：</td>
                    <td>
                        <input type="text" id="guidecardId" /></td>
                </tr>
                <tr>
                    <td>导游姓名：</td>
                    <td>
                        <input type="text" id="guidename" /></td>
                </tr>
                <tr>
                    <td>导游身份证：</td>
                    <td>
                        <input type="text" id="Idcard" /></td>
                </tr>
                <tr>
                    <td>导游头像：</td>
                    <td>
                        <input type="text" id="guidedp" /></td>
                </tr>
                <tr>
                    <td>导游介绍：</td>
                    <td>
                        <input type="text" id="guideintroduce" /></td>
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
                    <td>ID：</td>
                    <td>
                        <input type="text" id="guideDid" /></td>
                    <td>
                        <input type="button" id="delbtn" value="删除" onclick="DelClick()" /></td>
                </tr>
            </table>
        </div> 
        <div class="dgv">
            <table>
                <tr>
                    <td>导游ID：</td>
                    <td>
                        <input type="text" id="UguideId" /></td>
                </tr>
                <tr>
                    <td>导游证号：</td>
                    <td>
                        <input type="text" id="UguidecardId" /></td>
                </tr>
                <tr>
                    <td>导游姓名：</td>
                    <td>
                        <input type="text" id="Uguidename" /></td>
                </tr>
                <tr>
                    <td>导游身份证：</td>
                    <td>
                        <input type="text" id="UIdcard" /></td>
                </tr>
                <tr>
                    <td>导游头像：</td>
                    <td>
                        <input type="text" id="Uguidedp" /></td>
                </tr>
                <tr>
                    <td>导游介绍：</td>
                    <td>
                        <input type="text" id="Uguideintroduce" /></td>
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
