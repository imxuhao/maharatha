Ext.define('Chaching.view.batchposting.batches.BatchesGrid', {
    extend: 'Chaching.view.common.grid.ChachingGridPanel',
    requires: [
        'Chaching.view.batchposting.batches.BatchesGridController'
    ],
    controller: 'batchposting-batches-batchesgrid',
    xtype: 'widget.batchposting.batches',
    store: 'batchposting.batches.BatchesStore',
    name: 'BatchPosting.Batches',
    modulePermissions: {
        read: abp.auth.isGranted('Pages.BatchPosting.Batches'),
        create: abp.auth.isGranted('Pages.BatchPosting.Batches.Create'),
        edit: abp.auth.isGranted('Pages.BatchPosting.Batches.Edit'),
        destroy: abp.auth.isGranted('Pages.BatchPosting.Batches.Delete'),
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
        {
        xtype: 'displayfield',
        value: abp.localization.localize("BatchPosting"),
        ui: 'headerTitle'
        },
        '->', {
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
    viewWndTitleConfig: {
        title: app.localize('ViewBatch'),
        iconCls: 'fa fa-th'
    },
    createNewMode: 'tab',
    isSubMenuItemTab: true,
    columns: [
         {
             text: app.localize('Posting'),
             dataIndex: 'post',
             sortable: false,
             groupable: false,
             hideable: false,
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
                  width: '100%'
              },
              editor: {
                  xtype: 'datefield',
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
          } , {
              xtype: 'gridcolumn',
              text: app.localize('Module'),
              dataIndex: 'typeOfBatch',
              sortable: true,
              groupable: false,
              hidden:false,
              width: '18%',
              filterField: {
                  xtype: 'tagfield',
                  valueField: 'typeOfBatchId',
                  displayField: 'typeOfBatch',
                  queryMode : 'local',
                  width: '100%',
                  isEnum: true,
                  forceSelection: true,
                  searchProperty : 'typeOfBatchId',
                  store: {
                      fields: [{ name: 'typeOfBatchId' }, { name: 'typeOfBatch' }],
                      data: [
                          //{ typeOfBatch: app.localize('ShowAllModules'), typeOfBatchId: '0' }
                          { typeOfBatch: app.localize('Journal'), typeOfBatchId: '1,2,10,11' }
                          , { typeOfBatch: app.localize('AccountsPayable'), typeOfBatchId: '3' }
                          , { typeOfBatch: app.localize('Receivables'), typeOfBatchId: '4' }
                          , { typeOfBatch: app.localize('PettyCash'), typeOfBatchId: '6,7,9' }
                          , { typeOfBatch: app.localize('Payroll'), typeOfBatchId: '5' }
                          , { typeOfBatch: app.localize('CreditCard'), typeOfBatchId: '16' }
                      ]
                  }
              }
          } , {
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