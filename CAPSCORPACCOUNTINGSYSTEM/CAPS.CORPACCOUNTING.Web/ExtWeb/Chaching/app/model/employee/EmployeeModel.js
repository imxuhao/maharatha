Ext.define('Chaching.model.employee.EmployeeModel', {
    extend: 'Chaching.model.base.BaseModel',
    config: {
        searchEntityName: 'Employee'
    },

    fields: [
        { name: 'employeeId', type: 'int', isPrimaryKey: true },
        { name: 'lastName', type: 'string' },
        { name: 'firstName', type: 'string' },
        { name: 'employeeRegion', type: 'string' },
        { name: 'ssnTaxId', type: 'string' },
        { name: 'federalTaxId', type: 'string' },
        { name: 'is1099', type: 'boolean' },
        { name: 'isW9OnFile', type: 'boolean' },
        { name: 'isIndependantContractor', type: 'boolean' },
        { name: 'isCorporation', type: 'boolean' },
        { name: 'isProducer', type: 'boolean' },
        { name: 'isDirector', type: 'boolean' },
        { name: 'isDirPhoto', type: 'boolean' },
        { name: 'isSetDesigner', type: 'boolean' },
        { name: 'isEditor', type: 'boolean' },
        { name: 'isArtDirector', type: 'boolean' },
        { name: 'isApproved', type: 'boolean' },
        { name: 'isActive', type: 'boolean' }
    ]
});
