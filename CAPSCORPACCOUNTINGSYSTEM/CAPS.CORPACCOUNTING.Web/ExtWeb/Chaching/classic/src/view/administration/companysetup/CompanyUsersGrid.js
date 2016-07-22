/**
 * The class is created to provide main UI to access Company Users .
 * Author: kamal
 * Date: 26/05/2016
 */
/**
 * @class Chaching.view.administration.organization.CompanyUsersGrid
 * UI design for Conpany Users.
 * @alias widget.administration.organizationunits.companyusersgrid
 */
Ext.define('Chaching.view.administration.companysetup.CompanyUsersGrid', {
    extend: 'Chaching.view.common.grid.ChachingGridPanel',

    requires: [
        'Chaching.view.administration.companysetup.CompanyUsersGridController',
        'Chaching.store.administration.organization.CompanyUsersStore'
    ],
    xtype: 'administration.organizationunits.companyusersgrid',
    name: 'administration.organization',
    controller: 'administration.organization.companyusersgrid',
    modulePermissions: {
        read: abp.auth.isGranted('Pages.Administration.OrganizationUnits.ManageMembers'),
        create: abp.auth.isGranted('Pages.Administration.Users.Create'),
        edit: abp.auth.isGranted('Pages.Administration.Users.Edit'),
        destroy: abp.auth.isGranted('Pages.Administration.Users.Delete')
    },
    padding: 5,
    gridId: 26,
    store: 'administration.organization.CompanyUsersStore',
    headerButtonsConfig: [
      {
          xtype: 'displayfield',
          value: abp.localization.localize("Users"),
          ui: 'headerTitle'
      }, '->', {
          xtype: 'button',
          scale: 'small',
          ui: 'actionButton',
          action: 'create',
          text: abp.localization.localize("Add").toUpperCase(),
          tooltip: app.localize('CreateNewUser'),
          checkPermission: true,
          iconCls: 'fa fa-plus',
          routeName: 'users.create',
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
    viewWndTitleConfig: {
        title: app.localize('ViewUser'),
        iconCls: 'fa fa-th'
    },
    createNewMode: 'tab',
    isSubMenuItemTab: true,
    columns: [
        {
            xtype: 'gridcolumn',
            text: app.localize('Name'),
            dataIndex: 'name',
            sortable: true,
            groupable: true,
            width: '17%',
            filterField: {
                xtype: 'textfield',
                width: '100%'
            }, editor: {
                xtype: 'textfield',
                allowBlank: false
            }
        },
        {
            xtype: 'gridcolumn',
            text: app.localize('Surname'),
            dataIndex: 'surname',
            sortable: true,
            groupable: true,
            width: '17%',
            filterField: {
                xtype: 'textfield',
                width: '100%'
            }, editor: {
                xtype: 'textfield',
                allowBlank: false
            }
        },
         {
             xtype: 'gridcolumn',
             text: app.localize('FullName'),
             dataIndex: 'fullName',
             sortable: true,
             groupable: true,
             width: '17%'
         }, {
             xtype: 'gridcolumn',
             text: app.localize('Logon'),
             dataIndex: 'userName',
             sortable: true,
             groupable: true,
             width: '17%',
             filterField: {
                 xtype: 'textfield',
                 width: '100%'
             }
         },
         {
             xtype: 'gridcolumn',
             text: app.localize('UserEmailAddress'),
             dataIndex: 'emailAddress',
             sortable: true,
             groupable: true,
             width: '20%',
             filterField: {
                 xtype: 'textfield',
                 width: '100%'
             },
             editor: {
                 xtype: 'textfield'
             }
         },
         {
             xtype: 'gridcolumn',
             text: app.localize('DefaultRole').initCap(),
             dataIndex: 'defaultRole',
             sortable: true,
             groupable: true,
             width: '15%',
             filterField: {
                 xtype: 'combobox',
                 width: '100%',
                 forceSelection: true,
                 //searchProperty: '',
                 isEnum: true,
                 displayField: 'displayName',
                 valueField: 'roleId',
                 emptyText: app.localize('SelectOption'),
                 queryMode: 'local',
                 store: 'roles.RolesStore'

             }, editor: {
                 xtype: 'combobox',
                 width: '100%',
                 displayField: 'displayName',
                 valueField: 'roleId',
                 emptyText: app.localize('SelectOption'),
                 queryMode: 'local',
                 store: 'roles.RolesStore'
             }
         },{
             xtype: 'gridcolumn',
             text: app.localize('IsActive'),
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
                     data: [{ text: 'YES', value: 'true' }, { text: 'NO', value: 'false' }]
                 }
             }, editor: {
                 xtype: 'checkbox'
             }
         },
         {
             xtype: 'gridcolumn',
             text: app.localize('IsLocked'),
             dataIndex: 'isLocked',
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
                     data: [{ text: 'YES', value: 'true' }, { text: 'NO', value: 'false' }]
                 }
             }, editor: {
                 xtype: 'checkbox'
             }
         }
    ]
});
