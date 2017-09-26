<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserReg.aspx.cs" Inherits="web.UserReg" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <!--#include file="include/headercon.html"-->
    <link href="CSS/UserReg.css" rel="stylesheet" />
    <script src="JS/UserReg.js"></script>
</head>
<body>
    <form id="form1" runat="server" class="layui-form" >
        <!--#include file="include/header.html"-->
        <!--#include file="include/nav.html"-->
        <div id="regmain">
            <div id="regleft">
                <div>
                    <img src="image/reg-pic.png" />
                </div>
                <div class="begininfor">
                    注册惟游天下帐号<p class="l_f">简单几步，即可开启您的惟游天下游学之旅！</p>
                </div>
            </div>
            <div id="regright">
                <div class="regitem1">
                    <div id="stratinfor">开启您的惟游之旅-注册</div>
                    <div id="reglogin"><a href="UserLogin.aspx">登录</a></div>
                </div>

                <div class="layui-form-item">
                    <label class="layui-form-label"><i class="layui-icon">&#xe612;</i> </label>
                    <div class="layui-input-block">
                        <input type="text" id="txtUsername" placeholder="请输入账号" class="layui-input" />
                    </div>
                </div>
                <div class="layui-form-item">
                    <label class="layui-form-label"><i class="layui-icon">&#xe642;</i> </label>
                    <div class="layui-input-block">
                        <input type="password" id="txtRPwd" placeholder="请输入密码，数字、字母组成，6~12位" class="layui-input" />
                    </div>
                </div>
                <div class="layui-form-item">
                    <label class="layui-form-label"><i class="layui-icon">&#xe650;</i></label>
                    <div class="layui-input-block">
                        <input type="radio" name="sex" value="男" title="男" checked=""/>
                        <input type="radio" name="sex" value="女" title="女"/>
                    </div>
                </div>
                <div class="layui-form-item">
                    <label class="layui-form-label"><i class="layui-icon">&#xe63b;</i> </label>
                    <div class="layui-input-block">
                        <input type="text" id="txtRTel" placeholder="请输入手机号" class="layui-input" />
                    </div>
                </div>
                <div class="layui-form-item">
                    <label class="layui-form-label"><i class="layui-icon">&#xe611;</i> </label>
                    <div class="layui-input-block">
                        <input type="text" id="txtRQQ" placeholder="请输入QQ号" class="layui-input" />
                    </div>
                </div>

                <div class="layui-form-item">
                    <label class="layui-form-label"><i class="layui-icon">&#xe643;</i> </label>
                    <div class="layui-inline">
                        <input type="text" id="txtCheckCode" placeholder="请输入验证码" class="layui-input" style="width: 120px;" />
                    </div>
                    <div class="layui-inline">
                        <img src="/mg/image.aspx" id="imgcheckcode" onclick="RefreshCkc()" />
                    </div>
                </div>
                <div class="layui-form-item">
                    <div class="layui-input-block">
                        <input type="button" class="layui-btn layui-btn-big" id="btnReg" onclick="Reg()" value="马上注册"/>
                    </div>
                </div>
            </div>
        </div>
        <!--#include file="include/footer.html"-->
    </form>
</body>
</html>