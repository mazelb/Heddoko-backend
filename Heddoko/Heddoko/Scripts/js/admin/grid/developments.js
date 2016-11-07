$(function() {
    Developments.init();
});

var Developments = {
    isDeleted: false,
    controls: {
        form: null,
        grid: null,
        filterModel: null,
        addModel: null,
        btnUpload: null
    },

    validators: {
        modelValidator: null
    },

    datasources: function() {
        //Datasources context
        this.developments = Developments.getDatasource();
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
                            type: "string"
                        },
                        client: {
                            nullable: false,
                            type: "string"
                        },
                        secret: {
                            nullable: false,
                            type: "string"
                        },
                        enabled: {
                            editable: true,
                            type: "boolean"
                        }
                    }
                }
            }
        });
    },

    init: function() {
        var control = $('#developmentsGrid');
        this.controls.form = $('.developmentsForm');

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
                            title: i18n.Resources.Name                         
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
                            editable: true,
                            title: i18n.Resources.Status,
                            template: function (ed) {
                                return Format.developments.enabled(ed.enabled);
                            },
                            editor: KendoDS.emptyEditor
                        },
                        {
                            command: [
                                {
                                    name: "edit",
                                    text: i18n.Resources.Edit,
                                    className: "k-grid-edit"
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

            this.controls.addModel = kendo.observable({
                reset: this.onReset.bind(this),
                submit: this.onAdd.bind(this),
                model: this.getEmptyModel()
            });

            kendo.bind(this.controls.form, this.controls.addModel);

            this.validators.addModel = this.controls.form.kendoValidator({
                validateonBlur: true,
                rules: {
                    maxLengthValidationLocation: Validator.equipment.location.maxLengthValidation
                }
            })
                .data("kendoValidator");
        }
    },
    getEmptyModel: function () {
        return {
            name: null,
            client: null,
            secret: null,
            enabled: false
        };
    },
    onReset: function (e) {
        this.controls.addModel.set('model', this.getEmptyModel());
    },
    onAdd: function (e) {
        Notifications.clear();
        if (this.validators.addModel.validate()) {
            var obj = this.controls.addModel.get('model');

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
};

Datasources.bind(Developments.datasources);