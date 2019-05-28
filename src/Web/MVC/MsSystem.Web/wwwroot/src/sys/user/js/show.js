define(function (require, exports, module) {
    require('jquery-extension');
    require("vue");
    require("vue-filters");
    require("validate");
    require("axios");
    require("toastr");
    require("pace");
    var utils = require('utils');
    var vm = new Vue({
        el: '#msapp',
        data: {
            "User": {},
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
                $('#form-user').validate();
                $('#formSave').on('click', function() {
                    vm.saveFormData();
                });
            },
            getFormData: function () {
                var vthis = this;
                vthis.Id = $.getUrlParam('id');
                axios.get('/Sys/User/Get?Id=' + vthis.Id).then(function (response) {
                    if (!utils.permission.isAuth(response)) {
                        return;
                    }
                    vthis.$set(vthis, 'User', response.data.User);
                }).catch(function (error) {
                    console.log(error);
                });
            },
            saveFormData: function () {
                var isvalidate = $('#form-user').valid();
                if (isvalidate === false) {
                    return;
                }
                var vthis = this;
                var url = parseInt(vthis.Id) > 0 ? '/Sys/User/Update' : '/Sys/User/Add';
                var param = {
                    "User": vthis.User
                };
                axios.post(url, param).then(function (response) {
                    utils.permission.authResponse(response);
                });

            }
        }
    });
});