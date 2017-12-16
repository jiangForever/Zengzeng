var goods = {
    pcitures: [],//记得要清空
    init: function () {
        var _that = this;

        $('#testtable5').yhhDataTable({
            'tbodyRow': {
                'write': function (d) { /*表格生成每行数据的方法*/
                    var imgs = "";
                    if (d.Pictures) {
                        for (var i = 0; i < d.Pictures.length; i++) {
                            imgs += "<img src='http://localhost:3432/Image/" + d.Pictures[i].PictureUrl + "' style='width: 33px;'/>";
                        }
                    }
                    //console.log(d);
                    return " <tr><td>" +
                        d.ID
                        + "</td><td>" +
                        d.Price
                        + "</td><td>" +
                        d.Title
                        + "</td><td>" +
                       (d.SubTitle || "")
                        + "</td><td>" +
                       imgs
                        + "</td><td>" +
                       d.Sort
                        + "</td><td>" +
                       (d.Source || "")
                        + "</td><td>" +
                        "<input type='button' value='编辑' class='btn btn-primary remark' data-toggle='modal' data-target='#myModal'/>"
                        + "</td></tr>";//that.drawRow(d);
                }
            },
            'paginate': {
                'visibleGo': true, /*是否开启直接翻至某页功能*/
                'type': 'full', /*默认按钮样式递增（numbers只有数字按钮，updown增加上下页按钮，full增加首尾页按钮）*/
                'displayLen': 10,  /*每页显示条数*/
                'currentPage': 1 /*当前页码（初始页码）*/
            },
            'serverSide': true, /*是否从服务器获取数据*/
            /*ajax参数*/
            'ajaxParam': {
                'url': "/api/Goods/GetGoodsPage", /*url地址*/
                'type': 'post', /*ajax传输方式*/
                'dataType': 'json', /*ajax传送数据格式*/
                'data': {} /*传到服务器的数据*/
            },
            'sendDataHandle': function (d) {
                d.PageNumber = d.currentPage;
                delete d.currentPage;
                d.PageCount = d.displayDataLen;
                delete d.displayDataLen;
                d.ID = $("#goodsid").val();
                //console.log('检索结果入参：' + JSON.stringify(d));
                return d;
            },  /*传递到服务器的数据预处理方法*/
            'backDataHandle': function (d) {
                //console.log('检索结果出参：' + JSON.stringify(d));
                var r = { 'errFlag': false, 'errMsg': '', 'dataLen': 0, 'data': [], 'origData': null };
                if (d == null) {
                    r.errFlag = true, r.errMsg = _ERR_MSG;
                } else if (d.Code != '1') {
                    r.errFlag = true, r.errMsg = d.Message;
                } else {
                    r.errMsg = d.Message, r.dataLen = d.Data.TotalCount, r.data = d.Data.Data;
                    r.origData = d.Data.Data;
                }
                return r;
            },  /*预处理从服务器的接收数据或者js传入的数据*/
            'beforeShow': function () {/** loadingDialog.show();*/ },  /*显示之前的额外处理事件*/
            'afterShow': function (errFlag, errMsg, dataLen, listData) {
            }  /*显示之后的额外处理事件*/
        });

        $("#search").click(function () {
            $('#testtable5').yhhDataTable("refresh")
        });

        $('#testtable5').on('click', '.remark', function () {
            console.log(this);
        })

        $('#myModal').on('hide.bs.modal', function (_t) {
            //alert('嘿，我听说您喜欢模态框...');
        })

        $('#myModal').on('show.bs.modal', function (_t) {

        })

        $("#submit").click(function () {
            var data = {};
            data.Price = $("#price").val();
            data.Title = $("#title").val();
            data.SubTitle = $("#subtitle").val();
            data.Source = $("#source").val();
            data.Sort = $("#sort").val();
            data.Pictures = [];
            for (var i = 0; i < _that.pcitures.length; i++) {
                data.Pictures.push({ "PictureUrl": _that.pcitures[i] });
            }
            $.ajax({
                url: "/api/Goods/AddGoods",
                data: data,
                dataType: "json",
                type: "post",
                success: function (res) {
                    $('#testtable5').yhhDataTable("refresh");
                    $('#myModal').modal('hide')
                },
                error: function () {
                    alert("出错了！");
                    $('#myModal').modal('hide')
                }
            });
        });

        $("#btnUpload").click(function (_t) {
            var formData = new FormData();
            formData.append("data", $("#inputfile")[0].files[0]);
            formData.append("fileName", $("#inputfile")[0].files[0].name);
            formData.append("folder", "Goods");
            $.ajax({
                url: "http://localhost:3432/Service/UpLoadBase64",
                type: 'POST',
                data: formData,
                // 告诉jQuery不要去处理发送的数据
                processData: false,
                // 告诉jQuery不要去设置Content-Type请求头
                contentType: false,
                beforeSend: function () {
                    console.log("正在进行，请稍候");
                },
                success: function (responseStr) {
                    if (responseStr.IsSuccess) {
                        //console.log("成功" + responseStr);
                        _that.pcitures.push(responseStr.ImageUrl);
                        var imgHtml = "<img src='http://localhost:3432/Image/" + responseStr.ImageUrl + "' style='width: 100px;'/>";
                        $("#pictures").append(imgHtml);
                    } else {
                        alert("上传文件失败");
                    }
                },
                error: function (responseStr) {
                    alert("上传文件失败");
                }
            });

        });
    }
};

