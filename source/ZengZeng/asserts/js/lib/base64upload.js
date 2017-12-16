; var File = {
    UploadBaseURL: "",  //上传文件根路径
    Img_folder: "",     //根目录
    SiteDomain_Res: "",//一般资源路径
    DynamicImagePath: "",//图片资源路径
    UploadBase64: function (fileName, folder, data, option, IsWaterMark, WaterMarkType) {
        fileName = fileName || "";
        folder = folder || "";
        IsWaterMark = IsWaterMark || "";
        WaterMarkType = WaterMarkType || "";
        option = option || {};
        var _option = {
            successCallback: option.successCallback,
            errorCallback: option.errorCallback,
            showLoadingFun: option.showLoadingFun,
            hideLoadingFun: option.hideLoadingFun
        };
        if (typeof (_option.showLoadingFun) == "function") {
            _option.showLoadingFun();
        } else {
            //lib.showLoading();
        }
        $.ajax({
            type: "post",
            url: File.UploadBaseURL + "/Service/UpLoadBase64",
            dataType: "text",
            data: "fileName=" + fileName + "&baseFolder=&folder=" + folder + "&IsWaterMark=" + IsWaterMark + "&WaterMarkType=" + WaterMarkType + "&data=" + encodeURIComponent(data),
            success: function (res) {
                res = JSON.parse(res);
                if (res.IsSuccess) {
                }
                else {
                    alert("上传文件错误，请重试");
                    return false;
                }
                if (typeof (_option.successCallback) == "function") {
                    _option.successCallback(res.ImageUrl);
                }
            },
            error: function (err) {
                if (typeof (_option.errorCallback) == "function") {
                    _option.errorCallback(err);
                }
                else {
                    alert("上传文件错误，请重试");
                }
            },
            complete: function (res) {
                if (typeof (_option.hideLoadingFun) == "function") {
                    _option.hideLoadingFun();
                } else {
                    //lib.hideLoading && lib.hideLoading();
                }
            }
        });
    },
    /**
      * 格式化图片Url
      * @param value 图片地址 
      * @param width 宽度
      * @param height 高度
      * @param type 0:等比缩放；1：裁剪；2：补白；3：以宽度为准进行等比缩放；4：以高度为准进行等比缩放
      * @param quality 图片质量（1--100）
      * @param folder 格式(jpg、png),如果格式为“_raw”则取原格式（jpg/png）
      * @param shape 1:正方形(圆形) 2：长方形
      */
    formatImgUrl: function (value, width, height, type, quality, folder, shape) {
        type = type || 0;
        quality = quality || 80;
        folder = folder || "jpg";
        shape = shape || 1;

        if (value == undefined || value == '') {
            switch (shape) {
                case 1:
                    return SiteDomain_Res + "img/noimage1.png";
                case 2:
                    return SiteDomain_Res + "img/noimage2.png";
                case 3:
                    return SiteDomain_Res + "img/noimage3.png";
                case 4:
                    return SiteDomain_Res + "img/noimage4.png";
            }
        }

        var _imgUrl = "";
        var arr = value.toString().split("/");
        var folders = arr[arr.length - 1].split(".");
        var _folder = folder || folders[folders.length - 1];
        if (folder == '_raw') {
            _folder = folders[folders.length - 1];
        }

        folders.pop();
        arr[arr.length - 1] = folders.join("");

        for (var i = arr.length - 1; i >= 0; i--) {
            if (i >= arr.length - 4) {
                _imgUrl = arr[i] + _imgUrl;
            }
            else {
                _imgUrl = arr[i] + "/" + _imgUrl;
            }
        }

        _imgUrl = File.DynamicImagePath + _imgUrl + "_" + width + "x" + height + "_" + type + "_0_" + quality + "." + _folder;
        return _imgUrl;

    },
}