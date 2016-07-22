
Ext.define('Chaching.view.users.PermissionsSaveForm', {
    extend: 'Chaching.view.common.form.ChachingFormPanel',
    alias: ['host.users.create', 'host.users.edit'],
    requires: [
        'Chaching.view.users.PermissionsSaveFormController'
    ],
    controller: 'users-permissionssaveform',
    name: 'Administration.Users',
    openInPopupWindow: true,
    hideDefaultButtons: true,
    layout: 'vbox',
    listeners: {
        resize: 'onPermissionsSaveFormResize'
    },
    defaultFocus: 'textfield#newRole',
    items:[
        {
            xtype: 'label',
            padding: '10 10 0 10',
            text: app.localize('PermissionsSaveMessage'),
            //itemId: 'permissionsSaveTextItemId',
            ui: 'fieldLabelTop',
            width: '100%'
        },{
        xtype: 'textfield',
        padding: '0 10 0 10',
        //fieldLabel : app.localize('PermissionsSaveMessage'),
        name: 'newRole',
        itemId: 'newRole',
        ui: 'fieldLabelTop',
        width: '100%'
    }],
    bbar: ['->', {
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
            click: 'onPermissionsWithRoleSave'
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
    }]
});
