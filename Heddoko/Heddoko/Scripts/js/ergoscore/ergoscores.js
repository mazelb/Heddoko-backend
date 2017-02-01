$(function () {
    Ergoscores.init();
});

var Ergoscores = {

    controls: {
        chart: null,
        grid: null,
        filterModel: null,
        baseUnitInputs: null,
        startDatePicker: null,
        endDatePicker: null
    },

    datasources: function() {
        this.ergoscoreBarChart = Ergoscores.getChartDatasource();

        this.ergoscoreLeaderboard = Ergoscores.getLeaderboardDatasource();

        this.ergoscoreUsersDD = Ergoscores.getDatasourceDD();
    },

    getChartDatasource: function() {
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

    getLeaderboardDatasource: function() {
        return new kendo.data.DataSource({
            pageSize: KendoDS.pageSize,
            serverPaging: true,
            serverFiltering: true,
            serverSorting: false,
            transport: KendoDS.buildTransport('/admin/api/ergoscoreLeaderboard'),
            schema: {
                data: "response",
                total: "total",
                errors: "Errors",
                model: {
                    id: "id",
                    fields: {
                        id: {
                            editable: false,
                            nullable: true
                        },
                        score: {
                            editable: false,
                            type: "number"
                        },
                        name: {
                            editable: false,
                            type: "string"
                        }
                    }
                }
            }
        });
    },

    getDatasourceDD: function () {
        return new kendo.data.DataSource({
            serverPaging: false,
            serverFiltering: true,
            serverSorting: false,
            transport: KendoDS.buildTransport('/admin/api/ergoscoreLeaderboard'),
            schema: {
                data: "response",
                total: "total",
                errors: "Errors",
                model: {
                    id: "id"
                }
            }
        });
    },

    init: function () {
        var chart = $("#ergoscoreBarChart");
        var grid = $("#ergoscoreLeaderboard");
        var filter = $(".ergoscoreFilter");
        var baseUnits = $(".baseUnitOptions");
        var startDate = $("#startDate");
        var endDate = $("#endDate");

        if (chart.length > 0) {
            this.controls.chart = chart.kendoChart({
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
                users: Datasources.ergoscoreUsersDD,
                usersFilter: [],
                startDateSelected: this.controls.startDatePicker.value,
                endDateSelected: this.controls.endDatePicker.value,
                find: this.onFilter.bind(this)
            });

            kendo.bind(filter, this.controls.filterModel);

            baseUnits.bind("change", this.refresh);            
        }

        if (grid.length > 0)
        {
            this.controls.grid = grid.kendoGrid({
                dataSource: Datasources.ergoscoreLeaderboard,
                sortable: false,
                selectable: false,
                scrollable: false,
                resizeable: true,
                autoBind: true,
                pageable: {
                    refresh: true,
                    pageSize: [10, 50, 100]
                },
                columns: [
                    {
                        field: "name",
                        title: i18n.Resources.NameOfUser
                    },
                    {
                        field: "score",
                        title: i18n.Resources.Ergoscore,
                        template: function (e) {
                            return Format.ergoscore.score(e.score);
                        }
                    }
                ]
            }).data("kendoGrid");

            KendoDS.bind(this.controls.grid, true);
        }
    },

    onFilter: function (e) {
        var chartFilters = this.buildChartFilter();
        if (chartFilters) {
            this.controls.chart.dataSource.filter(chartFilters);
        }

        var gridFilters = this.buildGridFilter();
        if (gridFilters) {
            this.controls.grid.dataSource.filter(gridFilters);
        }
    },

    buildChartFilter: function (usersFilter) {
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

    buildGridFilter: function(usersFilter) {
        usersFilter = this.controls.filterModel.usersFilter;
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

        return filters.length === 0 ? {} : filters;
    },

    // Call after chart options have been changed
    refresh: function () {
        var chart = Ergoscores.controls.chart;
        var baseUnitInputs = $("input:radio[name=baseUnit]");

        chart.options.categoryAxis.baseUnit = baseUnitInputs.filter(":checked").val();

        chart.refresh();
    },

    startChange: function () {
        var start = Ergoscores.controls.startDatePicker.value();
        var end = Ergoscores.controls.endDatePicker.value();
        if (start) {
            Ergoscores.controls.endDatePicker.min(start)
        }
        else if (end) {
            Ergoscores.controls.startDatePicker.max(end);
        }
        else {
            end = new Date();
            Ergoscores.controls.startDatePicker.max(end);
            Ergoscores.controls.endDatePicker.min(end);
        }
    },

    endChange: function () {
        var end = Ergoscores.controls.endDatePicker.value();
        var start = Ergoscores.controls.startDatePicker.value();
    
        if (end) {
            Ergoscores.controls.startDatePicker.max(end)
        }
        else if (start) {
            Ergoscores.controls.endDatePicker.min(start);
        }
        else {
            end = new Date();
            Ergoscores.controls.startDatePicker.max(end);
            Ergoscores.controls.endDatePicker.min(end);
        }
    }
};

Datasources.bind(Ergoscores.datasources);