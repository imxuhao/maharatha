Ext.define('Chaching.view.tenants.TenantsFormController', {
    extend: 'Chaching.view.common.form.ChachingFormPanelController',
    alias: 'controller.tenants-tenantsform',
    onFormResize: function (form, newWidth, newHeight, oldWidth, oldHeight) {
        var me = this,
           view = me.getView();
        var gridPanel = view.down('grid[itemId=moduleListGridItemId]');
        if (gridPanel) {
            gridPanel.setHeight(newHeight - 100);
        }
    },
    showRandomPassword: function () {
        var me = this,
        password = me.lookupReference('adminPassword'),
        passwordRepeat = me.lookupReference('adminPasswordRepeat');
        password.reset();
        passwordRepeat.reset();
    },
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
        if (!cmp.isEditMode) {
            var me = this;
            var task = new Ext.util.DelayedTask(function () {
                me.enableDisableCopyTenantsTab(cmp);
            });
            task.delay(1000);
        }
    },
    enableDisableCopyTenantsTab: function (cmp) {
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
           view = me.getView(),
           grid = view.down('gridpanel[itemId=moduleListGridItemId]'),
           tenantListCombo = view.down('combobox[itemId=tenantItemId]'),
           moduleListGridStore = grid.getStore();

        if (tenantListCombo.getValue() == undefined) {
            abp.message.info(app.localize('SelectTenantToCopyModules'));
            return;
        } else {
            var modules = [
                   { name: 'Vendors', displayName: app.localize('VendorsModule') },
                   { name: 'Users', displayName: app.localize('UsersModule') },
                   { name: 'Customers', displayName: app.localize('CustomersModule') },
                   { name: 'Employees', displayName: app.localize('EmployeesModule') },
                   { name: 'Roles', displayName: app.localize('RolesModule') },
                   { name: 'ChartofAccounts', displayName: app.localize('ChartofAccountsModule') },
                   { name: 'ProjectChartofAccounts', displayName: app.localize('ProjectChartofAccountsModule') }
            ];
            moduleListGridStore.loadData(modules);
        }
    },

    onSaveClicked: function (btn) {
        var me = this,
            view = me.getView(),
            parentGrid = view.parentGrid,
            values = view.getValues(),
            organizationCombo = view.down('combobox[itemId=organizationId]'),
            tenancyNameTextfield = view.down('textfield[itemId=tenancyName]');

        if (organizationCombo && organizationCombo.getValue() == null) {
            abp.message.error(app.localize('SelectOrganization'));
            return;
        }
        if (tenancyNameTextfield && tenancyNameTextfield.getValue().trim() == '') {
            abp.message.error(app.localize('SelectTenancyName'));
            tenancyNameTextfield.focus(100);
            return;
        }
        if (parentGrid) {
            var gridStore = parentGrid.getStore(),
                idPropertyField = gridStore.idPropertyField,
                operation;
            var record = Ext.create(gridStore.model.$className);
            Ext.apply(record.data, values);
            var target;
            if (view.openInPopupWindow) {
                target = view.up('window');
            } else {
                target = view;
            }
            var myMask = new Ext.LoadMask({
                msg: 'Please wait...',
                target: target
            });
            if (values && parseInt(values[idPropertyField]) > 0) {
                record = me.prepareRequest(record, values, view);
                me.saveTenant(record, values, idPropertyField, operation, parentGrid, me, gridStore, myMask);
            } else {
                abp.message.confirm(app.localize('TenantCreationWarningMessage'), app.localize('Warning'), function (isConfirmed) {
                    if (isConfirmed) {
                        record = me.prepareRequest(record, values, view);
                        me.saveTenant(record, values, idPropertyField, operation, parentGrid, me, gridStore, myMask);
                    } else {
                        myMask.hide();
                        return false;
                    }
                });
            }
        }
    },

    saveTenant: function (record, values, idPropertyField, operation, parentGrid, me, gridStore, myMask) {
        var me = this;
        if (!record) return record;
        myMask.show();
        if (values && parseInt(values[idPropertyField]) > 0) {
            operation = Ext.data.Operation({
                params: record.data,
                parentGrid: parentGrid,
                records: [record],
                controller: me,
                operationMask: myMask,
                callback: me.onOperationCompleteCallBack
            });
            gridStore.update(operation);
        } else if (values && parseInt(values[idPropertyField]) === 0) {
            record.id = 0;
            record.set('id', 0);
            operation = Ext.data.Operation({
                params: record.data,
                parentGrid: parentGrid,
                controller: me,
                operationMask: myMask,
                callback: me.onOperationCompleteCallBack
            });
            gridStore.create(values, operation);
        } else {
            myMask.hide();
        }
    },
    prepareRequest: function (record, values, view) {
        var record = Ext.create('Chaching.model.tenants.TenantsModel');
        values.name = values.tenancyName;
        Ext.apply(record.data, values);
        var selectedRecords = view.down('gridpanel[itemId=moduleListGridItemId]').getSelection();
        var tenantListCombo = view.down('combobox[itemId=tenantItemId]');
        record.set('organizationUnitId', values.organizationUnitId);
        record.set('sourceTenantId', tenantListCombo.getValue());
        record.set('adminEmailAddress', app.localize('SendActivationEmailDefaultValue'));
        if (selectedRecords && selectedRecords.length > 0) {
            var moduleListArray = [];
            Ext.each(selectedRecords, function (rec) {
                moduleListArray.push(rec.get('name'));
            });
            record.data.moduleList = moduleListArray;
        }
        return record;
    }
    
});
