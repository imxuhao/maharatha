﻿/**
 * DataModel to represent entity schema for Autofill Customers.
 */
Ext.define('Chaching.model.utilities.autofill.CustomerModel', {
    extend: 'Ext.data.Model',
    fields: [
        { name: 'name', hidden: true, width: '8%' },
        { name: 'customerId', hidden: true, isPrimaryKey: true },
        { name: 'agencyId', type: 'int', hidden: true, mapping: 'customerId' },
        { name: 'thirdPartyCustomerId', type: 'int', hidden: true, mapping: 'customerId' },
        { name: 'lastName', headerText: 'LastName', hidden: false, width: '8%', minWidth : 90 },
        { name: 'firstName', headerText: 'FirstName', hidden: false, width: '8%' , minWidth : 90},
        { name: 'customerNumber', headerText: 'CustomerNumber', hidden: false, width: '8%', minWidth : 90 }
    ]
});


