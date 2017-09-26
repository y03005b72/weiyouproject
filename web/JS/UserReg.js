/// <reference path="jquery-3.1.1.min.js" />

layui.use(['form', 'layedit', 'laydate'], function () {
    var form = layui.form()
    , layer = layui.layer
    , layedit = layui.layedit
    , laydate = layui.laydate;

    //创建一个编辑器
    var editIndex = layedit.build('LAY_demo_editor');

    //自定义验证规则
    form.verify({
        title: function (value) {
            if (value.length < 5) {
                return '标题至少得5个字符啊';
            }
        }
      , pass: [/(.+){6,12}$/, '密码必须6到12位']
      , content: function (value) {
          layedit.sync(editIndex);
      }
    });

    //监听指定开关
    form.on('switch(switchTest)', function (data) {
        layer.msg('开关checked：' + (this.checked ? 'true' : 'false'), {
            offset: '6px'
        });
        layer.tips('温馨提示：请注意开关状态的文字可以随意定义，而不仅仅是ON|OFF', data.othis)
    });

    //监听提交
    form.on('submit(demo1)', function (data) {
        layer.alert(JSON.stringify(data.field), {
            title: '最终的提交信息'
        })
        return false;
    });
});

function Reg() {
    var username = $.trim($("#txtUsername").val());
    var pwd = $.trim($("#txtRPwd").val());
    var sex = $('.layui-input-block input[name="sex"]:checked ').val();
    var tel = $.trim($("#txtRTel").val());
    var qq = $.trim($("#txtRQQ").val());
    $.post("../ajax/userregAjax.ashx", { "username": username, "pwd": pwd, "sex": sex, "tel": tel, "qq": qq, "cmd": "reg" }, function (data) {
        var data = eval('(' + data + ')');//json字符串转成js对象
        if (data.Success) {
            alert(data.Info);
            window.location.href = 'UserLogin.aspx';
        }
        else {
            alert(data.Info);
        }
    });
}