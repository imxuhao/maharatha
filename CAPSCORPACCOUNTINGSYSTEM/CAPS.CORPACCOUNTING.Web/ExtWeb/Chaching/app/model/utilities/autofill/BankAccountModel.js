/**
 * DataModel to represent entity schema for Autofill Bank Accounts.
 */
Ext.define('Chaching.model.utilities.autofill.BankAccountModel', {
    extend: 'Ext.data.Model',
    fields: [
        { name: 'bankAccountId', hidden: true, isPrimaryKey: true },
        { name: 'defaultBank', hidden: true, mapping: 'bankAccountId' },
        { name: 'bankAccountNumber', headerText: 'BankAccountNumber', hidden: false, width: '8%', minWidth: 150 },
        { name: 'description', headerText: 'BankAccountDescription', hidden: false, width: '8%', minWidth: 90 },
        { name: 'bankAccountName', headerText: 'BankAccountName', hidden: false, width: '8%', minWidth: 130 }
    ]
});


