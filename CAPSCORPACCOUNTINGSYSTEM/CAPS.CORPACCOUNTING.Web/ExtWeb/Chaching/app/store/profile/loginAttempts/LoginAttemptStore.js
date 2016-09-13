/**
 * DataStore to perform Read operation on Users Login Attempts.
 */
Ext.define('Chaching.store.profile.loginAttempts.LoginAttemptStore', {
    extend: 'Chaching.store.base.BaseStore',
    model: 'Chaching.model.profile.loginAttempts.LoginAttemptModel',
    autoLoad:false,
    proxy: {
        type: 'chachingProxy',
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        api: {
            read: abp.appPath + 'api/services/app/userLogin/GetRecentUserLoginAttempts'
        }
    },
    idPropertyField: 'browserInfo'//important to set for add/update of records
});