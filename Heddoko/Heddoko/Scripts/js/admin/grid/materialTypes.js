$(function () {
    Datasources.bind(MaterialTypes.datasources);
    MaterialTypes.init();
});

var MaterialTypes = {
    controls: {
        grid: null,
        filterModel: null,
        addModel: null
    },
    validators: {
        modelValidator: null
    },
    datasources: function () {
        //Datasources context
        this.materialTypes = new kendo.data.DataSource({
            pageSize: KendoDS.pageSize,
            serverPaging: true,
            serverFiltering: true,
            serverSorting: false,
            transport: KendoDS.buildTransport('/admin/api/materialTypes'),
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
                        identifier: {
                            nullable: false,
                            type: "string",
                            validation: {
                                required: true,
                                maxLengthValidation: Validator.materialType.maxLengthValidation
                            }
                        }
                    }
                }
            }
        });
    },
    init: function () {
        var control = $("#materialTypes");
        var filter = $('.matrialTypesFilter');
        var model = $('.matrialTypesForm');

        if (control.length > 0) {
            this.controls.grid = control.kendoGrid({
                dataSource: Datasources.materialTypes,
                sortable: false,
                editable: "popup",
                selectable: false,
                scrollable: false,
                resizable: true,
                autoBind: true,
                pageable: {
                    refresh: true,
                    pageSizes: [10, 50, 100]
                },
                columns: [{
                    field: 'identifier',
                    title: i18n.Resources.Identifier
                }, {
                    command: [{
                        name: "edit",
                        text: i18n.Resources.Edit,
                        className: "k-grid-edit"
                    }, {
                        name: "destroy",
                        text: i18n.Resources.Delete,
                        className: "k-grid-delete"
                    }],
                    title: i18n.Resources.Actions,
                    width: '209px'
                }
                ],
                save: KendoDS.onSave,
                dataBound: KendoDS.onDataBound
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
                identifier: null
            });

            kendo.bind(model, this.controls.addModel);

            this.validators.addModel = model.kendoValidator({
                validateOnBlur: true,
                rules: {
                    maxLengthValidation: Validator.materialType.maxLengtfhValidation
                }
            }).data("kendoValidator");
        }
    },
    onReset: function(e) {
        this.controls.addModel.set('identifier', null);
    },
    onAdd: function(e) {
        Notifications.clear();
        if (this.validators.addModel.validate()) {
            var obj = {
                identifier: this.controls.addModel.identifier
            };

            this.controls.grid.dataSource.add(obj);
            this.controls.grid.dataSource.sync();
            this.controls.grid.dataSource.one('requestEnd', function (e) {
                if (e.type === "create") {
                    if (!e.response.Errors) {
                        this.onReset();
                    }
                }
            }.bind(this));
        }
    },
    onEnter: function (e) {
        if (e.keyCode == kendo.keys.ENTER) {
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
        var search = this.controls.filterModel.search;

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

        return filters.length == 0 ? {} : filters;
    }
};