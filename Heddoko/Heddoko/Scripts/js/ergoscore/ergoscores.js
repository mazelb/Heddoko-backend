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
        this.ergoscoreTeams = Ergoscores.getTeamsDatasource();
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

    getTeamsDatasource: function () {
        return new kendo.data.DataSource({
            pageSize: KendoDS.pageSize,
            serverPaging: true,
            serverFiltering: true,
            serverSorting: false,
            transport: KendoDS.buildTransport("/admin/api/teams"),
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
                        name: {
                            nullable: false,
                            type: "string",
                            validation: {
                                required: true,
                                maxLengthValidation: Validator.equipment.name.maxLengthValidation
                            }
                        }
                    }
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
                    baseUnit: "fit",
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
                teams: Datasources.ergoscoreTeams,
                usersFilter: [],
                teamsFilter: [],
                startDateSelected: this.controls.startDatePicker.value,
                endDateSelected: this.controls.endDatePicker.value,
                find: this.onFilter.bind(this),
                onTeamChange: this.onTeamChange.bind(this)
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

    // Build the filter array for the bar chart
    buildChartFilter: function (usersFilter) {
        var filters = [];

        usersFilter = this.buildUsersFilter();
        if (usersFilter)
        {
            filters.push(usersFilter);
        }

        var dateFilter = this.buildDateFilter();
        if (dateFilter)
        {
            filters.push(dateFilter);
        }

        var teamFilter = this.buildTeamFilter();
        if (teamFilter)
        {
            filters.push(teamFilter);
        }

        return filters.length === 0 ? {} : filters;
    },

    // Build to filter array for the leaderboard grid
    buildGridFilter: function(usersFilter) {
        var filters = [];
        usersFilter = this.buildUsersFilter();
        if (usersFilter) {
            filters.push(usersFilter);
        }
        var teamFilter = this.buildTeamFilter();
        if (teamFilter)
        {
            filters.push(teamFilter);
        }

        return filters.length === 0 ? {} : filters;
    },

    // build filter item for the team filter
    buildTeamFilter: function(teamsFilter) {
        teamsFilter = this.controls.filterModel.teamsFilter;
        var filter = null;

        if (typeof (teamsFilter) !== "undefined" && teamsFilter.length !== 0 && teamsFilter !== null) {
            var teamIDs = [];
            teamsFilter.forEach(function (element) {
                teamIDs.push(element);
            });
            filter = {
                field: "Teams",
                operator: "eq",
                value: teamIDs.toString()
            };
        }
        return filter;
    },

    // build filter item for the users filter
    buildUsersFilter: function(usersFilter)
    {
        usersFilter = this.controls.filterModel.usersFilter;
        var filter = null;

        if (typeof (usersFilter) !== "undefined" && usersFilter.length !== 0 && usersFilter !== null) {
            var userIDs = [];
            usersFilter.forEach(function (element) {
                userIDs.push(element);
            });
            filter = {
                field: "Users",
                operator: "eq",
                value: userIDs.toString()
            };
        }
        return filter;
    },

    // build filter item for the date filter
    buildDateFilter: function(dateFilter) 
    {
        var dateFilter = {
            start: this.controls.startDatePicker.value(),
            end: this.controls.endDatePicker.value()
        };

        var filter = null;

        if (dateFilter.start != null || dateFilter.end != null) {
            if (dateFilter.start != null) {
                dateFilter.start = parseInt((dateFilter.start.getTime() / 1000).toFixed(0));
            }
            if (dateFilter.end != null) {
                dateFilter.end = parseInt((dateFilter.end.getTime() / 1000).toFixed(0));
            }
            var dates = [];
            dates.push(dateFilter.start);
            dates.push(dateFilter.end);

            filter = {
                field: "Dates",
                operator: "eq",
                value: dates.toString()
            };
        }

        return filter;
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
    },

    onTeamChange: function (e) {
        var filters = []
        var teamFilter = this.buildTeamFilter();
        if (teamFilter) {
            filters.push(teamFilter)
            Datasources.ergoscoreUsersDD.filter(filters);
        }
    }
};

Datasources.bind(Ergoscores.datasources);