$(function () {
    SensorSets.init();
});

var SensorSets = {
    isDeleted: false,
    controls: {
        grid: null,
        filterModel: null,
        addModel: null
    },

    validators: {
        modelValidator: null
    },

    datasources: function () {

    },

    init: function () {
        var control = $("#sensorSetsGrid");
        var filter = $('.sensorSetsFilter');
        var model = $('.sensorSetsForm');

        if (control.length > 0) {
            //TODO: BENB
        }
    },

    onDataBound: function (e) {
        //TODO: BENB
    },

    getEmptyModel: function () {
        //TODO: BENB
    },

    onShowDeleted: function () {
        //TODO: BENB
    },

    onRestore: function (e) {
        //TODO: BENB
    },

    onReset: function (e) {
        //TODO: BENB
    },

    onAdd: function (e) {
        //TODO: BENB
    },

    onEnter: function (e) {
        //TODO: BENB
    },

    onFilter: function (e) {
        //TODO: BENB
    },

    buildFilter: function (search) {
        //TODO: BENB
    }
};

Datasources.bind(SensorSets.datasources);