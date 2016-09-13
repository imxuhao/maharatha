Ext.define('Chaching.view.financials.journals.JournalEntryGrid', {
    extend: 'Chaching.view.common.grid.ChachingGridPanel',
    xtype: 'financials.journals.entry',
    requires: [
        'Chaching.view.financials.journals.JournalEntryGridController'
    ],
    modulePermissions: {
        read: abp.auth.isGranted('Pages.Financials.Journals.Entry'),
        create: abp.auth.isGranted('Pages.Financials.Journals.Entry.Create'),
        edit: abp.auth.isGranted('Pages.Financials.Journals.Entry.Edit'),
        destroy: abp.auth.isGranted('Pages.Financials.Journals.Entry.Delete'),
        attach: abp.auth.isGranted('Pages.Financials.Journals.Entry.Attach')
    },
    attachmentConfig: {
        objectType: 'AccountingHeaderTransactionsUnit',
        objectIdField: 'accountingDocumentId'
    },
    controller: 'financials-journals-journalentrygrid',
    gridId: 18,
    headerButtonsConfig: [
    {
        xtype: 'displayfield',
        value: abp.localization.localize("JournalEntry"),
        ui: 'headerTitle'
    }, '->', {
        xtype: 'button',
        scale: 'small',
        ui: 'actionButton',
        action: 'create',
        text: abp.localization.localize("Add").toUpperCase(),
        checkPermission: true,
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
        title: app.localize('EditJournalEntry'),
        iconCls: 'fa fa-pencil'
    },
    createWndTitleConfig: {
        title: app.localize('CreateNewJournalEntry'),
        iconCls: 'fa fa-plus'
    },
    viewWndTitleConfig: {
        title: app.localize('ViewJournalEntry'),
        iconCls: 'fa fa-th'
    },
    createNewMode: 'tab',
    isSubMenuItemTab: true,
    store: 'financials.journals.JournalStore',
    columns: [
         {
             text: app.localize('Post'),
             xtype: 'checkcolumn',
             dataIndex: 'isPosted',
             width: '6%',
             checked: true,
             listeners: {
                 checkchange: function (column, recordIndex, checked) {
                     var store = this.up('grid').getStore();
                     //Ext.each(store, function (record) {
                     //    if (record.set('batchName').trim().length() != 0)
                     //        record.set('isPrimary', false);
                     //});
                 }
             }
         },
          {
              xtype: 'gridcolumn',
              text: app.localize('CreatedBy'),
              dataIndex: 'createdUser',
              sortable: true,
              groupable: true,
              width: '10%',
              filterField: {
                  xtype: 'textfield',
                  width: '100%',
                  entityName: "",
                  emptyText: app.localize('UserSearch')
              }
          }, {
              xtype: 'gridcolumn',
              text: app.localize('JournalType'),
              dataIndex: 'journalType',
              sortable: true,
              groupable: true,
              width: '10%',
              filterField: {
                  xtype: 'combobox',
                  valueField: 'journalTypeId',
                  displayField: 'journalType',
                  width: '100%',
                  queryMode: 'local',
                  loadStoreOnCreate: true,
                  forceSelection: true,
                  isEnum: true,
                  searchProperty: 'journalTypeId',
                  store: 'utilities.JournalTypeListStore'
              }, editor: {
                  xtype: 'combobox',
                  valueField: 'journalTypeId',
                  displayField: 'journalType',
                  queryMode: 'local',
                  forceSelection: true,
                  store: 'utilities.JournalTypeListStore'
              }
          },
          {
              xtype: 'gridcolumn',
              text: app.localize('Journal#'),
              dataIndex: 'documentReference',
              sortable: true,
              groupable: true,
              width: '10%',
              filterField: {
                  xtype: 'textfield',
                  width: '100%',
                  emptyText: app.localize('Journal#Search')
              }
          },
          {
              xtype: 'gridcolumn',
              text: app.localize('TransactionDate'),
              dataIndex: 'transactionDate',
              sortable: true,
              groupable: true,
              width: '12%',
              renderer: Chaching.utilities.ChachingRenderers.dateSearchFieldRenderer,
              filterField: {
                  xtype: 'dateSearchField',
                  width: '100%',
                  dataIndex: 'transactionDate'
              },editor: {
                  xtype: 'datefield',
                  format: Chaching.utilities.ChachingGlobals.defaultExtDateFieldFormat
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
                  width: '100%',
                  emptyText: app.localize('DescriptionSearch')
              },editor: {
                  xtype:'textfield'
              }
          },
          {
              xtype: 'gridcolumn',
              text: app.localize('ControlTotal'),
              dataIndex: 'controlTotal',
              sortable: true,
              groupable: true,
              width: '10%',
              filterField: {
                  xtype: 'textfield',
                  width: '100%',
                  emptyText: app.localize('ControlTotalSearch')
              }
          }, {
              xtype: 'gridcolumn',
              text: app.localize('BatchName'),
              dataIndex: 'batchName',
              sortable: true,
              groupable: true,
              width: '12%'
          },
          {
              xtype: 'gridcolumn',
              text: app.localize('Transaction#'),
              dataIndex: 'accountingDocumentId',
              sortable: true,
              groupable: true,
              width: '10%',
              filterField: {
                  xtype: 'textfield',
                  width: '100%',
                  emptyText: app.localize('Transaction#Search')
              }
          }

    ]
});
