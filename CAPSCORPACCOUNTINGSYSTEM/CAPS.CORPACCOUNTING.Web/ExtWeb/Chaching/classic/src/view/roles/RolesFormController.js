Ext.define('Chaching.view.roles.RolesFormController', {
    extend: 'Chaching.view.common.form.ChachingFormPanelController',
    alias: 'controller.roles-rolesform',
    doPreSaveOperation: function (record, values, idPropertyField) {
        var me = this,
             view = me.getView(),
        treePanel = view.down('treepanel[itemId=permissionsItemId]'),
        grantedPermissionNames = [];
        var selectedPermissions = treePanel.getChecked();
        Ext.Array.each(selectedPermissions, function (rec) {
            grantedPermissionNames.push(rec.get('name'));
        });
        record.data.role = {
            id: values.id == "0" ? null : values.id,
            displayName: values.displayName,
            isDefault: values.isDefault
        };
        record.data.grantedPermissionNames = grantedPermissionNames;
        return record;
    }
    
});
