Ext.define('Chaching.view.common.form.ChachingFormPanelModel', {
    extend: 'Ext.app.ViewModel',
    alias: 'viewmodel.common-form-chachingformpanel',
    data: {
        name: 'Chaching'
    },
    stores: {
        StandardGroupTotalList: {
            fields: [{ name: 'name' }, { name: 'value' }, {
                name: 'standardGroupTotal', convert: function (value, record) {
                    return record.get('name');
                }
            }, {
                name: 'standardGroupTotalId', convert: function (value, record) {
                    return record.get('value');
                }
            }],
            xtype: 'ajax',
            autoLoad: false,
            proxy: {
                actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
                type: 'chachingProxy',
                url: abp.appPath + 'api/services/app/coaUnit/StandardGroupTotalList',
                reader: {
                    type: 'json',
                    rootProperty: 'result',
                    totalProperty: 'result.totalCount'
                }
            }
        },

        linkChartOfAccountList: {
            fields: [{ name: 'name' }, { name: 'value' }, {
                name: 'linkChartOfAccount', convert: function (value, record) {
                    return record.get('name');
                }
            }, {
                name: 'linkChartOfAccountID', convert: function (value, record) {
                    return record.get('value');
                }
            }],
            xtype: 'ajax',
            autoLoad: false,
            proxy: {
                actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
                type: 'chachingProxy',
                url: abp.appPath + 'api/services/app/coaUnit/GetCoaList',
                reader: {
                    type: 'json',
                    rootProperty: 'result',
                    totalProperty: 'result.totalCount'
                }
            }
        },
        editionsForComboBox: {
            fields: [{ name: 'displayText' }, { name: 'value' }, {
                name: 'editionDisplayName', convert: function (value, record) {
                    return record.get('displayText');
                }
            }, {
                name: 'editionId', convert: function (value, record) {
                    return record.get('value');
                }
            }],
            xtype: 'ajax',
            autoLoad: false,
            proxy: {
                actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
                type: 'chachingProxy',
                url: abp.appPath + 'api/services/app/edition/GetEditionComboboxItems',
                reader: {
                    type: 'json',
                    rootProperty: 'result',
                    totalProperty: 'result.totalCount'
                }
            }
        },
        rolesForCheckBox: {

            fields: [{ name: 'displayName' }, { name: 'name' }, {
                name: 'displayName', convert: function (value, record) {
                    return record.get('displayName');
                }
            }, {
                name: 'name', convert: function (value, record) {
                    return record.get('name');
                }
            }],
            xtype: 'ajax',
            autoLoad: false,
            proxy: {
                actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
                type: 'chachingProxy',
                url: abp.appPath + 'api/services/app/role/GetRoles',
                reader: {
                    type: 'json',
                    rootProperty: 'result',
                    totalProperty: 'result.totalCount'
                }
            }
        },
        typeOfSubAccountList: {
            fields: [{ name: 'name' }, { name: 'value' }, {
                name: 'typeofSubAccount', convert: function (value, record) {
                    return record.get('name');
                }
            }, {
                name: 'typeofSubAccountId', convert: function (value, record) {
                    return record.get('value');
                }
            }],
            xtype: 'ajax',
            autoLoad: false,
            proxy: {
                actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
                type: 'chachingProxy',
                url: abp.appPath + 'api/services/app/subAccountUnit/GetTypeofSubAccountList',
                reader: {
                    type: 'json',
                    rootProperty: 'result',
                    totalProperty: 'result.totalCount'
                }
            }
        },
        getOrganizations: {
            fields: [{ name: 'name' }, { name: 'value' }, {
                name: 'organizationName', convert: function (value, record) {
                    return record.get('name');
                }
            }, {
                name: 'organizationId', convert: function (value, record) {
                    return record.get('value');
                }
            }],
            xtype: 'ajax',
            autoLoad: false,
            extraParams: {
                'Id': 1
            },
            proxy: {
                actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
                type: 'chachingProxy',
                url: abp.appPath + 'api/services/app/jobUnit/GetOrganizationUnits',
                reader: {
                    type: 'json',
                    rootProperty: 'result',
                    totalProperty: 'result.totalCount'
                }
            }
        },

        typeofConsolidationList: {
            fields: [{ name: 'name' }, { name: 'value' }, {
                name: 'typeofConsolidation', convert: function (value, record) {
                    return record.get('name');
                }
            }, {
                name: 'typeofConsolidationId', convert: function (value, record) {
                    return record.get('value');
                }
            }],
            xtype: 'ajax',
            proxy: {
                actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
                type: 'chachingProxy',
                url: abp.appPath + 'api/services/app/accountUnit/GetTypeofConsolidationList',
                reader: {
                    type: 'json',
                    rootProperty: 'result',
                    totalProperty: 'result.totalCount'
                }
            }
        }
        ,
        typeOfAccountList: {
            fields: [{ name: 'name' }, { name: 'value' }, {
                name: 'typeOfAccount', convert: function (value, record) {
                    return record.get('name');
                }
            }, {
                name: 'typeOfAccountId', convert: function (value, record) {
                    return record.get('value');
                }
            }],
            xtype: 'ajax',
            proxy: {
                actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
                type: 'chachingProxy',
                url: abp.appPath + 'api/services/app/accountUnit/GetTypeOfAccountList',
                reader: {
                    type: 'json',
                    rootProperty: 'result',
                    totalProperty: 'result.totalCount'
                }
            }
        }
        ,
        typeOfCurrencyList: {
            fields: [{ name: 'name' }, { name: 'value' }, {
                name: 'typeOfCurrency', convert: function (value, record) {
                    return record.get('name');
                }
            }, {
                name: 'typeOfCurrencyId', convert: function (value, record) {
                    return record.get('value');
                }
            }],
            xtype: 'ajax',
            proxy: {
                actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
                type: 'chachingProxy',
                url: abp.appPath + 'api/services/app/accountUnit/GetTypeOfCurrencyList',
                reader: {
                    type: 'json',
                    rootProperty: 'result',
                    totalProperty: 'result.totalCount'
                }
            }
        },
        typeOfCurrencyRateList: {
            fields: [{ name: 'name' }, { name: 'value' }, {
                name: 'typeOfCurrencyRate', convert: function (value, record) {
                    return record.get('name');
                }
            }, {
                name: 'typeOfCurrencyRateId', convert: function (value, record) {
                    return record.get('value');
                }
            }],
            xtype: 'ajax',
            proxy: {
                actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
                type: 'chachingProxy',
                url: abp.appPath + 'api/services/app/accountUnit/GetTypeOfCurrencyRateList',
                reader: {
                    type: 'json',
                    rootProperty: 'result',
                    totalProperty: 'result.totalCount'
                }
            }
        }
    }

});
