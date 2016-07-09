Ext.define('Chaching.view.users.UsersFormController', {
    extend: 'Chaching.view.common.form.ChachingFormPanelController',
    alias: 'controller.users-usersform',
    loadCompanyRoles : function(view , record , item , index , e , eOpts) {
        var me = this,
            view = me.getView();
        var rolesGrid = view.down('gridpanel[itemId=companyRolesListGridItemId]');
        var rolesStore = rolesGrid.getStore();
        var proxy = rolesStore.getProxy();
        proxy.api.read = abp.appPath + 'api/services/app/user/GetRolesByTenant',
        rolesStore.removeAll();
        rolesStore.getProxy().setExtraParams({ id: record.get('tenantId') });
        rolesStore.load();
    },
    doPreSaveOperation: function (record, values, idPropertyField) {
        var me = this,
             view = me.getView();
        record.data.user = values;
        Ext.apply(record.data, values);
        //get roles information
        var rolesListRecords = view.down('gridpanel[itemId=rolesListGridItemId]').getSelection();
        //get company information
        var companyListRecords = view.down('gridpanel[itemId=companyListGridItemId]').getSelection();
        if (rolesListRecords && rolesListRecords.length > 0) {
           var rolesListArray = [];
           Ext.each(rolesListRecords, function (rec) {
                rolesListArray.push(rec.get('displayName'));
            });
            record.data.assignedRoleNames = rolesListArray;
        }
        if (companyListRecords && companyListRecords.length > 0) {
            var tenantListArray = [];
            Ext.each(companyListRecords, function (rec) {
                tenantListArray.push({ tenantId: rec.get('tenantId'), tenantName: rec.get('tenantName') });
            });
            record.data.tenantList = tenantListArray;
        }
        
        return record;
    }
    ,
    showRandomPassword: function () {
        var me = this;
        password = me.lookupReference('password');
        passwordRepeat = me.lookupReference('passwordRepeat');
        password.reset();
        passwordRepeat.reset();
    }

});
