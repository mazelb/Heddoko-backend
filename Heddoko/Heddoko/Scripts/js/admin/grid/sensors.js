$(function () {
    Sensors.init();
});

var Sensors = {
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
        this.sensors = Sensors.getDatasource();

        this.sensorsDD = Sensors.getDatasourceDD();

        this.sensorTypes = new kendo.data.DataSource({
            data: _.values(Enums.SensorType.array)
        });

        this.sensorTypes.read();

        this.sensorQAStatusTypes = new kendo.data.DataSource({
            data: _.values(Enums.SensorsQAStatusType.array)
        });

        this.sensorQAStatusTypes.read();
    },

    getDatasourceDD: function (id) {
        return new kendo.data.DataSource({
            serverPaging: false,
            serverFiltering: true,
            serverSorting: false,
            transport: KendoDS.buildTransport('/admin/api/sensors'),
            schema: {
                data: "response",
                total: "total",
                errors: "Errors",
                model: {
                    id: "id"
                },
            },
            filter: [{
                field: 'Used',
                operator: 'eq',
                value: id
            }]
        });
    },

    getDatasource: function () {
        return new kendo.data.DataSource({
            pageSize: KendoDS.pageSize,
            serverPaging: true,
            serverFiltering: true,
            serverSorting: false,
            transport: KendoDS.buildTransport('/admin/api/sensors'),
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
                        type: {
                            nullable: false,
                            type: "number",
                            validation: {
                                required: true,
                                min: 0,
                                max: KendoDS.maxInt
                            },
                            editor: Sensors.typeDDEditor
                        },
                        version: {
                            nullable: false,
                            type: "string",
                            validation: {
                                required: true,
                                maxLengthValidation: Validator.equipment.version.maxLengthValidation
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
                        /*firmwareID: {
                            nullable: false,
                            type: "number",
                            validation: {
                                max: KendoDS.maxInt
                            }
                        },*/
                        status: {
                            nullable: false,
                            type: "number",
                            validation: {
                                required: true,
                                min: 0,
                                max: KendoDS.maxInt
                            }
                        },
                        qaStatus: {
                            nullable: false,
                            type: "number",
                            validation: {
                                required: true,
                                min: 0,
                                max: KendoDS.maxInt
                            }
                        },
                        /*
                        sensorSet: {
                            nullable: false,
                            type: "number",
                            validation: {
                                required: true,
                                min: 0,
                                max: KendoDS.maxInt
                            }
                        }
                        */
                        anatomicalPosition: {
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

    typeDDEditor: function () {
        $('<input required data-text-field="text" data-value-field="value" data-value-primitive="true" data-bind="value: ' + options.field + '"/>')
        .appendTo(container)
        .kendoDropDownList({
            autoBind: true,
            dataSource: Datasources.sensorTypes
        });
    },

    qaStatusDDEditor: function () {
        $('<input required data-text-field="text" data-value-field="value" data-value-primitive="true" data-bind="value: ' + options.field + '"/>')
        .appendTo(container)
        .kendoDropDownList({
            autoBind: true,
            dataSource: Datasources.sensorQAStatusTypes
        });
    },

    init: function () {
        var control = $("#sensorsGrid");
        var filter = $('.sensorsFilter');
        this.controls.form = $('.sensorsForm');

        if (control.length > 0) {
            this.controls.grid = control.kendoGrid({
                dataSource: Datasources.sensors,
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
                        field: "type",
                        title: i18n.Resources.type, 
                        template: function (e) {
                            return Format.sensors.type(e.type);
                        },
                        editor: Sensors.typeDDEditor
                    },
                    {
                        field: "version",
                        title: i18n.Resources.version
                    },
                    {
                        field: "location",
                        title: i18n.Resources.location
                    },
                    /*{
                        field: "firmware",
                        title: i18n.Resources.FirmwareVersion,
                        template: function (e) {
                            return Format.firmware.version(e);
                        },
                        editor: Firmwares.ddEditorDataboards
                    },*/
                    {
                        field: "status",
                        title: i18n.Resources.Status,
                        template: function (e) {
                            return Format.equipment.equipmentStatus(e.status);
                        },
                        editor: Equipments.equipmentStatusDDEditor
                    },
                    {
                        field: "qaStatus",
                        title: i18n.Resources.qaStatus,
                        template: function (e) {
                            return Format.equipment.equipmentQAStatus(e.qaStatus);
                        },
                        editor: Sensors.qaStatusDDEditor
                    },
                    /*{
                        field: "setID",
                        title: i18n.Resources.sensorSet
                    },*/
                    {
                        field: "anatomicalPosition",
                        title: i18n.Resources.anatomicalPosition,
                        template: function (e) {
                            return Format.equipment.anatomicalPosition(e.anatomicalPosition);
                        },
                        editor: Equipments.anatomicalPositionTypes
                    },
                    {
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

            this.controls.addModel = kendo.observable({
                reset: this.onReset.bind(this),
                submit: this.onAdd.bind(this),
                statuses: Datasources.equipmentStatusTypes,
                qaStatuses: Datasources.sensorQAStatusTypes,
                //firmwares: Datasources.firmwaresDataboards,
                sensorTypes: Datasources.sensorTypes,
                anatomicalPositions: Datasources.anatomicalPositionTypes,
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

    ddEditor: function (container, options) {
        $('<input required data-text-field="name" data-value-field="id" data-value-primitive="true" data-bind="value: ' + options.field + '"/>')
        .appendTo(container)
        .kendoDropDownList({
            autoBind: true,
            dataSource: Sensors.getDatasourceDD(options.model.id)
        });
    },

    onDataBound: function (e) {
        KendoDS.onDataBound(e);

        var grid = Sensors.controls.grid;
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
            type: null,
            version: null,
            location: null,
            //firmwareID: null,
            status: null,
            qaStatus: null,
            //sensorSet: null,
            anatomicalPosition: null
        }
    },

    onShowDeleted: function () {
        this.isDeleted = $(e.currentTarget).prop("checked");
        this.onFilter();
    },

    onRestore: function (e) {
        var grid = Sensors.controls.grid;

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

Datasources.bind(Sensors.datasources);