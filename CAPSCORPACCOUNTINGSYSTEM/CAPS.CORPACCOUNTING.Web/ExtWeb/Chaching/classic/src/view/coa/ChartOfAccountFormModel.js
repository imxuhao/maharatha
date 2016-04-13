Ext.define('Chaching.view.coa.ChartOfAccountFormModel', {
    extend: 'Chaching.view.common.form.ChachingFormPanelModel',
    alias: 'viewmodel.coa-chartofaccountform',
    data: {
        name: 'Chaching'
    },

    stores: {
        StandardGroupTotalList: {
            fields: [{ name: 'name' }, { name: 'value' }, {
                name: 'standardGroupTotal', convert: function (value, record) {
                    return record.get('name');
                }
            }, {
                name: 'standardGroupTotalId', convert: function (value, record) {
                    return record.get('value');
                }
            }],
            xtype: 'ajax',
            autoLoad: true,
            proxy: {
                actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
                type: 'chachingProxy',
                url: abp.appPath + 'api/services/app/coaUnit/StandardGroupTotalList',
                reader: {
                    type: 'json',
                    rootProperty: 'result',
                    totalProperty: 'result.totalCount'
                }
            }
        },

        linkChartOfAccountList: {
            fields: [{ name: 'name' }, { name: 'value' }, {
                name: 'linkChartOfAccount', convert: function (value, record) {
                    return record.get('name');
                }
            }, {
                name: 'linkChartOfAccountID', convert: function (value, record) {
                    return record.get('value');
                }
            }],
            xtype: 'ajax',
            autoLoad: true,
            proxy: {
                actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
                type: 'chachingProxy',
                url: abp.appPath + 'api/services/app/coaUnit/GetCoaList',
                reader: {
                    type: 'json',
                    rootProperty: 'result',
                    totalProperty: 'result.totalCount'
                }
            }
        }
    }
});
