define(function (require, exports, module) {
    require('jquery-extension');
    require("vue");
    require("vue-filters");
    require("axios");
    require("toastr");
    require("validate");
    require("pace");
    var utils = require('utils');

    var vm = new Vue({
        el: '#msapp',
        data: {
            "SysSystem": {},
            "Id": 0
        },
        created: function () {

        },
        mounted: function () {
            this.init();
        },
        methods: {
            init: function () {
                this.getFormData();
                utils.permission.initEvent();
                $('#formSave').on('click', function () {
                    vm.saveFormData();
                });
            },
            getFormData: function () {
                var vthis = this;
                vthis.Id = $.getUrlParam('id');
                axios.get('/Sys/System/Get?Id=' + vthis.Id).then(function (response) {
                    if (!utils.permission.isAuth(response)) {
                        return;
                    }
                    vthis.$set(vthis, 'SysSystem', response.data);
                }).catch(function (error) {
                    console.log(error);
                });
            },
            saveFormData: function () {
                var isvalidate = $('#form-system').valid();
                if (isvalidate === false) {
                    return;
                }
                var vthis = this;
                var url = parseInt(vthis.Id) > 0 ? '/Sys/System/Update' : '/Sys/System/Add';
                axios.post(url, vthis.SysSystem).then(function (response) {
                    utils.permission.authResponse(response);
                });
            }
        }
    });
});