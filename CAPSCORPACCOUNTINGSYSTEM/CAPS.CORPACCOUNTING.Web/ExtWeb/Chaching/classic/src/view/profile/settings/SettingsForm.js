
Ext.define('Chaching.view.profile.settings.SettingsForm', {
    extend: 'Chaching.view.common.form.ChachingFormPanel',
    alias: ['settings.createView'],
    requires: [
        'Chaching.view.profile.settings.SettingsFormController'
    ],

    controller: 'profile-settings-settingsform',
    modulePermissions: {
        //read: abp.auth.isGranted('Pages.Administration.Users'),
        create: true,//abp.auth.isGranted('Pages.Administration.Users.Create'),
        edit: true//abp.auth.isGranted('Pages.Administration.Users.Edit'),
        // destroy: abp.auth.isGranted('Pages.Administration.Users.Delete')
    },
    name: 'MySettings',
    openInPopupWindow: true,
    hideDefaultButtons: false,
    layout: 'vbox',
    defaults: {
        bodyStyle: { 'background-color': 'trasparent' },
        blankText: app.localize('MandatoryToolTipText')
    },
    items: [
        {
            xtype: 'textfield',
            name: 'name',
            itemId : 'name',
            allowBlank: false,
            fieldLabel: app.localize('Name'),
            width: '100%',
            ui: 'fieldLabelTop',
            emptyText: app.localize('Name')
        },
        {
            xtype: 'textfield',
            name: 'surname',
            allowBlank: false,
            fieldLabel: app.localize('Surname'),
            width: '100%',
            ui: 'fieldLabelTop',
            emptyText: app.localize('Surname')
        },
         {
             xtype: 'textfield',
             name: 'emailAddress',
             allowBlank: false,
             fieldLabel: app.localize('EmailAddress'),
             width: '100%',
             ui: 'fieldLabelTop',
             emptyText: app.localize('EmailAddress')
         },
         {
             xtype: 'textfield',
             name: 'userName',
             allowBlank: false,
             fieldLabel: app.localize('UserName'),
             width: '100%',
             ui: 'fieldLabelTop',
             reference: "userName",
             readOnly: true,
             emptyText: app.localize('UserName')
         },
         {
             xtype: 'label',
             reference : 'infoLabel',
             bind : {
                 hidden: '{!userName.readOnly}'
             },
             cls: 'helpText',
             text: app.localize('CanNotChangeAdminUserName'),
             width: '100%'
         },
         {
             xtype: 'combobox',
             name: 'timezone',
             allowBlank: false,
             fieldLabel: app.localize('Timezone'),
             valueField: 'value',
             displayField: 'name',
             queryMode : 'local',
             width: '100%',
             ui: 'fieldLabelTop',
             itemId: "timezone",
             reference: 'timezone',
             store: Ext.create('Chaching.store.TimezoneStore'),
             editable: false
         }
    ]
});
