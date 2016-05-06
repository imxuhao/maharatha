Ext.define('Chaching.model.financials.accounts.DivisionsModel', {
    extend: 'Chaching.model.base.BaseModel',
    config: {
        searchEntityName: 'Job'
    },
    fields: [
            { name: 'jobId', type: 'int' ,isPrimaryKey: true},
            { name: 'jobNumber', type: 'string' },
            { name: 'caption', type: 'string' },
            //{ name: 'isCorporateDefault', type: 'boolean'},
            //{ name: 'rollupCenterId', type: 'int'},
            //{ name: 'chartOfAccountId', type: 'int'},
            //{ name: 'rollupAccountId', type: 'int'},
            { name: 'typeOfCurrencyId', type: 'int', defaultValue: null, convert: nullHandler },
            //{ name: 'rollupJobId', type: 'int'},
            //{ name: 'typeOfJobStatusId', type: 'int'},
            //{ name: 'typeOfBidSoftwareId', type: 'int'},
            //{ name: 'isApproved', type: 'boolean'},
            { name: 'isActive', type: 'boolean'}
            //{ name: 'isICTDivision', type: 'boolean'},
            //{ name: 'typeofProjectId', type: 'int'},
            //{ name: 'taxRecoveryId', type: 'int'},
            //{ name: 'organizationUnitId', type: 'int'},
            //{ name: 'detailReport', type: 'int'},
            //{ name: 'director', type: 'string'},
            //{ name: 'agency', type: 'string'}
    ]
});