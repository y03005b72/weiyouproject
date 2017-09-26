/// <reference path="jquery-3.2.0.min.js" />

function AddClick() {
    var guideid = 1000000;
    var tripname = $.trim($("#tripname").val());
    var tripprice = $.trim($("#tripprice").val());
    var tripcontent = $.trim($("#tripcontent").val());
    var trippicture = $.trim($("#trippicture").val());
    $.post("../ajax/ShopAjax.ashx", { "guideid": guideid, "tripname": tripname, "tripprice": tripprice, "tripcontent": tripcontent, "trippicture": trippicture,"cmd": "tripadd" }, function (data) {
        var data = eval('(' + data + ')');//json字符串转成js对象
        if (data.Success) {
            alert(data.Info);
        }
        else {
            alert(data.Info);
        }
    });
}

function DelClick() {
    var tripDid = $.trim($("#tripDid").val());
    $.post("../ajax/ShopAjax.ashx", { "tripDid": tripDid, "cmd": "tripdel" }, function (data) {
        var data = eval('(' + data + ')');//json字符串转成js对象
        if (data.Success) {
            alert(data.Info);
        }
        else {
            alert(data.Info);
        }
    });
}

function UpDateClick() {
    var UtripId = $.trim($("#UtripId").val());
    var Utripname = $.trim($("#Utripname").val());
    var Utripprice = $.trim($("#Utripprice").val());
    var Utripcontent = $.trim($("#Utripcontent").val());
    var Utrippicture = $.trim($("#Utrippicture").val());
    $.post("../ajax/ShopAjax.ashx", { "UtripId": UtripId, "Utripname": Utripname, "Utripprice": Utripprice, "Utripcontent": Utripcontent, "Utrippicture": Utrippicture, "cmd": "tripupdate" }, function (data) {
        var data = eval('(' + data + ')');//json字符串转成js对象
        if (data.Success) {
            alert(data.Info);
        }
        else {
            alert(data.Info);
        }
    });
}