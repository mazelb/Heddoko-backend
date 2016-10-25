$(function () {
    ErgoScore.init();
});

var ErgoScore = {

    getData: function () {
        // TODO - Benb - actually get the ERGOSCORES from the controller
        var userScore = 46;
        var orgScore = 24;

        return { userScore, orgScore};
    },

    init: function () {
        ErgoScore.createGauge();
    },

    createGauge: function () {
        var values = ErgoScore.getData();

        $("#gauge").kendoLinearGauge({
            pointer: [{
                value: values.userScore,
                color: "#c30000"
            }, {
                value: values.orgScore,
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
    }
};
