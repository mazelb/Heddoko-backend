/**
 * @file teams.js
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
$(function() {
    Teams.init();
});

var Teams = {
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

    datasources: function() {
        //Datasources context
        this.teams = Teams.getDatasource();

        this.teamsDD = Teams.getDatasourceDD();

        this.teamStatusTypes = new kendo.data.DataSource({
            data: _.values(Enums.TeamStatusType.array)
        });

        this.teamStatusTypes.read();
    },

    getDatasource: function() {
        return new kendo.data.DataSource({
            pageSize: KendoDS.pageSize,
            serverPaging: true,
            serverFiltering: true,
            serverSorting: false,
            transport: KendoDS.buildTransport("/admin/api/teams"),
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
                                required: true,
                                maxLengthValidation: Validator.equipment.name.maxLengthValidation
                            }
                        },
                        address: {
                            nullable: false,
                            type: "string",
                            validation: {
                                required: true,
                                maxLengthValidation: Validator.organization.address.maxLengthValidation
                            }
                        },
                        notes: {
                            nullable: false,
                            type: "string",
                            validation: {
                                required: true,
                                maxLengthValidation: Validator.equipment.notes.maxLengthValidation
                            }
                        }
                    }
                }
            }
        });
    },

    getDatasourceDD: function(id) {
        return new kendo.data.DataSource({
            serverPaging: false,
            serverFiltering: true,
            serverSorting: false,
            transport: KendoDS.buildTransport("/admin/api/teams"),
            schema: {
                data: "response",
                total: "total",
                errors: "Errors",
                model: {
                    id: "id"
                },
            },
            filter: [
                {
                    field: 'Used',
                    operator: 'eq',
                    value: id
                }
            ]
        });
    },

    init: function() {
        var control = $("#teamsGrid");
        var filter = $(".teamsFilter");
        this.controls.form = $(".teamsForm");

        if (control.length > 0) {
            this.controls.grid = control.kendoGrid({
                    dataSource: Datasources.teams,
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
                    toolbar: [
                        {
                            template:
                                '<div class="grid-checkbox"><span><input class="chk-show-deleted" type="checkbox"/>' +
                                    i18n.Resources.ShowDeleted +
                                    '</span></div>'
                        }
                    ],
                    columns: [
                        {
                            field: "idView",
                            title: i18n.Resources.ID,
                            editor: KendoDS.emptyEditor
                        }, {
                            field: 'name',
                            title: i18n.Resources.Name
                        }, {
                            field: 'address',
                            title: i18n.Resources.Address,
                            editor: KendoDS.textAreaDDEditor
                        },
                        {
                            field: "status",
                            title: i18n.Resources.Status,
                            template: function(e) {
                                return Format.team.status(e.status);
                            },
                            editor: Teams.statusTypesDDEditor
                        }, {
                            field: 'notes',
                            title: i18n.Resources.Notes,
                            template: function(e) {
                                return Format.notes(e.notes);
                            },
                            editor: KendoDS.textAreaDDEditor
                        }, {
                            command: [
                                {
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
                                }
                            ],
                            title: i18n.Resources.Actions,
                            width: '165px'
                        }
                    ],
                    save: KendoDS.onSave,
                    detailInit: this.detailInit,
                    dataBound: this.onDataBound
                })
                .data("kendoGrid");

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

            kendo.bind(this.controls.form, this.controls.addModel);

            this.validators.addModel = this.controls.form.kendoValidator({
                    validateOnBlur: true,
                    rules: {
                        maxLengthValidationNotes: Validator.equipment.notes.maxLengthValidation
                    }
                })
                .data("kendoValidator");

            $('.chk-show-deleted', this.controls.grid.element).click(this.onShowDeleted.bind(this));
        }
    },

    detailInit: function(e) {
        var datasourceItem = Users.getDatasource();

        var grid = $("<div/>")
            .appendTo(e.detailCell)
            .kendoGrid({
                dataSource: datasourceItem,
                sortable: false,
                editable: "popup",
                selectable: false,
                scrollable: false,
                resizeable: true,
                autoBind: false,
                pageable: {
                    refresh: true,
                    pageSizes: [10, 50, 100]
                },
                columns: [
                    {
                        field: 'username',
                        title: i18n.Resources.Username,
                        editor: KendoDS.emptyEditor
                    },
                    {
                        field: 'name',
                        title: i18n.Resources.Name,
                        editor: KendoDS.emptyEditor
                    },
                    {
                        field: 'phone',
                        title: i18n.Resources.Phone,
                        editor: KendoDS.emptyEditor
                    },
                    {
                        field: 'email',
                        title: i18n.Resources.Email,
                        editor: KendoDS.emptyEditor
                    },
                    {
                        field: 'status',
                        title: i18n.Resources.Status,
                        editor: Users.statusDDEditor,
                        template: function (e) {
                            return Format.user.status(e.status);
                        }
                    },
                    {
                        command: [
                            {
                                name: "edit",
                                text: i18n.Resources.Edit,
                                className: "k-grid-edit"
                            },
                            {
                                text: i18n.Resources.ResendActivation,
                                className: "k-grid-resend",
                                click: Teams.onResendActivation.bind(this)
                            }
                        ],
                        title: i18n.Resources.Actions,
                        width: '165px'
                    }
                ],
                save: KendoDS.onSave,
                edit: function (ed) {
                    ed.model.set("teamID", e.data.id);
                },
                dataBound: this.onDataBound
            }).data("kendoGrid");

        KendoDS.bind(grid, true);

        datasourceItem.filter({
            field: "TeamID",
            operator: "eq",
            value: parseInt(e.data.id)
        });
    },

    onDataBound: function(e) {
        KendoDS.onDataBound(e);

        var grid = Teams.controls.grid;
        var enumarable = Enums.TeamStatusType.enum;

        $(".k-grid-delete", grid.element)
            .each(function() {
                var currentDataItem = grid.dataItem($(this).closest("tr"));

                if (currentDataItem.status === enumarable.Deleted) {
                    $(this).remove();
                }
            });

        $(".k-grid-edit", grid.element)
            .each(function() {
                var currentDataItem = grid.dataItem($(this).closest("tr"));

                if (currentDataItem.status === enumarable.Deleted) {
                    $(this).remove();
                }
            });

        $(".k-grid-restore", grid.element)
            .each(function() {
                var currentDataItem = grid.dataItem($(this).closest("tr"));

                if (currentDataItem.status === enumarable.Active) {
                    $(this).remove();
                }
            });
    },

    ddEditor: function(container, options) {
        $('<input required data-text-field="nameView" data-value-field="id" data-value-primitive="true" data-bind="value: ' + options.field + '"/>')
            .appendTo(container)
            .kendoDropDownList({
                autoBind: true,
                dataSource: Teams.getDatasourceDD(options.model.id)
            });
    },

    statusTypesDDEditor: function(container, options) {
        $('<input required data-text-field="text" data-value-field="value" data-value-primitive="true" data-bind="value: ' + options.field + '"/>')
            .appendTo(container)
            .kendoDropDownList({
                autoBind: true,
                dataSource: Datasources.kitStatusTypes
            });
    },

    getEmptyModel: function() {
        return {
            name: null,
            address: null,
            notes: null,
            status: Enums.TeamStatusType.Active
        };
    },

    onShowDeleted: function(e) {
        this.isDeleted = $(e.currentTarget).prop("checked");
        this.onFilter();
    },

    onRestore: function(e) {
        var grid = Teams.controls.grid;

        var item = grid.dataItem($(e.currentTarget).closest("tr"));
        item.set("status", Enums.TeamStatusType.enum.Active);
        grid.dataSource.sync();
    },

    onReset: function(e) {
        this.controls.addModel.set("model", this.getEmptyModel());
    },

    onAdd: function(e) {
        Notifications.clear();
        if (this.validators.addModel.validate()) {
            var obj = this.controls.addModel.get("model");

            this.controls.grid.dataSource.add(obj);
            this.controls.grid.dataSource.sync();
            this.controls.grid.dataSource.one("requestEnd",
                function(ev) {
                    if (ev.type === "create" && !ev.response.Errors) {
                        Datasources.teamsDD.read();
                        this.onReset();
                    }
                }.bind(this));
        }
    },

    onEnter: function(e) {
        if (e.keycode === kendo.keys.ENTER) {
            this.onFilter(e);
        }
    },

    onFilter: function(e) {
        var filters = this.buildFilter();
        if (filters) {
            this.controls.grid.dataSource.filter(filters);
        }
    },

    onResendActivation: function (e) {
        e.preventDefault();

        var item = Users.controls.grid.dataItem($(e.currentTarget).closest("tr"));

        Ajax.post("/admin/api/users/activation/resend",
       {
           userId: item.id
       }).success(this.onResendActivationSuccess);
    },
    onResendActivationSuccess: function (e) {
        if (e) {
            Notifications.info(i18n.Resources.EmailHasBeenSent);
        } else {
            Notifications.error(i18n.Resources.Error);
        }
    },

    buildFilter: function(search) {
        Notifications.clear();
        search = this.controls.filterModel.search;

        var filters = [];

        if (typeof (search) !== "undefined" && search !== "" && search !== null) {
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

Datasources.bind(Teams.datasources);