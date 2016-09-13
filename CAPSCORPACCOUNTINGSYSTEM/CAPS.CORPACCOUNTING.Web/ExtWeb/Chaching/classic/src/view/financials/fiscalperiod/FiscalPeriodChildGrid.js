
Ext.define('Chaching.view.financials.fiscalperiod.FiscalPeriodChildGrid', {
    extend: 'Chaching.view.common.grid.ChachingGridPanel',
    xtype: 'financials.fiscalperiodchildgrid',
    modulePermissions: {
        read: abp.auth.isGranted('Pages.Financials.FiscalPeriod'),
        create: abp.auth.isGranted('Pages.Financials.FiscalPeriod.Create'),
        edit: abp.auth.isGranted('Pages.Financials.FiscalPeriod.Edit'),
        destroy: abp.auth.isGranted('Pages.Financials.FiscalPeriod.Delete')
    },
    padding: 5,
    gridId: 20,
    itemId : 'fiscalPeriodGrid',
    controller: 'financials.fiscalperiodchildgrid',
    store: 'financials.fiscalperiod.FiscalPeriodStore',
    headerButtonsConfig: [{
        xtype: 'displayfield',
        value: app.localize('FiscalYearPeriod'),
        ui: 'headerTitle'
    }, '->', {
        xtype: 'button',
        scale: 'small',
        ui: 'actionButton',
        action: 'create',
        text: abp.localization.localize("Add").toUpperCase(),
        tooltip: app.localize('CreateNewFiscalPeriod'),
        checkPermission: true,
        iconCls: 'fa fa-plus',
        //routeName: 'project.projectmaintenance.projects.create',
        iconAlign: 'left'
    }],
    requireExport: false,
    requireMultiSearch: false,
    requireMultisort: false,
    isEditable: true,
    editingMode: 'cell',
    columnLines: true,
    multiColumnSort: false,
    createNewMode: 'inline',
    isSubMenuItemTab: false,
    showPagingToolbar: false,
    autoScroll: true,
    columns: [{
        text: app.localize('ClosingReport').initCap(),
        dataIndex: 'reportName',
        sortable: false,
        groupable: false,
        width: '15%',
        editor: {
            xtype: 'combobox',
            //store: 'customers.CustomersStore',
            store : {
                fields: [{ name: 'reportName' }, { name: 'reportId' }],
                data : [{reportName : 'Cost Manager Report', reportId : '1'},
                    {reportName : 'Summary Report', reportId : '1'}
                ]
            },
            valueField: 'reportId',
            displayField: 'reportName',
            queryMode: 'local'
        }
    }, {
        text: app.localize('MonthYear').initCap(),
        dataIndex: 'monthYear',
        sortable: false,
        groupable: false,
        width: '10%', 
        editor: {
            xtype: 'combobox',
            store: {
                fields: [{ name: 'monthYear' }, { name: 'month' }, { name: 'year' }],
                data: [{ monthYear: 'Jan-2015'  },
                        { monthYear: 'Feb-2015'   },
                        { monthYear: 'Mar-2015'  },
                        { monthYear: 'Apr-2015'  },
                        { monthYear: 'May-2015'   },
                        { monthYear: 'Jun-2015'  },
                        { monthYear: 'Jul-2015'  },
                        { monthYear: 'Aug-2015'   },
                        { monthYear: 'Sep-2015'  },
                        { monthYear: 'Oct-2015'  },
                        { monthYear: 'Nov-2015'   },
                        { monthYear: 'Dec-2015'  },
                        { monthYear: 'Jan-2016'   },
                        { monthYear: 'Feb-2016'   },
                        { monthYear: 'Mar-2016'  },
                        { monthYear: 'Apr-2016'  },
                        { monthYear: 'May-2016'   },
                        { monthYear: 'Jun-2016'  },
                        { monthYear: 'Jul-2016'  },
                        { monthYear: 'Aug-2016'   },
                        { monthYear: 'Sep-2016'  },
                        { monthYear: 'Oct-2016'  },
                        { monthYear: 'Nov-2016'   },
                        { monthYear: 'Dec-2016'  }
                ]
            },
            valueField: 'monthYear',
            displayField: 'monthYear',
            queryMode: 'local'
        }
    },
    {
        xtype: 'checkcolumn',
        text: app.localize('PreClose').initCap(),
        dataIndex: 'isPreClose',
        sortable: false,
        groupable: false,
       // renderer: Chaching.utilities.ChachingRenderers.rightWrongMarkRenderer,
        width: '15%',
        listeners: {
            checkchange: 'onFiscalPeriodModeChange'
        }
        //,
        //editor: {
        //    xtype: 'checkboxfield'
        //}
    }, {
        xtype : 'checkcolumn',
        text: app.localize('Close').initCap(),
        dataIndex: 'isClose',
        sortable: false,
        groupable: false,
       // renderer: Chaching.utilities.ChachingRenderers.rightWrongMarkRenderer,
        width: '15%',
        listeners: {
            checkchange: 'onFiscalPeriodModeChange'
        }
        //,
        //editor: {
        //    xtype: 'checkboxfield'
        //}
    }]
});
