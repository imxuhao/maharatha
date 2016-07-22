Ext.define('Chaching.view.users.PermissionsSaveFormController', {
    extend: 'Chaching.view.common.window.ChachingWindowPanelController',
    alias: 'controller.users-permissionssaveform',
    ParentWindowObj: null,
    onPermissionsSaveFormResize: function () {

    },
    onPermissionsWithRoleSave: function(btn, eOpts) {
        var me = this,
            view = me.getView(),
            form = view.getForm(),
            parentWindow = me.ParentWindowObj,
            parentView = parentWindow.getView(),
            treePanel = parentView.down('treepanel[itemId=usersPermissionsItemId]'),
            roleTextfield = view.down('textfield[itemId=newRole]'),
            grantedPermissionNames = [];
            var role = {};
            role.id = null;
            role.displayName = (roleTextfield ? roleTextfield.getValue() : '');
            role.isDefault = false;
            if (treePanel) {
            var selectedPermissions = treePanel.getChecked();
            Ext.Array.each(selectedPermissions, function (rec) {
                grantedPermissionNames.push(rec.get('name'));
            });
            var treeStore = treePanel.getStore();
            treeStore.removeAll();
            var userId = treeStore.getProxy().extraParams.id;
            treeStore.getProxy().setExtraParam('userId', userId);
            treeStore.getProxy().setExtraParam('role', role);
            treeStore.getProxy().setExtraParam('grantedPermissionNames', grantedPermissionNames);
            treeStore.getProxy().api.update = abp.appPath + 'api/services/app/user/UpdateUserPermissionsUnit';
            treeStore.update();
            //me.doPostSaveOperations(null, null, '{ success: true }');
        }
        me.CloseWindow();
    },
    onCancelClicked: function () {
        var me = this,
            view = me.getView(),
            roleTextfield = view.down('textfield[itemId=newRole]');
            parentWindow = me.ParentWindowObj,
            parentView = parentWindow.getView();
            treePanel = parentView.down('treepanel[itemId=usersPermissionsItemId]');
            if (treePanel) {
                var treeStore = treePanel.getStore();
                var selectedPermissions = treePanel.getChecked();
                Ext.Array.each(selectedPermissions, function (rec) {
                    grantedPermissionNames.push(rec.get('name'));
                });
                treeStore.getProxy().setExtraParam('id', treeStore.getProxy().extraParams.id);
                treeStore.getProxy().setExtraParam('grantedPermissionNames', grantedPermissionNames);
                treeStore.getProxy().api.update = abp.appPath + 'api/services/app/user/UpdateUserPermissions';
                treeStore.update();
                //me.doPostSaveOperations(null, null, '{ success: true }');
            }
            me.CloseWindow();
    },
    CloseWindow: function () {
        debugger;
        var me = this,
           view = me.getView(),
           parentWindow = me.ParentWindowObj,
           parentView = parentWindow.getView();
        if (view && view.openInPopupWindow) {
            var wnd = view.up('window');
            Ext.destroy(wnd);
            // Destroy window
            var pWnd = parentView.up('window');
            Ext.destroy(pWnd);
            return;
        }
        Ext.destroy(view);
        // Destroy parent window
        Ext.destroy(parentView);
    }

});
