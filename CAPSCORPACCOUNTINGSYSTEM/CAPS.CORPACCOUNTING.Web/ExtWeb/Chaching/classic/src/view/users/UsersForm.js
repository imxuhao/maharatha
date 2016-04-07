
Ext.define('Chaching.view.users.UsersForm', {
    extend: 'Chaching.view.common.form.ChachingFormPanel',
    alias: ['users.create', 'users.edit'],
    requires: [
        'Chaching.view.users.UsersFormController',
        'Chaching.view.users.UsersFormModel'
    ],

    controller: 'users-usersform',
    viewModel: {
        type: 'users-usersform'
    },
    name: 'Users',
    openInPopupWindow: true,
    hideDefaultButtons: false,
    layout: 'vbox',
    autoScroll: true,
    defaults: {
        bodyStyle: { 'background-color': 'trasparent' },
        labelAlign: 'top'
    },
    defaultFocus: 'textfield#name',
    items: [{
        xtype: 'hiddenfield',
        name: 'id',
        value: 0
    }
    ,
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
        name: 'SetRandomPassword',
        labelAlign: 'right',
        inputvalue: true,
        checked: true,
        boxLabelCls: 'checkboxLabel',
        listeners: {
            change: function (checkbox, newValue, oldValue, eOpts) {
                var txtpassword = Ext.getCmp('txtpassword');
                var txtpasswordRepeat = Ext.getCmp('txtpasswordRepeat');
                if (newValue) {
                    txtpassword.hide();
                    txtpasswordRepeat.hide();

                }
                else {
                    txtpassword.show();
                    txtpasswordRepeat.show();
                }
                txtpasswordRepeat.reset();
                txtpassword.reset();
            }
        }
    },
     {
         xtype: 'textfield',
         name: 'password',
         inputType: 'password',
         fieldLabel: app.localize('Password'),
         width: '100%',
         ui: 'fieldLabelTop',
         emptyText: app.localize('Password'),
         id: 'txtpassword',
         hidden: true


     },

      {
          xtype: 'textfield',
          name: 'passwordRepeat',
          inputType: 'password',
          fieldLabel: app.localize('PasswordRepeat'),
          width: '100%',
          ui: 'fieldLabelTop',
          emptyText: app.localize('PasswordRepeat'),
          id: 'txtpasswordRepeat',
          hidden: true
      },

    {
        xtype: 'checkbox',
        boxLabel: app.localize('sendactivationemail'),
        name: 'sendactivationemail',
        labelAlign: 'right',
        inputValue: true,
        checked: true,
        boxLabelCls: 'checkboxLabel'
    }, {
        xtype: 'checkbox',
        boxLabel: app.localize('active'),
        name: 'isactive',
        labelAlign: 'right',
        inputValue: true,
        checked: true,
        boxLabelCls: 'checkboxLabel'
    }
    ]

});
