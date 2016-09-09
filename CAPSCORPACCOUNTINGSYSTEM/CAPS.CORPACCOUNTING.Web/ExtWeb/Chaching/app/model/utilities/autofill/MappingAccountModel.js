Ext.define('Chaching.model.utilities.autofill.MappingAccountModel',
{
    extend: 'Ext.data.Model',
    fields: [
        { name: 'accountId', type: 'int' },
        { name: 'accountNumber', type: 'string', hidden: false, headerText: app.localize('AccountNumber') },
        { name: 'caption', type: 'string', hidden: false, headerText: app.localize('Caption') },
        { name: 'description', type: 'string' },
        { name: 'chartOfAccountId', type: 'int' },
        { name: 'linkAccountId', type: 'int', convert: nullHandler, mapping: 'accountId' },
        { name: 'linkAccount', type: 'string', mapping: 'accountNumber' },
        { name: 'mapAccountId', type: 'string', mapping: 'linkAccountId' }
    ]
});
