Ext.define('Chaching.view.common.form.ChachingTransactionFormPanelModel', {
    extend: 'Ext.app.ViewModel',
    alias: 'viewmodel.common-form-chachingtransactionformpanel',
    data: {
        name: 'Chaching'
    },
    stores: {
        typeOfCurrencyList: {
            fields: [{ name: 'name' }, { name: 'value' }, {
                name: 'typeOfCurrency', convert: function (value, record) {
                    return record.get('name');
                }
            }, {
                name: 'typeOfCurrencyId', convert: function (value, record) {
                    return record.get('value');
                }
            }],
            // xtype: 'ajax',
            remoteSort: false,
            remoteFilter: false,
            autoLoad: false,
            proxy: {
                actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
                type: 'chachingProxy',
                url: abp.appPath + 'api/services/app/accountUnit/GetTypeOfCurrencyList',
                reader: {
                    type: 'json',
                    rootProperty: 'result',
                    totalProperty: 'result.totalCount'
                }
            }
        },
        typeOfCheckGroup: {
            fields: [{ name: 'name' }, { name: 'value' }, {
                name: 'typeOfCheckGroup', mapping:'name'
            }, {
                name: 'typeOfCheckGroupId', mapping:'value'
            }],
            remoteSort: false,
            remoteFilter: false,
            autoLoad: false,
            proxy: {
                actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
                type: 'chachingProxy',
                url: abp.appPath + 'api/services/app/list/GetCheckGroupList',
                reader: {
                    type: 'json',
                    rootProperty: 'result',
                    totalProperty: 'result.totalCount'
                }
            }
        }
    }

});
