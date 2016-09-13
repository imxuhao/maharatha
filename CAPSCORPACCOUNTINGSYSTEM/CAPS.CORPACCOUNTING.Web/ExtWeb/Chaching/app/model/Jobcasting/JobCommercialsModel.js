/**
 * DataModel to represent entity schema for Job Details.
 */
Ext.define('Chaching.model.Jobcasting.JobCommercialsModel', {
    extend: 'Chaching.model.projects.projectmaintenance.ProjectModel',
    //config: {
    //    searchEntityName: ''
    //},
    fields: [
            { name: 'jobCommercialId', type: 'int', defaultValue: null, convert: nullHandler },
            { name: 'jobId', type: 'int', isPrimaryKey: true },
            { name: 'bidDate', type: 'date'},
            { name: 'awardDate', type: 'date'},
            { name: 'shootingDate', type: 'date'},
            { name: 'wrapDate', type: 'date'},
            { name: 'roughCutDate', type: 'date'},
            { name: 'airDate', type: 'date'},
            { name: 'dateClosed', type: 'date'},
            { name: 'finalShootDate', type: 'date'},
            { name: 'productOwner', type: 'string'},
            { name: 'productName', type: 'string'},
            { name: 'executiveProducerId', type: 'int', defaultValue: null, convert: nullHandler },
            { name: 'directorEmployeeId', type: 'int', defaultValue: null, convert: nullHandler },
            { name: 'producerEmployeeId', type: 'int', defaultValue: null, convert: nullHandler },
            { name: 'dirOfPhotoEmployeeId', type: 'int', defaultValue: null, convert: nullHandler },
            { name: 'setDesignerEmployeeId', type: 'int', defaultValue: null, convert: nullHandler },
            { name: 'artDirectorEmployeeId', type: 'int', defaultValue: null, convert: nullHandler },
            { name: 'editorEmployeeId', type: 'int', defaultValue: null, convert: nullHandler },
            { name: 'salesRepId', type: 'int', defaultValue: null, convert: nullHandler },
            { name: 'agencyId', type: 'int', defaultValue: null, convert: nullHandler },
            { name: 'agencyClientCustomerId', type: 'int', defaultValue: null, convert: nullHandler },
            { name: 'thirdPartyCustomerId', type: 'int', defaultValue: null, convert: nullHandler },
            { name: 'agencyProducer', type: 'string'},
            { name: 'agencyProducerContactInfo', type: 'string'},
            { name: 'agencyArtDirector', type: 'string'},
            { name: 'agencyArtDirContactInfo', type: 'string'},
            { name: 'agencyWriter', type: 'string'},
            { name: 'agencyWriterContactInfo', type: 'string'},
            { name: 'agencyBusinessManager', type: 'string'},
            { name: 'agencyBusMgrContactInfo', type: 'string'},
            { name: 'agencyJobNumber', type: 'string'},
            { name: 'agencyPONumber', type: 'string'},
            { name: 'agencyName', type: 'string' },
            {name:'agency',type:'string'},
            { name: 'agencyAddress', type: 'string'},
            { name: 'agencyPhone', type: 'string'},
            { name: 'commercialTitle1', type: 'string'},
            { name: 'commercialTitle2', type: 'string'},
            { name: 'commercialTitle3', type: 'string'},
            { name: 'commercialTitle4', type: 'string'},
            { name: 'commercialTitle5', type: 'string'},
            { name: 'commercialTitle6', type: 'string'},            
            { name: 'projectTotal', type: 'float'},
            { name: 'cGITotal', type: 'float'},
            { name: 'markupPercent', type: 'float'},
            { name: 'markupTotal', type: 'float'},
            { name: 'rDARevenue', type: 'float'},
            { name: 'incomeAccrual', type: 'float'},
            { name: 'costAccrual', type: 'float'},
            { name: 'postProductionCompany', type: 'float'},
            { name: 'dubbingHouse', type: 'string'},
            { name: 'storageHouse', type: 'string'},
            { name: 'isBudgetLocked', type: 'boolean'},
            { name: 'commercialNumber', type: 'string'},
            { name: 'commercialLength', type: 'string'},
            { name: 'preProductionDays', type: 'int'},
            { name: 'strikeDays', type: 'int'},
            { name: 'preLightDays', type: 'int'},
            { name: 'preLightHours', type: 'int'},
            { name: 'strikeHours', type: 'int'},
            { name: 'studioShootDays', type: 'int'},
            { name: 'shootHours', type: 'int'},
            { name: 'locationDays', type: 'int'},
            { name: 'locationHours', type: 'int'},
            { name: 'isCostPlus', type: 'boolean'},
            { name: 'isWrapUpInsurance', type: 'boolean'},
            { name: 'isFringeAccountSeparate', type: 'boolean'},
            { name: 'isOTon', type: 'boolean'},
            { name: 'agencyEmail', type: 'string' },
            {name:'directorName',type:'string'}
    ]
});

