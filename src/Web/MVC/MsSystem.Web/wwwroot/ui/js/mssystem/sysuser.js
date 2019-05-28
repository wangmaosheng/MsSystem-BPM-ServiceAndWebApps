/*
*   creater :wms
*   time:   2017年7月29日11:20:11
*   desc:   用户选择控件
*/
;
(function($) {
    "use strict";
    var UserSelector = function(element,options) {
        
    };
    UserSelector.prototype = {
        init: function(element, options) {
            
        }
    };

    $.fn.userSelector = function (option) {
        var $this = this,
            data = $this.data('userSelector');

        if (!data) {
            data = new UserSelector(this, option);

            $this = $(data.$element);

            $this.data('userSelector', data);
            return;
        }
        var result = data.setOptions(option);
        return result;
    };
    $.fn.userSelector.defaults = {
        
    };
    $.fn.userSelector.Constructor = UserSelector;


})(jQuery);