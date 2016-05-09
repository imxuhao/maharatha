
Ext.define('Chaching.view.payables.vendors.VendorsGrid', {
    extend: 'Chaching.view.common.grid.ChachingGridPanel',

    requires: [
        'Chaching.view.payables.vendors.VendorsGridController'
    ],

    controller: 'payables-vendors-vendorsgrid',

    xtype: 'widget.payables.vendors',
    store: 'payables.vendors.VendorsStore',
    name: 'Payables.Vendors',
    modulePermissions: {
        read: abp.auth.isGranted('Pages.Payables.Vendors'),
        create: abp.auth.isGranted('Pages.Payables.Vendors.Create'),
        edit: abp.auth.isGranted('Pages.Payables.Vendors.Edit'),
        destroy: abp.auth.isGranted('Pages.Payables.Vendors.Delete'),
    },
    padding: 5,
    gridId: 15,
    headerButtonsConfig: [
      {
          xtype: 'displayfield',
          value: abp.localization.localize("Vendors"),
          ui: 'headerTitle'
      }, '->', {
          xtype: 'button',
          scale: 'small',
          ui: 'actionButton',
          action: 'create',
          text: abp.localization.localize("CreatingNewVendors").toUpperCase(),
          tooltip: app.localize('CreatingNewVendors'),
          checkPermission: true,
          iconCls: 'fa fa-plus',
          routeName: 'coa.create',
          iconAlign: 'left'
      }],
    requireExport: true,
    requireMultiSearch: true,
    requireMultisort: true,
    isEditable: false,
    editingMode: 'row',
    columnLines: true,
    multiColumnSort: true,
    editWndTitleConfig: {
        title: app.localize('EditVendors'),
        iconCls: 'fa fa-pencil'
    },
    createWndTitleConfig: {
        title: app.localize('CreatingNewVendors'),
        iconCls: 'fa fa-plus'
    },
    createNewMode: 'tab',
    isSubMenuItemTab: true,

    columns: [{
        xtype: 'gridcolumn',
        text: app.localize('VendorName'),
        dataIndex: 'lastName',
        sortable: true,
        groupable: true,
        width: '15%',
        filterField: {
            xtype: 'textfield',
            width: '15%',
            emptyText: app.localize('VendorSearch')
        }
    }, {

        xtype: 'gridcolumn',
        text: app.localize('Address1'),
        dataIndex: 'line1',
        sortable: true,
        groupable: true,
        isAssociationField: true,

        width: '15%',
        filterField: {
            xtype: 'textfield',
            width: '15%',
            emptyText: app.localize('AddressSearch')
        }, renderer: Chaching.utilities.ChachingRenderers.renderFirstAddress
    }
    , {
        xtype: 'gridcolumn',
        text: app.localize('Telephone'),
        dataIndex: 'phone1',
        sortable: true,
        groupable: true,
        isAssociationField: true,
        width: '10%',
        renderer: Chaching.utilities.ChachingRenderers.renderPhone1,
        filterField: {
            xtype: 'textfield',
            width: '15%',
            entityName: "Address",
            emptyText: app.localize('TelephoneSearch')
        }
    }
    , {
        xtype: 'gridcolumn',
        text: app.localize('Email'),
        dataIndex: 'email',
        sortable: true,
        groupable: true,
        width: '15%',
        isAssociationField: true,
        renderer: Chaching.utilities.ChachingRenderers.renderEmail,
        filterField: {
            xtype: 'textfield',
            width: '15%',
            emptyText: app.localize('EmailSearch')
        }
    }
    , {
        xtype: 'gridcolumn',
        text: app.localize('1099Code'),
        dataIndex: 'typeof1099Box',
        sortable: true,
        groupable: true,
        width: '10%',
        filterField: {
            xtype: 'textfield',
            width: '15%',
            emptyText: app.localize('1099CodeSearch')
        }
    }
    , {
        xtype: 'gridcolumn',
        text: app.localize('PaymentTerms'),
        dataIndex: 'paymentTerms',
        sortable: true,
        groupable: true,
        width: '15%',
        filterField: {
            xtype: 'textfield',
            width: '15%',
            entityName: "",
            emptyText: app.localize('PaymentTermsSearch')
        }
    }
    , {
        xtype: 'gridcolumn',
        text: app.localize('TotalOutstandingInvoices'),
        width: '13%'
    }
    ,
    {
        xtype: 'gridcolumn',
        text: app.localize('DateCreated'),
        dataIndex: 'creationTime',
        sortable: true,
        groupable: true,
        width: '10%',
        renderer: Chaching.utilities.ChachingRenderers.dateSearchFieldRenderer
    }
     , {
         xtype: 'gridcolumn',
         text: app.localize('City'),
         dataIndex: 'city',
         width: '13%',
         renderer: Chaching.utilities.ChachingRenderers.renderCity,
         hidden: true
     }
       , {
           xtype: 'gridcolumn',
           text: app.localize('State'),
           dataIndex: 'state',
           width: '13%',
           renderer: Chaching.utilities.ChachingRenderers.renderState,
           hidden: true,
           filterField: {
               xtype: 'textfield',
               width: '15%',
               entityName: "",
               emptyText: app.localize('StateSearch')
           }
       }
         , {
             xtype: 'gridcolumn',
             text: app.localize('Zip'),
             dataIndex: 'postalCode',
             renderer: Chaching.utilities.ChachingRenderers.renderPostalCode,
             width: '13%',
             hidden: true,
             filterField: {
                 xtype: 'textfield',
                 width: '15%',
                 entityName: "",
                 emptyText: app.localize('PostalCodeSearch')
             }
         }
     , {
         xtype: 'gridcolumn',
         text: app.localize('PreviousYearPayments'),
         width: '13%',
         hidden: true
     }
      , {
          xtype: 'gridcolumn',
          text: app.localize('CurrentYearPayments'),
          width: '13%',
          hidden: true
      }
      , {
          xtype: 'gridcolumn',
          text: app.localize('OpenPurchaseOrders'),
          width: '13%',
          hidden: true
      }
       , {
           xtype: 'gridcolumn',
           text: app.localize('SSN'),
           dataIndex: 'ssnTaxId',
           width: '13%',
           hidden: true,
           filterField: {
               xtype: 'textfield',
               width: '15%',
               entityName: "",
               emptyText: app.localize('SSNSearch')
           }
       }
      , {
          xtype: 'gridcolumn',
          text: app.localize('FederalTaxID'),
          dataIndex: 'fedralTaxId',
          width: '13%',
          hidden: true,
          filterField: {
              xtype: 'textfield',
              width: '15%',
              entityName: "",
              emptyText: app.localize('FedralTaxSearch')
          }
      }

       //, {
       //    xtype: 'gridcolumn',
       //    text: app.localize('Corporation'),
       //    dataIndex: 'isCorporation',
       //    sortable: true,
       //    groupable: true,
       //    width: '10%',
       //    hidden: true,

       //    renderer: Chaching.utilities.ChachingRenderers.rightWrongMarkRenderer,
       //    filterField: {
       //        xtype: 'combobox',
       //        valueField: 'value',
       //        displayField: 'text',
       //        store: {
       //            fields: [{ name: 'text' }, { name: 'value' }],
       //            data: [{ text: 'YES', value: 'true' }, { text: 'NO', value: 'false' }]
       //        }
       //    }
       //}

       , {
           xtype: 'gridcolumn',
           text: app.localize('Corporation'),
           width: '13%',
           hidden: true
       }
            , {
                xtype: 'gridcolumn',
                text: app.localize('IndependentContractor'),
                dataIndex: 'isIndependentContractor',
                sortable: true,
                groupable: true,
                width: '10%',
                hidden: true,

                renderer: Chaching.utilities.ChachingRenderers.rightWrongMarkRenderer,
                filterField: {
                    xtype: 'combobox',
                    valueField: 'value',
                    displayField: 'text',
                    store: {
                        fields: [{ name: 'text' }, { name: 'value' }],
                        data: [{ text: 'YES', value: 'true' }, { text: 'NO', value: 'false' }]
                    }
                }
            }
    ]
});
