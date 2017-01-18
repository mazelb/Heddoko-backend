/**
 * @file general.js
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
$(function () {
    if ($.fn.datepicker) {
        $('#Birthday').datepicker({
            autoclose: true,
        });
    }

    if ($.fn.slimScroll) {
        $('#nav').slimScroll({
            height: '100%'
        });
    }
});