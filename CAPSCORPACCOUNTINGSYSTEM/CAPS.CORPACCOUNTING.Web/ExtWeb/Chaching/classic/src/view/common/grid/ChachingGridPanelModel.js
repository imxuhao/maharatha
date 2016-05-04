Ext.define('Chaching.view.common.grid.ChachingGridPanelModel', {
    extend: 'Ext.app.ViewModel',
    alias: 'viewmodel.common-grid-chachinggridpanel',
    data: {
        name: 'Chaching'
    },
    stores: {
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
                        Id:null
                    },
                    reader: {
                        type: 'json',
                        rootProperty: 'result',
                        totalProperty: 'result.totalCount'
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
                        rootProperty: 'result',
                        totalProperty: 'result.totalCount'
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
        proxy: {
                        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
                        type: 'chachingProxy',
                        url: abp.appPath + 'api/services/app/vendorUnit/GetTypeofAddressList',
                        reader: {
                type: 'json',
                rootProperty: 'result'
                        }
        }
                }
    }

});
