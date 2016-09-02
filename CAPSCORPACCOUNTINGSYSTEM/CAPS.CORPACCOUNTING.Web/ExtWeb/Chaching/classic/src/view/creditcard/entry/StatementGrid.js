/**
 * The class is created to provide main UI to access Credit card open statements .
 * Author: kamal
 * Date: 30/08/2016
 */
/**
 * @class Chaching.view.creditcard.entry.StatementGrid
 * UI design for Credit Card->OpenStatements.
 * @alias widget.creditcard.entry
 */
Ext.define('Chaching.view.creditcard.entry.StatementGrid', {
    extend: 'Chaching.view.common.grid.ChachingGridPanel',

    requires: [
        'Chaching.view.creditcard.entry.StatementGridController'
    ],
    xtype: 'creditcard.entry.statementgrid',
    name: 'CreditCard.Entry',
    controller: 'creditcard-entry-statementgrid',
    modulePermissions: {
        read: abp.auth.isGranted('Pages.CreditCard.Entry'),
        create: true,//abp.auth.isGranted('Pages.CreditCard.Entry.Create'),
        edit: true,//abp.auth.isGranted('Pages.CreditCard.Entry.Edit'),
        destroy: abp.auth.isGranted('Pages.CreditCard.Entry.Delete')
    },
    padding: 5,
    gridId: 32,
    store: 'creditcard.entry.StatementDetailStore',
    headerButtonsConfig: [
      {
          xtype: 'displayfield',
          value: abp.localization.localize("CreditCardStatement"),
          ui: 'headerTitle'
      }, '->',
      {
          xtype: 'button',
          scale: 'small',
          ui: 'actionButton',
          action: 'create',
          text: abp.localization.localize("Add").toUpperCase(),
          tooltip: app.localize('CreateCreditCardInvoice'),
          checkPermission: true,
          iconCls: 'fa fa-plus',
          routeName: 'banking.banksetup.create',
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
        title: app.localize('EditCCStatement').initCap(),
        iconCls: 'fa fa-pencil'
    },
    createWndTitleConfig: {
        title: app.localize('CreateCCStatement').initCap(),
        iconCls: 'fa fa-plus'
    },
    viewWndTitleConfig: {
        title: app.localize('ViewCCStatement'),
        iconCls: 'fa fa-th'
    },
    createNewMode: 'tab',
    isSubMenuItemTab: true,
    listeners: {
        cellclick: 'onStatementClick'
    },
    columns: [
         {
             xtype: 'gridcolumn',
             text: app.localize('CreditCardHolder'),
             dataIndex: 'description',
             sortable: true,
             groupable: true,
             width: '15%',
             filterField: {
                 xtype: 'textfield',
                 width: '100%'
             }, editor: {
                 xtype: 'textfield',
                 allowBlank: false
             }
         }, {
             xtype: 'gridcolumn',
             text: app.localize('Invoice#'),
             dataIndex: 'documentReference',
             sortable: true,
             groupable: false,
             width: '15%',
             renderer: ChachingRenderers.rendererHyperLink,
             filterField: {
                 xtype: 'textfield',
                 width: '100%'
             }
         }, {
             xtype: 'gridcolumn',
             text: app.localize('PostingDate'),
             dataIndex: 'transactionDate',
             sortable: true,
             groupable: false,
             width: '10%',
             renderer: ChachingRenderers.dateSearchFieldRenderer,
             filterField: {
                 xtype: 'dateSearchField',
                 width: '100%',
                 dataIndex: 'transactionDate'
             }, editor: {
                 xtype: 'datefield',
                 format: Chaching.utilities.ChachingGlobals.defaultExtDateFieldFormat
             }
         }, {
             xtype: 'gridcolumn',
             text: app.localize('CreditCardTotal'),
             dataIndex: 'controlTotal',
             sortable: true,
             groupable: false,
             width: '15%',
             renderer: Chaching.utilities.ChachingRenderers.amountsRenderer,
             filterField: {
                 xtype: 'textfield',
                 width: '100%'
             }, editor: {
                 xtype: 'numberfield',
                 minValue: 0, //prevents negative numbers
                 // Remove spinner buttons, and arrow key and mouse wheel listeners
                 hideTrigger: true,
                 keyNavEnabled: false,
                 mouseWheelEnabled: false
             }
         }, {
             xtype: 'gridcolumn',
             text: app.localize('CreditCardApGenerated'),
             dataIndex: 'apGenerated',
             sortable: false,
             groupable: false,
             width: '10%',
             renderer : ChachingRenderers.statusRenderer,
             filterField: {
                 xtype: 'combobox',
                 valueField: 'value',
                 displayField: 'text',
                 store: {
                     fields: [{ name: 'text' }, { name: 'value' }],
                     data: [{ text: 'Yes', value: 'true' }, { text: 'No', value: 'false' }]
                 }
             }
         }, {
             xtype: 'gridcolumn',
             text: app.localize('CreditCardBuildAp'),
             dataIndex: 'buildAP',
             sortable: true,
             groupable: false,
             width: '10%'
         },
          {
              xtype: 'gridcolumn',
              text: app.localize('Trans#'),
              dataIndex: 'accountingDocumentId',
              sortable: true,
              groupable: false,
              width: '15%',
              filterField: {
                  xtype: 'textfield',
                  width: '100%'
              }
          }
    ]
});
