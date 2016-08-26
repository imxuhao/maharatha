/**
 * DataModel to represent entity schema for Divisions.
 */
Ext.define('Chaching.model.financials.accounts.DivisionsModel', {
    extend: 'Chaching.model.base.BaseModel',
    config: {
        searchEntityName: 'Job'
    },
    fields: [
            { name: 'jobId', type: 'int', isPrimaryKey: true, headerText: 'Job Id', hidden: true, width: '8%' },
            {
                name: 'creditJobNumber', mapping: 'jobNumber', headerText: 'job Desc', hidden: true, width: '8%'
            },
            {
                name: 'creditJobId', mapping: 'jobId', headerText: 'Credit Job Id', hidden: true, width: '8%'
            },
            { name: 'jobNumber', type: 'string', headerText: 'JobDivisionNumber', hidden: false, width: '10%', minWidth: 90 },

            { name: 'rollupJobId', type: 'int', mapping: 'jobId' },

            { name: 'caption', type: 'string', headerText: 'Caption', hidden: false, width:'15%',minWidth:110 },
            { name: 'isDivision', type: 'boolean', headerText: 'Division', hidden: false, width: '11%', minWidth: 70 },
          
            { name: 'typeOfCurrencyId', type: 'int', hidden: true, defaultValue: null, convert: nullHandler },
           { name: 'typeOfCurrency', type: 'string', headerText: 'Currency', defaultValue: null, convert: nullHandler },
           { name: 'number', type: 'string', headerText: 'Number', defaultValue: null, convert: nullHandler },
            { name: 'isActive', type: 'boolean',hidden: true}
          
    ]
});