Ext.define('Chaching.view.tenants.TenantsGridController', {
    extend: 'Chaching.view.common.grid.ChachingGridPanelController',
    alias: 'controller.tenants-tenantsgrid',
    loginAsThisTenantClick: function (menu, e, eOpts) {
        var parentMenu = menu.parentMenu,
            widgetRec = parentMenu.widgetRecord;
       // create tenant user view
        var tenantUsersView = Ext.widget('tenantusers');
        var tenantUserGrid = tenantUsersView.down('grid');
        //load tenant users
        if (tenantUserGrid) {
            tenantUsersView.tenantId = widgetRec.get('id');
           var tenantUsersStore = tenantUserGrid.getStore();
           var proxy = tenantUsersStore.getProxy();
           proxy.setExtraParams({ tenantId: widgetRec.get('id') });
           tenantUsersStore.load();
        }

    },
    doAfterCreateAction: function (createMode, formView, isEdit, record) {
        var me = this,
         form = formView.down('form').getForm();
        var copyFromTenantsTab = formView.down('*[itemId=moduleListGridItemId]');
        if (formView && isEdit) {
            form.findField('tenancyName').setReadOnly(true);
            //form.findField('isUseHostDatabase').setHidden(true);
            //form.findField('connectionString').setHidden(true);
            form.findField('isSetRandomPassword').setHidden(true);
            form.findField('adminPassword').setHidden(true);
            form.findField('adminPasswordRepeat').setHidden(true);
            form.findField('adminEmailAddress').setReadOnly(true);
            form.findField('organizationUnitId').setReadOnly(true);
            form.findField('adminEmailAddress').setHidden(true);
            form.findField('shouldChangePasswordOnNextLogin').setHidden(true);
            form.findField('sendActivationEmail').setHidden(true);
            if (copyFromTenantsTab) {
                copyFromTenantsTab.setDisabled(true);
            }
            if (record.get('editionId') == null) {
                record.set('editionId', "null");
            }
        }
        var organizationStore = form.findField('organizationUnitId').getStore();
        organizationStore.load();
        var viewModel = formView.down('form').getViewModel();
        var editionStore = viewModel.getStore('editionsForComboBox');
        editionStore.load();
       
    },
    doRowSpecificEditDelete: function (button, grid) {
        var menu = button.menu;
        var tenantsGrid = Ext.ComponentQuery.query('*[name = Tenants]')[0];
        var separatorButton = menu.items.get('actionMenuSeparator');
        if (button.menu) {
         var loginAsThisTenantActionMenu = button.menu.down('menuitem#loginAsThisTenantActionMenu');
         if (tenantsGrid && tenantsGrid.modulePermissions.impersonation && loginAsThisTenantActionMenu && button.widgetRec ) {
            loginAsThisTenantActionMenu.show();
            separatorButton.show();
        } else {
             loginAsThisTenantActionMenu.hide();
             separatorButton.hide();
         }

        }
    }
});
