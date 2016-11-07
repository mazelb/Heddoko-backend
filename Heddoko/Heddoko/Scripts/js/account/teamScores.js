$(function () {
    TeamScores.init();
});

var TeamScores = {

    controls: {
        grid: null
    },

    datasources: function() {
        this.teamScores = TeamScores.getDatasource();
    },

    getDatasource: function() {
        return new kendo.data.DataSource({
            pageSize: KendoDS.pageSize,
            serverPaging: true,
            serverFiltering: true,
            serverSorting: false,
            transport: KendoDS.buildTransport("analyst/api/teamscore"),
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
                        }
                    }
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
                        field: "id",
                        title: i18n.Resources.ID
                    },
                    {
                        field: "ergoScore",
                        title: i18n.Resources.Ergoscore
                    }
                ]
            }).data("kendoGrid");
        }
    }
};

Datasources.bind(TeamScores.datasources);
