/**
 * @file applicationsApprove.js
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 12 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
$(function() {
    ApplicationsApprove.init();
});

var ApplicationsApprove = {
    isDeleted: false,
    controls: {
        grid: null
    },

    validators: {
        modelValidator: null
    },

    datasources: function() {
        //Datasources context
        this.applicationsApprove = ApplicationsApprove.getDatasource();
        this.applicationsDD = ApplicationsApprove.getDatasourceDD();
    },

    getDatasource: function(id) {
        return new kendo.data.DataSource({
            pageSize: KendoDS.pageSize,
            serverPaging: true,
            serverFiltering: true,
            serverSorting: false,
            transport: KendoDS.buildTransport('/admin/api/applications'),
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
                        redirectUrl: {
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
            dataSource: Datasources.applicationsDD
        });
    },

    init: function() {
        var control = $('#applicationsApproveGrid');

        if (control.length > 0) {
            this.controls.grid = control.kendoGrid({
                    dataSource: Datasources.applicationsApprove,
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
                            field: 'redirectUrl',
                            title: i18n.Resources.RedirectUrl,
                            editor: KendoDS.emptyEditor
                        },
                        {
                            filed: 'enabled',
                            title: i18n.Resources.Status,
                            template: function (e) {
                                return Format.applications.enabled(e.enabled);
                            },
                            editor: ApplicationsApprove.statusDDEditor
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

        var grid = ApplicationsApprove.controls.grid;
        
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
        var item = ApplicationsApprove.controls.grid.dataItem($(e.currentTarget).closest("tr"));
        item.set('enabled', true);
        ApplicationsApprove.controls.grid.dataSource.sync();
    },

    onDisable: function (e) {
        var item = ApplicationsApprove.controls.grid.dataItem($(e.currentTarget).closest("tr"));
        item.set('enabled', false);
        ApplicationsApprove.controls.grid.dataSource.sync();
    }
};

Datasources.bind(ApplicationsApprove.datasources);