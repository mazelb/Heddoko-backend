/**
 * @file usersAdmin.js
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
$(function () {
    UsersAdmin.init();
});

var UsersAdmin = {
    isDeleted: false,
    controls: {
        grid: null,
        filterModel: null,
        addModel: null
    },
    validators: {
        addModel: null
    },
    init: function () {
        var control = $("#usersAdminGrid");
        var filter = $('.usersAdminFilter');

        if (control.length > 0) {
            this.controls.grid = control.kendoGrid({
                dataSource: Datasources.users,
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
                toolbar:[
                    {
                        template:
                             '<div class="grid-checkbox"><span><input class="chk-show-deleted" type="checkbox"/>' +
                                i18n.Resources.ShowDeleted +
                                '</span></div>'
                    }
                ],
                columns: [{
                    field: 'organizationName',
                    title: i18n.Resources.Organization,
                    editor: KendoDS.emptyEditor
                }, {
                    field: 'name',
                    title: i18n.Resources.Name,
                    editor: KendoDS.emptyEditor
                }, {
                    field: 'phone',
                    title: i18n.Resources.Phone,
                    editor: KendoDS.emptyEditor
                }, {
                    field: 'username',
                    title: i18n.Resources.Username,
                    editor: KendoDS.emptyEditor
                }, {
                    field: 'email',
                    title: i18n.Resources.Email,
                    editor: KendoDS.emptyEditor
                }, {
                    field: 'role',
                    title: i18n.Resources.Role,
                    editor: KendoDS.emptyEditor,
                    template: function (e) {
                        return Format.user.role(e.role);
                    }
                },{
                    field: 'kitID',
                    title: i18n.Resources.Kit,
                    editor: KendoDS.emptyEditor,
                    template: function (e) {
                        return Format.user.kit(e);
                    }
                }, {
                    field: 'teamID',
                    title: i18n.Resources.Team,
                    editor: KendoDS.emptyEditor,
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
                        text: i18n.Resources.ResendActivation,
                        className: "k-grid-resend",
                        click: this.onResendActivation.bind(this)
                    }],
                    title: i18n.Resources.Actions,
                    width: '165px'
                }
                ],
                save: KendoDS.onSave,
                dataBound: this.onDataBound
            }).data("kendoGrid");

            KendoDS.bind(this.controls.grid, true);

            $('.chk-show-deleted', this.controls.grid.element).click(this.onShowDeleted.bind(this));

            this.controls.filterModel = kendo.observable({
                find: this.onFilter.bind(this),
                search: null,
                keyup: this.onEnter.bind(this)
            });

            kendo.bind(filter, this.controls.filterModel);
        }
    },
    onEnter: function (e) {
        if (e.keyCode == kendo.keys.ENTER) {
            this.isDeleted = $(e.currentTarget).prop('checked');
            this.onFilter();
        }
    },
    onShowDeleted: function (e) {
        this.isDeleted = $(e.currentTarget).prop('checked');
        this.onFilter();
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

        return filters.length == 0 ? {} : filters;
    },
    onDataBound: function (e) {
        KendoDS.onDataBound(e);

        $(".k-grid-resend", UsersAdmin.controls.grid.element)
          .each(function () {
              var currentDataItem = UsersAdmin.controls.grid.dataItem($(this).closest("tr"));

              if (currentDataItem.status !== Enums.UserStatusType.enum.Invited) {
                  $(this).remove();
              }
          });

    },
    onResendActivation: function (e) {
        e.preventDefault();

        var item = UsersAdmin.controls.grid.dataItem($(e.currentTarget).closest("tr"));

        Ajax.post("/admin/api/users/activation/resend",
       {
           userId: item.id
       }).success(this.onResendActivationSuccess);
    },
    onResendActivationSuccess: function(e) {
        if (e) {
            Notifications.info(i18n.Resources.EmailHasBeenSent);
        } else {
            Notifications.error(i18n.Resources.Error);
        }
    }
};
