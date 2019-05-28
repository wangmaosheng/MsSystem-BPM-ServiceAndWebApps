define(function (require, exports, module) {
    require("jquery-extension");
    require('pace');
    require("axios");
    var layer = require("layer");
    var page = {
        data: {
            release: {
                pageIndex: 1,
                pageSize: 6,
                totalPages: 0
            },
            flag: true
        },
        init: function () {
            page.loadRelease();
            page.initEvent();
        },
        initEvent: function () {
            $('#releaselog').next('button').click(function () {
                if (page.data.release.totalPages > 1) {
                    page.data.release.pageIndex++;
                    page.loadRelease();
                }
            });
        },
        loadRelease: function () {
            //防止一直加载数据
            if (page.data.flag) {
                var index = layer.load(3, {
                    shade: [0.5, '#fff']
                });
                axios.get('/home/releaselog?pageindex=' + page.data.release.pageIndex + '&pagesize=' + page.data.release.pageSize)
                    .then(function (response) {
                        var data = response.data.Items;
                        for (var i = 0; i < data.length; i++) {
                            var item = data[i];
                            var time = $.unixToDate(item.CreateTime);
                            var ts = $.toTimeSpan(time);
                            var html =
                                '<div class="feed-element"><a href= "" class="pull-left" ><img alt="image" class="img-circle" src="/ui/img/profile_small.jpg"></a>' +
                                '<div class="media-body "><small class="pull-right">' + ts + '前发布</small><strong>wms</strong><br>' +
                                '<small class="text-muted">版本号：' + item.VersionNumber + '</small><div class="well">' + item.Memo + '</div>' +
                                '</div></div>';
                            $('#releaselog').append(html);
                        }
                        page.data.release.totalPages = response.data.TotalPages;
                        if (data.length < page.data.release.pageSize) {
                            $('#releaselog').next('button').remove();
                            $('#releaselog').parent().append('<div class="text-center red">~~木有了:-D</div>');
                            page.data.flag = false;
                        } else {
                            $('#releaselog').next('button').show();
                        }
                        layer.close(index);
                    });
            }
        }
    };
    page.init();
});