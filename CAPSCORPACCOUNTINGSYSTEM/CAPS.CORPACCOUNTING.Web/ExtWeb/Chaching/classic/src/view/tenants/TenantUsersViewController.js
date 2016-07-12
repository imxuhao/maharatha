Ext.define('Chaching.view.tenants.TenantUsersViewController', {
    extend: 'Chaching.view.common.window.ChachingWindowPanelController',
    alias: 'controller.tenants-tenantusersview',
    onLogInThisUserClick: function (btn) {
        var me = this,
          view = me.getView(),
        grid = view.down('grid');
        if (grid) {
            var selectedUsers = grid.getSelection();
            if (selectedUsers.length == 1) {
                Ext.Ajax.request({
                    url: abp.appPath + 'Account/ImpersonateUser',
                    jsonData: Ext.encode({ tenantId: view.tenantId, userId: selectedUsers[0].get('value') }),
                    success: function (response) {
                        var res = Ext.decode(response.responseText);
                        if (res.success) {
                            window.location.href = res.targetUrl;
                        } else {
                            abp.message.error(res.error.message, 'Error');
                        }
                    },
                    failure: function(response) {
                        var res = Ext.decode(response.responseText);
                        Ext.toast(res.error.message);
                        console.log(response);
                    }
                });
            }
        }
    },

    onTenantUsersCancel: function (btn) {
        var me = this,
           view = me.getView();
        Ext.destroy(view);
    }

});
