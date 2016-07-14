/**
 * DataModel to represent entity schema for Transaction Batches.
 */
Ext.define('Chaching.model.batchposting.batches.BatchesModel', {
    extend: 'Chaching.model.base.BaseModel',
    config: {
        searchEntityName: ''
    },
    fields: [
        { name: 'batchId', type: 'int', isPrimaryKey: true },
        { name: 'description', type: 'string' },
        { name: 'typeOfBatchId', type: "int", defaultVaule: null, convert: nullHandler },
        { name: 'typeOfBatch', type: 'string' },
        { name: 'defaultTransactionDate', type: 'date', dateFormat: 'c' },
        { name: 'defaultCheckDate', type: 'date', dateFormat: 'c' },
        { name: 'postingDate', type: 'date', dateFormat: 'c' },
        { name: 'controlTotal', type: 'float' },
        { name: 'recurMonthIncrement', type: 'int' },
        { name: 'isRetained', type: 'boolean' },
        { name: 'isDefault', type: 'boolean' },
        { name: 'isActive', type: 'boolean' },
        { name: 'typeOfInactiveStatusId', type: "int", defaultVaule: null, convert: nullHandler },
        { name: 'typeOfInactiveStatus', type: 'string' },
        { name: 'isBatchFinalized', type: 'boolean' },
        { name: 'isUniversal', type: 'boolean' },
        { name: 'post', type: 'boolean' },
        {
             name: 'typeOfBatchDummy', type: 'string', convert: function (value, record) {
                 return record.get('typeOfBatch');
             }
         }
    ]
});