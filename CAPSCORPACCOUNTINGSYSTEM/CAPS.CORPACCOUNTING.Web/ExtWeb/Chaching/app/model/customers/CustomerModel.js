Ext.define('Chaching.model.CustomerModel', {
    extend: 'Ext.data.Model',
    fields: [{ name: 'name', headerText: 'Last Name', hidden: false, width: '8%' },
        { name: 'column1', headerText: 'First Name', hidden: false, width: '8%' },
        { name: 'column2', headerText: 'Customer #', hidden: false, width: '8%' },
        { name: 'value', headerText: 'Value', hidden: true, width: '8%' }, {
            name: 'customerId', headerText: 'CustomerId', hidden: true, width: '8%', convert: function (value, record) {
            return record.get('value');
        }
    }]
});


