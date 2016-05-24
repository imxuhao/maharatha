Ext.define('Chaching.store.utilities.autofill.SubAccountsStore', {
    extend: 'Chaching.store.base.BaseStore',
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
    }],
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
