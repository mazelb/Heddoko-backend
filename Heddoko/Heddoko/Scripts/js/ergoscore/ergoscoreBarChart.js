$(function () {
    ErgoscoreBarChart.init();
});

var ErgoscoreBarChart = {

    controls: {
        chart: null,
        filterModel: null,
        baseUnitInputs: null
    },

    datasources: function() {
        this.ergoscoreBarChart = ErgoscoreBarChart.getDatasource();
    },

    getDatasource: function() {
        return new kendo.data.DataSource({
            pageSize: KendoDS.pageSize,
            serverPaging: true,
            serverFiltering: true,
            serverSorting: false,
            transport: KendoDS.buildTransport("/admin/api/ergoscorerecord"),
            schema: {
                data: "response",
                total: "total",
                errors: "Errors",
                model: {
                    id: "id",
                    fields: {
                        userId: {
                            editable: false,
                            type: "number"
                        },
                        recordScore: {
                            editable: false,
                            type: "number"
                        },
                        hourlyScore: {
                            editable: false,
                            type: "object"
                        },
                        date: {
                            editable: false,
                            type: "date"
                        }
                    }
                }
            },
            filter: []
        });
    },

    init: function () {
        var control = $("#ergoscoreBarChart");
        var filter = $(".ergoscoreChartFilter");
        var baseUnits = $(".baseUnitOptions");

        if (control.length > 0) {
            this.controls.chart = control.kendoChart({
                dataSource: Datasources.ergoscoreBarChart,
                title: {
                    text: "Team ErgoScores"
                },
                series: [{
                    type: "column",
                    aggregate: "avg",
                    field: "recordScore",
                    categoryField: "date"
                }],
                categoryAxis: {
                    baseUnit: "",
                    majorGridLines: {
                        visible: false
                    }
                },
                valueAxis: {
                    line: {
                        visible: false
                    },
                    min: 0,
                    max: 100 
                },
                chartArea: {
                    height: 600
                }
            }).data("kendoChart");

            KendoDS.bind(this.controls.chart, true);

            this.controls.filterModel = kendo.observable({
                users: Datasources.teamScoresDD,
                usersFilter: [],
                find: this.onFilter.bind(this)
            });

            kendo.bind(filter, this.controls.filterModel);

            baseUnits.bind("change", this.refresh);            
        }
    },

    onFilter: function (e) {
        var filters = this.buildFilter();
        if (filters) {
            this.controls.chart.dataSource.filter(filters);
        }
    },

    buildFilter: function (usersFilter) {
        usersFilter = this.controls.filterModel.usersFilter;
        var filters = [];

        if (typeof (usersFilter) !== "undefined" && usersFilter.length !== 0 && usersFilter !== null) {
            var userIDs = [];
            usersFilter.forEach(function (element) {
                userIDs.push(element);
            });
            filters.push({
                field: "Search",
                operator: "eq",
                value: userIDs.toString()
            });
        }

        return filters.length === 0 ? {} : filters;
    },

    // Call after chart options have been changed
    refresh: function () {
        var chart = ErgoscoreBarChart.controls.chart;
        var baseUnitInputs = $("input:radio[name=baseUnit]");

        chart.options.categoryAxis.baseUnit = baseUnitInputs.filter(":checked").val();

        chart.refresh();
    }
};

Datasources.bind(ErgoscoreBarChart.datasources);