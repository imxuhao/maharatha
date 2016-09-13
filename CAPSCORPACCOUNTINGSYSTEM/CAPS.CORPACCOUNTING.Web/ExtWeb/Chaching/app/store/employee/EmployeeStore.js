/**
 * DataStore to perform Read operation on Employees.
 */
Ext.define('Chaching.store.employee.EmployeeStore', {
    extend: 'Chaching.store.base.BaseStore',
    model:'Chaching.model.employee.EmployeeModel',
    pageSize: 1000,
    //storeId:'directorsListStore',
    proxy: {
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        type: 'chachingProxy',
        url: abp.appPath + 'api/services/app/employeeUnit/GetEmployeeUnits',
        reader: {
            type: 'json',
            rootProperty: 'result.items'
        }
    }
});
