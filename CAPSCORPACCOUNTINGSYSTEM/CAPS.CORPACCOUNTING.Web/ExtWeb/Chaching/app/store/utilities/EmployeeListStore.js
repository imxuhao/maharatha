Ext.define('Chaching.store.utilities.EmployeeListStore', {
    extend: 'Chaching.store.base.BaseStore',
    fields: [{ name: 'name' }, { name: 'value' }, {
        name: 'employeeName', convert: function (value, record) {
            return record.get('name');
        }
    }, {
        name: 'employeeId', convert: function (value, record) {
            return record.get('value');
        }
    }],
    extraParams: {
        property: ''

    },
    proxy: {
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        type: 'chachingProxy',
        url: abp.appPath + 'api/services/app/employeeUnit/GetEmployeeList',
        reader: {
            type: 'json',
            rootProperty: 'result'
        }
    }
});


