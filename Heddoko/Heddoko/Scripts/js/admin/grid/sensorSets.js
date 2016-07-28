$(function () {
    SensorSets.init();
});

var SensorSets = {
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
        this.sensors = SensorSets.getDatasource();
    },

    getDatasource: function() {
        return new kendo.data.DataSource({
            pageSize: KendoDS.pageSize,
            serverPaging: true,
            serverFiltering: true,
            serverSorting: false,
            transport: KendoDS.buildTransport('/admin/api/sensorSets'),
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
                        /*
                        sensors: {
                            
                        }
                        */
                        qaStatus: {
                            nullable: false,
                            type: "number",
                            validation: {
                                required: true,
                                min: 0,
                                max: KendoDS.maxInt
                            }
                        },
                        kitID: {
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

    getDatasourceDD: function (id) {
        return new kendo.data.DataSource({
            serverPaging: false,
            serverFiltering: true,
            serverSorting: false,
            transport: KendoDS.buildTransport('/admin/api/sensorSets'),
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

    init: function () {
        var control = $("#sensorSetsGrid");
        var filter = $('.sensorSetsFilter');
        var model = $('.sensorSetsForm');

        if (control.length > 0) {
            this.controls.grid = control.kendoGrid({
                dataSource: Datasources.sensorSets,
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
                        field: "qaStatus",
                        title: i18n.Resources.qaStatus,
                        template: function (e) {
                            return Format.equipment.equipmentQAStatus(e.qaStatus);
                        },
                        editor: Sensors.qaStatusDDEditor
                    },
                    {
                        field: "kitID",
                        title: i18n.Resources.KitID,
                    }
                ]
            }).data("kendoGrid");

            KendoDS.bind(this.controls.form, this.controls.addModel);

            $('.chk-show-deleted', this.controls.grid.element).click(this.onShowDeleted.bind(this));
        }
    },

    onDataBound: function (e) {
        KendoDS.onDataBound(e);

        var grid = SensorSets.controls.grid;

        $(".k-grid-delete", grid.element).each(function () {
            var currentDataItem = grid.dataItem($(this).closest("tr"));
        });

        $(".k-grid-edit", grid.element).each(function () {
            var currentDataItem = grid.dataItem($(this).closest("tr"));
        });

        $(".k-grid-restore", grid.element).each(function () {
            var currentDataItem = grid.dataItem($(this).closest("tr"));
        });
    },

    ddEditor: function (container, options) {
        $('<input required data-text-field="name" data-value-field="idView" data-value-primitive="true" data-bind="value: ' + options.field + '"/>')
        .appendTo(container)
        .kendoDropDownList({
            autoBind: true,
            dataSource: SensorSets.getDatasourceDD(options.model.idView)
        });
    },

    getEmptyModel: function () {
        return {
            idView: null,
            qaStatus: null,
            kitID: null
        }
    },

    onShowDeleted: function () {
        this.isDeleted = $(e.currentTarget).prop("checked");
        this.onFilter();
    },

    onRestore: function (e) {
        var grid = Sensors.controls.grid;

        var item = grid.dataItem($(e.currentTarget).closest("tr"));
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

Datasources.bind(SensorSets.datasources);