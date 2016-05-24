﻿Ext.define('Chaching.view.invoices.BatchGrid', {
    extend: 'Chaching.view.common.grid.ChachingGridPanel',
    requires: [
        'Chaching.view.payables.invoices.BatchGridController'
    ],
    controller: 'payables-invoices-batchgrid',
    xtype: 'widget.payables.invoices.batch',
    store: 'payables.invoices.BatchStore',
    name: 'invoices.Batch',
    modulePermissions: {
        read: abp.auth.isGranted('Pages.Payables.Invoices.Batch'),
        create: abp.auth.isGranted('Pages.Payables.Invoices.Batch.Create'),
        edit: abp.auth.isGranted('Pages.Payables.Invoices.Batch.Edit'),
        destroy: abp.auth.isGranted('Pages.Payables.Invoices.Batch.Delete'),
    },
    padding: 5,
    gridId: 19,
    groupingConfig: {
        ftype: 'grouping',
        groupHeaderTpl: '<b>TransactionType: {name} ({rows.length} Item{[values.rows.length > 1 ? "s" : ""]})</b>',
        hideGroupedHeader: true,
        startCollapsed: false
    },
    headerButtonsConfig: [
        //{
        //xtype: 'displayfield',
        //value: abp.localization.localize("BatchPosting"),
        //ui: 'headerTitle'
        //},
    {
        xtype: 'checkboxgroup',
        border: false,
        columns: 7,
        vertical: false,
        defaults: {
            inputValue: 'true',
            uncheckedValue: 'false',
            boxLabelAlign: 'before',
            width:110
            //ui: 'default',
            //boxLabelCls: 'checkboxLabel'
        },

        items: [
    {
        //xtype: 'checkboxfield',
        name: 'BatchType',
        boxLabel: app.localize('ShowAllModules') + ":",
        inputValue: 'All'
    },
      {
          //xtype: 'checkboxfield',
          name: 'BatchType',
          boxLabel: app.localize('Journal') + ":",
          inputValue: '1,2,10,11',
          padding: '0 0 0 5px'
      },
      {
          //xtype: 'checkboxfield',
          name: 'BatchType',
          boxLabel: app.localize('AccountsPayable') + ":",
          inputValue: '3',
          padding: '0 0 0 5px'
      }
      ,
      {
          //xtype: 'checkboxfield',
          name: 'BatchType',
          boxLabel: app.localize('Receivables') + ":",
          inputValue: '4',
          padding: '0 0 0 5px'
      }
      ,
      {
          //xtype: 'checkboxfield',
          name: 'BatchType',
          boxLabel: app.localize('PettyCash') + ":",
          inputValue: '6,7,9',
          labelWidth:100,
          padding: '0 0 0 5px'
      },
      {
          //xtype: 'checkboxfield',
          name: 'BatchType',
          boxLabel: app.localize('Payroll') + ":",
          inputValue: '5',
          padding: '0 0 0 5px'
      },
      {
          //xtype: 'checkboxfield',
          name: 'BatchType',
          boxLabel: app.localize('CreditCard') + ":",
          inputValue: '',
          padding: '0 0 0 5px'
      }

        ]
    }, '->', {
        xtype: 'button',
        scale: 'small',
        ui: 'actionButton',
        action: 'create',
        text: abp.localization.localize("Add").toUpperCase(),
        checkPermission: false,
        iconCls: 'fa fa-plus',
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
        title: app.localize('EditBatch'),
        iconCls: 'fa fa-pencil'
    },
    createWndTitleConfig: {
        title: app.localize('CreatingNewBatch'),
        iconCls: 'fa fa-plus'
    },
    createNewMode: 'tab',
    isSubMenuItemTab: true,
    columns: [
         {
             text: app.localize('Posting'),
             width: '13%',
             xtype: 'checkcolumn'

         },
          {
              text: app.localize('BatchName'),
              dataIndex: 'description',
              xtype: 'gridcolumn',
              sortable: false,
              groupable: false,
              width: '13%',
              editor: {
                  xtype: 'textfield',
                  name: 'description'
              },
              filterField: {
                  xtype: 'textfield'

              }
          },
          {
              text: app.localize('PostingDate'),
              dataIndex: 'postingDate',
              xtype: 'gridcolumn',
              sortable: false,
              groupable: false,
              width: '13%',
              renderer: Chaching.utilities.ChachingRenderers.dateSearchFieldRenderer,
              filterField: {
                  xtype: 'dateSearchField',
                  dataIndex: 'postingDate',
                  width: '100%'
              },
              editor: {
                  xtype: 'textfield',
                  name: 'postingDate'
              }
          }
          ,
          {
              text: app.localize('ControlTotal'),
              dataIndex: 'controlTotal',
              xtype: 'gridcolumn',
              sortable: false,
              groupable: false,
              width: '13%',
              editor: {
                  xtype: 'textfield',
                  name: 'controlTotal'
              },
              filterField: {
                  xtype: 'textfield'

              }
          }
           ,
          {
              text: app.localize('BatchAmount'),
              dataIndex: 'batchAmount',
              xtype: 'gridcolumn',
              sortable: false,
              groupable: false,
              width: '13%',
          }
           , {
               xtype: 'gridcolumn',
               text: app.localize('RetainBatch'),
               dataIndex: 'isRetained',
               sortable: true,
               groupable: true,
               width: '18%',
               renderer: Chaching.utilities.ChachingRenderers.rightWrongMarkRenderer,
               filterField: {
                   xtype: 'combobox',
                   valueField: 'value',
                   displayField: 'text',
                   width: '100%',
                   store: {
                       fields: [{ name: 'text' }, { name: 'value' }],
                       data: [{ text: 'YES', value: 'true' }, { text: 'NO', value: 'false' }]
                   }
               }
           }, {
               xtype: 'gridcolumn',
               text: app.localize('PostingStatus'),
               dataIndex: 'postingStatus',
               sortable: true,
               groupable: true,
               width: '18%',
               renderer: Chaching.utilities.ChachingRenderers.rightWrongMarkRenderer,
               filterField: {
                   xtype: 'combobox',
                   valueField: 'value',
                   displayField: 'text',
                   width: '100%',
                   store: {
                       fields: [{ name: 'text' }, { name: 'value' }],
                       data: [{ text: 'YES', value: 'true' }, { text: 'NO', value: 'false' }]
                   }
               }
           },
          {
              xtype: 'gridcolumn',
              text: app.localize('BatchOwner'),
              dataIndex: 'createdUser',
              sortable: true,
              groupable: true,
              width: '15%',
              hidden: true,
              filterField: {
                  xtype: 'textfield',
                  width: '100%',
                  entityName: "",
                  emptyText: app.localize('BatchOwnerSearch')
              }
          }, {
              xtype: 'gridcolumn',
              text: app.localize('CommunityBatch'),
              dataIndex: 'isUniversal',
              sortable: true,
              groupable: true,
              width: '18%',
              hidden: true,
              renderer: Chaching.utilities.ChachingRenderers.rightWrongMarkRenderer,
              filterField: {
                  xtype: 'combobox',
                  valueField: 'value',
                  displayField: 'text',
                  width: '100%',
                  store: {
                      fields: [{ name: 'text' }, { name: 'value' }],
                      data: [{ text: 'YES', value: 'true' }, { text: 'NO', value: 'false' }]
                  }
              }
          }, {
              xtype: 'gridcolumn',
              text: app.localize('FinalizedtoPost'),
              dataIndex: 'isBatchFinalized',
              sortable: true,
              groupable: true,
              width: '18%',
              renderer: Chaching.utilities.ChachingRenderers.rightWrongMarkRenderer,
              filterField: {
                  xtype: 'combobox',
                  valueField: 'value',
                  displayField: 'text',
                  width: '100%',
                  store: {
                      fields: [{ name: 'text' }, { name: 'value' }],
                      data: [{ text: 'YES', value: 'true' }, { text: 'NO', value: 'false' }]
                  }
              }
          }
    ]
});