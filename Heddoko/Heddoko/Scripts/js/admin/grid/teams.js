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
        addModel: null,
        workerModel: null,
        teamOptions: null
    },

    validators: {
        modelValidator: null,
        workerValidator: null
    },

    datasources: function() {
        //Datasources context
        this.teams = Teams.getDatasource();

        this.teamsDD = Teams.getDatasourceDD();

        this.teamUsers = Teams.getUsersDatasource();

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

    getUsersDatasource: function() {
        return new kendo.data.DataSource({
            pageSize: KendoDS.pageSize,
            serverPaging: true,
            serverFiltering: true,
            serverSorting: false,
            transport: KendoDS.buildTransport('/admin/api/users'),
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
                        email: {
                            nullable: false,
                            type: "string",
                            validation: {
                                required: true,
                                maxLengthValidation: Validator.user.email.maxLengthValidation
                            }
                        },
                        username: {
                            nullable: false,
                            type: "string",
                            validation: {
                                required: true
                            }
                        },
                        firstname: {
                            nullable: false,
                            type: "string",
                            validation: {
                                required: true,
                                maxLengthValidation: Validator.user.firstname.maxLengthValidation
                            }
                        },
                        lastname: {
                            nullable: false,
                            type: "string",
                            validation: {
                                required: true,
                                maxLengthValidation: Validator.user.lastname.maxLengthValidation
                            }
                        },
                        phone: {
                            nullable: false,
                            type: "string",
                            validation: {
                                required: true,
                                maxLengthValidation: Validator.user.phone.maxLengthValidation
                            }
                        },
                        licenseID: {
                            nullable: true,
                            type: "number",
                            validation: {
                                required: true,
                                min: 0,
                                max: KendoDS.maxInt
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
                        },
                        teamID: {
                            nullable: true,
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

    init: function() {
        var control = $("#teamsGrid");
        var filter = $(".teamsFilter");
        var options = $(".teamOptions");
        var worker = $(".workerForm");
        this.controls.form = $(".teamsForm");

        var datasourceItem = Datasources.teamUsers;

        if (control.length > 0) {
            this.controls.grid = control.kendoGrid({
                    dataSource: datasourceItem,
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
                        field: 'role',
                        title: i18n.Resources.Role,
                        editor: KendoDS.emptyEditor,
                        template: function (e) {
                            return Format.user.role(e.role);
                        }
                    },
                    {
                        field: 'status',
                        title: i18n.Resources.Status,
                        editor: Users.statusDDEditor,
                        template: function (e) {
                            return Format.user.status(e.status);
                        }
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

            this.controls.workerModel = kendo.observable({
                reset: this.onWorkerReset.bind(this),
                submit: this.onWorkerAdd.bind(this),
                model: this.getEmptyWorker()
            });

            kendo.bind(worker, this.controls.workerModel);

            this.validators.workerValidator = worker.kendoValidator({
                validateOnBlur: true,
                rules: {
                    maxLengthValidationName: Validator.organization.name.maxLengthValidation
                }
            }).data("kendoValidator");

            this.controls.teamOptions = kendo.observable({
                teams: Datasources.teamsDD,
                options: this.getEmptyOptions,
                changeTeamShown: this.onTeamChange.bind(this)
            });

            kendo.bind(options, this.controls.teamOptions);

            datasourceItem.filter({
                field: "TeamID",
                operator: "eq",
                value: parseInt(this.controls.teamOptions.options.teamID)
            });

            $('.chk-show-deleted', this.controls.grid.element).click(this.onShowDeleted.bind(this));
        }
    },

    /*detailInit: function(e) {
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
    },*/

    onDataBound: function(e) {
        KendoDS.onDataBound(e);

        var grid = Teams.controls.grid;
        var enumarable = Enums.UserStatusType.enum;

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

                if (!(currentDataItem.status === enumarable.Deleted)) {
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

    getEmptyOptions: function() {
        return {
            teamID: null
        };
    },

    getEmptyWorker: function() {
        return {
            username: null,
            email: null,
            role: 4 //Worker
        }
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

            /*this.controls.teamOptions.dataSource.add(obj);
            this.controls.grid.dataSource.sync();
            this.controls.grid.dataSource.one("requestEnd",
                function(ev) {
                    if (ev.type === "create" && !ev.response.Errors) {
                        Datasources.teamsDD.read();
                        this.onReset();
                    }
                }.bind(this));*/
        }
    },

    onWorkerReset: function(e) {
        this.controls.workerModel.set("model", this.getEmptyWorker());
    },

    onWorkerAdd: function(e) {
        Notifications.clear();
        if (this.validators.workerValidator.validate()) {
            var obj = this.controls.workerModel.get("model");
            obj.teamID = this.controls.teamOptions.options.teamID;;

            this.controls.grid.dataSource.add(obj);
            this.controls.grid.dataSource.sync();
            this.controls.grid.dataSource.one("requestEnd",
                function (ev) {
                    if (ev.type === "create" && !ev.response.Errors) {
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

    onTeamChange: function(e) {
        var filters = this.buildFilter();

        Datasources.teamUsers.filter(filters);
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
        team = this.controls.teamOptions.options.teamID;

        var filters = [];

        if (typeof (search) !== "undefined" && search !== "" && search !== null) {
            filters.push({
                field: "Search",
                operator: "eq",
                value: search
            });
        }

        filters.push({
            field: "TeamID",
            operator: "eq",
            value: parseInt(team)
        });

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