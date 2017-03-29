(function ($) {
    $.extend($,
        {
            //显示消息：如果有easyui，则调用easyui的message组件显示消息
            alertMsg: function (msg, title, funcSuc) {
                //error,question,info,warning
                if ($.messager) {
                    $.messager.alert(title, msg, "info", function () {
                        if (funcSuc) funcSuc();
                    });
                } else {
                    alert(title + "\r\n     " + msg);
                    if (funcSuc) funcSuc();
                }
            },
            //统一处理 返回的json数据格式
            procAjaxData: function (data, funcSuc, funcErr) {
                if (!data || !data.Statu) {
                    return;
                }

                switch (data.Statu) {
                    case "ok":
                        $.alertMsg(data.Msg, "系统提示", function () { funcSuc(data); });
                        break;
                    case "err":
                        $.alertMsg(data.Msg, "系统提示", function () { funcErr(data); });
                        break;
                    case "nologin":
                        $.alertMsg(data.Msg, "系统提示", function () { if (window.top) window.top.location = data.BackUrl; else window.location = data.BackUrl; });
                        break;
                }
            }
        });
}(jQuery));