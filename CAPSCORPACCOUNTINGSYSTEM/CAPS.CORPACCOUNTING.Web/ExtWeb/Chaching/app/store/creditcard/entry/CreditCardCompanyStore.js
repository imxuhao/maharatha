/**
 * DataStore to perform CRUD operation on Credit Card Company.
 */
Ext.define('Chaching.store.creditcard.entry.CreditCardCompanyStore', {
    extend: 'Chaching.store.base.BaseStore',
    model: 'Chaching.model.creditcard.entry.CreditCardCompanyModel',
    proxy: {
        type: 'chachingProxy',
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        api: {
            create: abp.appPath + 'api/services/app/creditCardCompany/CreateCCCompanyDocumentUnit',
            read: abp.appPath + 'api/services/app/creditCardCompany/GetCreditCardCompanies',
            update: abp.appPath + 'api/services/app/creditCardCompany/UpdateCCCompanyDocumentUnit',
            destroy: abp.appPath + 'api/services/app/creditCardCompany/DeleteCCCompanyDocumentUnit'
        }
    },
    idPropertyField: 'bankAccountId'//important to set for add/update of records
});