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

        if (roleTextfield && roleTextfield.getValue().trim() == '') {
            abp.message.error(app.localize('SelectRoleName')).done(function(){
                roleTextfield.focus(true,100);
            });
            return;
        }
        else{
            var role = {};
            role.id = null;
            role.displayName = roleTextfield.getValue();
            role.isDefault = false;
            if (treePanel) {
                var selectedPermissions = treePanel.getChecked();
                Ext.Array.each(selectedPermissions, function (rec) {
                    grantedPermissionNames.push(rec.get('name'));
                });
                var treeStore = treePanel.getStore();
                //treeStore.removeAll();
                //var userId = treeStore.getProxy().extraParams.id;
                //treeStore.getProxy().setExtraParam('userId', userId);
                //treeStore.getProxy().setExtraParam('role', role);
                //treeStore.getProxy().setExtraParam('grantedPermissionNames', grantedPermissionNames);
                //treeStore.getProxy().api.update = abp.appPath + 'api/services/app/user/UpdateUserPermissionsUnit';
                //treeStore.update();
                var input = new Object();
                var userId = treeStore.getProxy().extraParams.id;
                input.role = role;
                input.userId = userId;
                input.grantedPermissionNames = grantedPermissionNames;
                Ext.Ajax.request({
                    url: abp.appPath + 'api/services/app/user/UpdateUserPermissionsUnit',
                    jsonData: Ext.encode(input),
                    method: 'POST',
                    headers: {
                        'Accept': 'application/json'
                    },
                    success: function (response,a,b) {
                        abp.notify.success(app.localize('SuccessMessage'), app.localize('Success'));
                    },
                    failure: function (response) {
                        abp.message.error(response.error.message, app.localize('Error'));
                    }
                });
                //{
                //    callback: function (records, response, success) {
                //        if (success) {
                //            //ChachingGlobals.showErrorMessage(response);
                //            abp.notify.success(app.localize('SuccessMessage'), app.localize('Success'));
                //        }
                //        else {
                //            //ChachingGlobals.showErrorMessage(response);
                //            abp.message.error(response.error.message, app.localize('Error'));
                //        }
                //    }
                //});
                
            }
            me.CloseWindow();
        }
        
    },
    onCancelClicked: function () {
        var me = this,
            view = me.getView(),
            roleTextfield = view.down('textfield[itemId=newRole]'),
            parentWindow = me.ParentWindowObj,
            parentView = parentWindow.getView(),
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
                abp.notify.success(app.localize('SuccessMessage'), app.localize('Success'));
            }
            me.CloseWindow();
    },
    CloseWindow: function () {
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
