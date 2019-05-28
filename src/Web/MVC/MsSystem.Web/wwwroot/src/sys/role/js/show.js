define(function (require, exports, module) {
    require("vue");
    require("vue-filters");
    require("axios");
    require("validate");
    require("toastr");
    require("pace");
    var utils = require('utils');
    var vm = new Vue({
        el: '#msapp',
        data: {
            "Id": 0,
            "SysRole": {},
            'SystemId': 0
        },
        mounted: function () {
            this.init();
        },
        methods: {
            init: function () {
                this.SystemId = $.getUrlParam('systemid');
                this.getFormData();
                this.initEvent();
            },
            initEvent: function () {
                utils.permission.initEvent();
                $('#form-role').validate();
                $('#formSave').on('click', function () {
                    vm.saveFormData();
                });
            },
            getFormData: function () {
                var vthis = this;
                vthis.Id = $.getUrlParam('id');
                axios.get('/Sys/Role/Get?Id=' + vthis.Id).then(function (response) {
                    if (!utils.permission.isAuth(response)) {
                        return;
                    }
                    vthis.$set(vthis, 'SysRole', response.data == '' ? {} : response.data);
                }).catch(function (error) {
                    console.log(error);
                });
            },
            saveFormData: function () {
                var isvalidate = $('#form-role').valid();
                if (isvalidate === false) {
                    return;
                }
                var vthis = this;
                var url;
                if (parseInt(vthis.Id) > 0) {
                    url = '/Sys/Role/Update';
                } else {
                    vthis.SysRole.SystemId = vthis.SystemId;
                    url = '/Sys/Role/Add';
                }
                axios.post(url, vthis.SysRole).then(function (response) {
                    utils.permission.authResponse(response);
                });
            }
        }
    });
});