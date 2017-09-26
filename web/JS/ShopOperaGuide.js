/// <reference path="jquery-3.2.0.min.js" />

function AddClick() {
    var shopid = 100000;
    var guidecardId = $.trim($("#guidecardId").val());
    var guidename = $.trim($("#guidename").val());
    var Idcard = $.trim($("#Idcard").val());
    var guidedp = $.trim($("#guidedp").val());
    var guideintroduce = $.trim($("#guideintroduce").val());
    $.post("../ajax/ShopAjax.ashx", { "shopid":shopid,"guidecardId": guidecardId, "guidename": guidename, "Idcard": Idcard, "guidedp": guidedp, "guideintroduce": guideintroduce, "cmd": "guideadd" }, function (data) {
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
    var guideDid = $.trim($("#guideDid").val());
    $.post("../ajax/ShopAjax.ashx", { "guideDid": guideDid,"cmd": "guidedel" }, function (data) {
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
    var UguideId = $.trim($("#UguideId").val());
    var UguidecardId = $.trim($("#UguidecardId").val());
    var Uguidename = $.trim($("#Uguidename").val());
    var UIdcard = $.trim($("#UIdcard").val());
    var Uguidedp = $.trim($("#Uguidedp").val());
    var Uguideintroduce = $.trim($("#Uguideintroduce").val());
    $.post("../ajax/ShopAjax.ashx", { "UguideId": UguideId, "UguidecardId": UguidecardId, "Uguidename": Uguidename, "UIdcard": UIdcard, "Uguidedp": Uguidedp, "Uguideintroduce": Uguideintroduce, "cmd": "guideupdate" }, function (data) {
        var data = eval('(' + data + ')');//json字符串转成js对象
        if (data.Success) {
            alert(data.Info);
        }
        else {
            alert(data.Info);
        }
    });
}