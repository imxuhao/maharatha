/**
 * DataStore to perform Read/Delete operation on Bank Check Numbers.
 */
Ext.define('Chaching.store.banking.banksetup.BankCheckNumberStore', {
    extend: 'Chaching.store.base.BaseStore',
    model: 'Chaching.model.banking.banksetup.BankCheckRangesModel',
    proxy: {
        type: 'chachingProxy',
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        api: {
            create: abp.appPath + '',
            read: abp.appPath + 'api/services/app/bankAccountUnit/GetBankAccountPaymentRangeByBankAccountId',
            update: abp.appPath + '',
            destroy: abp.appPath + 'api/services/app/bankAccountUnit/DeleteBankAccountPaymentRange'
        }
    },
    idPropertyField: 'bankAccountPaymentRangeId'//important to set for add/update of records
});
