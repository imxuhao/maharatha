
Ext.define('Chaching.view.profile.settings.SettingsForm', {
    extend: 'Chaching.view.common.form.ChachingFormPanel',
    alias: ['settings.createView'],
    requires: [
        'Chaching.view.profile.settings.SettingsFormController'
    ],

    controller: 'profile-settings-settingsform',

    name: 'MySettings',
    openInPopupWindow: true,
    hideDefaultButtons: false,
    layout: 'vbox',
    defaults: {
        bodyStyle: { 'background-color': 'trasparent' },
       // labelAlign: 'top',
        blankText: app.localize('MandatoryToolTipText')
    },
    //  defaultFocus: 'textfield#tenancyName',

    items: [
        {
            xtype: 'textfield',
            name: 'name',
            allowBlank: false,
            fieldLabel: app.localize('Name').initCap(),
            width: '100%',
            ui: 'fieldLabelTop',
            emptyText: app.localize('Name')
        },
        {
            xtype: 'textfield',
            name: 'surname',
            allowBlank: false,
            fieldLabel: app.localize('Surname').initCap(),
            width: '100%',
            ui: 'fieldLabelTop',
            emptyText: app.localize('Surname')
        },
         {
             xtype: 'textfield',
             name: 'emailAddress',
             allowBlank: false,
             fieldLabel: app.localize('EmailAddress').initCap(),
             width: '100%',
             ui: 'fieldLabelTop',
             emptyText: app.localize('EmailAddress')
         },
         {
             xtype: 'textfield',
             name: 'userName',
             allowBlank: false,
             fieldLabel: app.localize('UserName').initCap(),
             width: '100%',
             ui: 'fieldLabelTop',
             reference: "userName",
             readOnly: true,
             emptyText: app.localize('UserName')
         },
         {
             xtype: 'label',
             bind : {
                 hidden: '{!userName.readOnly}'
             },
             text: app.localize('CanNotChangeAdminUserName').initCap(),
             width: '100%',
         },
         {
             xtype: 'combobox',
             name: 'timezone',
             allowBlank: false,
             fieldLabel: app.localize('Timezone').initCap(),
             valueField: 'value',
             displayField: 'name',
             queryMode : 'local',
             width: '100%',
             ui: 'fieldLabelTop',
             itemId: "timezone",
             store: Ext.create('Chaching.store.TimezoneStore'),
             editable : false
         }
    ]
});
