function Login() {
    var shoppername = $.trim($("#txtLShoppername").val());
    var password = $.trim($("#txtLPwd").val());
    if (shoppername == "" || password == "") {
        alert("旅社账号或者密码不能为空");
    }
    else {
        $.post("../ajax/shopregAjax.ashx", { "shoppername": shoppername, "password": password, "cmd": "shoplogin" }, function (data) {
            var data = eval('(' + data + ')');//json字符串转成js对象
            if (data.Success) {
                //$.cookie('qq', qq, { expire: 7, path: '/' });
                //$.cookies('qq'.null);
                alert(data.Info);
                window.location.href = 'ShopOpera.aspx';
            }
            else {
                alert(data.Info);
            }
        });
    }
}
