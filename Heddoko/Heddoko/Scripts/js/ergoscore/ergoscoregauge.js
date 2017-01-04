$(function () {
    ErgoScoreGauge.init();
});

var ErgoScoreGauge = {

    init: function () {
        Ajax.get("/admin/api/ergoscore/get").success(this.onGetSuccess);
        // TODO - Reimplement once ErgoScoreController is properly implemented
        //Ajax.get("/admin/api/ergoscore/get").success(this.onGetSuccess);
        //Ajax.get("/api/v1/ergoscore").success(this.onGetSuccess);
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
