﻿$(function () {
    Databoards.init();
});

var Databoards = {
    isDeleted: false,
    controls: {
        form: null,
        grid: null,
        filterModel: null,
        addModel: null
    },

    validators: {
        modelValidator: null
    },

    datasources: function () {
        //Datasources context
        this.databoards = Databoards.getDatasource();
    },

    getDatasource: function () {
        return new kendo.data.DataSource({
            pageSize: KendoDS.pageSize,
            serverPaging: true,
            serverFiltering: true,
            serverSorting: false,
            transport: KendoDS.buildTransport("/admin/api/databoards"),
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
                        version: {
                            nullable: false,
                            type: "string",
                            validation: {
                                required: true,
                                maxLengthValidation: Validator.equipment.version.maxLengthValidation
                            }
                        },
                        firmwareID: {
                            nullable: true,
                            type: "number",
                            validation: {
                                max: KendoDS.maxInt
                            }
                        },
                        location: {
                            nullable: false,
                            type: "string",
                            validation: {
                                required: true,
                                maxLengthValidation: Validator.equipment.location.maxLengthValidation
                            }
                        },
                        status: {
                            nullable: false,
                            type: "number",
                            validation: {
                                required: true,
                                min: 0,
                                max: KendoDS.maxInt
                            }
                        }
                    }
                }
            }
        });
    },

    init: function () {
        var control = $("#databoardsGrid");
        var filter = $(".databoardsFilter");
        this.controls.form = $(".databoardsForm");

        if (control.length > 0) {
            this.controls.grid = control.kendoGrid({
                dataSource: Datasources.databoards,
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
                toolbar: [{
                    template: '<div class="grid-checkbox"><span><input class="chk-show-deleted" type="checkbox"/>' + i18n.Resources.ShowDeleted + '</span></div>'
                }],
                columns: [
                {
                    field: "idView",
                    title: i18n.Resources.ID
                },
                {
                    field: "firmwareID",
                    title: i18n.Resources.FirmwareVersion,
                    template: function (e) {
                        return Format.firmware.version(e);
                    },
                    editor: Firmwares.ddEditorDataboards
                },
                {
                    field: "version",
                    title: i18n.Resources.Version
                },
                {
                    field: "location",
                    title: i18n.Resources.PhysicalLocation
                },
                {
                    field: "status",
                    title: i18n.Resources.Status,
                    template: function (e) {
                        return Format.equipment.equipmentStatus(e.status);
                    },
                    editor: Equipments.equipmentStatusDDEditor
                }, {
                    command: [{
                        name: "edit",
                        text: i18n.Resources.Edit,
                        className: "k-grid-edit"
                    }, {
                        name: "destroy",
                        text: i18n.Resources.Delete,
                        className: "k-grid-delete"
                    }, {
                        text: i18n.Resources.Restore,
                        className: "k-grid-restore",
                        click: this.onRestore
                    }],
                    title: i18n.Resources.Actions,
                    width: '165px'
                }
                ],
                save: KendoDS.onSave,
                dataBound: this.onDataBound
            }).data("kendoGrid");

            KendoDS.bind(this.controls.grid, true);

            this.controls.filterModel = kendo.observable({
                find: this.onFilter.bind(this),
                search: null,
                keyup: this.onEnter.bind(this)
            });

            kendo.bind(filter, this.controls.filterModel);

            this.controls.addModel = kendo.observable({
                reset: this.onReset.bind(this),
                submit: this.onAdd.bind(this),
                sizes: Datasources.sizeTypes,
                statuses: Datasources.equipmentStatusTypes,
                firmwares: Datasources.firmwaresDataboards,
                model: this.getEmptyModel()
            });

            kendo.bind(this.controls.form, this.controls.addModel);

            this.validators.addModel = this.controls.form.kendoValidator({
                validateOnBlur: true,
                rules: {
                    maxLengthValidationLocation: Validator.equipment.location.maxLengthValidation
                }
            }).data("kendoValidator");

            $('.chk-show-deleted', this.controls.grid.element).click(this.onShowDeleted.bind(this));
        }
    },

    onDataBound: function (e) {
        KendoDS.onDataBound(e);

        var grid = Databoards.controls.grid;
        var enumarable = Enums.EquipmentStatusType.enum;

        $(".k-grid-delete", grid.element).each(function () {
            var currentDataItem = grid.dataItem($(this).closest("tr"));

            if (currentDataItem.status === enumarable.Trash) {
                $(this).remove();
            }
        });

        $(".k-grid-edit", grid.element).each(function () {
            var currentDataItem = grid.dataItem($(this).closest("tr"));

            if (currentDataItem.status === enumarable.Trash) {
                $(this).remove();
            }
        });

        $(".k-grid-restore", grid.element).each(function () {
            var currentDataItem = grid.dataItem($(this).closest("tr"));

            if (currentDataItem.status !== enumarable.Trash) {
                $(this).remove();
            }
        });
    },

    getEmptyModel: function () {
        return {
            version: null,
            firmwareID: null,
            location: null,
            status: null
        };
    },

    onShowDeleted: function (e) {
        this.isDeleted = $(e.currentTarget).prop("checked");
        this.onFilter();
    },

    onRestore: function (e) {
        var grid = Databoards.controls.grid;

        var item = grid.dataItem($(e.currentTarget).closest("tr"));
        item.set("status", Enums.EquipmentStatusType.enum.Ready);
        grid.dataSource.sync();
    },

    onReset: function (e) {
        this.controls.addModel.set("model", this.getEmptyModel());
    },

    onAdd: function (e) {
        Notifications.clear();
        if (this.validators.addModel.validate()) {
            var obj = this.controls.addModel.get("model");

            this.controls.grid.dataSource.add(obj);
            this.controls.grid.dataSource.sync();
            this.controls.grid.dataSource.one("requestEnd", function (ev) {
                if (ev.type === "create"
                && !ev.response.Errors) {
                    this.onReset();
                }
            }.bind(this));
        }
    },

    onEnter: function (e) {
        if (e.keycode === kendo.keys.ENTER) {
            this.onFilter(e);
        }
    },

    onFilter: function (e) {
        var filters = this.buildFilter();
        if (filters) {
            this.controls.grid.dataSource.filter(filters);
        }
    },

    buildFilter: function (search) {
        Notifications.clear();
        search = this.controls.filterModel.search;

        var filters = [];

        if (typeof (search) !== "undefined"
         && search !== ""
         && search !== null) {
            filters.push({
                field: "Search",
                operator: "eq",
                value: search
            });
        }

        if (this.isDeleted) {
            filters.push({
                field: "IsDeleted",
                operator: "eq",
                value: true
            });
        }

        return filters.length === 0 ? {} : filters;
    }
};

Datasources.bind(Databoards.datasources);
