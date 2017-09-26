<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserChange.aspx.cs" Inherits="web.UserChange" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <!--#include file="include/headercon.html"-->
    <script src="JS/UserChange.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <!--#include file="include/header.html"-->
    <!--#include file="include/nav.html"-->
        <div class="dgv">
            <table>
                <tr>
                    <td>身份证：</td>
                    <td>
                        <input type="text" id="txtidCard" /></td>
                </tr>
                <tr>
                    <td>真实姓名：</td>
                    <td>
                        <input type="text" id="txtname" /></td>
                </tr>
                <tr>
                    <td>邮箱：</td>
                    <td>
                        <input type="text" id="txtemail" /></td>
                </tr>
                <tr>
                    <td>头像：</td>
                    <td>
                        <input type="text" id="txtdp" /></td>
                </tr>
                <tr>
                    <td>
                        <input type="button" id="addbtn" value="提交" onclick="UpdateClick()" />
                    </td>
                </tr>
            </table>
        </div>
    <!--#include file="include/footer.html"-->
    </form>
</body>
</html>
