/**
 * This Class is base ViewModel class for all forms which are inherited from base formpanel.
 * Author: Krishna Garad
 * Date: 04/05/2016
 */
/**
 * @class Chaching.view.common.form.ChachingFormPanelModel
 * The base ViewModel class for all formpanels 
 */
Ext.define('Chaching.view.common.form.ChachingFormPanelModel', {
    extend: 'Ext.app.ViewModel',
    alias: 'viewmodel.common-form-chachingformpanel',
    data: {
        name: 'Chaching'
    },
    /**
    * @cfg {object}
    * A config option for for stores in viewmodel
    */
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
            remoteSort: false,
            remoteFilter: false,
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
        TypeOfChartList: {
            fields: [{ name: 'name' }, { name: 'value' }, {
                name: 'typeOfChart', convert: function (value, record) {
                    return record.get('name');
                }
            }, {
                name: 'typeOfChartId', convert: function (value, record) {
                    return record.get('value');
                }
            }],
            xtype: 'ajax',
            remoteSort: false,
            remoteFilter: false,
            autoLoad: false,
            proxy: {
                actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
                type: 'chachingProxy',
                url: abp.appPath + 'api/services/app/coaUnit/GetTypeOfChartList',
                reader: {
                    type: 'json',
                    rootProperty: 'result',
                    totalProperty: 'result.totalCount'
                }
            }
        },

        linkChartOfAccountList: {
            fields: [{ name: 'coaId' }, { name: 'caption' }, {name:'typeOfChartId'}, {
                name: 'linkChartOfAccount', convert: function (value, record) {
                    return record.get('caption');
                }
            }, {
                name: 'linkChartOfAccountID', convert: function (value, record) {
                    return record.get('coaId');
                }
            }],
            xtype: 'ajax',
            remoteSort: false,
            remoteFilter: false,
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
            //xtype: 'ajax',
            autoLoad: false,
            remotSort: false,
            remoteFilter : false,
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
            remoteSort: false,
            remoteFilter : false,
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
           // xtype: 'ajax',
            remoteSort: false,
            remoteFilter : false,
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
           // xtype: 'ajax',
            remoteSort: false,
            remoteFilter : false,
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
           // xtype: 'ajax',
            remoteSort: false,
            remoteFilter:false,
            autoLoad: false,
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
        },
        linkAccountListByCoaId: {

            fields: [{ name: 'name' }, { name: 'value' }, {
                name: 'linkAccount', convert: function (value, record) {
                    return record.get('name');
                }
            }, {
                name: 'linkAccountId', convert: function (value, record) {
                    return record.get('value');
                }
            }],
            xtype: 'ajax',
            extraParams: {
                id: 0
            },
            proxy: {
                actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
                type: 'chachingProxy',
                url: abp.appPath + 'api/services/app/accountUnit/GetLinkAccountListByCoaId',
                reader: {
                    type: 'json',
                    rootProperty: 'result',
                    totalProperty: 'result.totalCount'
                }
            }
        },
        rollupAccountList: {
            fields: [{ name: 'name' }, { name: 'value' }, {
                name: 'rollupAccount', convert: function (value, record) {
                    return record.get('name');
                }
            }, {
                name: 'rollupAccountId', convert: function (value, record) {
                    return record.get('value');
                }
            }],
            xtype: 'ajax',
            proxy: {
                actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
                type: 'chachingProxy',
                url: abp.appPath + 'api/services/app/accountUnit/GetRollupAccountsList',
                extraParams: {
                    Id: null
                },
                reader: {
                    type: 'json',
                    rootProperty: 'result'
                }
            }
        },

        rollupDivisionList: {
            fields: [{ name: 'name' }, { name: 'value' }, {
                name: 'rollupDivision', convert: function (value, record) {
                    return record.get('name');
                }
            }, {
                name: 'rollupDivisionId', convert: function (value, record) {
                    return record.get('value');
                }
            }],
            xtype: 'ajax',
            proxy: {
                actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
                type: 'chachingProxy',
                url: abp.appPath + 'api/services/app/jobUnit/GetDivisionList',
                reader: {
                    type: 'json',
                    rootProperty: 'result'
                }
            }
        },
        vendorTypeList: {
            fields: [{ name: 'name' }, { name: 'value' }, {
                name: 'typeofvendor', convert: function (value, record) {
                    return record.get('name');
                }
            }, {
                name: 'typeofvendorId', convert: function (value, record) {
                    return record.get('value');
                }
            }],
            xtype: 'ajax',
            remoteSort: false,
            remoteFilter : false,
            proxy: {
                actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
                type: 'chachingProxy',
                url: abp.appPath + 'api/services/app/vendorUnit/GetTypeofVendorList',
                reader: {
                    type: 'json',
                    rootProperty: 'result'
                }
            }
        },
        typeof1099BoxList: {
            fields: [{ name: 'name' }, { name: 'value' }, {
                name: 'typeof1099Box', convert: function (value, record) {
                    return record.get('name');
                }
            }, {
                name: 'typeof1099BoxId', convert: function (value, record) {
                    return record.get('value');
                }
            }],
            xtype: 'ajax',
            remoteSort: false,
            remoteFilter: false,
            proxy: {
                actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
                type: 'chachingProxy',
                url: abp.appPath + 'api/services/app/vendorUnit/GetTypeof1099T4List',
                reader: {
                    type: 'json',
                    rootProperty: 'result'
                }
            }
        },
        genericRollupAccountList: {
            fields: [{ name: 'name' }, { name: 'value' }],
            xtype: 'ajax',
            proxy: {
                actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
                type: 'chachingProxy',
                url: abp.appPath + 'api/services/app/jobUnit/GetGenericRollupAccountsList',
                reader: {
                    type: 'json',
                    rootProperty: 'result'
                }
            }
        },
        typeofAddressList: {
            fields: [{ name: 'name' }, { name: 'value' }, {
                name: 'typeofAddress', convert: function (value, record) {
                    return record.get('name');
                }
            }, {
                name: 'typeofAddressId', convert: function (value, record) {
                    return record.get('value');
                }
            }],
            xtype: 'ajax',
            remoteSort: false,
            remoteFilter: false,
            proxy: {
                actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
                type: 'chachingProxy',
                url: abp.appPath + 'api/services/app/vendorUnit/GetTypeofAddressList',
                reader: {
                    type: 'json',
                    rootProperty: 'result'
                }
            }
        },
        typeOfTaxList: {
            fields: [{ name: 'name' }, { name: 'value' }, {
                name: 'typeofTax', convert: function (value, record) {
                    return record.get('name');
                }
            }, {
                name: 'typeofTaxId', convert: function (value, record) {
                    return record.get('value');
                }
            }],
            xtype: 'ajax',
            remoteFilter: false,
            remoteSort : false,
            proxy: {
                actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
                type: 'chachingProxy',
                url: abp.appPath + 'api/services/app/vendorUnit/GetTypeOfTaxList',
                reader: {
                    type: 'json',
                    rootProperty: 'result'
                }
            }
        },
        getTaxRecoveryList: {
            fields: [{ name: 'name' }, { name: 'value' }],
            xtype: 'ajax',
            proxy: {
                actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
                type: 'chachingProxy',
                url: abp.appPath + 'api/services/app/jobUnit/GetTaxRecovery',
                reader: {
                    type: 'json',
                    rootProperty: 'result'
                }
            }
        },
        getTaxCreditList: {
            fields: [{ name: 'name' }, { name: 'value' }, { name: 'taxCreditId', mapping: 'value' }],
            xtype: 'ajax',
            proxy: {
                actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
                type: 'chachingProxy',
                url: abp.appPath + 'api/services/app/jobUnit/GetTaxCreditList',
                reader: {
                    type: 'json',
                    rootProperty: 'result'
                }
            }
        },
        paymentTermsList: {
            fields: [{ name: 'name' }, { name: 'value' }, {
                name: 'paymentTerms', convert: function (value, record) {
                    return record.get('name');
                }
            }, {
                name: 'paymentTermsId', convert: function (value, record) {
                    return record.get('value');
                }
            }],
            xtype: 'ajax',
            proxy: {
                actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
                type: 'chachingProxy',
                url: abp.appPath + 'api/services/app/vendorUnit/GetPaymentTermsList',
                reader: {
                    type: 'json',
                    rootProperty: 'result'
                }
            }
        },
        getAccountsListLines: {
            fields: [{ name: 'name' }, { name: 'value' }, {

                name: 'account', convert: function (value, record) {
                    return record.get('name');
                }
            }, {
                name: 'accountId', convert: function (value, record) {
                    return record.get('value');
                }
            }],
            xtype: 'ajax',
            extraParams: {
                value: false
            },
            proxy: {
                actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
                type: 'chachingProxy',
                url: abp.appPath + 'api/services/app/vendorUnit/GetAccountsList',
                reader: {
                    type: 'json',
                    rootProperty: 'result'
                }
            }
        },

        getAccountsList: {
            fields: [{ name: 'name' }, { name: 'value' }, {

                name: 'account', convert: function (value, record) {
                    return record.get('name');
                }
            }, {
                name: 'accountId', convert: function (value, record) {
                    return record.get('value');
                }
            }],
            xtype: 'ajax',
            extraParams: {
                value: false
            },
            proxy: {
                actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
                type: 'chachingProxy',
                url: abp.appPath + 'api/services/app/vendorUnit/GetAccountsList',
                reader: {
                    type: 'json',
                    rootProperty: 'result'
                }
            }
        }
    }

});
