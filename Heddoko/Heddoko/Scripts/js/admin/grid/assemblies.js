$(function () {
    Assemblies.init();
});

var Assemblies = {
    controls: {
        form: null,
        grid: null,
        filterModel: null,
        addModel: null
    },

    datasources: function () {
        this.assemblies = Assemblies.getDatasource();
    },

    getDatasource: function () {
        return new kendo.data.DataSource({
            pageSize: KendoDS.pageSize,
            serverPaging: true,
            serverFiltering: true,
            serverSorting: false,
            transport: KendoDS.buildTransport("/admin/api/kits"),
            schema: {
                data: "response",
                total: "total",
                errors: "Errors",
                // TODO: Benb - CHANGE THE JSON MODEL !!!
                model: {
                    fields: {
                        assembly: {
                            nullable: false,
                            type: "text"
                        },
                        onHand: {
                            nullable: false,
                            type: "number"
                        },
                        Producible: {
                            nullable: false,
                            type: "number"
                        }
                    }
                }
            }
        }).
    },

    init: function () {
        var control = $("#assembliesGrid");

        if (control.length > 0) {
            this.controls.grid = control.kendoGrid({
                dataSource: Datasources.assemblies,
                sortable: false,
                selectable: false,
                resizeable: true,
                autoBind: true,
                pageable: {
                    refresh: true,
                    pageSizes: [10, 50, 100]
                },
                toolbar: [{
                    template: '<div class="grid-checkbox"><span><input class="chk-show-deleted" type="checkbox"/>' + i18n.Resources.ShowDeleted + '</span></div>'
                }],
                columns: [
                    {
                        field: "assembly",
                        title: i18n.Resources.Assemblies
                    },
                    {
                        field: "onHand",
                        title: i18n.Resources.QuantityOnHand
                    },
                    {
                        field: "Producible",
                        title: i18n.Resources.QuantityProducible
                    }
                ]
            }).data("kendoGrid");
        }
    }
};
