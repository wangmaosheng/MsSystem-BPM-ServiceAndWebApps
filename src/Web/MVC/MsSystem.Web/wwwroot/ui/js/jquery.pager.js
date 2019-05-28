/*
* jQuery pager plugin
*
* Usage: refer example "pager.html"
*
* Parameter:
* dataCount         总记录数(必须的)
* currentPage       当前页(必须的，默认1)
* pageSize          每页记录数(必须的)
* currentPageName   URL当前也参数名
* pageSizeName      URL每页记录数参数名
* pageNumber        页码个数(必须的，默认5)
* pageName          文本显示定义(空则不现实)['', '上页', '下页', ''],
* oneWrite          一页是否输出控件(默认不输出)
* callBack          翻页回调事件(必须的)
*
* Result:
* <ul class="pages">
*   <li>首页</li><li>上页</li>
*   <li>2</li><li>3</li><li class="current">4</li><li>5</li><li>6</li>
*   <li>下页</li><li>尾页</li>
* </ul>
*/
(function ($) {
    $.fn.pager = function (options) {
        //默认参数设置
        var defaults = {
            dataCount: 95,
            currentPage: 1,
            pageSize: 10,
            currentPageName: 'p',
            pageSizeName: 'ps',
            pageNumber: 5,
            pageName: ['首页', '上页', '下页', '尾页'],
            oneWrite: false,
            callBack: null
        };
        var _this = this;
        //合并配置
        var options = $.extend(defaults, options);
        var methods = {
            pageCount: 1,
            //取进位
            getCeil: function (num, div) {
                return Math.ceil(num / div);
            },
            //渲染分页控件
            renderPager: function (options) {
                //div属性取值
                if (_this.attr('data-pageindex') && _this.attr('data-pagesize') && _this.attr('data-datacount')) {
                    options.currentPage = parseInt(_this.attr('data-pageindex'));
                    options.pageSize = parseInt(_this.attr('data-pagesize'));
                    options.dataCount = parseInt(_this.attr('data-datacount'));
                }
                //总页数
                methods.pageCount = methods.getCeil(options.dataCount, options.pageSize);
                //只有1/0页不输出
                if (!options.oneWrite && (methods.pageCount == 1 || methods.pageCount == 0)) {
                    return;
                };
                //间隔页码
                var spaceNumber = methods.getCeil(options.pageNumber, 2);
                //起始页
                var startCount = (options.currentPage + spaceNumber) > methods.pageCount ?
                    (methods.pageCount - options.pageNumber) :
                    options.currentPage - (options.pageNumber - spaceNumber);
                startCount = startCount >= 1 ? startCount : 1;
                //结尾页
                var endCount = options.currentPage < spaceNumber ? spaceNumber * 2 :
                    options.currentPage + (options.pageNumber - spaceNumber);
                endCount = methods.pageCount > endCount ? endCount : methods.pageCount;
                //计算起始页码
                startCount = ((endCount - startCount) == options.pageNumber &&
                    options.currentPage > (endCount - spaceNumber)) ? startCount + 1 : startCount;
                //计算结束页码
                endCount = ((endCount - startCount) == options.pageNumber &&
                    options.currentPage < endCount) ? endCount - 1 : endCount;
                //标记
                var $pager = $('<div class="pager"></div>');
                //首页
                if (options.pageName[0] != '') {
                    $pager.append(options.currentPage > 1 ?
                        methods.renderAction(options.pageName[0], 1, options.pageSize, options.callBack) :
                        methods.renderDisabledAction(options.pageName[0]));
                }
                //上页
                if (options.pageName[1] != '') {
                    $pager.append(options.currentPage > 1 ?
                        methods.renderAction(options.pageName[1], options.currentPage - 1, options.pageSize, options.callBack) :
                        methods.renderDisabledAction(options.pageName[1]));
                }
                //页码
                for (var i = startCount; i <= endCount; i++) {
                    $pager.append(options.currentPage == i ?
                        methods.renderAction(i, i, options.pageSize, options.callBack, true) :
                        methods.renderAction(i, i, options.pageSize, options.callBack));
                }
                //下页
                if (options.pageName[2] != '') {
                    $pager.append(options.currentPage < endCount ?
                        methods.renderAction(options.pageName[2], options.currentPage + 1, options.pageSize, options.callBack)
                        : methods.renderDisabledAction(options.pageName[2]));
                }
                //尾页
                if (options.pageName[3] != '') {
                    $pager.append(options.currentPage < methods.pageCount ?
                        methods.renderAction(options.pageName[3], methods.pageCount, options.pageSize, options.callBack)
                        : methods.renderDisabledAction(options.pageName[3]));
                }
                $pager.append('<span>' + options.currentPage + '/' + methods.pageCount + '页</span>');
                $pager.append('<span>共' + options.dataCount + '条</span>');
                return $pager;
            },
            //Request.QueryString
            getQueryString: function (options) {
                var query = document.location.search.substr(1);
                if (query == "") return "";
                var querys = query.split('&');
                var querystring = "";
                for (var i = 0; i < querys.length; i++) {
                    var temp = querys[i].split('=');
                    if (temp.length < 2) {
                        continue;
                    }
                    if (temp[0] == options.currentPageName || temp[0] == options.pageSizeName) {
                        continue;
                    }
                    querystring += temp[0] + '=' + temp[1] + "&";
                }
                return querystring;
            },
            renderAction: function (txt, currentPage, pageSize, callBack, isCurrent) {
                var $action = $('<a ' + (isCurrent ? 'class="current"' : '') + ' href="javascript:void(0);">' + txt + '</a>');
                if (!isCurrent)
                    $action.click(function () {
                        var querystring = methods.getQueryString(options);
                        document.location.href = "?" + querystring + options.currentPageName + "=" + currentPage + "&" + options.pageSizeName + "=" + pageSize;
                        if (callBack)
                            callBack(currentPage);
                    });
                return $action;
            },
            renderDisabledAction: function (txt) {
                return '<a class="disabled" href="javascript:void(0);">' + txt + '</a>';
            }
        };
        this.each(function () {
            $(this).empty().append(methods.renderPager(options));
        });
    };
})(jQuery);