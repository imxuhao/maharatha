/**
 * The class is created to provide main UI to access Credit card Company .
 * Author: kamal
 * Date: 30/08/2016
 */
/**
 * @class Chaching.view.creditcard.entry.CreditCardCompanyGrid
 * UI design for Credit Card->CreditCardCompanyGrid.
 * @alias widget.creditcard.entry.ccdcompanies
 */
Ext.define('Chaching.view.creditcard.entry.CreditCardCompanyGrid', {
    extend: 'Chaching.view.common.grid.ChachingGridPanel',

    requires: [
        'Chaching.view.creditcard.entry.CreditCardCompanyGridController'
    ],
    xtype: 'creditcard.entry.ccdcompanies',
    name: 'CreditCard.Entry.CreditCardCompanies',
    controller: 'creditcard-entry-creditcardcompanygrid',
    modulePermissions: {
        read: abp.auth.isGranted('Pages.CreditCard.Entry.CreditCardCompanies'),
        create: abp.auth.isGranted('Pages.CreditCard.Entry.CreditCardCompanies.Create'),
        edit: abp.auth.isGranted('Pages.CreditCard.Entry.CreditCardCompanies.Edit'),
        destroy: abp.auth.isGranted('Pages.CreditCard.Entry.CreditCardCompanies.Delete')
    },
    padding: 5,
    gridId: 34,
    store: 'creditcard.entry.CreditCardCompanyStore',
    headerButtonsConfig: [
      {
          xtype: 'displayfield',
          value: abp.localization.localize("CreditCardCompany"),
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
    actionColumnMenuItemsConfig: [{
        text: app.localize('Clone'),
        iconCls: 'fa fa-clone',
        itemId: 'cloneActionMenu',
        clickActionName: 'cloneActionClick'
    }],
    requireExport: true,
    requireMultiSearch: true,
    requireMultisort: true,
    isEditable: true,
    editingMode: 'row',
    columnLines: true,
    multiColumnSort: true,
    editWndTitleConfig: {
        title: app.localize('EditCreditCardCompany'),
        iconCls: 'fa fa-pencil'
    },
    createWndTitleConfig: {
        title: app.localize('CreateCreditCardCompany'),
        iconCls: 'fa fa-plus'
    },
    viewWndTitleConfig: {
        title: app.localize('ViewCreditCardCompany'),
        iconCls: 'fa fa-th'
    },
    createNewMode: 'tab',
    isSubMenuItemTab: true,
    columns: [
        {
            xtype: 'gridcolumn',
            text: app.localize('CreditCardCompany'),
            dataIndex: 'description',
            sortable: true,
            groupable: false,
            width: '35%',
            filterField: {
                xtype: 'textfield',
                width: '100%'
            }, editor: {
                xtype: 'textfield'
            }
        },
         {
             xtype: 'gridcolumn',
             text: app.localize('AccountName'),
             dataIndex: 'bankAccountName',
             sortable: true,
             groupable: false,
             width: '20%',
             filterField: {
                 xtype: 'textfield',
                 width: '100%'
             }, editor: {
                 xtype: 'textfield'
             }
         },
          {
              xtype: 'gridcolumn',
              text: app.localize('AccountNumber'),
              dataIndex: 'bankAccountNumber',
              sortable: true,
              groupable: false,
              width: '20%',
              filterField: {
                  xtype: 'textfield',
                  width: '100%'
              }, editor: {
                  xtype: 'textfield'
              }
          },
          {
              xtype: 'gridcolumn',
              text: app.localize('Batch'),
              dataIndex: 'batchDesc',
              sortable: true,
              groupable: false,
              width: '20%',
              sorter: {
                  property: 'batchDesc',
                  sortOnEntity: ''
              },
              filterField: {
                  entityName: '',
                  xtype: 'textfield',
                  width: '100%'
              }, editor: {
                  //xtype: 'textfield'

                    xtype: 'combobox',
                    width: '100%',
                    displayField: 'description',
                    valueField: 'batchId',
                    queryMode: 'local',
                    store: 'utilities.BatchListStore'//Ext.create('Chaching.store.utilities.BatchListStore')
              }
          }
    ]
});
