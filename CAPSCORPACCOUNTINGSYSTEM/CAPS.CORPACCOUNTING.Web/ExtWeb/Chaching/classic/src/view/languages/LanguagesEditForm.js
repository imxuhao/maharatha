
Ext.define('Chaching.view.languages.LanguagesEditForm', {
    extend: 'Chaching.view.common.form.ChachingFormPanel',

    requires: [
        'Chaching.view.languages.LanguagesEditFormController'
    ],
    controller: 'languages-languageseditform',
    name: 'Administration.Languages',
    modulePermissions: {
        read: abp.auth.isGranted('Pages.Administration.Languages'),
        create: abp.auth.isGranted('Pages.Administration.Languages.Create'),
        edit: abp.auth.isGranted('Pages.Administration.Languages.Edit'),
        destroy: abp.auth.isGranted('Pages.Administration.Languages.Delete')
    },
    openInPopupWindow: true,
    hideDefaultButtons: true,
    layout: 'vbox',
    setDefferedValuesOnEdit: true,
    defferedValueSetDelay: 5,
    defaults: {
        bodyStyle: { 'background-color': 'trasparent' },
        blankText: app.localize('MandatoryToolTipText')
    },
    //defaultFocus: 'textfield#tenancyName',
    items: [
        {
            xtype: 'hiddenfield',
            name: 'id',
            value: 0
        },
        {
            xtype: 'displayfield',
            name: 'key',
           // ui: 'fieldLabelTop',
            fieldLabel: app.localize('Key'),
            margin: '0 0 0 10'
        },
        {
             xtype: 'textareafield',
             //grow: true,
             name: 'baseValue',
             itemId: 'baseValueId',
             disabled:true,
             ui: 'fieldLabelTop',
             width:'100%',
             fieldLabel: app.localize('Base Value'),
             margin: '0 0 0 10',
             anchor: '100%'
         },
         {
              xtype: 'textareafield',
              name: 'targetValue',
              itemId: 'targetValueId',
              ui: 'fieldLabelTop',
              width: '100%',
              fieldLabel: app.localize('Target Value'),
              margin: '0 0 0 10',
              anchor: '100%'
          }
    ],
    buttons: [
        {
            xtype: 'button',
            scale: 'small',
            iconCls: 'fa fa-save',
            iconAlign: 'left',
            text: app.localize('Previous').toUpperCase(),
            ui: 'actionButton',
            name: 'Previous',
            itemId: 'BtnPrevious',
            reference: 'BtnPrevious',
            listeners: {
                click: 'onPreviousClicked'
            }
        },
        {
            xtype: 'button',
            scale: 'small',
            iconCls: 'fa fa-close',
            iconAlign: 'left',
            text: app.localize('Cancel').toUpperCase(),
            ui: 'actionButton',
            name: 'Cancel',
            itemId: 'BtnCancel',
            reference: 'BtnCancel',
            listeners: {
                click: 'onCancelClicked'
            }
        },
        {
            xtype: 'button',
            scale: 'small',
            iconCls: 'fa fa-save',
            iconAlign: 'left',
            text: app.localize('Save').toUpperCase(),
            ui: 'actionButton',
            name: 'Save',
            itemId: 'BtnSave',
            reference: 'BtnSave',
            listeners: {
                click: 'onSaveClicked'
            }
        },
        {
            xtype: 'button',
            scale: 'small',
            iconCls: 'fa fa-edit',
            iconAlign: 'left',
            text: app.localize('Edit').toUpperCase(),
            ui: 'actionButton',
            name: 'Edit',
            itemId: 'BtnEdit',
            reference: 'BtnEdit',
            hidden: true,
            listeners: {
                click: 'onEditButtonClicked'
            }
        }
        
    ]

});
