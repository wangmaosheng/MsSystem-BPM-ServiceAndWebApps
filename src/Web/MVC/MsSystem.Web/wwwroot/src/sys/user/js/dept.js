define(function (require, exports, module) {
    require('jquery-extension');
    require("vue");
    require("vue-filters");
    require("axios");
    require("toastr");
    require("pace");
    var utils = require('utils');
    var vm = new Vue({
        el: '#msapp',
        data: {
            'UserId': null,
            'UserName': null,
            'Depts': [],
            'DeptId': null
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
                vthis.Id = $.getUrlParam('userid');
                axios.get('/Sys/User/GetUserDept?userid=' + vthis.Id).then(function (response) {
                    if (!utils.permission.isAuth(response)) {
                        return;
                    }
                    vthis.$set(vthis, 'UserId', response.data.UserId);
                    vthis.$set(vthis, 'UserName', response.data.UserName);
                    vthis.$set(vthis, 'Depts', response.data.Depts);
                    vthis.$set(vthis, 'DeptId', response.data.DeptId);
                }).catch(function (error) {
                    console.log(error);
                });
            },
            saveFormData: function () {
                var vthis = this;
                var param = {
                    'UserId': vthis.UserId,
                    "DeptId": vthis.DeptId
                };
                axios.post('/Sys/User/SaveUserDept', param).then(function (response) {
                    utils.permission.authResponse(response);
                });
            },
            selectDept: function(deptid) {
                $.each(vm.Depts, function(i,item) {
                    if (item.Value === deptid) {
                        item.Selected = true;
                        vm.DeptId = deptid;
                    } else {
                        item.Selected = false;
                    }
                });
            }
        }
    });
});