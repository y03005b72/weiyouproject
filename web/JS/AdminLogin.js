function Login() {
    var adminname = $.trim($("#txtLAdminname").val());
    var password = $.trim($("#txtLPwd").val());
    if (adminname == "" || password == "") {
        alert("管理员或者密码不能为空");
    }
    else {
        $.post("../ajax/adminregAjax.ashx", { "adminname": adminname, "password": password, "cmd": "adminlogin" }, function (data) {
            var data = eval('(' + data + ')');//json字符串转成js对象
            if (data.Success) {
                //$.cookie('qq', qq, { expire: 7, path: '/' });
                //$.cookies('qq'.null);
                alert(data.Info);
                window.location.href = 'AdminOpera.aspx';
            }
            else {
                alert(data.Info);
            }
        });
    }
}
