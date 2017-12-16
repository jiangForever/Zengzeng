;function clip(options) {
    this.size = 200; // 裁切宽高
    this.uploadSuccessCallback = options.uploadSuccessCallback;//此对象为上传成功之后调用的方法
    this.uploadDocument = options.uploadDocument;//上传按钮绑定的jquery Document
    this.init();
    this.posX = 0;
    this.posY = 0;
    this.lastPosX = 0;
    this.lastPosY = 0;
    this.scale = 1;
    this.last_scale = 1;
    this.min = 1;
    this.r_w = 1 / 2; //视口中心点到左端与总宽度比率
    this.r_h = 1 / 2;


}
function getUrlParam(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
}
var _type = getUrlParam('_type');
var _html = '<div class="appCre"><div class="mask"style="display: block"></div><div class="appCre-pop"><div class="appCre-chose"><ul><li class="Cre">拍照</li><li class="Album">相册</li></ul></div><button class="appCre-exit">取消</button></div></div>';
clip.prototype = {
    init: function () {
        var _this = this;
        var $file = _this.uploadDocument;
        if (false) {
            $('body').on('click', '.appCre .Cre', function () {
                $('.appCre').remove();
                var params = { APPUpLoad: "opendCamera", type: "1" }
                mallcoo.photo.opendCamera(callbackok, callbackerr, params);
                zloading = _this.dialog('', { buttons: false, show_close_button: false, modal: false, custom_class: 'ZebraDialog_loading' });
            });
            $('body').on('click', '.appCre .Album', function () {
                $('.appCre').remove();
                var params = { APPUpLoad: "openAlbum", type: "1" }
                mallcoo.photo.openAlbum(callbackok, callbackerr, params);
                zloading = _this.dialog('', { buttons: false, show_close_button: false, modal: false, custom_class: 'ZebraDialog_loading' });
            });
            $('body').on('click', '.appCre .appCre-exit', function () {
                $('.appCre').remove();
            });
            function callbackok(dataURL) {
                console.log("dataURL = " + dataURL);
                if (!dataURL) { zloading.close(); return }
                var b64 = dataURL.substring(dataURL.indexOf(",") + 1);
                File.UploadBase64("11.jpg", File.Img_folder, b64, {
                    successCallback: function (res) {
                        console.log("res = " + JSON.stringify(res));
                        _this.uploadSuccessCallback(res);
                    },
                    errorCallback: function (res) {
                        console.log("errorCallback = " + res.err);
                        $mcDialog(res.err);
                    }
                })
                zloading.close();
            }
            function callbackerr(error) {
                //alert("调用摄像头或相册出错：" + error);
                zloading.close();
            }

            $file.click(function () {
                $('body').append(_html);
                return false;
            });
        } else {
            _this.uploadDocument.makeThumb({
                width: 1000,
                height: 1000,
                //mark: {padding: 5, src: 'mark.png', width: 30, height: 30},
                success: function (dataURL, tSize, file, sSize, fEvt) {
                    var thumb = new Image();
                    thumb.src = dataURL;
                    // 可以得到图片名, 高度等信息, 用来做一些判断, 比如图片大小是否符合要求等..
                    var zloading = _this.dialog('', { buttons: false, show_close_button: false, modal: false, custom_class: 'ZebraDialog_loading' });
                    $('#uploadFrame').show();
                    var img = new Image(), scale;
                    img.src = dataURL;
                    img.alt = file.name;
                    img.id = 'clip_img';

                    img.addEventListener('load', setupCanvases, false);
                    function setupCanvases() {
                        zloading.close();
                        var w1 = _this.w1 = parseInt(img.width),
                            h1 = _this.h1 = parseInt(img.height),
                            w = _this.w = parseInt($('#frame').width()),
                            h = _this.h = parseInt($('#frame').height());
                        document.getElementById('preview').appendChild(img);
                        // 以高度为放缩基点
                        if (w1 / h1 < w / h) {
                            scale = _this.scale = h / h1;
                            _this.posX = _this.lastPosX = (w - w1 * scale) / 2;
                            _this.min = 200 / h1;
                        } else {
                            scale = _this.scale = w / w1;
                            _this.posY = _this.lastPosY = (h - h1 * scale) / 2;
                            _this.min = 200 / w1;
                        }
                        _this.show('transformend');
                        _this.multitouch();
                        _this.uploadDocument.val('');
                    }
                    var title = file.name;
                }
            });
        }
        //保存
        $('#clip').bind('click', function () {
            _this.draw();
            _this.reset();
        })
        //取消
        $('#cancel').click(function () {
            _this.reset();
        })
        function bind(object, fun, args) {
            var args = Array.prototype.slice.call(arguments).slice(2);
            return function () {
                return fun.call(object, this);
            }
        }
    },
    loadImage: function (url, callback) {
        var img = new Image(); //创建一个Image对象，实现图片的预下载
        img.onload = function () {
            img.onload = null;
            callback(img);
        }
        img.src = url;
    },
    openAlbum2: function (dataURL) {
        $('#uploadFrame').show();
        var _this = this;
        this.zloading = this.dialog('', { buttons: false, show_close_button: false, modal: false, custom_class: 'ZebraDialog_loading' });
        this.loadImage(dataURL, this.setupCanvases.bind(this));
    },
    setupCanvases: function (img) {
        var scale;
        var w1 = this.w1 = parseInt(img.width),
            h1 = this.h1 = parseInt(img.height),
            w = this.w = parseInt($('#frame').width()),
            h = this.h = parseInt($('#frame').height());
        document.getElementById('preview').appendChild(img);
        img.id = 'clip_img';
        this.zloading.close();
        // 以高度为放缩基点
        if (w1 / h1 < w / h) {
            scale = this.scale = h / h1;
            this.posX = this.lastPosX = (w - w1 * scale) / 2;
            this.min = 200 / h1;
        } else {
            scale = this.scale = w / w1;
            this.posY = this.lastPosY = (h - h1 * scale) / 2;
            this.min = 200 / w1;
        }
        this.show('transformend');
        this.multitouch();
        this.uploadDocument.val('');
    },
    fileSelect2: function (ele) {
        // 提交 loading
        var zloading = this.dialog('', { buttons: false, show_close_button: false, modal: false, custom_class: 'ZebraDialog_loading' });
        var _this = this, f = ele.files[0];
        var reader = new FileReader();
        reader.onload = (function (file) {
            return function () {
                $('#uploadFrame').show();
                var img = document.createElement('img'), scale;
                img.src = this.result;
                img.alt = file.name;
                img.id = 'clip_img';
                img.addEventListener('load', setupCanvases, false);
                function setupCanvases() {
                    zloading.close();
                    var w1 = _this.w1 = parseInt(img.width),
                        h1 = _this.h1 = parseInt(img.height),
                        w = _this.w = parseInt($('#frame').width()),
                        h = _this.h = parseInt($('#frame').height());
                    document.getElementById('preview').appendChild(img);
                    // 以高度为放缩基点
                    if (w1 / h1 < w / h) {
                        scale = _this.scale = h / h1;
                        _this.posX = _this.lastPosX = (w - w1 * scale) / 2;
                        _this.min = 200 / h1;
                    } else {
                        scale = _this.scale = w / w1;
                        _this.posY = _this.lastPosY = (h - h1 * scale) / 2;
                        _this.min = 200 / w1;
                    }
                    _this.show('transformend');
                    _this.multitouch();
                    _this.uploadDocument.val('');
                }
            };
        })(f);
        //读取文件内容
        reader.readAsDataURL(f);
    },
    draw: function () {
        var _this = this;
        var img = document.querySelector('#clip_img'),
            size = this.size,
            scale = this.scale;
        if (!img) { return false }
        var w = this.w, h = this.h, w1 = this.w1, h1 = this.h1,
            pos = this.getViewPortPos(),
            x1 = pos.x,
            y1 = pos.y,
            Zsize = size / scale;
        var canvas;
        if (!canvas) {
            canvas = document.createElement('canvas');
            canvas.width = size;
            canvas.height = size;
        }
        ctx = canvas.getContext('2d');
        ctx.clearRect(0, 0, size, size);
        ctx.drawImage(img, x1, y1, Zsize, Zsize, 0, 0, size, size);
        var imgData = canvas.toDataURL();

        document.querySelector('#result').src = imgData
        var b64 = imgData.substring(imgData.indexOf(",") + 1);
        File.UploadBase64("11.jpg", File.Img_folder, b64, {
            successCallback: function (res) {
                _this.uploadSuccessCallback(res);
            },
            errorCallback: function (res) {
                $mcDialog(res.err);
            }
        })
    },
    multitouch: function () {
        var _this = this;

        var hammertime = Hammer(document.getElementById('frame'), {
            transform_always_block: true,
            drag_block_horizontal: true,
            drag_block_vertical: true,
            drag_min_distance: 0
        });
        hammertime.on('touch drag dragend transform transformend', function (ev) {
            manageMultitouch(ev);
        });

        function manageMultitouch(ev) {
            switch (ev.type) {
                case 'drag':
                    _this.posX = ev.gesture.deltaX + _this.lastPosX;
                    _this.posY = ev.gesture.deltaY + _this.lastPosY;
                    break;

                case 'transform':
                    _this.scale = Math.max(_this.min, Math.min(_this.last_scale * ev.gesture.scale, 100));
                    //以容器为中心点,放大后左半边的位移等于(中心点坐标-放大后左边大小)
                    var getPos = _this.getPosTransform();
                    _this.posX = getPos.x;
                    _this.posY = getPos.y;
                    break;
            }
            _this.show(ev.type);
        }
    },
    show: function (type) {
        var matrix = 'matrix(' + this.scale + ',0,0,' + this.scale + ',' + this.posX + ',' + this.posY + ')';
        document.querySelector('#preview').style.webkitTransform = matrix;
        if (type == "dragend" || type == "transformend") {
            this.lastPosX = this.posX;
            this.lastPosY = this.posY;
            this.last_scale = this.scale;

            // r_w,r_h 以视口中心点为基点,计算左侧占比
            var r = this.getPosRatio(this.scale);
            this.r_w = r.r_w;
            this.r_h = r.r_h;
        }
    },
    // 裁切框左上角x,y
    getViewPortPos: function () {
        // 视口中心点的左侧占比*总长度 等于 视口中心x,y
        return {
            x: this.w1 * this.r_w - this.size / 2 / this.scale,
            y: this.h1 * this.r_h - this.size / 2 / this.scale
        }
    },
    // img位移x,y
    getPosTransform: function () {
        var w1 = this.w1,
            h1 = this.h1,
            w = this.w,
            h = this.h,
            scale = this.scale;
        return {
            x: (w / 2 - w1 * this.r_w * scale),
            y: (h / 2 - h1 * this.r_h * scale)
        }
    },
    // 获取位移比率,以容器中心点为基点,放大后左半边的占比(中心点+左位移 除以 总大小)
    getPosRatio: function () {
        var w1 = this.w1,
            h1 = this.h1,
            w = this.w,
            h = this.h,
            scale = this.scale;
        return {
            r_w: (w / 2 - this.posX) / (w1 * scale),
            r_h: (h / 2 - this.posY) / (h1 * scale)
        }
    },
    // 重置
    reset: function () {
        this.posX = 0;
        this.posY = 0;
        this.lastPosX = 0;
        this.lastPosY = 0;
        $('#uploadFrame').hide();
        $('#preview').empty();
        $('#preview').css({ '-webkit-transform': 'none' });
    },
    // 消息提示
    dialog: function (str, json) {
        return new $.Zebra_Dialog(str, json);
    }
}



