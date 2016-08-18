/**
 * The class is created to provide main UI to access fiscal periods.
 * Author: kamal
 * Date: 26/05/2016
 */
/**
 * @class Chaching.view.financials.preferences.FiscalPeriodGrid
 * UI design for Fical Period.
 * @alias widget.financials.preferences.fiscalperiod
 */
Ext.define('Chaching.view.financials.fiscalperiod.FiscalPeriodGrid', {
    extend: 'Chaching.view.common.grid.ChachingGridPanel',

    requires: [
        'Chaching.view.financials.fiscalperiod.FiscalPeriodGridController'
    ],
    xtype: 'financials.fiscalperiod',
    name : 'Financials.FiscalPeriod',
    controller: 'financials.fiscalperiodgrid',
    modulePermissions: {
        read: abp.auth.isGranted('Pages.Financials.FiscalPeriod'),
        create: abp.auth.isGranted('Pages.Financials.FiscalPeriod.Create'),
        edit: abp.auth.isGranted('Pages.Financials.FiscalPeriod.Edit'),
        destroy: abp.auth.isGranted('Pages.Financials.FiscalPeriod.Delete')
    },
    padding: 5,
    gridId: 22,
    store: 'financials.fiscalperiod.FiscalYearStore',
    headerButtonsConfig: [
      {
          xtype: 'displayfield',
          value: abp.localization.localize("FiscalYearPeriod"),
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
          routeName: 'financials.fiscalperiod.create',
          iconAlign: 'left'
      }],
    requireExport: true,
    requireMultiSearch: true,
    requireMultisort: true,
    isEditable: true,
    editingMode: 'row',
    columnLines: true,
    multiColumnSort: true,
    editWndTitleConfig: {
        title: app.localize('EditFiscalPeriod').initCap(),
        iconCls: 'fa fa-pencil'
    },
    createWndTitleConfig: {
        title: app.localize('CreateNewFiscalPeriod').initCap(),
        iconCls: 'fa fa-plus'
    },
    viewWndTitleConfig: {
        title: app.localize('ViewFiscalPeriod'),
        iconCls: 'fa fa-th'
    },
    createNewMode: 'tab',
    forceFit: true,
    isSubMenuItemTab: true,
    //listeners: {
    //    cellclick: 'onProjectsCellClick'
    //},
    columns: [
         {
             xtype: 'gridcolumn',
             text: app.localize('FiscalStartDate'),
             dataIndex: 'yearStartDate',
             sortable: true,
             groupable: true,
             flex:1,
             //width: '15%',
             renderer : Chaching.utilities.ChachingRenderers.renderDateOnly,
            // renderer: Chaching.utilities.ChachingRenderers.rendererHyperLink,
             filterField: {
                 xtype: 'dateSearchField',
                 width: '100%',
                 emptyText: app.localize('ToolTipFiscalStartDate')
             }, editor: {
                 xtype: 'datefield',
                 allowBlank: false
             }
         }, {
             xtype: 'gridcolumn',
             text: app.localize('FiscalEndDate').initCap(),
             dataIndex: 'yearEndDate',
             sortable: true,
             groupable: true,
             flex:1,
             //width: '15%',
             renderer: Chaching.utilities.ChachingRenderers.renderDateOnly,
             filterField: {
                 xtype: 'datefield',
                 width: '100%',
                 emptyText: app.localize('ToolTipFiscalEndDate')
             }, editor: {
                 xtype: 'datefield',
                 allowBlank: false
             }
         }, {
             xtype: 'gridcolumn',
             text: app.localize('FiscalYearOpen').initCap(),
             dataIndex: 'isYearOpen',
             sortable: false,
             groupable: false,
             flex: 1,
             align : 'left',
             renderer: Chaching.utilities.ChachingRenderers.rightWrongMarkRenderer//,
             //width: '13%'
         }

    ]
});
