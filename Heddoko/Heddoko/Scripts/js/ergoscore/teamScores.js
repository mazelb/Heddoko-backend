$(function () {
    TeamScores.init();
});

var TeamScores = {

    controls: {
        grid: null
    },

    datasources: function() {
        this.teamScores = TeamScores.getDatasource();

        this.teamScoresDD = TeamScores.getDatasourceDD();
    },

    getDatasource: function() {
        return new kendo.data.DataSource({
            pageSize: KendoDS.pageSize,
            serverPaging: true,
            serverFiltering: true,
            serverSorting: false,
            transport: KendoDS.buildTransport('/admin/api/teamscore'),
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

    getDatasourceDD: function() {
        return new kendo.data.DataSource({
            serverPaging: false,
            serverFiltering: true,
            serverSorting: false,
            transport: KendoDS.buildTransport('/admin/api/teamscore'),
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
        var control = $("#ergoscoreGrid");

        if (control.length > 0) {
            this.controls.grid = control.kendoGrid({
                dataSource: Datasources.teamScores,
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
                        template: function(e) {
                            return Format.ergoscore.score(e.score);
                        }
                    }
                ]
            }).data("kendoGrid");
        }
    },
};

Datasources.bind(TeamScores.datasources);
