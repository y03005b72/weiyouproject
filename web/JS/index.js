/// <reference path="jquery-3.1.1.min.js" />
var i = 0;
var j = 0;
var timer;
$(function () {
    $(".ig").eq(0).show().siblings().hide();
    ShowTime();
    $(".tab").hover(function () {
        i = $(this).index();
        Show();
        clearInterval(timer);
    }, function () {
        ShowTime();
    });

    $(".btn1").click(function () {
        clearInterval(timer);
        if (i == 0) {
            i = 8;
        }
        i--;
        Show();
        ShowTime();
    });

    $(".btn2").click(function () {
        clearInterval(timer);
        if (i == 7) {
            i = -1;
        }
        i++;
        Show();
        ShowTime();
    });
});

function Show() {
    $(".ig").eq(i).fadeIn(500).siblings().fadeOut(500).stop(true, true);
    $(".tab").eq(i).addClass("bg").siblings().removeClass("bg");
}

function ShowTime() {
    timer = setInterval(function () {
        i++;
        if (i == 6) {
            i = 0;
        }
        Show();
    }, 3000);
}


$(function () {
    ShowTab();
    $("#carousel").hover(function () {
        $(".flip").show();
    }, function () {
        $(".flip").hide();
    });
    $(".cpic").eq(0).show().siblings().hide();
    BeginCarousel();
    $(".tab").hover(function () {
        i = $(this).index();
        clearInterval(timer);
        ShowPicTab();
    }, function () {
        BeginCarousel();
    });
    $(".preflip").click(function () {
        clearInterval(timer);
        i--;
        if (i == -1) {
            i = 4;
        }
        ShowPicTab();
        BeginCarousel();
    });
    $(".nextflip").click(function () {
        clearInterval(timer);
        i++;
        if (i == 5) {
            i = 0;
        }
        ShowPicTab();
        BeginCarousel();
    });
});


function ShowTab() {
    var tabNum = $(".important").find("li");
    var contentNum = $(".contents").children();
    var timer;
    $(tabNum).each(function (index) {
        $(this).hover(function () {
            var that = $(this)
            timer = window.setTimeout(function () {
                $(contentNum).eq(index).css({ "display": "block" });
                $(contentNum).eq(index).siblings().css({ "display": "none" });
                that.find("a").css({ "background": "#FFF", "color": "#fff" });
                that.find("strong").show();
                that.siblings().find("strong").hide();
                that.siblings().find("a").css({ "background": "#fff", "color": "black" });
            }, 100)
        }, function () {
            window.clearTimeout(timer);
        })
    })
}