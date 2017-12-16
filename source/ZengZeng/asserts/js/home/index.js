var index = {
    formdata: {
        ivatorValue: ""
    },
    init: function () {
        //初始化文件上传
        //var file = new File();
        File.UploadBaseURL = "";
        File.SiteDomain_Res = "";
        File.DynamicImagePath = "";
        File.Img_folder = "avator";

        //初始化页面的js
        new clip({
            uploadDocument: $("#files2"),
            uploadSuccessCallback: function (imagepath) {
                $("#avatorImage").attr("src", "/Image/" + imagepath);
                $("#ivatorValue").val(imagepath);
            }
        });

        //给文本框加入事件
        var _that = this;
        $("#formdata input").bind('input propertychange', function () {
            _that.getFormData();
            _that.checkInput();
        });

        $("#footer").click(function () {
            //保存用户的基本信息
            var formdata = _that.getFormData();
            $.ajax({
                url: "/api/Card/GenerateCard",
                data: formdata,
                dataType: "json",
                type: "POST",
                success: function (res) {
                    if (res.Code != 1) {
                        alert(res.Message);
                        return;
                    }
                    //得到unique
                    var unique = res.Data;
                    var cardurl = "/Card/ShareCard";
                    location.href = cardurl;

                },
                error: function () {
                    alert("服务器请求出错，请联系管理员！");
                }
            });
        });
    },
    getFormData: function () {
        var formdata = index.formdata;
        formdata.Avator = $("#ivatorValue").val();
        formdata.OpenId = $("#openid").val();
        formdata.NickName = $("#nickname").val();
        formdata.Mobile = $("#mobile").val();
        formdata.Address = $("#address").val();
        formdata.CardNo = $("#cardno").val();
        return formdata;
    },

    checkInput: function () {
        //检查头像
        var formdata = index.formdata;
        if (!formdata.Avator) {
            $("#footer").removeClass().addClass("aui-bar aui-bar-tab  footbtn");
            return false;
        }

        if (!formdata.NickName) {
            $("#footer").removeClass().addClass("aui-bar aui-bar-tab  footbtn");
            return false;
        }

        if (!formdata.Mobile || !/\d{11}/.test(formdata.Mobile)) {
            $("#footer").removeClass().addClass("aui-bar aui-bar-tab  footbtn");
            return false;
        }

        if (!formdata.Address) {
            $("#footer").removeClass().addClass("aui-bar aui-bar-tab  footbtn");
            return false;
        }

        if (!formdata.CardNo) {
            $("#footer").removeClass().addClass("aui-bar aui-bar-tab  footbtn");
            return false;
        }
        $("#footer").removeClass().addClass("aui-bar aui-bar-tab  footbtn active");
        return true;
    }
};