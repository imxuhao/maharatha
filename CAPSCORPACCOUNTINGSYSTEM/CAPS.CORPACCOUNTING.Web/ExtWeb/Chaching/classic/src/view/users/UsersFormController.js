Ext.define('Chaching.view.users.UsersFormController', {
    extend: 'Chaching.view.common.form.ChachingFormPanelController',
    alias: 'controller.users-usersform',
    loadCompanyRoles : function(view , record , item , index , e , eOpts) {
        var me = this,
            view = me.getView();
        var rolesGrid = view.down('gridpanel[itemId=companyRolesListGridItemId]');
        var rolesStore = rolesGrid.getStore();
        rolesStore.removeAll();
        rolesStore.getProxy().setExtraParams({ id: record.get('tenantId') });
        rolesStore.load();
    },
    doPreSaveOperation: function (record, values, idPropertyField) {
        var me = this,
             view = me.getView();
        //record = Ext.create('Chaching.model.tenants.TenantsModel');
        Ext.apply(record.data, values);
        //get roles information
        var rolesListGridStore = view.down('gridpanel[itemId=rolesListGridItemId]').getStore();
        var rolesRecords = rolesListGridStore.getModifiedRecords();
        //get company information
        var companyListGridStore = view.down('gridpanel[itemId=companyListGridItemId]').getStore();
        var companyRecords = companyListGridStore.getModifiedRecords();
        if (rolesRecords && rolesRecords.length > 0) {
            rolesListArray = [];
            Ext.each(rolesRecords, function (rec) {
                rolesListArray.push(rec.get('id'));
            });
            record.data.roleList = rolesListArray;
        }
        if (companyRecords && companyRecords.length > 0) {
            companyListArray = [];
            Ext.each(companyRecords, function (rec) {
                companyListArray.push(rec.get('tenantId'));
            });
            record.data.companyList = companyListArray;
        }
        return record;
    },
    showRandomPassword: function () {
        var me = this;
        setRandomPassword = me.lookupReference('setRandomPassword');
        password = me.lookupReference('password');
        passwordRepeat = me.lookupReference('passwordRepeat');
        if (setRandomPassword.value) {
            password.hide();
            passwordRepeat.hide();
        }
        else {
            password.show();
            passwordRepeat.show();
        }
        password.reset();
        passwordRepeat.reset();
    },

});
