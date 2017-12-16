$(document).ready(function () {

    Date.prototype.Format = function (fmt) { //author: meizz   
        var o = {
            "M+": this.getMonth() + 1,                 //月份   
            "d+": this.getDate(),                    //日   
            "h+": this.getHours(),                   //小时   
            "m+": this.getMinutes(),                 //分   
            "s+": this.getSeconds(),                 //秒   
            "q+": Math.floor((this.getMonth() + 3) / 3), //季度   
            "S": this.getMilliseconds()             //毫秒   
        };
        if (/(y+)/.test(fmt))
            fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
        for (var k in o)
            if (new RegExp("(" + k + ")").test(fmt))
                fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
        return fmt;
    }

    $('#testtable5').yhhDataTable({
        'tbodyRow': {
            'write': function (d) { /*表格生成每行数据的方法*/
                //console.log(d);
                return " <tr><td>" +
                    d.ID
                    + "</td><td>" +
                    d.NickName
                    + "</td><td>" +
                    d.Mobile
                    + "</td><td>" +
                    d.CardNo
                    + "</td><td>" +
                    d.Address
                    + "</td><td>" +
                   "<img src='http://localhost:3432/Image/" + d.Avator + "' style='width: 33px;'/>"
                    + "</td><td>" +
                   new Date(d.CreateDate).Format("yyyy-MM-dd hh:mm:ss")
                    + "</td><td>" +
                   (d.Remark || "")
                    + "</td><td>" +
                    "<input type='button' value='备注' class='btn btn-primary  remark' data-toggle='modal' data-target='#myModal'/>"
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
            'url': "/api/User/GetUserPage", /*url地址*/
            'type': 'post', /*ajax传输方式*/
            'dataType': 'json', /*ajax传送数据格式*/
            'data': {} /*传到服务器的数据*/
        },
        'sendDataHandle': function (d) {
            d.PageNumber = d.currentPage;
            delete d.currentPage;
            d.PageCount = d.displayDataLen;
            delete d.displayDataLen;
            d.ID = $("#userid").val();
            d.Mobile = $("#mobile").val();
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

});