Ext.define('Chaching.view.roles.RolesFormController', {
    extend: 'Chaching.view.common.form.ChachingFormPanelController',
    alias: 'controller.roles-rolesform',
    onRolesFormResize: function (form, newWidth, newHeight, oldWidth, oldHeight) {
        var me = this,
           view = me.getView();
        var treePanel = view.down('treepanel[itemId=permissionsItemId]');
        if (treePanel) {
            treePanel.setHeight(newHeight - 100);
        }
    },
    onTreeCheckChange:function (node, checked, e, eOpts) {
        var me = this;
        var parentNode = node.parentNode;
        node.cascadeBy(function (n) {
            n.set('checked', checked);
        });
    },
    onEditButtonClicked: function (editBtn) {
        var me = this,
            view = me.getView(),
            childGrids = view.query('gridpanel'),
            form = view.getForm(),
            fields = form.getFields().items;
        //form.title = form.title.replace('View', 'Edit');

        Ext.each(fields, function (field) {
            if (field.name !== 'isDefault')
            if (field.xtype !== "hiddenfield" && !field.isFilterField) {
                field.setDisabled(false);
                if (typeof (field.setEmptyText) === "function") {
                    field.setEmptyText(field.originalEmptyText);
                }
            }
        });

        var defaultActionToolBar = view.defaultActionToolBar;
        if (defaultActionToolBar) {
            var defaultActionButtons = defaultActionToolBar.query('button');
            if (defaultActionButtons && defaultActionButtons.length > 0) {
                Ext.each(defaultActionButtons, function (button) {
                    if (button.name !== 'Cancel' && button.name !== "Edit" && typeof (button.hide) === "function") {
                        button.show();
                    }
                    if (button.name === "Edit") button.hide();
                });
            }
        }

    },
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
