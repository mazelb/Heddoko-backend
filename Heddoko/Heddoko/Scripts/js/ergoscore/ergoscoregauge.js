/**
 * @file ergoscoreGauge.js
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 12 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
$(function () {
    ErgoScoreGauge.init();
});

var ErgoScoreGauge = {

    init: function () {
        Ajax.get("/api/v1/ergoscore").success(this.onGetSuccess);
    },

    createGauge: function (data) {

        var gauge = $("#gauge");

        if (gauge.length > 0)
        {
            gauge.kendoLinearGauge({
                pointer: [{
                    value: data.userScore,
                    color: "#c30000"
                }
                ],

                scale: {
                    majorUnit: 10,
                    minorUnit: 5,
                    min: 0,
                    max: 103,
                    vertical: true
                }
            });
        } 
    },

    onGetSuccess: function (e) {
        if(e)
        {
            ErgoScoreGauge.createGauge(e);
        }  
    }

};
