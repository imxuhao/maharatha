Ext.define('Chaching.view.users.UsersPermissionFormController', {
    extend: 'Chaching.view.common.form.ChachingFormPanelController',
    alias: 'controller.users-usersPermissionform',
    onUsersPermissionFormResize: function (form, newWidth, newHeight, oldWidth, oldHeight) {
        var me = this,
           view = me.getView();
        var treePanel = view.down('treepanel[itemId=usersPermissionsItemId]');
        if (treePanel) {
            treePanel.setHeight(newHeight - 50);
        }
    },
    onTreeCheckChange: function (node, checked, e, eOpts) {
        var me = this;
        var parentNode = node.parentNode;
        node.cascadeBy(function (n) {
            n.set('checked', checked);
        });
    },
    onPermissionsSaveClicked: function (btn) {
        var me = this,
             view = me.getView(),
             values = view.getValues(),
        treePanel = view.down('treepanel[itemId=usersPermissionsItemId]'),
        treeStore = treePanel.getStore(),
        grantedPermissionNames = [];
        var myMask = new Ext.LoadMask({
            msg: 'Please wait...',
            target: view.up('window')
        });
        var selectedPermissionsCount = treePanel.getChecked();
        var originalNodeCount = treeStore.selectedNodeCount;

        if (originalNodeCount !== selectedPermissionsCount.length) {
            myMask.show();
            // Open popup message to create new Role
            var permissionWindow = Ext.create('Chaching.view.users.PermissionsSaveView', {
                autoShow: true,
                iconCls: 'fa fa-plus',
                name: 'Administration.Users'
            });
            var permissionsSaveFormController = permissionWindow.down('form').getController();
            permissionsSaveFormController.ParentWindowObj = me;
            myMask.hide();
            //Ext.Msg.prompt(app.localize('CreateNewRole'), " \"Changes were made to this user\'s Role\". Press YES to create and name the new Role or NO to cancel.", function (btn, text) {
            //    if (btn == 'ok') {
            //        debugger;
            //        var role = {};
            //        //var userInput = {};
            //        role.id = null;
            //        role.displayName = text;
            //        role.isDefault = false;
            //        //userInput.User.id = treeStore.getProxy().extraParams.id;
            //        //userInput.User.name = treeStore.getProxy().extraParams.name;
            //        //userInput.User.id = treeStore.getProxy().extraParams.Surname;
            //        //userInput.User.id = treeStore.getProxy().extraParams.EmailAddress;
            //        //userInput.User.id = treeStore.getProxy().extraParams.Password;
            //        //userInput.User.id = treeStore.getProxy().extraParams.IsActive;
            //        //userInput.User.id = treeStore.getProxy().extraParams.ShouldChangePasswordOnNextLogin;
            //        //userInput.SendActivationEmail = false;
            //        //userInput.TenantList = null;
            //        //treeStore.getProxy().setExtraParam('grantedPermissionNames', grantedPermissionNames);
            //        //treeStore.getProxy().setExtraParam('Role', role);

            //        userId = treeStore.getProxy().extraParams.id;
            //        treeStore.getProxy().setExtraParam('userId', userId);
            //        treeStore.getProxy().setExtraParam('role', role);
            //        treeStore.getProxy().setExtraParam('grantedPermissionNames', grantedPermissionNames);
            //        treeStore.getProxy().api.update = abp.appPath + 'api/services/app/user/UpdateUserPermissionsUnit';
            //        treeStore.update();
            //    }
            //    else {
            //        treeStore.getProxy().setExtraParam('id', treeStore.getProxy().extraParams.id);
            //        treeStore.getProxy().setExtraParam('grantedPermissionNames', grantedPermissionNames);
            //        treeStore.getProxy().api.update = abp.appPath + 'api/services/app/user/UpdateUserPermissions';
            //        treeStore.update();
            //    }
            //});
            // END
        }
        else {
            abp.notify.success(app.localize('SuccessMessage'), app.localize('Success'));
            me.onCancelClicked();
        }
            //me.onCancelClicked();
    },
    onResetPermissionsClicked: function (btn) {
        var me = this,
            view = me.getView(),
            values = view.getValues(),
            treePanel = view.down('treepanel[itemId=usersPermissionsItemId]'),
            treeStore = treePanel.getStore(),
            record = { id: treeStore.getProxy().extraParams.id };
        Ext.Ajax.request({
            url: abp.appPath + 'api/services/app/user/ResetUserSpecificPermissions',
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            jsonData: record,
            success: function (conn, response, options, eOpts) {
                var result = Ext.JSON.decode(conn.responseText);
                if (result.success) {
                    treeStore.getProxy().setExtraParam('id', treeStore.getProxy().extraParams.id);
                    treeStore.reload();
                    abp.notify.success(app.localize('SuccessMessage'), app.localize('Success'));
                    //me.doPostSaveOperations(null, null, result.success);
                }
                else {
                    Ext.Msg.alert('Error!', result.error.message);
                }
            },
            failure: function (conn, response, options, eOpts) {
                var respObj = Ext.JSON.decode(conn.responseText);
                Ext.Msg.alert("Error", respObj.status.statusMessage);
            }
        });

    },
    onCancelClicked: function () {
        var me = this,
           view = me.getView();
        if (view && view.openInPopupWindow) {
            var wnd = view.up('window');
            Ext.destroy(wnd);
            return;
        }
        Ext.destroy(view);
    }
});