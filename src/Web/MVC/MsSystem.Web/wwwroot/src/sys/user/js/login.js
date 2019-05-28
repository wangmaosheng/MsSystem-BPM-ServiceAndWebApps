define(function (require, exports, module) {
    require("jsencrypt");
    var isinprogress = false;
    var page = {
        init: function() {
            if (window.top !== window.self) { window.top.location = window.location };
            $('input[name=username]').focus();
        },
        initEvent: function() {


            $('#signin').on('click', function() {
                page.login();
            });
        },
        login: function () {
            if (isinprogress) {
                return;
            }
            var username = $('input[name=username]').val();
            if (!username) {
                $('input[name=username]').focus();
                return;
            }
            var password = $('input[name=password]').val();
            if (!password) {
                $('input[name=password]').val();
                return;
            }

            var encrypt = new JSEncrypt();
            $.ajax({
                url:'/Sys/User/Login'

            });
        }
    };
    page.init();
});