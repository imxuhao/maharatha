﻿Ext.define('Chaching.view.tenants.TenantUsersViewController', {
    extend: 'Chaching.view.common.window.ChachingWindowPanelController',
    alias: 'controller.tenants-tenantusersview',
    onLogInThisUserClick: function (btn) {
        var me = this,
          view = me.getView(),
        grid = view.down('grid');
        if (grid) {
            var selectedUsers = grid.getSelection();
            if (selectedUsers.length == 1) {
                grid.setLoading(true);
                Ext.Ajax.request({
                    url: abp.appPath + 'Account/ImpersonateUser',
                    jsonData: Ext.encode({ tenantId: view.tenantId, userId: selectedUsers[0].get('value') }),
                    success: function (response) {
                        grid.setLoading(false);
                        var res = Ext.decode(response.responseText);
                        if (res.success) {
                            window.location.href = res.targetUrl;
                        } else {
                            abp.message.error(res.error.message, 'Error');
                        }
                    },
                    failure: function (response) {
                        grid.setLoading(false);
                        abp.message.error(app.localize('CascadeImpersonationErrorMessage'), app.localize('Error'));
                    }
                });
            } else {
                abp.message.warn(app.localize('TenantInformationMessage'), app.localize('InformationMessage'));
            }
        }
    },

    onTenantUsersCancel: function (btn) {
        var me = this,
           view = me.getView();
        Ext.destroy(view);
    }

});
