function Login() {
    var username = $.trim($("#txtLUsername").val());
    var password = $.trim($("#txtLPwd").val());
    if (username == "" || password == "") {
        alert("用户名或者密码不能为空");
    }
    else {
        $.post("../ajax/userregAjax.ashx", { "username": username, "password": password, "cmd": "login" }, function (data) {
            var data = eval('(' + data + ')');//json字符串转成js对象
            if (data.Success) {
                //$.cookie('qq', qq, { expire: 7, path: '/' });
                //$.cookies('qq'.null);
                alert(data.Info);
                window.location.href = 'ShopList.aspx';
            }
            else {
                alert(data.Info);
            }
        });
    }
}
