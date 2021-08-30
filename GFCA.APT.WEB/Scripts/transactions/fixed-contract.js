;(function ($, window, document, undefined) {
    "use strict";

    //region declare class fixed contract //
    let fixedContract = function (element, options) {
        let self = this;

        self.element = element;
        self.options = $.extend({}, $.fn.fixedContract.defaults, options);

        $(document).on("commandSend", function () {
            let data = {};
            self.commandSend(data);
        });


        self.commandSend = function (data) {

        }

        //private function
        function pageLoad() {

        }
    }
    //endregion declare class fixed contract //

    $.fn.fixedContract.defaults = {

    };

    $.fn.fixedContract.Constructor = fixedContract;

    let old = $.fn.fixedContract;
    $.fn.fixedContract.noConflict = function () {
        $.fn.fixedContract = old;
        return this;
    };


}(jQuery, window, document));