/*
    我的工作列表
    creater : wms
    desc    : 树选择工作报告
*/
;
(function ($) {
    var yearday;
    var date = new Date();
    var year = date.getFullYear();
    var vm = null;
    var defaultoptions = {
        type: 1,
        islog: false,
        vuecallback: null,
        el: '#app',
        showchild: true,
        showlog: false
    };
    //合并之后的参数配置
    var resoptions = {};

    var page = {
        init: function (options) {
            //合并参数
            resoptions = $.extend({}, defaultoptions, options);
            page.initYears(function () {
                page.initEvent();
                page.initMonthTrigger();
                page.initVue();
                if (resoptions.type == 1 || resoptions.type == 2) { //日|周
                    page.initDayOrWeekTrigger();
                    page.focusWeek();
                } else {
                    page.focusMonth();
                }
                if (resoptions.islog) {
                    $('#chkworktype .worktype').click(function () {
                        vm.searchEntity.PageIndex = 0;
                        vm.searchEntity.PageSize = 0;
                        var _this = $(this);
                        var worktype = _this.attr('data-worktype');
                        if (!_this.hasClass('active')) {
                            resoptions.type = worktype;
                            vm.searchEntity.WorkType = worktype;
                            $('#chkworktype .worktype').removeClass('active');
                            _this.addClass('active');
                            if (worktype == 3) {
                                resoptions.showchild = false;
                                page.initMonthTrigger();
                                page.unbindWeek();
                                page.focusMonth();
                                //vm.myworkname = '本月';
                                //vm.mynextworkname = '下月';
                            } else {
                                page.initDayOrWeekTrigger();
                                resoptions.showchild = true;
                                page.focusWeek();
                                //if (worktype == 2) {
                                //    vm.myworkname = '本周';
                                //    vm.mynextworkname = '下周';
                                //} else {
                                //    vm.myworkname = '今日';
                                //    vm.mynextworkname = '明日';
                                //}
                            }
                        }
                    });
                } else {
                    $('#hiswork .worktype').click(function () {
                        vm.searchEntity.PageIndex = 0;
                        vm.searchEntity.PageSize = 0;
                        var _this = $(this);
                        if (!_this.hasClass('newactive')) {
                            $('#hiswork .worktype').removeClass('newactive');
                            _this.addClass('newactive');
                            var type = _this.attr('data-searchtype');
                            if (type == 1) {
                                resoptions.showlog = true;
                            } else {
                                resoptions.showlog = false;
                            }
                            var start;
                            var end;
                            var curyear = $('.select_year select').val();
                            if (resoptions.type == 3) {
                                resoptions.showchild = false;
                                page.unbindWeek();
                                var chosemonth = $('.timetree-month .timetree-head.month .j_timetree_spread:not(.time-text) em');
                                $.each(chosemonth, function (i, e) {
                                    var st = $(this).attr('style');
                                    if (st && st.length > 0) {
                                        var pmonth = $(this).parent().parent();
                                        start = curyear + '-' + pmonth.attr('fisrsttime');
                                        end = curyear + '-' + pmonth.attr('lasttime');
                                        return false;
                                    }
                                });
                                if (start != undefined && end != undefined) {
                                    page.loadData(start, end);
                                }
                            } else {
                                resoptions.showchild = true;
                                var choseweek = $('.timetree-month .timetree-weeklist .j_week.newactive');
                                if (choseweek.length > 0) {
                                    start = curyear + '-' + choseweek.attr('fisrsttime');
                                    end = curyear + '-' + choseweek.attr('lasttime');
                                    page.loadData(start, end);
                                }
                            }

                        }
                    });
                }
            });
        },
        //解绑事件
        unbindWeek: function () {
            $('.j_week .router,.j_week .time-circle').unbind('click');
            $('.timetree-month .timetree-weeklist').hide();
        },
        initEvent: function () {
            $("#mCSB_5").hover(function () {
                $("#mCSB_5").perfectScrollbar({ suppressScrollX: true });
            }, function () {
                $("#mCSB_5").perfectScrollbar("destroy");
                });


            page.resizeHeight();
        },
        //天或者周绑定事件
        initDayOrWeekTrigger: function () {
            $('.j_week .router,.j_week .time-circle').on('click', function () {
                $(".j_week .time-circle").css("background", "");
                $(".j_week .time-circle").css("box-shadow", "");
                $(".j_week .time-circle").parent().find("span").css("color", "");
                $(this).parent().find(".time-circle").css("background", "#C29654");
                $(this).parent().find(".time-circle").css("box-shadow", "0 0 0 1px #C29654");
                $(this).parent().find("span").css("color", "#C29654");
                $(".j_week").removeClass("newactive");
                $(this).parent().addClass("newactive");
                var begintime = $(this).parent().attr("fisrsttime");
                var endtime = $(this).parent().attr("lasttime");
                var year = $("#mCSB_5").attr("year");
                page.loadData(year + "-" + begintime, year + "-" + endtime);
            });
        },
        //月份绑定事件
        initMonthTrigger: function () {
            $('.j_timetree_spread').on('click', function () {
                $(".time-circle").css("background", "");
                $(this).parent().find(".time-circle").css("box-shadow", "");
                $(".time-circle").css("width", "");
                $(".time-circle").css("border", "");
                $(".time-circle").css("box-shadow", "");
                if (resoptions.showchild) {
                    var isnon = $(this).parent().parent().find(".timetree-weeklist").css("display");
                    if (isnon == "none") {
                        $(".timetree-weeklist").hide(200);
                        $(this).parent().parent().find(".timetree-weeklist").show(200);
                    }
                    else {
                        $(".timetree-weeklist").hide();
                        $(this).parent().parent().find(".timetree-weeklist").show();
                    }
                }
                $(".j_week").removeClass("active");
                var begintime = $(this).parent().attr("fisrsttime");
                var endtime = $(this).parent().attr("lasttime");
                var year = $("#mCSB_5").attr("year");
                if (resoptions.type == 3) {
                    page.loadData(year + "-" + begintime, year + "-" + endtime);
                }
            });
        },
        //初始化年份下拉
        initYears: function (func) {
            var newdate = new Date("2016-01-01");
            var firstyear = newdate.getFullYear();
            var html = "";
            for (var i = firstyear; i <= year; i++) {
                html += "<option>" + i + "</option>";
            }
            $(".select_year select").append(html);
            $(".select_year option").each(function () {
                var yeartext = $(this).html();
                if (year == yeartext) {
                    $(this).attr("selected", "true");
                }
            });
            $('.select_year select').niceSelect();

            //下拉切换
            $('.select_year select').on('change', function () {
                var choseyear = $(this).val();
                var weeklist = page.getWeeks(choseyear);
                $('#mCSB_5').attr('year', choseyear);

                $(".timetree-weeklist").html("");
                $.each(weeklist, function (name, value) {
                    var html = "";
                    html += '<li class="j_week" fisrsttime=' + value[4] + ' lasttime=' + value[3] + ' typeid="2">';
                    html += '	<a class="router"><span>' + value[0] + '</span></a>';
                    html += '	<em class="time-circle"></em>';
                    html += '</li>';
                    $("#" + value[2] + "").append(html);
                });
                var date = new Date();
                var yearxin = date.getFullYear();
                page.initMonthTrigger();
                if (resoptions.type == 3) {
                    if (yearxin == choseyear) {
                        page.focusMonth();
                    }
                    else {
                        page.focusMonth(1);
                    }
                } else {
                    page.initDayOrWeekTrigger();
                    if (yearxin == choseyear) {
                        var w = theWeek();
                        page.getCurrentWeek(w);
                        page.focusWeek();
                    }
                    else {
                        page.getCurrentWeek(1);
                        page.focusWeek(1);
                    }
                }
            });

            func();
        },
        //初始化当前周
        initWeek: function () {
            var weeklist = page.getWeeks(year);
            $.each(weeklist, function (name, value) {
                var html = "";
                html += '<li class="j_week" fisrsttime=' + value[4] + ' lasttime=' + value[3] + ' typeid="2">';
                html += '	<a class="router"><span>' + value[0] + '</span><em class="arrow-s"></em><em class="arrow-b"></em></a>';
                html += '	<em class="time-circle"></em>';
                html += '</li>';
                $("#" + value[2] + "").append(html);
            });
            var month = date.getMonth() + 1;
            page.getCurrentWeek(month);
            $('#mCSB_5').perfectScrollbar({ suppressScrollX: true });
        },
        getWeeks: function (year) {
            $("#mCSB_5").attr("year", year);
            var aar = new Array();
            var firstday = year + "-01-01";
            var getfirstday = getWeekNumber(firstday);

            firstday = new Date(Date.parse(firstday.replace(/-/g, "/")));
            var zhouji = getfirstday.split("-");
            var zhanjitian = 7 - zhouji[1] + 1;
            var fisrtweekday = adddate(firstday, 1 - zhouji[1]);
            fisrtweekday = getriyue(fisrtweekday);
            var fisrtweeklastday = adddate(firstday, zhanjitian - 1);
            fisrtweeklastday = getriyue(fisrtweeklastday);
            if (year % 4 == 0) {
                yearday = 366;
            }
            else {
                yearday = 365;
            }
            var qcfirstday = yearday - zhanjitian;
            var haveweekshu = Math.floor(qcfirstday / 7) + 1;
            for (var i = 0; i < haveweekshu; i++) {
                if (i == 0) {
                    var m = parseInt(i) + 1;
                    aar[i] = ["第" + m + "周", "" + fisrtweekday + "~" + fisrtweeklastday, "1", fisrtweeklastday, fisrtweekday];
                }
                else {
                    var zhongzhuanday = year + '-' + aar[i - 1][3];
                    zhongzhuanday = new Date(Date.parse(zhongzhuanday.replace(/-/g, "/")));
                    var zhoufisrstday = getriyue(adddate(zhongzhuanday, 1));
                    var iswhatmotn = adddate(zhongzhuanday, 7).getMonth() + 1;

                    var m = parseInt(i) + 1;
                    var zhoulastday = getriyue(adddate(zhongzhuanday, 7));
                    aar[i] = ["第" + m + "周", "" + zhoufisrstday + "~" + zhoulastday, iswhatmotn, zhoulastday, zhoufisrstday];
                }
            }
            return aar;
        },
        getCurrentWeek: function (weekcontent) {
            $(".timetree-month").each(function () {
                var weekcontentname = $(this).find(".j_timetree_spread strong").html();
                if (weekcontentname == weekcontent) {
                    var isnon = $(this).find(".timetree-weeklist").css("display");
                    if (isnon == "none") {
                        $(".timetree-weeklist").hide(200);
                        $(this).find(".timetree-weeklist").show(200);
                    } else {
                        $(".timetree-weeklist").hide();
                        $(this).find(".timetree-weeklist").show();
                    }
                    $(this).find(".j_timetree_spread").trigger("click");
                    var height = $(this).find(".j_timetree_spread:eq(1)").offset().top - 100;
                    $('#mCSB_5_container').parent().scrollTop(height);
                }
            });
        },
        resizeHeight: function () {
            var treeheight = document.documentElement.clientHeight;
            treeheight = treeheight - 100;
            $(".js_reportleft_scroll").css("height", treeheight + "px");
        },
        initVue: function () {
            vm = new Vue({
                el: resoptions.el,
                data: {
                    DataSources: [],
                    searchEntity: {},
                },
                methods: {
                    getList: function () {
                        var url;
                        if (resoptions.islog || resoptions.showlog) {
                            this.flag = false;
                            url = '/api/WorkLogApi/List';
                        } else {
                            this.flag = true;
                            url = '/OA/Work/List';
                        }
                        var _this = this;
                        axios.get(url, { params: this.searchEntity }).then(function(response) {
                            _this.DataSources = response.data.OaWork;
                            _this.searchEntity = response.data.OaWorkSearch;
                        });
                    },
                }
            });
        },
        focusMonth: function (curmonth) {
            if (curmonth == undefined) {
                curmonth = theMonth();
            }
            $('.timetree-month .timetree-head .j_timetree_spread.time-text strong').each(function () {
                if ($(this).text() == curmonth) {
                    //父级月点击
                    $(this).parent().parent().find('.j_timetree_spread').click();
                }
            });
        },
        //初始加载定位当前位置
        focusWeek: function (curweek) {
            if (curweek == undefined) {
                curweek = theWeek();
            }
            var str = '第' + curweek + '周';
            $('.timetree-month .timetree-weeklist .j_week .router span').each(function () {
                if ($(this).text() == str) {
                    //父级月点击
                    $(this).parent().parent().parent().parent().find('div .j_timetree_spread').click();
                    //父级a标签点击
                    $(this).parent().click();
                    var month = $(this).parent().parent().parent().attr('id');
                    if (month > 6) {
                        var num = $(this).parent().parent().parent().find('li').length;
                        var li_height = $(this).parent().parent().height();
                        var height = $('#mCSB_5_container').parent().outerHeight() / 2 + num * li_height;
                        $('#mCSB_5_container').parent().scrollTop(height);
                    }
                    return false;
                }
            });
        },
        //加载数据
        loadData: function (startdate, enddate) {
            vm.searchEntity.WorkType = resoptions.type;
            vm.searchEntity.StartDate = startdate;
            vm.searchEntity.EndDate = enddate;
            vm.getList();
        }
    };
    function getWeekNumber(d) {
        var arr = d.split("-");
        var newd = new Date(arr[0], arr[1] - 1, arr[2], " ", " ", " ");
        var getDay = newd.getDay();
        switch (getDay) {
            case 1:
                getDay = "周一-1";
                break;
            case 2:
                getDay = "周二-2";
                break;
            case 3:
                getDay = "周三-3";
                break;
            case 4:
                getDay = "周四-4";
                break;
            case 5:
                getDay = "周五-5";
                break;
            case 6:
                getDay = "周六-6";
                break;
            case 0:
                getDay = "周日-7";
                break;
        }
        return getDay;
    }
    function adddate(date, n) {
        var time = date.getTime();
        var newTime = time + n * 24 * 60 * 60 * 1000;
        return new Date(newTime);
    };
    function getriyue(d) {
        var month = d.getMonth() + 1;
        var day = d.getDate();
        if (month < 10) {
            month = "0" + month;
        }
        if (day < 10) {
            day = "0" + day;
        }
        return month + '-' + day;
    }
    function theMonth() {
        var now = new Date();
        return now.getMonth() + 1;
    }
    function theWeek() {
        var now = new Date();
        var totalDays = 0;
        var years = now.getYear();
        if (years < 1000)
            years += 1900;
        var days = new Array(12);
        days[0] = 31;
        days[2] = 31;
        days[3] = 30;
        days[4] = 31;
        days[5] = 30;
        days[6] = 31;
        days[7] = 31;
        days[8] = 30;
        days[9] = 31;
        days[10] = 30;
        days[11] = 31;

        //判断是否为闰年，针对2月的天数进行计算
        if (Math.round(now.getYear() / 4) == now.getYear() / 4) {
            days[1] = 29;
        } else {
            days[1] = 28;
        }

        if (now.getMonth() == 0) {
            totalDays = totalDays + now.getDate();
        } else {
            var curMonth = now.getMonth();
            for (var count = 1; count <= curMonth; count++) {
                totalDays = totalDays + days[count - 1];
            }
            totalDays = totalDays + now.getDate();
        }
        var firstday = years + "-01-01";
        var getfirstday = getWeekNumber(firstday);
        var zhouji = getfirstday.split("-");
        var zhanjitian = 7 - zhouji[1] + 1;
        totalDays = totalDays - zhanjitian;
        var week;
        //得到第几周
        if (totalDays % 7 == 0) {
            week = Math.ceil(totalDays / 7) + 1;
        }
        else {
            week = Math.ceil(totalDays / 7) + 1;
        }
        return week;
    }

    $.oawork = page;
})(jQuery);
