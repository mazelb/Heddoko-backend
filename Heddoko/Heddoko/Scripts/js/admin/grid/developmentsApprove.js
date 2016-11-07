$(function() {
    DevelopmentsApprove.init();
});

var DevelopmentsApprove = {
    isDeleted: false,
    controls: {
        grid: null
    },

    validators: {
        modelValidator: null
    },

    datasources: function() {
        //Datasources context
        this.developments = DevelopmentsApprove.getDatasource();
        this.developmentsDD = DevelopmentsApprove.getDatasourceDD();
    },

    getDatasource: function(id) {
        return new kendo.data.DataSource({
            pageSize: KendoDS.pageSize,
            serverPaging: true,
            serverFiltering: true,
            serverSorting: false,
            transport: KendoDS.buildTransport('api/v1/developments'),
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
                                required: true
                            }
                        },
                        client: {
                            nullable: false,
                            type: "string",
                            validation: {
                                required: true
                            }
                        },
                        secret: {
                            nullable: false,
                            type: "string",
                            validation: {
                                required: true
                            }
                        },
                        enabled: {
                            type: "boolean",
                            validation: {
                                required: true
                            }
                        },
                    }
                }
            }
        });
    },

    getDatasourceDD: function() {
        return new kendo.data.DataSource({
            data: [{ value: true, text: "Enabled" }, { value: false, text: "Disabled" }]
        });
    },

    statusDDEditor: function (container, options) {
        $('<input required data-text-field="text" data-value-field="value"  data-value-primitive="true" data-bind="value:' + options.field + '"/>')
        .appendTo(container)
        .kendoDropDownList({
            dataTextField: "text",
            dataValueField: "value",
            autoBind: true,
            dataSource: Datasources.developmentsDD
        });
    },

    init: function() {
        var control = $('#developmentsApproveGrid');

        if (control.length > 0) {
            this.controls.grid = control.kendoGrid({
                    dataSource: Datasources.developments,
                    sortable: false,
                    editable: "popup",
                    selectable: false,
                    scrollable: false,
                    resizeable: true,
                    autoBind: true,
                    pageable: {
                        refresh: true,
                        pageSizes: [10, 50, 100]
                    },
                    columns: [
                        {
                            field: 'name',
                            title: i18n.Resources.Name,
                            editor: KendoDS.emptyEditor
                        },
                        {
                            field: 'client',
                            title: i18n.Resources.Client,
                            editor: KendoDS.emptyEditor
                        },
                        {
                            field: 'secret',
                            title: i18n.Resources.Secret,
                            editor: KendoDS.emptyEditor
                        },
                        {
                            filed: 'enabled',
                            title: i18n.Resources.Status,
                            template: function (e) {
                                return Format.developments.enabled(e.enabled);
                            },
                            editor: DevelopmentsApprove.statusDDEditor
                        },
                        {
                            command: [                            
                                {
                                    name: "Enable",
                                    text: i18n.Resources.Enable,
                                    className: "k-grid-enable",
                                    click: this.onEnable
                                },
                                {
                                    name: "Disable",
                                    text: i18n.Resources.Disable,
                                    className: "k-grid-disable",
                                    click: this.onDisable
                                }
                            ],
                            title: i18n.Resources.Actions,
                            width: '165px'
                        }
                    ],
                    save: KendoDS.onSave,
                    dataBound: this.onDataBound
                })
                .data("kendoGrid");

            KendoDS.bind(this.controls.grid, true);
        }
    },

    onDataBound: function (e) {
        KendoDS.onDataBound(e);

        var grid = DevelopmentsApprove.controls.grid;
        
        $(".k-grid-enable", grid.element)
            .each(function () {
                var currentDataItem = grid.dataItem($(this).closest("tr"));
                console.log(currentDataItem.enabled)
                if (currentDataItem.enabled == true) {
                    $(this).remove();
                }
            });

        $(".k-grid-disable", grid.element)
            .each(function () {
                var currentDataItem = grid.dataItem($(this).closest("tr"));

                if (currentDataItem.enabled != true) {
                    $(this).remove();
                }
            });
    },

    onEnable: function (e) {
        var item = DevelopmentsApprove.controls.grid.dataItem($(e.currentTarget).closest("tr"));
        item.set('enabled', true);
        DevelopmentsApprove.controls.grid.dataSource.sync();
    },

    onDisable: function (e) {
        var item = DevelopmentsApprove.controls.grid.dataItem($(e.currentTarget).closest("tr"));
        item.set('enabled', false);
        DevelopmentsApprove.controls.grid.dataSource.sync();
    }
};

Datasources.bind(Developments.datasources);