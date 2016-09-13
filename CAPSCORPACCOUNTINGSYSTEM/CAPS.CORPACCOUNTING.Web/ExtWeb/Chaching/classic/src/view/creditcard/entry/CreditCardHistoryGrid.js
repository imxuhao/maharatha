/**
 * The class is created to provide main UI to access Credit card History .
 * Author: kamal
 * Date: 30/08/2016
 */
/**
 * @class Chaching.view.creditcard.entry.CreditCardHistoryGrid
 * UI design for Credit Card->CreditCardHistoryGrid.
 * @alias widget.creditcard.entry.ccdhistory
 */
Ext.define('Chaching.view.creditcard.entry.CreditCardHistoryGrid', {
    extend: 'Chaching.view.common.grid.ChachingGridPanel',

    requires: [
        'Chaching.view.creditcard.entry.CreditCardHistoryGridController'
    ],
    xtype: 'creditcard.entry.ccdhistory',
    name: 'CreditCard.Entry.CreditCardHistory',
    controller: 'creditcard-entry-creditcardhistorygrid',
    modulePermissions: {
        read: abp.auth.isGranted('Pages.CreditCard.Entry.CreditCardHistory'),
        create: true,//abp.auth.isGranted('Pages.CreditCard.Entry.CreditCardHistory.Create'),
        edit: true,//abp.auth.isGranted('Pages.CreditCard.Entry.CreditCardHistory.Edit'),
        destroy: abp.auth.isGranted('Pages.CreditCard.Entry.CreditCardHistory.Delete')
    },
    padding: 5,
    gridId: 35,
    store: 'creditcard.entry.CreditCardHistoryStore',
    headerButtonsConfig: [
      {
          xtype: 'displayfield',
          value: abp.localization.localize("CreditCardHistory"),
          ui: 'headerTitle'
      }, '->'],
    requireActionColumn : false,
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
    listeners: {
        cellclick: 'onStatementDateClick'
    },
    columns: [
        {
            xtype: 'gridcolumn',
            text: app.localize('CreditCardStatementDate'),
            dataIndex: 'statementDate',
            sortable: true,
            groupable: false,
            width: '10%',
            renderer: ChachingRenderers.dateSearchFieldRenderer,
            filterField: {
                xtype: 'dateSearchField',
                width: '100%',
                dataIndex: 'statementDate'
            }
        },
         {
             xtype: 'gridcolumn',
             text: app.localize('PO#'),
             dataIndex: 'purchaseOrderNumber',
             sortable: true,
             groupable: true,
             width: '15%',
             filterField: {
                 xtype: 'textfield',
                 width: '100%'
             }
         },
         {
             xtype: 'gridcolumn',
             text: app.localize('JobDivision'),
             dataIndex: 'jobNumber',
             sortable: true,
             groupable: true,
             width: '15%',
             filterField: {
                 xtype: 'textfield',
                 width: '100%'
             }
         },
          {
              xtype: 'gridcolumn',
              text: app.localize('AccountLine'),
              dataIndex: 'accountNumber',
              sortable: true,
              groupable: true,
              width: '15%',
              filterField: {
                  xtype: 'textfield',
                  width: '100%'
              }
          },
          {
              xtype: 'gridcolumn',
              text: app.localize('SubAccount1'),
              dataIndex: 'subAccountNumber1',
              sortable: true,
              groupable: true,
              width: '15%',
              filterField: {
                  xtype: 'textfield',
                  width: '100%'
              }
          },
          {
              xtype: 'gridcolumn',
              text: app.localize('SubAccount2'),
              dataIndex: 'subAccountNumber2',
              sortable: true,
              groupable: true,
              width: '15%',
              filterField: {
                  xtype: 'textfield',
                  width: '100%'
              }
          },
          {
              xtype: 'gridcolumn',
              text: app.localize('SubAccount3'),
              dataIndex: 'subAccountNumber3',
              sortable: true,
              groupable: true,
              width: '15%',
              filterField: {
                  xtype: 'textfield',
                  width: '100%'
              }
          },
          {
              xtype: 'gridcolumn',
              text: app.localize('SubAccount4'),
              dataIndex: 'subAccountNumber4',
              sortable: true,
              groupable: true,
              width: '15%',
              filterField: {
                  xtype: 'textfield',
                  width: '100%'
              }
          },
          {
              xtype: 'gridcolumn',
              text: app.localize('SubAccount5'),
              dataIndex: 'subAccountNumber5',
              sortable: true,
              groupable: true,
              width: '15%',
              filterField: {
                  xtype: 'textfield',
                  width: '100%'
              }
          },
          {
              xtype: 'gridcolumn',
              text: app.localize('SubAccount6'),
              dataIndex: 'subAccountNumber6',
              sortable: true,
              groupable: true,
              width: '15%',
              filterField: {
                  xtype: 'textfield',
                  width: '100%'
              }
          },
          {
              xtype: 'gridcolumn',
              text: app.localize('SubAccount7'),
              dataIndex: 'subAccountNumber7',
              sortable: true,
              groupable: true,
              width: '15%',
              filterField: {
                  xtype: 'textfield',
                  width: '100%'
              }
          },
          {
              xtype: 'gridcolumn',
              text: app.localize('SubAccount8'),
              dataIndex: 'subAccountNumber8',
              sortable: true,
              groupable: true,
              width: '15%',
              filterField: {
                  xtype: 'textfield',
                  width: '100%'
              }
          },
          {
              xtype: 'gridcolumn',
              text: app.localize('SubAccount9'),
              dataIndex: 'subAccountNumber9',
              sortable: true,
              groupable: true,
              width: '15%',
              filterField: {
                  xtype: 'textfield',
                  width: '100%'
              }
          },
          {
              xtype: 'gridcolumn',
              text: app.localize('SubAccount10'),
              dataIndex: 'subAccountNumber10',
              sortable: true,
              groupable: true,
              width: '15%',
              filterField: {
                  xtype: 'textfield',
                  width: '100%'
              }
          },
          
          {
              xtype: 'gridcolumn',
              text: app.localize('Amount'),
              dataIndex: 'amount',
              sortable: true,
              groupable: true,
              width: '15%',
              filterField: {
                  xtype: 'textfield',
                  width: '100%'
              }
          },
          {
              xtype: 'gridcolumn',
              text: app.localize('Description'),
              dataIndex: 'description',
              sortable: true,
              groupable: true,
              width: '15%',
              filterField: {
                  xtype: 'textfield',
                  width: '100%'
              }
          },
          {
              xtype: 'gridcolumn',
              text: app.localize('ItemMemo'),
              dataIndex: 'itemMemo',
              sortable: true,
              groupable: true,
              width: '15%',
              filterField: {
                  xtype: 'textfield',
                  width: '100%'
              }
          },
          {
              xtype: 'gridcolumn',
              text: app.localize('Vendor'),
              dataIndex: 'vendorName',
              sortable: true,
              groupable: true,
              width: '15%',
              filterField: {
                  xtype: 'textfield',
                  width: '100%'
              }
          },
          {
              xtype: 'gridcolumn',
              text: app.localize('ChargeDate'),
              dataIndex: 'chargeDate',
              sortable: true,
              groupable: false,
              width: '10%',
              renderer: ChachingRenderers.dateSearchFieldRenderer,
              filterField: {
                  xtype: 'dateSearchField',
                  width: '100%',
                  dataIndex: 'chargeDate'
              }
          },
         {
             xtype: 'gridcolumn',
             text: app.localize('CreditCardHolder'),
             dataIndex: 'cardHolder',
             sortable: true,
             groupable: true,
             width: '15%',
             filterField: {
                 xtype: 'textfield',
                 width: '100%'
             }
         }, {
             xtype: 'gridcolumn',
             text: app.localize('CreditCardApPayment#'),
             dataIndex: 'apPaymentNumber',
             sortable: true,
             groupable: true,
             width: '15%',
             filterField: {
                 xtype: 'textfield',
                 width: '100%'
             }
         }, {
             xtype: 'gridcolumn',
             text: app.localize('CreditCardApCodingJobDivision'),
             dataIndex: 'apCodingJobNumber',
             sortable: true,
             groupable: true,
             width: '15%',
             filterField: {
                 xtype: 'textfield',
                 width: '100%'
             }
         }, {
             xtype: 'gridcolumn',
             text: app.localize('CreditCardApCodingAccountLine'),
             dataIndex: 'apCodingAccountNumber',
             sortable: true,
             groupable: true,
             width: '15%',
             filterField: {
                 xtype: 'textfield',
                 width: '100%'
             }
         },

          {
              xtype: 'gridcolumn',
              text: app.localize('APCodingSubAccount1'),
              dataIndex: 'apCodingSubAccountNumber1',
              sortable: true,
              groupable: true,
              width: '15%',
              filterField: {
                  xtype: 'textfield',
                  width: '100%'
              }
          },
          {
              xtype: 'gridcolumn',
              text: app.localize('APCodingSubAccount2'),
              dataIndex: 'APCodingSubAccountNumber2',
              sortable: true,
              groupable: true,
              width: '15%',
              filterField: {
                  xtype: 'textfield',
                  width: '100%'
              }
          },
          {
              xtype: 'gridcolumn',
              text: app.localize('APCodingSubAccount3'),
              dataIndex: 'APCodingSubAccountNumber3',
              sortable: true,
              groupable: true,
              width: '15%',
              filterField: {
                  xtype: 'textfield',
                  width: '100%'
              }
          },
          {
              xtype: 'gridcolumn',
              text: app.localize('APCodingSubAccount4'),
              dataIndex: 'APCodingSubAccountNumber4',
              sortable: true,
              groupable: true,
              width: '15%',
              filterField: {
                  xtype: 'textfield',
                  width: '100%'
              }
          },
          {
              xtype: 'gridcolumn',
              text: app.localize('APCodingSubAccount5'),
              dataIndex: 'APCodingSubAccountNumber5',
              sortable: true,
              groupable: true,
              width: '15%',
              filterField: {
                  xtype: 'textfield',
                  width: '100%'
              }
          },
          {
              xtype: 'gridcolumn',
              text: app.localize('APCodingSubAccount6'),
              dataIndex: 'APCodingSubAccountNumber6',
              sortable: true,
              groupable: true,
              width: '15%',
              filterField: {
                  xtype: 'textfield',
                  width: '100%'
              }
          },
          {
              xtype: 'gridcolumn',
              text: app.localize('APCodingSubAccount7'),
              dataIndex: 'APCodingSubAccountNumber7',
              sortable: true,
              groupable: true,
              width: '15%',
              filterField: {
                  xtype: 'textfield',
                  width: '100%'
              }
          },
          {
              xtype: 'gridcolumn',
              text: app.localize('APCodingSubAccount8'),
              dataIndex: 'APCodingSubAccountNumber8',
              sortable: true,
              groupable: true,
              width: '15%',
              filterField: {
                  xtype: 'textfield',
                  width: '100%'
              }
          },
          {
              xtype: 'gridcolumn',
              text: app.localize('APCodingSubAccount9'),
              dataIndex: 'APCodingSubAccountNumber9',
              sortable: true,
              groupable: true,
              width: '15%',
              filterField: {
                  xtype: 'textfield',
                  width: '100%'
              }
          },
          {
              xtype: 'gridcolumn',
              text: app.localize('APCodingSubAccount10'),
              dataIndex: 'APCodingSubAccountNumber10',
              sortable: true,
              groupable: true,
              width: '15%',
              filterField: {
                  xtype: 'textfield',
                  width: '100%'
              }
          },

         {
             xtype: 'gridcolumn',
             text: app.localize('Attachment'),
             dataIndex: 'uploadDocumentLog',
             sortable: true,
             groupable: false,
             width: '15%',
             filterField: {
                 xtype: 'textfield',
                 width: '100%'
             }
         }, {
             xtype: 'gridcolumn',
             text: app.localize('CreditCardStatus'),
             dataIndex: 'status',
             sortable: false,
             groupable: false,
             width: '10%',
             filterField: {
                 xtype: 'combobox',
                 valueField: 'value',
                 displayField: 'text',
                 store: {
                     fields: [{ name: 'text' }, { name: 'value' }],
                     data: [{ text: 'Posted', value: 'true' }, { text: 'Unposted', value: 'false' }]
                 }
             }
         }
    ]
});
