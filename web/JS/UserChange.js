/// <reference path="jquery-3.2.0.min.js" />


function UpdateClick() {
    var txtidCard = $.trim($("#txtidCard").val());
    var txtname = $.trim($("#txtname").val());
    var txtemail = $.trim($("#txtemail").val());
    var txtdp = $.trim($("#txtdp").val());
    $.post("../ajax/userregAjax.ashx", { "txtidCard": txtidCard, "txtname": txtname, "txtemail": txtemail, "txtdp": txtdp, "cmd": "updateuser" }, function (data) {
        var data = eval('(' + data + ')');//json字符串转成js对象
        if (data.Success) {
            alert(data.Info);
        }
        else {
            alert(data.Info);
        }
    });
}