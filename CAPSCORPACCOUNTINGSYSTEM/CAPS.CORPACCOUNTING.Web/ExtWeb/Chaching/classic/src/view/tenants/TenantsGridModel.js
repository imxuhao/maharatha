Ext.define('Chaching.view.tenants.TenantsGridModel', {
    extend: 'Ext.app.ViewModel',
    alias: 'viewmodel.tenants-tenantsgrid',
    data: {
        name: 'Chaching'
    },
    stores: {
        editionsForComboBox: {
            fields: [{ name: 'displayText' }, { name: 'value' }, { name: 'editionDisplayName',convert:function(value, record) {
                return record.get('displayText');
            } }, { name: 'editionId',convert:function(value, record) {
                return record.get('value');
            } }],
            xtype: 'ajax',
            autoLoad: false,
            proxy: {
                actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
                type: 'chachingProxy',
                url: abp.appPath + 'api/services/app/edition/GetEditionComboboxItems',
                reader: {
                    type: 'json',
                    rootProperty: 'result',
                    totalProperty: 'result.totalCount'
                }
            }
        }
    }

});
