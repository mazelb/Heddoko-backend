$(function () {
    ErgoscoreBarChart.init();
});

var ErgoscoreBarChart = {

    controls: {
        chart: null,
        filterModel: null,
        baseUnitInputs: null,
        startDatePicker: null,
        endDatePicker: null
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
        var startDate = $("#startDate");
        var endDate = $("#endDate");

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

            var today = kendo.date.today();

            this.controls.startDatePicker = startDate.kendoDateTimePicker({
                value: null,
                max: today,
                change: this.startChange
            }).data("kendoDateTimePicker");

            this.controls.endDatePicker = endDate.kendoDateTimePicker({
                value: null,
                min: today,
                change: this.endChange
            }).data("kendoDateTimePicker");

            this.controls.filterModel = kendo.observable({
                users: Datasources.teamScoresDD,
                usersFilter: [],
                startDateSelected: this.controls.startDatePicker.value,
                endDateSelected: this.controls.endDatePicker.value,
                find: this.onFilter.bind(this),
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
        var dateFilter = {
            start: this.controls.startDatePicker.value(),
            end: this.controls.endDatePicker.value()
        };
        var filters = [];

        if (typeof (usersFilter) !== "undefined" && usersFilter.length !== 0 && usersFilter !== null) {
            var userIDs = [];
            usersFilter.forEach(function (element) {
                userIDs.push(element);
            });
            filters.push({
                field: "Users",
                operator: "eq",
                value: userIDs.toString()
            });
        }

        if (dateFilter.start != null || dateFilter.end != null)
        {
            if (dateFilter.start != null) {
                dateFilter.start = parseInt((dateFilter.start.getTime() / 1000).toFixed(0));
            }
            if (dateFilter.end != null) {
                dateFilter.end = parseInt((dateFilter.end.getTime() / 1000).toFixed(0));
            }
            var dates = [];
            dates.push(dateFilter.start);
            dates.push(dateFilter.end);

            filters.push({
                field: "Dates",
                operator: "eq",
                value: dates.toString()
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
    },

    startChange: function () {
        var start = ErgoscoreBarChart.controls.startDatePicker.value();
        var end = ErgoscoreBarChart.controls.endDatePicker.value();
        if (start) {
            ErgoscoreBarChart.controls.endDatePicker.min(start)
        }
        else if (end) {
            ErgoscoreBarChart.controls.startDatePicker.max(end);
        }
        else {
            end = new Date();
            ErgoscoreBarChart.controls.startDatePicker.max(end);
            ErgoscoreBarChart.controls.endDatePicker.min(end);
        }
    },

    endChange: function () {
        var end = ErgoscoreBarChart.controls.endDatePicker.value();
        var start = ErgoscoreBarChart.controls.startDatePicker.value();
    
        if (end) {
            ErgoscoreBarChart.controls.startDatePicker.max(end)
        }
        else if (start) {
            ErgoscoreBarChart.controls.endDatePicker.min(start);
        }
        else {
            end = new Date();
            ErgoscoreBarChart.controls.startDatePicker.max(end);
            ErgoscoreBarChart.controls.endDatePicker.min(end);
        }
    }
};

Datasources.bind(ErgoscoreBarChart.datasources);