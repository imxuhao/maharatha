Ext.define('Chaching.store.utilities.autofill.SubAccountsStore', {
    extend: 'Chaching.store.base.BaseStore',
    pageSize: 1000,
    fields: [{ name: 'name' }, { name: 'value' }, {
        name: 'subAccount1', convert: function (value, record) {
            return record.get('name');
        }
    }, {
        name: 'subAccountId1', convert: function (value, record) {
            return record.get('value');
        }
    }, {
        name: 'subAccount2', convert: function (value, record) {
            return record.get('name');
        }
    }, {
        name: 'subAccountId2', convert: function (value, record) {
            return record.get('value');
        }
    }, {
        name: 'subAccount3', convert: function (value, record) {
            return record.get('name');
        }
    }, {
        name: 'subAccountId3', convert: function (value, record) {
            return record.get('value');
        }
    }, {
        name: 'subAccount4', convert: function (value, record) {
            return record.get('name');
        }
    }, {
        name: 'subAccountId4', convert: function (value, record) {
            return record.get('value');
        }
    }, {
        name: 'subAccount5', convert: function (value, record) {
            return record.get('name');
        }
    }, {
        name: 'subAccountId5', convert: function (value, record) {
            return record.get('value');
        }
    }, {
        name: 'subAccount6', convert: function (value, record) {
            return record.get('name');
        }
    }, {
        name: 'subAccountId6', convert: function (value, record) {
            return record.get('value');
        }
    }, {
        name: 'subAccount7', convert: function (value, record) {
            return record.get('name');
        }
    }, {
        name: 'subAccountId7', convert: function (value, record) {
            return record.get('value');
        }
    }, {
        name: 'subAccount8', convert: function (value, record) {
            return record.get('name');
        }
    }, {
        name: 'subAccountId8', convert: function (value, record) {
            return record.get('value');
        }
    }, {
        name: 'subAccount9', convert: function (value, record) {
            return record.get('name');
        }
    }, {
        name: 'subAccountId9', convert: function (value, record) {
            return record.get('value');
        }
    }, {
        name: 'subAccount10', convert: function (value, record) {
            return record.get('name');
        }
    }, {
        name: 'subAccountId10', convert: function (value, record) {
            return record.get('value');
        }
    },
    /////////////
    {
        name: 'creditSubAccount1', convert: function (value, record) {
            return record.get('name');
        }
    }, {
        name: 'creditSubAccountId1', convert: function (value, record) {
            return record.get('value');
        }
    }, {
        name: 'creditSubAccount2', convert: function (value, record) {
            return record.get('name');
        }
    }, {
        name: 'creditSubAccountId2', convert: function (value, record) {
            return record.get('value');
        }
    }, {
        name: 'creditSubAccount3', convert: function (value, record) {
            return record.get('name');
        }
    }, {
        name: 'creditSubAccountId3', convert: function (value, record) {
            return record.get('value');
        }
    }, {
        name: 'creditSubAccount4', convert: function (value, record) {
            return record.get('name');
        }
    }, {
        name: 'creditSubAccountId4', convert: function (value, record) {
            return record.get('value');
        }
    }, {
        name: 'creditSubAccount5', convert: function (value, record) {
            return record.get('name');
        }
    }, {
        name: 'creditSubAccountId5', convert: function (value, record) {
            return record.get('value');
        }
    }, {
        name: 'creditSubAccount6', convert: function (value, record) {
            return record.get('name');
        }
    }, {
        name: 'creditSubAccountId6', convert: function (value, record) {
            return record.get('value');
        }
    }, {
        name: 'creditSubAccount7', convert: function (value, record) {
            return record.get('name');
        }
    }, {
        name: 'creditSubAccountId7', convert: function (value, record) {
            return record.get('value');
        }
    }, {
        name: 'creditSubAccount8', convert: function (value, record) {
            return record.get('name');
        }
    }, {
        name: 'creditSubAccountId8', convert: function (value, record) {
            return record.get('value');
        }
    }, {
        name: 'creditSubAccount9', convert: function (value, record) {
            return record.get('name');
        }
    }, {
        name: 'creditSubAccountId9', convert: function (value, record) {
            return record.get('value');
        }
    }, {
        name: 'creditSubAccount10', convert: function (value, record) {
            return record.get('name');
        }
    }, {
        name: 'creditSubAccountId10', convert: function (value, record) {
            return record.get('value');
        }
    }
    ],
    proxy: {
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        type: 'chachingProxy',
        extraParams: {
            jobId: null
        },
        url: abp.appPath + 'api/services/app/list/GetSubAccountList',
        reader: {
            type: 'json',
            rootProperty: 'result'
        }
    }
});
