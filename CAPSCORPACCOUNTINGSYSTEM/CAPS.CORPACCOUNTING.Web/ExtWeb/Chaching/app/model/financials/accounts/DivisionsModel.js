Ext.define('Chaching.model.financials.accounts.DivisionsModel', {
    extend: 'Chaching.model.base.BaseModel',
    config: {
        searchEntityName: 'Job'
    },
    fields: [
            { name: 'jobId', type: 'int', isPrimaryKey: true, headerText: 'Job Id', hidden: true, width: '8%' },
            //{
            //    name: 'jobDesc', mapping: 'jobNumber', headerText: 'job Desc', hidden: true, width: '8%'
            //},
            {
                name: 'creditJobDesc', mapping: 'jobNumber', headerText: 'job Desc', hidden: true, width: '8%'
            },
            {
                name: 'creditJobId', mapping: 'jobId', headerText: 'job Desc', hidden: true, width: '8%'
            },
            { name: 'jobNumber', type: 'string', headerText: 'job Number', hidden: false, width: '8%' },
            { name: 'caption', type: 'string', headerText: 'Caption', hidden: false, width: '8%' },
            //{ name: 'isCorporateDefault', type: 'boolean'},
            //{ name: 'rollupCenterId', type: 'int'},
            //{ name: 'chartOfAccountId', type: 'int'},
            //{ name: 'rollupAccountId', type: 'int'},
            { name: 'typeOfCurrencyId', type: 'int', hidden: true, defaultValue: null, convert: nullHandler },
            //{ name: 'rollupJobId', type: 'int'},
            //{ name: 'typeOfJobStatusId', type: 'int'},
            //{ name: 'typeOfBidSoftwareId', type: 'int'},
            //{ name: 'isApproved', type: 'boolean'},
            { name: 'isActive', type: 'boolean',hidden: true}
            //{ name: 'isICTDivision', type: 'boolean'},
            //{ name: 'typeofProjectId', type: 'int'},
            //{ name: 'taxRecoveryId', type: 'int'},
            //{ name: 'organizationUnitId', type: 'int'},
            //{ name: 'detailReport', type: 'int'},
            //{ name: 'director', type: 'string'},
            //{ name: 'agency', type: 'string'}
    ]
});