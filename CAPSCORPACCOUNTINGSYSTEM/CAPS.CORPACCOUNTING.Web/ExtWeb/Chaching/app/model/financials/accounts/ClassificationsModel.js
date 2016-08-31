/**
 * DataModel to represent entity schema for Classifications.
 */
Ext.define('Chaching.model.financials.accounts.ClassificationsModel', {
    extend: 'Chaching.model.base.BaseModel',
    config: {
        searchEntityName: 'Job'
    },
    fields: [
            { name: 'classificationId', type: 'int', isPrimaryKey: true, headerText: 'Classification Id', hidden: true, width: '8%' },
            { name: 'Description', mapping: 'jobNumber', headerText: 'job Desc', hidden: true, width: '8%' },
            { name: 'caption', type: 'string', headerText: 'Caption', hidden: false, width: '15%', minWidth: 110 },
            { name: 'typeOfAccountClassificationId', type: 'int', hidden: true, defaultValue: null, convert: nullHandler },
            { name: 'typeOfAccountClassificatio', type: 'string', headerText: 'Currency', defaultValue: null, convert: nullHandler }
           
    ]
});