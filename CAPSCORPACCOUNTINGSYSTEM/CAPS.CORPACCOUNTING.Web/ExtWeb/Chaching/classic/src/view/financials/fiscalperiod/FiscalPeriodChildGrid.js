
Ext.define('Chaching.view.financials.fiscalperiod.FiscalPeriodChildGrid', {
    extend: 'Chaching.view.common.grid.ChachingGridPanel',
    xtype: 'widget.financials.fiscalperiodchildgrid',
    modulePermissions: {
        read: abp.auth.isGranted('Pages.Financials.FiscalPeriod'),
        create: abp.auth.isGranted('Pages.Financials.FiscalPeriod.Create'),
        edit: abp.auth.isGranted('Pages.Financials.FiscalPeriod.Edit'),
        destroy: abp.auth.isGranted('Pages.Financials.FiscalPeriod.Delete')
    },
    padding: 5,
    gridId: 22,
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
            //store: 'customers.CustomersStore',
            valueField: 'monthYear',
            displayField: 'monthYear',
            queryMode: 'local'
        }
    }, {
        xtype: 'checkcolumn',
        text: app.localize('PreClose').initCap(),
        dataIndex: 'isPreClose',
        sortable: false,
        groupable: false,
       // renderer: Chaching.utilities.ChachingRenderers.rightWrongMarkRenderer,
        width: '15%'
        //,
        //editor: {
        //    xtype: 'checkboxfield'
        //}
    }, {
        xtype : 'checkcolumn',
        text: app.localize('Close').initCap(),
        dataIndex: 'isPeriodOpen',
        sortable: false,
        groupable: false,
       // renderer: Chaching.utilities.ChachingRenderers.rightWrongMarkRenderer,
        width: '15%'
        //,
        //editor: {
        //    xtype: 'checkboxfield'
        //}
    }]
});
