/**
 * @file users.js
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
$(function () {
    Users.init();
});

var Users = {
    isDeleted: false,
    controls: {
        grid: null,
        filterModel: null,
        addModel: null
    },
    validators: {
        addModel: null
    },
    datasources: function () {
        //Datasources context
        this.usersAdminDD = new kendo.data.DataSource({
            serverPaging: false,
            serverFiltering: false,
            serverSorting: false,
            transport: KendoDS.buildTransport('/admin/api/users'),
            schema: {
                data: "response",
                total: "total",
                errors: "Errors",
                model: {
                    id: "id"
                }
            },
            filter: [{
                field: 'Admin',
                operator: 'eq',
                value: false
            }]
        });

        this.userStatusTypes = new kendo.data.DataSource({
            data: _.values(_.filter(Enums.UserStatusType.array, function (u) { return u.value != Enums.UserStatusType.enum.Banned && u.value != Enums.UserStatusType.enum.Deleted }))
        });

        this.users = Users.getDatasource();

        this.usersDD = Users.getDatasourceDD();

        this.userRoles = new kendo.data.DataSource({
            data: _.values(_.filter(Enums.UserRoleType.array, function (u) {
                return u.value != Enums.UserRoleType.enum.Admin && u.value != Enums.UserRoleType.enum.User
                        && u.value != Enums.UserRoleType.enum.ServiceAdmin && u.value != Enums.UserRoleType.enum.LicenseUniversal
            }))
        });
    },

    getDatasourceDD: function (id) {
        return new kendo.data.DataSource({
            serverPaging: false,
            serverFiltering: true,
            serverSorting: false,
            transport: KendoDS.buildTransport('/admin/api/users'),
            schema: {
                data: "response",
                total: "total",
                errors: "Errors",
                model: {
                    id: "id"
                }
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

    ddEditor: function (container, options) {
        $('<input required data-text-field="name" data-value-field="id"  data-value-primitive="true" data-bind="value:' + options.field + '"/>')
        .appendTo(container)
        .kendoDropDownList({
            autoBind: true,
            dataSource: Datasources.usersAdminDD
        });
    },
    getDatasource: function () {
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

    init: function () {
        var control = $("#usersGrid");
        var filter = $('.usersFilter');
        var model = $('.usersForm');

        var datasourceItem = Datasources.users;

        if (control.length > 0) {
            this.controls.grid = control.kendoGrid({
                dataSource: datasourceItem,
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
                toolbar: [{
                    template: '<div class="grid-checkbox"><span><input id="chk-show-deleted" type="checkbox"/>' + i18n.Resources.ShowDeleted + '</span></div>'
                }],
                columns: [{
                    field: 'username',
                    title: i18n.Resources.Username,
                    editor: KendoDS.emptyEditor
                }, {
                    field: 'firstname',
                    hidden: true,
                    title: i18n.Resources.Firstname
                }, {
                    field: 'lastname',
                    hidden: true,
                    title: i18n.Resources.Lastname
                }, {
                    field: 'phone',
                    title: i18n.Resources.Phone,
                    editor: KendoDS.phoneEditor
                }, {
                    field: 'email',
                    title: i18n.Resources.Email,
                    editor: KendoDS.emailEditor
                }, {
                    field: 'role',
                    title: i18n.Resources.Role,
                    editor: Users.userRolesDDEditor,
                    template: function (e) {
                        return Format.user.role(e.role);
                    }
                }, {
                    field: 'teamID',
                    title: i18n.Resources.Team,
                    editor: Teams.ddEditor,
                    template: function (e) {
                        return Format.user.team(e);
                    }
                }, {
                    field: 'status',
                    title: i18n.Resources.Status,
                    editor: Users.statusDDEditor,
                    template: function (e) {
                        return Format.user.status(e.status);
                    }
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
                    }, {
                        text: i18n.Resources.ResendActivation,
                        className: "k-grid-resend",
                        click: this.onResendActivation.bind(this)
                    }],
                    title: i18n.Resources.Actions,
                    width: '165px'
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
                teamID: null,
                teams: Datasources.teamsDD,
                keyup: this.onEnter.bind(this),
                click: this.onFilter.bind(this),
                changeTeamShown: this.onTeamChange.bind(this)
            });

            kendo.bind(filter, this.controls.filterModel);

            this.controls.addModel = kendo.observable({
                reset: this.onReset.bind(this),
                submit: this.onAdd.bind(this),
                teams: Datasources.teamsDD,
                roles: Datasources.userRoles,
                model: this.getEmptyModel()
            });

            kendo.bind(model, this.controls.addModel);

            this.validators.addModel = model.kendoValidator({
                validateOnBlur: true,
                rules: {
                    maxLengthValidationName: Validator.organization.name.maxLengthValidation,
                    maxLengthValidationPhone: Validator.organization.phone.maxLengthValidation,
                    maxLengthValidationAddress: Validator.organization.address.maxLengthValidation,
                    maxLengthValidationNotes: Validator.organization.notes.maxLengthValidation
                }
            }).data("kendoValidator");

            $('#chk-show-deleted', Users.controls.grid.element).click(this.onShowDeleted.bind(this));
        }
    },
    onDataBound: function (e) {
        KendoDS.onDataBound(e);
        var enumerable = Enums.UserStatusType.enum;

        $(".k-grid-delete", Users.controls.grid.element).each(function () {
            var currentDataItem = Users.controls.grid.dataItem($(this).closest("tr"));
            if (currentDataItem) {
                if (currentDataItem.status == enumerable.Deleted) {
                    $(this).remove();
                }
            }
        });

        $(".k-grid-edit", Users.controls.grid.element).each(function () {
            var currentDataItem = Users.controls.grid.dataItem($(this).closest("tr"));
            if (currentDataItem) {
                if (currentDataItem.status == enumerable.Deleted) {
                    $(this).remove();
                }
            }
        });

        $(".k-grid-restore", Users.controls.grid.element).each(function () {
            var currentDataItem = Users.controls.grid.dataItem($(this).closest("tr"));
            if (currentDataItem) {
                if (currentDataItem.status != enumerable.Deleted) {
                    $(this).remove();
                }
            }
        });

        $(".k-grid-resend", Users.controls.grid.element)
          .each(function () {
              var currentDataItem = Users.controls.grid.dataItem($(this).closest("tr"));

              if (currentDataItem.status !== enumerable.Invited) {
                  $(this).remove();
              }
          });

    },
    onShowDeleted: function (e) {
        this.isDeleted = $(e.currentTarget).prop('checked');
        this.onFilter();
    },
    onRestore: function (e) {
        var item = Users.controls.grid.dataItem($(e.currentTarget).closest("tr"));
        item.set('status', Enums.UserStatusType.enum.Active);
        Users.controls.grid.dataSource.sync();
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
    statusDDEditor: function (container, options) {
        $('<input required data-text-field="text" data-value-field="value"  data-value-primitive="true" data-bind="value:' + options.field + '"/>')
        .appendTo(container)
        .kendoDropDownList({
            autoBind: true,
            dataSource: Datasources.userStatusTypes
        });
    },
    userRolesDDEditor: function (container, options) {
        $('<input required data-text-field="text" data-value-field="value"  data-value-primitive="true" data-bind="value:' + options.field + '"/>')
        .appendTo(container)
        .kendoDropDownList({
            autoBind: true,
            dataSource: Datasources.userRoles
        });
    },
    getEmptyModel: function () {
        return {
            email: null,
            username: null,
            firstname: null,
            lastname: null,
            role: null,
            teamID: null
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
    getEmptyOptions: function() {
        return {
            teamID: null
        };
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
    onTeamChange: function (e) {
        var filters = this.buildFilter();
        Datasources.users.filter(filters);
    },
    buildFilter: function (search) {
        Notifications.clear();
        var search = this.controls.filterModel.search;
        var team = this.controls.filterModel.teamID;

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

        if (typeof (team) !== "undefined"
         && team !== 0
         && team !== null) {
            filters.push({
                field: "TeamID",
                operator: "eq",
                value: parseInt(team)
            });
        }

        if (this.isDeleted) {
            filters.push({
                field: "IsDeleted",
                operator: "eq",
                value: true
            });
        }

        return filters.length == 0 ? {} : filters;
    }
};

Datasources.bind(Users.datasources);