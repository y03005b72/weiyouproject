<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShopperLogin.aspx.cs" Inherits="web.ShopperLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <!--#include file="include/headercon.html"-->
    <link href="CSS/UserLogin.css" rel="stylesheet" />
    <script src="JS/ShopperLogin.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <!--#include file="include/header.html"-->
        <!--#include file="include/nav1.html"-->
        <div id="regmain">
            <div id="regleft">
                <div>
                    <img src="image/login-pic.png" />
                </div>
                <div class="begininfor">
                    登录惟游天下帐号<p class="l_f">简单几步，即可开启您的惟游天下游学之旅！</p>
                </div>
            </div>
            <div id="regright">
                <div class="regitem1">
                    <div id="stratinfor">开启您的惟游之旅-登录</div>
                    <div id="reglogin"><a href="reg.aspx">注册</a></div>
                </div>

                <div class="layui-form-item">
                    <label class="layui-form-label"><i class="layui-icon">&#xe612;</i> </label>
                    <div class="layui-input-block">
                        <input type="text" id="txtLShoppername" placeholder="请输入账号" class="layui-input" />
                    </div>
                </div>
                <div class="layui-form-item">
                    <label class="layui-form-label"><i class="layui-icon">&#xe642;</i> </label>
                    <div class="layui-input-block">
                        <input type="password" id="txtLPwd" placeholder="请输入密码" class="layui-input" />
                    </div>
                </div>
                <div class="layui-form-item">
                    <div class="layui-input-block">
                        <input type="button" class="layui-btn layui-btn-big" id="btnReg" onclick="Login()" value="马上登录"/>
                    </div>
                </div>
            </div>
        </div>
        <!--#include file="include/footer.html"-->
    </form>
</body>
</html>
