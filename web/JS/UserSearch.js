$(function () {
    $("#txtTitle").keyup(function () {
        $("#dcon").show();
        var title = $.trim($(this).val());
        if (title != "") {
            $.post("../ajax/UserSearch.ashx", { "title": title }, function (data) {
                var data = eval('(' + data + ')');
                if (data.Success) {
                    $("#dcon ul").html("").append(data.Info);
                    $("#dcon ul li").bind("click", function () {
                        var txt = $(this).text();
                        $("#txtTitle").val(txt);
                        $("#dcon").hide();
                    });
                    $("#dcon ul li").bind("mouseover", function () {
                        $(this).addClass("bg");
                    });
                    $("#dcon ul li").bind("mouseup", function () {
                        $(this).removeClass("bg");
                    });
                }
            });
        }
        else {
            $("#dcon ul").html("");
        }
    });
});