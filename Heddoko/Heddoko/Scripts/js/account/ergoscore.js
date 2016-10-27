$(function () {
    ErgoScore.init();
});

var ErgoScore = {

    init: function () {
        Ajax.post("/api/v1/ergoscore/get").success(this.onGetSuccess);
    },

    createGauge: function () {
        $("#gauge").kendoLinearGauge({
            pointer: [{
                value: userScore,
                color: "#c30000"
            }, {
                value: orgScore,
                margin: 10
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
    },

    onGetSuccess: function (e) {
        if(e)
        {
            userScore = e.userScore
            orgScore = e.orgScore
        }
        ErgoScore.createGauge();
    }

};
