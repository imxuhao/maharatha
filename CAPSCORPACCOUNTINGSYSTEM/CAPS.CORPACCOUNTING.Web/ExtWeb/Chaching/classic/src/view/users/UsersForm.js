
Ext.define('Chaching.view.users.UsersForm', {
    extend: 'Chaching.view.common.form.ChachingFormPanel',
    alias: ['users.create', 'users.edit'],
    requires: [
        'Chaching.view.users.UsersFormController'
    ],

    controller: 'users-usersform',
    name: 'Administration.Users',
    openInPopupWindow: true,
    hideDefaultButtons: false,
    //layout: 'vbox',
    //autoScroll: true,
    scrollable: true,
    defaults: {
        bodyStyle: { 'background-color': 'trasparent' },
        labelAlign: 'left'
    },
    defaultFocus: 'textfield#name',

    items: [{
        xtype: 'tabpanel',
        //ui: 'formTabPanels',
        ui: 'submenuTabs',
        items : [{
            title: abp.localization.localize("UserInformations").initCap(),
            scrollable: true,
            iconCls: 'fa fa-user',
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
        }, {
            title: abp.localization.localize("Roles"),
            iconCls: 'fa fa-briefcase',
            xtype: 'grid',
            cls: 'chaching-grid',
            itemId : 'rolesListGridItemId',
            height : 400,
            scrollable: true,
            selType: 'checkboxmodel',
            columns: [
               { text: app.localize('RoleName'), dataIndex: 'displayName', flex: 1 }
            ],
            store: Ext.create('Chaching.store.roles.RolesStore')
        }, {
            title: abp.localization.localize("CompanyList"),
            iconCls: 'fa fa-list',
            itemId: 'companyListTab',
            disabled : true,
            layout : 'column',
            items : [{
                columnWidth: 0.5,
                xtype: 'grid',
                cls: 'chaching-grid',
                itemId: 'companyListGridItemId',
                height: 400,
                scrollable: true,
                selType: 'checkboxmodel',
                columns: [
                   { text: app.localize('CompanyName'), dataIndex: 'tenantName', flex: 1 }
                ],
                store: Ext.create('Chaching.store.administration.organization.TenantListStore'),
                listeners: {
                    rowdblclick: 'loadCompanyRoles'
                }
            }, {
                columnWidth: 0.5,
                padding: '0 0 0 10',
                xtype: 'grid',
                cls: 'chaching-grid',
                itemId: 'companyRolesListGridItemId',
                height: 400,
                scrollable: true,
                columns: [
                   { text: app.localize('RoleName'), dataIndex: 'displayName', flex: 1 }
                ],
                store: Ext.create('Chaching.store.roles.RolesStore')
            }]

           
        }]
    }]

});
