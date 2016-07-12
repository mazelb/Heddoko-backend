$(function () {
    PantsOctopi.init();
});

var PantsOctopi = {
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
        this.pantsOctopi = new kendo.data.DataSource({
            pageSize: KendoDS.pageSize,
            serverPaging: true,
            serverFiltering: true,
            serverSorting: false,
            transport: KendoDS.buildTransport('/admin/api/pantsoctopi'),
            schema: {
                data: "response",
                total: "total",
                errors: "errors",
                model: {
                    id: "id",
                    fields: {
                        id: {
                            editable: false,
                            nullable: true
                        },
                        size: {
                            nullable: false,
                            type: "number",
                            validation: {
                                required: true,
                                max: KendoDS.maxInt
                            }
                        },
                        location: {
                            nullable: false,
                            type: "string",
                            validation: {
                                required: true,
                                maxLengthValidation: Validator.organization.notes.maxLengthValidation
                            }
                        },
                        status: {
                            nullable: false,
                            type: "number",
                            validation: {
                                required: true,
                                min: 0,
                                max: kendoDS.maxInt
                            }
                        },
                        qaStatus: {
                            nullable: false,
                            type: "number",
                            validation: {
                                required: true,
                                min: 0,
                                max: kendoDS.maxInt
                            }
                        }
                    }
                }
            }
        });
    },

    init: function () {
        var control = $("#pantsoctopiGrid");
        var filter = $('.pantsoctopiFilter');
        var model = $('.pantsoctopiForm');

        if (control.length > 0) {
            this.controls.grid = control.kendoGrid({
                dataSource: DataSources.organizations,
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
                    template: '<div class="grid-checkbox"><span><input id="chk-show-deleted" type="checkbox"/>' + i18n.Resources.ShowDeleted + '</span></div>'
                }],
                columns: [
                {
                    field: 'id',
                    title: i18n.Resources.ID
                },
                {
                    field: 'size',
                    title: i18n.Resources.Size
                },
                {
                    field: 'location',
                    title: i18n.Resources.PhysicalLocation
                },
                {
                    field: 'status',
                    title: i18n.Resources.Status
                },
                {
                    field: 'qastatus',
                    title: i18n.Resources.QAStatus
                }
                ],
                save: KendoDS.onSave,
                detailInit: this.detailInit,
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
                model: this.getEmptyModel()
            });

            kendo.bind(model, this.controls.addModel);

            this.validators.addModel = model.kendoValidator({
                validateOnBlur: true,
                rules: {
                    maxLengthValidationLocation: Validator.organization.location.maxLengthValidation
                }
            }).data("kendoValidator");

            $('$chk-show-deleted', Organizations.controls.grid.element).click(this.onShowDeleted.bind(this));
        }
    },

    detailInit: function (e) {
        //TODO: BenB
        var grid = $("<div>").appendTo(e.detail.Cell).kendoGrid({

        }).data("kendoGrid");
    },

    sizeDDEditor: function (container, options) {

    },

    getEmptyModel: function () {
        return {
            id: null,
            size: null,
            location: null,
            status: null,
            qaStatus: null
        };
    },

    onReset: function (e) {

    },

    onAdd: function (e) {

    },

    onEnter: function (e) {

    },

    onFilter: function (e) {

    },

    buildFilter: function (search) {

    }
};

//Datasources.bind(PantsOctopi.datasources);