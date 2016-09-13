Ext.define('Chaching.store.utilities.autofill.SubAccountsStore', {
    extend: 'Chaching.store.base.BaseStore',
    pageSize: 1000,
    model : 'Chaching.model.financials.accounts.SubAccountsModel',
    //fields: [{ name: 'name' }, { name: 'value' }, {
    //    name: 'subAccount1Desc', convert: function (value, record) {
    //        return record.get('name');
    //    }
    //}, {
    //    name: 'subAccountId1', convert: function (value, record) {
    //        return record.get('value');
    //    }
    //}, {
    //    name: 'subAccount2Desc', convert: function (value, record) {
    //        return record.get('name');
    //    }
    //}, {
    //    name: 'subAccountId2', convert: function (value, record) {
    //        return record.get('value');
    //    }
    //}, {
    //    name: 'subAccount3Desc', convert: function (value, record) {
    //        return record.get('name');
    //    }
    //}, {
    //    name: 'subAccountId3', convert: function (value, record) {
    //        return record.get('value');
    //    }
    //}, {
    //    name: 'subAccount4Desc', convert: function (value, record) {
    //        return record.get('name');
    //    }
    //}, {
    //    name: 'subAccountId4', convert: function (value, record) {
    //        return record.get('value');
    //    }
    //}, {
    //    name: 'subAccount5Desc', convert: function (value, record) {
    //        return record.get('name');
    //    }
    //}, {
    //    name: 'subAccountId5', convert: function (value, record) {
    //        return record.get('value');
    //    }
    //}, {
    //    name: 'subAccount6Desc', convert: function (value, record) {
    //        return record.get('name');
    //    }
    //}, {
    //    name: 'subAccountId6', convert: function (value, record) {
    //        return record.get('value');
    //    }
    //}, {
    //    name: 'subAccount7Desc', convert: function (value, record) {
    //        return record.get('name');
    //    }
    //}, {
    //    name: 'subAccountId7', convert: function (value, record) {
    //        return record.get('value');
    //    }
    //}, {
    //    name: 'subAccount8Desc', convert: function (value, record) {
    //        return record.get('name');
    //    }
    //}, {
    //    name: 'subAccountId8', convert: function (value, record) {
    //        return record.get('value');
    //    }
    //}, {
    //    name: 'subAccount9Desc', convert: function (value, record) {
    //        return record.get('name');
    //    }
    //}, {
    //    name: 'subAccountId9', convert: function (value, record) {
    //        return record.get('value');
    //    }
    //}, {
    //    name: 'subAccount10Desc', convert: function (value, record) {
    //        return record.get('name');
    //    }
    //}, {
    //    name: 'subAccountId10', convert: function (value, record) {
    //        return record.get('value');
    //    }
    //},
    ///////////////
    //{
    //    name: 'creditSubAccount1Desc', convert: function (value, record) {
    //        return record.get('name');
    //    }
    //}, {
    //    name: 'creditSubAccountId1', convert: function (value, record) {
    //        return record.get('value');
    //    }
    //}, {
    //    name: 'creditSubAccount2Desc', convert: function (value, record) {
    //        return record.get('name');
    //    }
    //}, {
    //    name: 'creditSubAccountId2', convert: function (value, record) {
    //        return record.get('value');
    //    }
    //}, {
    //    name: 'creditSubAccount3Desc', convert: function (value, record) {
    //        return record.get('name');
    //    }
    //}, {
    //    name: 'creditSubAccountId3', convert: function (value, record) {
    //        return record.get('value');
    //    }
    //}, {
    //    name: 'creditSubAccount4Desc', convert: function (value, record) {
    //        return record.get('name');
    //    }
    //}, {
    //    name: 'creditSubAccountId4', convert: function (value, record) {
    //        return record.get('value');
    //    }
    //}, {
    //    name: 'creditSubAccount5Desc', convert: function (value, record) {
    //        return record.get('name');
    //    }
    //}, {
    //    name: 'creditSubAccountId5', convert: function (value, record) {
    //        return record.get('value');
    //    }
    //}, {
    //    name: 'creditSubAccount6Desc', convert: function (value, record) {
    //        return record.get('name');
    //    }
    //}, {
    //    name: 'creditSubAccountId6', convert: function (value, record) {
    //        return record.get('value');
    //    }
    //}, {
    //    name: 'creditSubAccount7Desc', convert: function (value, record) {
    //        return record.get('name');
    //    }
    //}, {
    //    name: 'creditSubAccountId7', convert: function (value, record) {
    //        return record.get('value');
    //    }
    //}, {
    //    name: 'creditSubAccount8Desc', convert: function (value, record) {
    //        return record.get('name');
    //    }
    //}, {
    //    name: 'creditSubAccountId8', convert: function (value, record) {
    //        return record.get('value');
    //    }
    //}, {
    //    name: 'creditSubAccount9Desc', convert: function (value, record) {
    //        return record.get('name');
    //    }
    //}, {
    //    name: 'creditSubAccountId9', convert: function (value, record) {
    //        return record.get('value');
    //    }
    //}, {
    //    name: 'creditSubAccount10Desc', convert: function (value, record) {
    //        return record.get('name');
    //    }
    //}, {
    //    name: 'creditSubAccountId10', convert: function (value, record) {
    //        return record.get('value');
    //    }
    //}
    //],
    proxy: {
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        type: 'chachingProxy',
        //extraParams: {
        //    jobId: null
        //},
        // url: abp.appPath + 'api/services/app/list/GetSubAccountList',
        urlToGetRecordById: abp.appPath + 'api/services/app/subAccountUnit/GetSubAccountUnitsById',
        api: {
            read: abp.appPath + 'api/services/app/list/GetSubAccountList',
            create: abp.appPath + 'api/services/app/subAccountUnit/CreateSubAccountUnit',
            update: abp.appPath + 'api/services/app/subAccountUnit/UpdateSubAccountUnit',
            destroy: abp.appPath + 'api/services/app/subAccountUnit/DeleteSubAccountUnit'
        },
        reader: {
            type: 'json',
            rootProperty: 'result'
        }
    },
    idPropertyField: 'subAccountId'//important to set for add/update of records
});
