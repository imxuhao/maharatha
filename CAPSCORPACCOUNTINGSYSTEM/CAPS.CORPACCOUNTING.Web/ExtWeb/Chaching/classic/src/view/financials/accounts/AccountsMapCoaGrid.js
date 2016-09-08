
Ext.define('Chaching.view.financials.accounts.AccountsMapCoaGrid', {
    extend: 'Chaching.view.common.grid.ChachingGridPanel',
    requires: [
        'Chaching.view.financials.accounts.AccountsMapCoaGridController'
    ],
    controller: 'financials-accounts-accountsmapcoagrid',
    store: 'financials.accounts.AccountsMapToCOAStore',
    modulePermissions: {
        read: abp.auth.isGranted('Pages.Financials.Accounts.Accounts'),
        create: abp.auth.isGranted('Pages.Financials.Accounts.Accounts.Create'),
        edit: abp.auth.isGranted('Pages.Financials.Accounts.Accounts.Edit'),
        destroy: abp.auth.isGranted('Pages.Financials.Accounts.Accounts.Delete'),
        attach: abp.auth.isGranted('Pages.Financials.Accounts.Accounts.Attach'),
        imports: abp.auth.isGranted('Pages.Financials.Accounts.Accounts.Import')
    },
    padding: 5,
    gridId: 36,
    isEditable: true,
    editingMode: 'row',
    columnLines: true,
    multiColumnSort: true,
    requireActionColumn:false,
    columns: [
        {
            xtype: 'gridcolumn',
            text: app.localize('AccountNumber'),
            dataIndex: 'accountNumber',
            sortable: true,
            groupable: true,
            flex: 1,
            filterField: {
                xtype: 'textfield',
                width: '15%',
                emptyText: app.localize('AccountSearch')
            }
        }, {
            xtype: 'gridcolumn',
            text: app.localize('Description'),
            dataIndex: 'caption',
            sortable: true,
            groupable: true,
            flex: 1,
            filterField: {
                xtype: 'textfield',
                width: '100%',
                emptyText: app.localize('DescriptionSearch')
            }
        }, {
            xtype: 'gridcolumn',
            text: app.localize('Classification'),
            dataIndex: 'typeOfAccount',
            itemId: 'typeOfAccountId',
            sortable: true,
            groupable: true,
            sorter: {
                property: 'typeOfAccount',
                sortOnEntity: ''
            },
            flex: 1,
            filterField: {
                xtype: 'combobox',
                queryMode: 'local',
                loadStoreOnCreate: true,
                isViewmodelStore: true,
                forceSelection: true,
                searchProperty: 'typeOfAccountId',
                valueField: 'typeOfAccountId',
                displayField: 'typeOfAccount',
                listConfig: {
                    minWidth: 300
                },
                bind: {
                    store: '{typeOfAccountList}'
                }
            }
        }, {
            xtype: 'gridcolumn',
            text: app.localize('Consolidation'),
            dataIndex: 'typeofConsolidation',
            sortable: true,
            groupable: true,
            sorter: {
                property: 'typeofConsolidation',
                sortOnEntity: ''
            },
            flex: 1,
            filterField: {
                xtype: 'combobox',
                valueField: 'typeofConsolidationId',
                displayField: 'typeofConsolidation',
                queryMode: 'local',
                loadStoreOnCreate: true,
                isViewmodelStore: true,
                forceSelection: true,
                isEnum: true,
                searchProperty: 'typeofConsolidationId',
                bind: {
                    store: '{typeofConsolidationList}'
                }
            }
        },
        {
            xtype: 'gridcolumn',
            text: app.localize('JournalsAllowed'),
            dataIndex: 'isEnterable',
            sortable: true,
            groupable: true,
            flex: 1,
            renderer: Chaching.utilities.ChachingRenderers.rightWrongMarkRenderer,
            filterField: {
                xtype: 'combobox',
                forceSelection: true,
                valueField: 'value',
                displayField: 'text',
                store: {
                    fields: [{ name: 'text' }, { name: 'value' }],
                    data: [{ text: 'YES', value: 'true' }, { text: 'NO', value: 'false' }]
                }
            }
        },
        {
            xtype: 'gridcolumn',
            text: app.localize('RollUpAccount'),
            dataIndex: 'isRollupAccount',
            sortable: true,
            groupable: true,
            flex: 1,
            renderer: Chaching.utilities.ChachingRenderers.rightWrongMarkRenderer,
            filterField: {
                xtype: 'combobox',
                forceSelection: true,
                valueField: 'value',
                displayField: 'text',
                store: {
                    fields: [{ name: 'text' }, { name: 'value' }],
                    data: [{ text: 'YES', value: 'true' }, { text: 'NO', value: 'false' }]
                }
            }
        },
        {
            xtype: 'gridcolumn',
            text: app.localize('EliminationAccount'),
            dataIndex: 'isElimination',
            sortable: true,
            groupable: true,
            flex: 1,
            renderer: Chaching.utilities.ChachingRenderers.rightWrongMarkRenderer,
            filterField: {
                xtype: 'combobox',
                forceSelection: true,
                valueField: 'value',
                displayField: 'text',
                store: {
                    fields: [{ name: 'text' }, { name: 'value' }],
                    data: [{ text: 'YES', value: 'true' }, { text: 'NO', value: 'false' }]
                }
            }
        }, {
            xtype: 'gridcolumn',
            text: app.localize('Currency'),
            dataIndex: 'typeOfCurrency',
            itemId: 'typeOfCurrencyId',
            sortable: true,
            groupable: true,
            flex: 1,
            sorter: {
                property: 'typeOfCurrency',
                sortOnEntity: ''
            },
            filterField: {
                xtype: 'combobox',
                valueField: 'typeOfCurrencyId',
                displayField: 'typeOfCurrency',
                queryMode: 'local',
                loadStoreOnCreate: true,
                isViewmodelStore: true,
                forceSelection: true,
                searchProperty: 'typeOfCurrencyId',
                bind: {
                    store: '{typeOfCurrencyList}'
                }
            }
        }, {
            xtype: 'gridcolumn',
            text: app.localize('RateTypeOverride'),
            dataIndex: 'typeOfCurrencyRate',
            sortable: true,
            groupable: true,
            flex: 1,
            sorter: {
                property: 'typeOfAccountRate',
                sortOnEntity: ''
            },
            filterField: {
                xtype: 'combobox',
                valueField: 'typeOfCurrencyRateId',
                displayField: 'typeOfCurrencyRate',
                loadStoreOnCreate: true,
                isViewmodelStore: true,
                forceSelection: true,
                searchProperty: 'typeOfCurrencyRateId',
                bind: {
                    store: '{typeOfCurrencyRateList}'
                }
            }
        },
        {
            xtype: 'gridcolumn',
            text: app.localize('Multi-CurrencyReval'),
            dataIndex: 'isAccountRevalued',
            sortable: true,
            groupable: true,
            flex: 1,
            renderer: Chaching.utilities.ChachingRenderers.rightWrongMarkRenderer,
            filterField: {
                xtype: 'combobox',
                forceSelection: true,
                valueField: 'value',
                displayField: 'text',
                store: {
                    fields: [{ name: 'text' }, { name: 'value' }],
                    data: [{ text: 'YES', value: 'true' }, { text: 'NO', value: 'false' }]
                }
            }
        }, {
            xtype: 'gridcolumn',
            text: app.localize('NewAccount'),
            dataIndex: 'linkAccount',
            sortable: true,
            groupable: true,
            hidden: false,
            flex: 1,
            sorter: {
                property: 'linkAccount',
                sortOnEntity: ''
            },
            filterField: {
                xtype: 'chachingcombobox',
                store: new Chaching.store.utilities.autofill.MappingAccountStore(),
                width: '100%',
                valueField: 'linkAccountId',
                displayField: 'linkAccount',
                queryMode: 'remote',
                minChars: 2,
                searchProperty: 'linkAccountId',
                modulePermissions: {
                    read: abp.auth.isGranted('Pages.Financials.Accounts.Accounts'),
                    create: abp.auth.isGranted('Pages.Financials.Accounts.Accounts.Create'),
                    edit: abp.auth.isGranted('Pages.Financials.Accounts.Accounts.Edit'),
                    destroy: abp.auth.isGranted('Pages.Financials.Accounts.Accounts.Delete')
                },
                primaryEntityCrudApi: {
                    read: abp.appPath + 'api/services/app/accountUnit/GetAccountsForMapping',
                    create: abp.appPath + 'api/services/app/accountUnit/CreateAccountUnit',
                    update: abp.appPath + 'api/services/app/accountUnit/UpdateAccountUnit',
                    destroy: abp.appPath + 'api/services/app/accountUnit/DeleteAccountUnit'
                },
                createEditEntityType: 'financials.accounts.accounts',
                createEditEntityGridController: 'financials-accounts-accountsgrid',
                entityType: 'Account',
                isTwoEntityPicker: false,
                listeners: {
                    beforequery: function (query, eOpts) {
                        var grid = this.up().grid;
                        if (grid) {
                            var coaId = grid.coaId;
                            var myStore = this.getStore();
                            myStore.getProxy().setExtraParam('id', coaId);
                        }
                    }
                }
            },
            editor: {
                xtype: 'chachingcombobox',
                store: new Chaching.store.utilities.autofill.MappingAccountStore(),
                width: '100%',
                valueField: 'linkAccountId',
                displayField: 'linkAccount',
                queryMode: 'remote',
                minChars: 2,
                modulePermissions: {
                    read: abp.auth.isGranted('Pages.Financials.Accounts.Accounts'),
                    create: abp.auth.isGranted('Pages.Financials.Accounts.Accounts.Create'),
                    edit: abp.auth.isGranted('Pages.Financials.Accounts.Accounts.Edit'),
                    destroy: abp.auth.isGranted('Pages.Financials.Accounts.Accounts.Delete')
                },
                primaryEntityCrudApi: {
                    read: abp.appPath + 'api/services/app/accountUnit/GetAccountsForMapping',
                    create: abp.appPath + 'api/services/app/accountUnit/CreateAccountUnit',
                    update: abp.appPath + 'api/services/app/accountUnit/UpdateAccountUnit',
                    destroy: abp.appPath + 'api/services/app/accountUnit/DeleteAccountUnit'
                },
                createEditEntityType: 'financials.accounts.accounts',
                createEditEntityGridController: 'financials-accounts-accountsgrid',
                entityType: 'Account',
                isTwoEntityPicker: false,
                listeners: {
                    beforequery: function (query, eOpts) {
                        var grid = this.up('grid');
                        if (grid) {
                            var coaId = grid.coaId;
                            var myStore = query.combo.getStore();
                            myStore.getProxy().setExtraParam('id', coaId);
                        }
                    }
                }
            }
        }
    ]

});

