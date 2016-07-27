
Ext.define('Chaching.view.users.UsersPermissionForm', {
    extend: 'Chaching.view.common.form.ChachingFormPanel',
    //alias: ['host.users.create', 'host.users.edit'],
    requires: [
        'Chaching.view.users.UsersPermissionFormController'
    ],
    controller: 'users-usersPermissionform',
    name: 'Administration.Users',
    openInPopupWindow: true,
    hideDefaultButtons: true,
    layout: 'vbox',
    listeners: {
        resize: 'onUsersPermissionFormResize'
    },
    items: {
            padding: '0 10 0 10',
            layout: 'fit',
            xtype: 'treepanel',
            itemId: 'usersPermissionsItemId',
            cls: 'chaching-grid',
            store: new Chaching.store.users.PermissionsTreeStore(),
            //store: new Chaching.store.roles.RolesTreeStore(),
            rootVisible: false,
            width: '100%',
            alwaysReload: false,
            scrollable: true,
            hideHeaders: true,
            columns: [{
                xtype: 'treecolumn',
                dataIndex: 'displayName',
                flex: 1
            }],
            listeners: {
                checkchange: 'onTreeCheckChange'
            }
    },
    bbar: [{
        xtype: 'button',
        scale: 'small',
        iconCls: 'fa fa-save',
        iconAlign: 'left',
        text: app.localize('ResetSpecialPermissions').toUpperCase(),
        ui: 'actionButton',
        name: 'ResetSpecialPermissions',
        itemId: 'BtnResetSpecialPermissions',
        listeners: {
            click: 'onResetPermissionsClicked'
        }
    },
    '->',
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
            click: 'onPermissionsSaveClicked'
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
