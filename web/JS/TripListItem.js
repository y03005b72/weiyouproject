function AddBuyCar() {
    var listTitle = document.getElementById("listTitle").innerHTML;
    var domimg = document.getElementById("listPicture");
    var listPicture = domimg.src;
    var listTxt = document.getElementById("listTxt").innerHTML;
    var listPrice = document.getElementById("listPrice").innerHTML;
    $.post("../ajax/buycarAjax.ashx", { "listTitle": listTitle, "listPicture": listPicture, "listTxt": listTxt, "listPrice": listPrice, "cmd": "addbuycar" }, function (data) {
        var data = eval('(' + data + ')');//json字符串转成js对象
        if (data.Success) {
            alert(data.Info);
        }
        else {
            alert(data.Info);
        }
     });
}