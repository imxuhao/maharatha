
Ext.define('Chaching.view.users.UsersGrid', {
    extend: 'Chaching.view.common.grid.ChachingGridPanel',

    requires: [
        'Chaching.view.users.UsersGridController',
        'Chaching.view.users.UsersGridModel'
    ],

    controller: 'users-usersgrid',
    viewModel: {
        type: 'users-usersgrid'
    },

    xtype: 'users',
    store: 'users.UsersStore',
    name: 'Administration.Users',
    padding: 5,
    headerButtonsConfig: [
    {
        xtype: 'displayfield',
        value: abp.localization.localize("Users"),
        ui: 'headerTitle'
    }, {
        xtype: 'displayfield',
        value: abp.localization.localize("UsersHeaderInfo"),
        ui: 'headerSubTitle'
    }, '->', {
        xtype: 'button',
        scale: 'small',
        ui: 'actionButton',
        action: 'create',
        text: abp.localization.localize("CreateNewUser").toUpperCase(),
        tooltip: app.localize('CreateNewUser'),
        checkPermission: true,
        iconCls: 'fa fa-plus',
        iconAlign: 'left'
    }],
    requireExport: true,
    requireMultiSearch: true,
    requireMultisort: true,
    isEditable: true,
    editingMode: 'row',
    columnLines: true,
    multiColumnSort: true,
    editWndTitleConfig: {
        title: app.localize('EditUser'),
        iconCls: 'fa fa-pencil'
    },
    createWndTitleConfig: {
        title: app.localize('CreateNewUser'),
        iconCls: 'fa fa-plus'
    },
    createNewMode: 'popup',
    columns: [
        {
            xtype: 'gridcolumn',
            text: app.localize('UserName'),
            dataIndex: 'userName',
            stateId: 'userName',
            sortable: true,
            width: '10%',
            groupable: true,
            // simplest filter configuration
            filterField: {
                xtype: 'textfield',
                width: '100%',
                emptyText: 'Enter User Name to search'
            }

        }, {
            xtype: 'gridcolumn',
            text: app.localize('Name'),
            dataIndex: 'name',
            sortable: true,
            groupable: true,
            width: '10%'
            // equivalent to filterField:true
            // as textfield is created by default
            ,
            filterField: {
                xtype: 'textfield',
                width: '100%',
                emptyText: 'Enter Name to search'
            },
            editor: {
                xtype: 'textfield'
            }
        }
        , {
            xtype: 'gridcolumn',
            text: app.localize('Surname'),
            dataIndex: 'surname',
            sortable: true,
            groupable: true,
            width: '10%',
            filterField: {
                xtype: 'textfield',
                width: '100%',
                emptyText: 'Enter Surname to search'
            },
            editor: {
                xtype: 'textfield'
            }
        },
    {
        xtype: 'gridcolumn',
        text: app.localize('Roles'),
        dataIndex: 'roles',
        sortable: true,
        groupable: true,
        width: '5%'
        ,
        renderer: function (val) {
            var rolesList = '';
            Ext.each(val, function (roles, index) {
                rolesList = rolesList + roles.roleName + ',';
            });
            return rolesList;
        }
    }
         , {
             xtype: 'gridcolumn',
             text: app.localize('EmailAddress'),
             dataIndex: 'emailAddress',
             sortable: true,
             groupable: true,
             width: '20%',
             filterField: {
                 xtype: 'textfield',
                 width: '100%',
                 emptyText: 'Enter Email Address to search'
             },
             editor: {
                 xtype: 'textfield'
             }
         }
        ,
        {
            xtype: 'gridcolumn',
            text: app.localize('EmailConfirm'),
            dataIndex: 'isEmailConfirmed',
            sortable: true,
            groupable: true,
            width: '5%',
            renderer: function (val) {
                if (val) return 'YES';
                else return 'NO';
            },
            filterField: {
                xtype: 'combobox',
                valueField: 'value',
                displayField: 'text',
                store: {
                    fields: [{ name: 'text' }, { name: 'value' }],
                    data: [{ text: 'YES', value: true }, { text: 'NO', value: false }]
                }
            }
        }
        ,
         {
             xtype: 'gridcolumn',
             text: app.localize('LastLoginTime'),
             dataIndex: 'lastLoginTime',
             sortable: true,
             groupable: true,
             width: '15%',
             renderer: Ext.util.Format.dateRenderer('m-d-Y'),
             filterField: {
                 xtype: 'datefield',
                 width: '100%'
             }
         }

        ,
        {
            xtype: 'gridcolumn',
            text: app.localize('Active'),
            dataIndex: 'isActive',
            sortable: true,
            groupable: true,
            width: '5%',
            renderer: function (val) {
                if (val) return 'YES';
                else return 'NO';
            },
            filterField: {
                xtype: 'combobox',
                valueField: 'value',
                displayField: 'text',
                store: {
                    fields: [{ name: 'text' }, { name: 'value' }],
                    data: [{ text: 'YES', value: true }, { text: 'NO', value: false }]
                }
            }, editor: {
                xtype: 'checkbox'
            }
        }
         ,
         {
             xtype: 'gridcolumn',
             text: app.localize('CreationTime'),
             dataIndex: 'creationTime',
             sortable: true,
             groupable: true,
             width: '20%',
             renderer: Ext.util.Format.dateRenderer('m-d-Y g:i A'),
             filterField: {
                 xtype: 'datefield',
                 width: '100%'
             }
         }
    ]
});
