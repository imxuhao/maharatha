
Ext.define('Chaching.view.users.UsersForm', {
    extend: 'Chaching.view.common.form.ChachingFormPanel',
    alias: ['widget.users.create', 'widget.users.edit'],
    requires: [
        'Chaching.view.users.UsersFormController'
    ],
    modulePermissions: {
        read: abp.auth.isGranted('Pages.Administration.Users'),
        create: abp.auth.isGranted('Pages.Administration.Users.Create'),
        edit: abp.auth.isGranted('Pages.Administration.Users.Edit'),
        destroy: abp.auth.isGranted('Pages.Administration.Users.Delete')
    },
    controller: 'users-usersform',
    name: 'Administration.Users',
    openInPopupWindow: false,
    hideDefaultButtons: false,
    layout: 'fit',
    scrollable: true,
    border: false,
    showFormTitle: false,
    displayDefaultButtonsCenter: true,
    defaults: {
        bodyStyle: { 'background-color': 'trasparent' },
        labelAlign: 'left'
    },
    defaultFocus: 'textfield#name',
    popupWndSize: {
        height: '90%',
        width: '90%'
    },
    listeners: {
        resize:'onUserFormResize'
    },
    items: [{
        xtype: 'tabpanel',
        ui: 'formTabPanels',
        //ui: 'submenuTabs',
        items : [{
            title: abp.localization.localize("UserInformations").initCap(),
            padding: '0 10 0 10',
            scrollable: true,
            iconCls: 'fa fa-user',
            defaultFocus: 'textfield#name',
            defaults: {
                labelWidth: 140
            },
            items: [{
                xtype: 'hiddenfield',
                name: 'id',
                value: 0
            },
            {
                xtype: 'textfield',
                name: 'name',
                itemId: 'name',
                fieldLabel: app.localize('Name'),
                width: '100%',
                ui: 'fieldLabelTop',
                emptyText: app.localize('UName')
            }, {
                xtype: 'textfield',
                name: 'surname',
                fieldLabel: app.localize('Surname'),
                width: '100%',
                ui: 'fieldLabelTop',
                emptyText: app.localize('USurname')
            },
            {
                xtype: 'textfield',
                name: 'emailAddress',
                fieldLabel: app.localize('EmailAddress'),
                width: '100%',
                ui: 'fieldLabelTop',
                emptyText: app.localize('UEmailAddress')
            },
            {
                xtype: 'textfield',
                name: 'userName',
                fieldLabel: app.localize('UserName'),
                width: '100%',
                ui: 'fieldLabelTop',
                emptyText: app.localize('UUserName')
            }
            ,
            {
                xtype: 'checkbox',
                boxLabel: app.localize('SetRandomPassword'),
                name: 'setRandomPassword',
                reference: 'isSetRandomPassword',
                labelAlign: 'right',
                inputvalue: true,
                checked: true,
                boxLabelCls: 'checkboxLabel',
                listeners: {
                    change: 'showRandomPassword'
                }
            },
             {
                 xtype: 'textfield',
                 name: 'password',
                 reference: 'password',
                 inputType: 'password',
                 fieldLabel: app.localize('Password'),
                 width: '100%',
                 ui: 'fieldLabelTop',
                 emptyText: app.localize('Password'),
                 bind: {
                     hidden: '{isSetRandomPassword.checked}'
                 }
             },

              {
                  xtype: 'textfield',
                  name: 'passwordRepeat',
                  reference: 'passwordRepeat',
                  inputType: 'password',
                  fieldLabel: app.localize('PasswordRepeat'),
                  width: '100%',
                  ui: 'fieldLabelTop',
                  emptyText: app.localize('PasswordRepeat'),
                  bind: {
                      hidden: '{isSetRandomPassword.checked}'
                  },
                  /*
                * Custom validator implementation - checks that the value matches what was entered into
                * the password1 field.
                */
                  validator: function (value) {
                      var password1 = this.previousSibling('[name=password]');
                      return (value === password1.getValue()) ? true : 'Passwords do not match.'
                  }
              },
              {
                  xtype: 'checkbox',
                  boxLabel: app.localize('ShouldChangePasswordOnNextLogin'),
                  name: 'shouldChangePasswordOnNextLogin',
                  labelAlign: 'right',
                  inputValue: true,
                  checked: true,
                  boxLabelCls: 'checkboxLabel'
              },
            {
                xtype: 'checkbox',
                boxLabel: app.localize('SendActivationEmail'),
                name: 'sendActivationEmail',
                labelAlign: 'right',
                inputValue: true,
                checked: true,
                boxLabelCls: 'checkboxLabel'
            }, {
                xtype: 'checkbox',
                boxLabel: app.localize('Active'),
                name: 'isActive',
                labelAlign: 'right',
                inputValue: true,
                checked: true,
                boxLabelCls: 'checkboxLabel'
            }
            ]
        }
        , {
            title: abp.localization.localize("Roles"),
            padding: '0 10 0 10',
            iconCls: 'fa fa-briefcase',
            itemId: 'rolesListGridItemId',
            layout: 'column',
            height: '100%',
            items: [
            {
            columnWidth: .5,
            xtype: 'grid',
            cls: 'chaching-grid',
            itemId: 'rolesListGridItemId',
            height: '100%',
            scrollable: true,
            selType: 'checkboxmodel',
            columns: [
               { text: app.localize('RoleName'), dataIndex: 'displayName', flex: 1 }
            ],
            store: Ext.create('Chaching.store.roles.RolesStore'),
            listeners: {
                rowclick: 'reloadPermissionsTree'
            }
            },
            {
                columnWidth: .5,
                xtype: 'label',
                padding: '10 0 0 20',
                style:{color:'#cacaca', fontSize: '15px'},
                itemId: 'RolesMessageItemId',
                text: abp.localization.localize("RowClickMessage"),
            },
            {
            columnWidth: .5,
            //title: abp.localization.localize("Permissions"),
            cls: 'chaching-grid',
            hidden : true,
            xtype: 'treepanel',
            name: 'permissions',
            itemId: 'permissionsListItemId',
            store: new Chaching.store.roles.RolesTreeViewStore(),
            rootVisible: false,
            width: '100%',
            height: '100%',
            alwaysReload: false,
            scrollable: true,
            border: false,
            hideHeaders: false,
            columns: [{
                xtype: 'treecolumn',
                text: abp.localization.localize("Permissions"),
                dataIndex: 'displayName',
                flex: 1
            }
            ],
            listeners: {
                checkchange: function (node, checked, e, eOpts) {
                    var me = this;
                    var parentNode = node.parentNode;
                    node.cascadeBy(function (n) {
                        n.set('checked', checked);
                    });
                }
            }
        }]
        }
        , {
            title: abp.localization.localize("LinkCompany"),
            padding: '0 10 0 10',
            iconCls: 'fa fa-list',
            itemId: 'companyListTab',
            disabled : true,
            layout: 'column',
            height: '100%',
            items : [{
                columnWidth: 0.5,
                xtype: 'grid',
                cls: 'chaching-grid',
                itemId: 'companyListGridItemId',
                scrollable: true,
                selModel: {
                    selType: 'checkboxmodel'
                },
                columns: [
                   { text: abp.localization.localize("LinkCompanyRoles"), dataIndex: 'roleDisplayName', flex: 1 }
                ],
                store: Ext.create('Chaching.store.users.CompanyRoleStore'),
                features: [{ ftype: 'grouping' }],
                listeners: {
                    rowclick: 'reloadPermissionsTreeLinkCompany'
                }
            },
            {
                columnWidth: .5,
                xtype: 'label',
                padding: '10 0 0 20',
                style: { color: '#cacaca', fontSize: '15px' },
                itemId: 'LinkCompanyMessageItemId',
                text: abp.localization.localize("RowClickMessage"),
            },
            {
            columnWidth: 0.5,
            cls: 'chaching-grid',
            hidden: true,
            //padding: '0 0 0 10',
            xtype: 'treepanel',
            name: 'permissions',
            itemId: 'permissionsCompanyListItemId',
            store: new Chaching.store.roles.RolesTreeStore(),
            rootVisible: false,
            width: '100%',
            height: '100%',
            alwaysReload: false,
            scrollable: true,
            border: false,
            hideHeaders: false,
            columns: [{
                xtype: 'treecolumn',
                text: abp.localization.localize("Permissions"),
                dataIndex: 'displayName',
                flex: 1
            }
            ],
            listeners: {
                checkchange: function (node, checked, e, eOpts) {
                    var me = this;
                    var parentNode = node.parentNode;
                    node.cascadeBy(function (n) {
                        n.set('checked', checked);
                    });
                }
            }

            }


            ]

           
        }]
    }]

});
