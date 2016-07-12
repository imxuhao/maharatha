Ext.define('Chaching.view.tenants.TenantsFormController', {
    extend: 'Chaching.view.common.form.ChachingFormPanelController',
    alias: 'controller.tenants-tenantsform',
    onOrganizationSelect: function (combo, record, eOpts) {
        var me = this,
            view = me.getView();
        var tenantListCombo = view.down('combobox[itemId=tenantItemId]');
        var tenantStore = tenantListCombo.getStore();
        tenantStore.getProxy().setExtraParams({ id: combo.getValue() });
        tenantStore.load();
        var tenancyNameField = view.down('textfield[itemId=tenancyName]');
        if (tenancyNameField) {
            me.enableDisableCopyTenantsTab(tenancyNameField);
        }
    },
    onTenancyNameEnter: function (cmp, event, eOpts) {
        var me = this;
        var task = new Ext.util.DelayedTask(function () {
            me.enableDisableCopyTenantsTab(cmp);
        });
        task.delay(1000);
    },
    enableDisableCopyTenantsTab : function(cmp) {
        var me = this,
         view = me.getView();
        var tenantStore = view.down('combobox[itemId=tenantItemId]').getStore();
        var tenantRecord = tenantStore.findRecord('tenancyName', cmp.getValue());
        if (tenantRecord == undefined && tenantStore.getCount() > 0) {
            view.down('gridpanel[itemId=moduleListGridItemId]').setDisabled(false);
        } else {
            view.down('gridpanel[itemId=moduleListGridItemId]').setDisabled(true);
        }
    },
    onTenantSelect: function (selModel, selected, eOpts) {
        var me = this,
           view = me.getView();
        var tenantListCombo = view.down('combobox[itemId=tenantItemId]');
        if (tenantListCombo.getValue() == undefined) {
            abp.message.info(app.localize('SelectTenantToCopyModules'));
            return;
        } else {
            var modules = [
                   { name: 'Vendors' },
                   { name: 'Users' },
                   { name: 'Customers' },
                   { name: 'Employees' },
                   { name: 'Roles' },
                   { name: 'ChartofAccounts' },
                   { name: 'ProjectChartofAccounts' }
            ];
            var moduleListGridStore = view.down('gridpanel[itemId=moduleListGridItemId]').getStore();
            moduleListGridStore.loadData(modules);
            
        }
    },
    doPreSaveOperation: function (record, values, idPropertyField) {
        var me = this,
             view = me.getView();

        var organizationCombo = view.down('combobox[itemId=organizationId]');
        if (organizationCombo && organizationCombo.getValue() == null) {
            abp.message.error(app.localize('SelectOrganization'));
            return;
        }
        if (organizationCombo && organizationCombo.getValue() == null) {
            abp.message.confirm(app.localize('TenantCreationWarningMessage'), app.localize('Warning'), function (btn) {
                if (btn) {
                    record = Ext.create('Chaching.model.tenants.TenantsModel');
                    Ext.apply(record.data, values);
                    var selectedRecords = view.down('gridpanel[itemId=moduleListGridItemId]').getSelection();
                    var tenantListCombo = view.down('combobox[itemId=tenantItemId]');
                    record.set('organizationUnitId', values.organizationUnitId);
                    record.set('sourceTenantId', tenantListCombo.getValue());
                    if (selectedRecords && selectedRecords.length > 0) {
                        var moduleListArray = [];
                        Ext.each(selectedRecords, function (rec) {
                            moduleListArray.push(rec.get('name'));
                        });
                        record.data.moduleList = moduleListArray;
                    }
                } else {
                    return false;
                }
            });
        }

        //record = Ext.create('Chaching.model.tenants.TenantsModel');
        //Ext.apply(record.data, values);
        //var selectedRecords = view.down('gridpanel[itemId=moduleListGridItemId]').getSelection();
        //var tenantListCombo = view.down('combobox[itemId=tenantItemId]');
        //record.set('organizationUnitId', values.organizationUnitId);
        //record.set('sourceTenantId', tenantListCombo.getValue());
        //if (selectedRecords && selectedRecords.length > 0) {
        //    var moduleListArray = [];
        //    Ext.each(selectedRecords, function (rec) {
        //        moduleListArray.push(rec.get('name'));
        //    });
        //    record.data.moduleList = moduleListArray;
        //}
        return record;
    }
    
});
