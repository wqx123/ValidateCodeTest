<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Web.Memmber.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="../js/jquery-3.2.0.min.js"></script>
    <script>
        $(function () {
            //注册验证码刷新事件
            $("#imgCode").click(function () {
                RefreshValidate();
            });
           
            $("#txtValidate").blur(function () {
                CheckValidate();
            });
        });

        //验证码刷新,使用函数来设置属性值,n选择器的index值,v为当前属性值
        function RefreshValidate() {
            var srcImg = $("#imgCode").attr("src");
            $("#imgCode").attr("src", function (n, v) {
                return v + "?id=" + new Date().getMilliseconds();
            });
        }

        //检查验证码
        function CheckValidate() {
            var val = $("#txtValidate").val();
            if (val != "") {
                $.post("../ashx/ValidateRegister.ashx", { "action": "CheckValidate", "txtValidate": val }, function (data) {
                    if (data == "yes") {
                        $("#tip").text("成功");
                    } else {
                        $("#tip").text("失败");
                    }
                });
            } else {
                $("#tip").text("不能为空");
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <input type="text" name="txtValidate" value="" id="txtValidate" />
            <a href="javascript:void(0)">
                <img src="/ashx/ValidateCode.ashx" alt="验证码" id="imgCode" />
            </a>
            <br />
            <span id="tip"></span>
        </div>
    </form>
</body>
</html>
