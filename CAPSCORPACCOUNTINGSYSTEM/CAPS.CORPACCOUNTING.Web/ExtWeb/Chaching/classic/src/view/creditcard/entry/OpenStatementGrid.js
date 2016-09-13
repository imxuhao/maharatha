/**
 * The class is created to provide main UI to access Credit card open statements .
 * Author: kamal
 * Date: 30/08/2016
 */
/**
 * @class Chaching.view.creditcard.entry.OpenStatementGrid
 * UI design for Credit Card->OpenStatements.
 * @alias widget.creditcard.entry
 */
Ext.define('Chaching.view.creditcard.entry.OpenStatementGrid', {
    extend: 'Chaching.view.common.grid.ChachingGridPanel',

    requires: [
        'Chaching.view.creditcard.entry.OpenStatementGridController',
        'Ext.menu.Menu'
    ],
    xtype: 'creditcard.entry.ccopenstatements',
    name: 'CreditCard.Entry.OpenStatements',
    controller: 'creditcard-entry-openstatementgrid',
    modulePermissions: {
        read: abp.auth.isGranted('Pages.CreditCard.Entry.OpenStatements'),
        create: true,//abp.auth.isGranted('Pages.CreditCard.Entry.Create'),
        edit: false,//abp.auth.isGranted('Pages.CreditCard.Entry.Edit'),
        destroy: abp.auth.isGranted('Pages.CreditCard.Entry.Delete')
    },
    padding: 5,
    gridId: 31,
    store: 'creditcard.entry.OpenStatementStore',
    headerButtonsConfig: [
      {
          xtype: 'displayfield',
          value: abp.localization.localize("CreditCardOpenStatement"),
          ui: 'headerTitle'
      }, '->',
      {
        xtype: 'splitbutton',
        ui: 'actionButton',
        iconCls: 'fa fa-credit-card',
        iconAlign: 'left',
        tooltip: app.localize('CreditCard'),
        menu: new Ext.menu.Menu({
            ui: 'accounts',
            items: [
                {
                    text: app.localize("SyncCreditCardTrans").toUpperCase(),
                    iconCls: 'fa fa-refresh',
                    itemId: 'syncCreditCardTrans',
                    handler: 'getNewTransactionsFromCCAccount'
                },
                {
                    text: app.localize("UploadCreditCardTrans").toUpperCase(),
                    iconCls: 'fa fa-file-archive-o',
                    itemId: 'uploadCreditCardTrans',
                    handler: 'uploadCreditCardTransClick'
                }
            ]
        })

      }],
    requireExport: true,
    requireMultiSearch: true,
    requireMultisort: true,
    isEditable: false,
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
    listeners : {
        cellclick : 'onStatementDateClick'
    },
    columns: [
         {
             xtype: 'gridcolumn',
             text: app.localize('CreditCardCompany'),
             dataIndex: 'description',
             sortable: true,
             groupable: true,
             width: '45%',
             filterField: {
                 xtype: 'textfield',
                 width: '100%'
             }, editor: {
                 xtype: 'textfield',
                 allowBlank: false
             }
         }, {
             xtype: 'gridcolumn',
             text: app.localize('CreditCardStatementDate'),
             dataIndex: 'documentDate',
             sortable: true,
             groupable: false,
             width: '15%',
             renderer: ChachingRenderers.rendererDateHyperLink,
             filterField: {
                xtype: 'dateSearchField',
                width: '100%',
                dataIndex: 'statementDate'
            }, editor: {
                xtype: 'datefield',
                format: Chaching.utilities.ChachingGlobals.defaultExtDateFieldFormat
            }
         }, {
             xtype: 'gridcolumn',
             text: app.localize('CreditCardStatementBalance'),
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
             text: app.localize('CreditCardStatus'),
             dataIndex: 'status',
             sortable: false,
             groupable: false,
             width: '15%',
             filterField: {
                 xtype: 'combobox',
                 valueField: 'value',
                 displayField: 'text',
                 store: {
                     fields: [{ name: 'text' }, { name: 'value' }],
                     data: [{ text: 'Current', value: 'Current' }, { text: 'Previous', value: 'Previous' }]
                 }
             },
             editor: {
                 xtype: 'combobox',
                 valueField: 'value',
                 displayField: 'text',
                 emptyText: app.localize('SelectOption'),
                 store: {
                     fields: [{ name: 'text' }, { name: 'value' }],
                     data: [{ text: 'Current', value: 'Current' }, { text: 'Previous', value: 'Previous' }]
                 }
             }
         }
    ]
});
