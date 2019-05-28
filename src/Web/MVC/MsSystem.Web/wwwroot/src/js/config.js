seajs.config({
    base: "/ui/js",
    alias: {
        "jquery": "jquery.min.js",
        "bootstrap": "bootstrap.min.js",
        "jquery-extension": "jquery-extension.js",
        "jquery-pager": "jquery.pager.js",
        "content": "content.min.js",
        "jsencrypt": "jsencrypt.min.js",

        "validate": "plugins/validate/jquery.validate.min.js",
        "validate_messages_zh": "plugins/validate/messages_zh.min.js",
        "slimscroll": "plugins/slimscroll/jquery.slimscroll.min.js",
        "layer": "plugins/layer/layer.min.js",
        "laydate": "plugins/laydate/laydate.js",
        "toastr": "plugins/toastr/toastr.min.js",
        "ztree": "plugins/ztree/jquery.ztree.all.js",
        "treegrid": "plugins/treegrid/js/jquery.treegrid.js",
        "metisMenu": "plugins/metisMenu/jquery.metisMenu.js",
        "pace": "plugins/pace/pace.min.js",
        "icheck": "plugins/iCheck/icheck.min.js",

        "niceSelect": "plugins/niceSelect/jquery.nice-select.min.js",
        "perfecrScroll": "plugins/perfect-scrollbar/perfect-scrollbar.jquery.js",
        "cookie": "jquery.cookie.js",

        "gooflow": "plugins/GooFlow/GooFlow.js",
        'draw':'draw-min.js',

        "vue": "vue/vue.js",
        "vue-filters": "vue/vue-filters.js",
        "axios": "axios.js",
        "utils": "utils.js"
    },
    preload: ["jquery"],
    debug: true
});