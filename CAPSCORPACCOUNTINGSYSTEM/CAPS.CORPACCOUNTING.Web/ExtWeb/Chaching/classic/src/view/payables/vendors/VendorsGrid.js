
Ext.define('Chaching.view.payables.vendors.VendorsGrid', {
    extend: 'Chaching.view.common.grid.ChachingGridPanel',

    requires: [
        'Chaching.view.payables.vendors.VendorsGridController'
    ],

    controller: 'payables-vendors-vendorsgrid',

    xtype: 'payables.vendors',
    store: 'payables.vendors.VendorsStore',
    name: 'Payables.Vendors',
    modulePermissions: {
        read: abp.auth.isGranted('Pages.Payables.Vendors'),
        create: abp.auth.isGranted('Pages.Payables.Vendors.Create'),
        edit: abp.auth.isGranted('Pages.Payables.Vendors.Edit'),
        destroy: abp.auth.isGranted('Pages.Payables.Vendors.Delete')
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
          text: abp.localization.localize("Add").toUpperCase(),
          tooltip: app.localize('CreatingNewVendors'),
          checkPermission: true,
          iconCls: 'fa fa-plus',
          routeName: 'coa.create',
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
        title: app.localize('EditVendors'),
        iconCls: 'fa fa-pencil'
    },
    createWndTitleConfig: {
        title: app.localize('CreatingNewVendors'),
        iconCls: 'fa fa-plus'
    },
    viewWndTitleConfig: {
        title: app.localize('ViewVendor'),
        iconCls: 'fa fa-th'
    },
    createNewMode: 'tab',
    isSubMenuItemTab: true,

    columns: [{
        xtype: 'gridcolumn',
        text: app.localize('VendorName'),
        dataIndex: 'lastName',
        sortable: true,
        groupable: true,
        width: '12%',
        filterField: {
            xtype: 'textfield',
            width: '100%',
            emptyText: app.localize('VendorSearch')
        }, editor: {
            xtype: 'textfield'
        }
    }, {

        xtype: 'gridcolumn',
        text: app.localize('Address1'),
        dataIndex: 'line1',
        sortable: true,
        groupable: true,
        isAssociationField: true,

        width: '12%',
        filterField: {
            xtype: 'textfield',
            width: '100%',
            entityName: 'Address',
            emptyText: app.localize('AddressSearch')
        },
        renderer: Chaching.utilities.ChachingRenderers.renderFirstAddress,
        editor: {
            xtype: 'textfield'
        }
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
            width: '100%',
            entityName: "Address",
            emptyText: app.localize('TelephoneSearch')
        }, editor: {
            xtype: 'textfield'
        }
    }
    , {
        xtype: 'gridcolumn',
        text: app.localize('Email'),
        dataIndex: 'email',
        sortable: true,
        groupable: true,
        width: '12%',
        isAssociationField: true,
        renderer: Chaching.utilities.ChachingRenderers.renderEmail,
        filterField: {
            xtype: 'textfield',
            width: '100%',
            emptyText: app.localize('EmailSearch')
        }, editor: {
            xtype: 'textfield'
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
            xtype: 'combobox',
            valueField: 'typeof1099BoxId',
            displayField: 'typeof1099Box',
            forceSelection: true,
            loadStoreOnCreate: true,
            searchProperty: 'typeof1099BoxId',
            isEnum: true,
            width: '100%',
            queryMode : 'local',
            store: 'utilities.TypeOf1099BoxListStore'
        }, editor: {
            xtype: 'combobox',
            valueField: 'typeof1099BoxId',
            displayField: 'typeof1099Box',
            queryMode: 'local',
            store: 'utilities.TypeOf1099BoxListStore'
        }
    }
    , {
        xtype: 'gridcolumn',
        text: app.localize('PaymentTerms'),
        dataIndex: 'paymentTerms',
        sortable: true,
        groupable: true,
        width: '12%',
        filterField: {
            xtype: 'combobox',
            valueField: 'paymentTermsId',
            displayField: 'paymentTerms',
            forceSelection: true,
            width: '100%',
            searchProperty: 'paymentTermsId',
            store: 'utilities.PaymentTermsListStore'
        },
        editor: {
            xtype: 'combobox',
            valueField: 'paymentTermsId',
            displayField: 'paymentTerms',
            queryMode: 'local',
            store: 'utilities.PaymentTermsListStore'
        }

    }
    , {
        xtype: 'gridcolumn',
        text: app.localize('TotalOutstandingInvoices'),
        dataIndex: 'TotalOutstandingInvoices',
        width: '10%'
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
         width: '10%',
         renderer: Chaching.utilities.ChachingRenderers.renderCity,
         hidden: true,
         isAssociationField: true,
         filterField: {
             xtype: 'textfield',
             width: '100%',
             entityName: "Address",
             emptyText: app.localize('CitySearch')
         }, editor: {
             xtype: 'textfield'
         }
     }
       , {
           xtype: 'gridcolumn',
           text: app.localize('State'),
           dataIndex: 'state',
           width: '10%',
           renderer: Chaching.utilities.ChachingRenderers.renderState,
           hidden: true,
           isAssociationField: true,
           filterField: {
               xtype: 'textfield',
               width: '100%',
               entityName: "Address",
               emptyText: app.localize('StateSearch')
           }, editor: {
               xtype: 'combobox',
               width: '100%',
               valueField: 'stateId',
               displayField: 'state',
               store: 'utilities.StateOrRegionListStore'
           }
       }
         , {
             xtype: 'gridcolumn',
             text: app.localize('Zip'),
             dataIndex: 'postalCode',
             renderer: Chaching.utilities.ChachingRenderers.renderPostalCode,
             width: '8%',
             hidden: true,
             isAssociationField: true,
             filterField: {
                 xtype: 'textfield',
                 width: '100%',
                 entityName: "Address",
                 emptyText: app.localize('ZipCodeSearch')
             }, editor: {
                 width: '100%',
                 xtype: 'textfield'
             }
         }
     , {
         xtype: 'gridcolumn',
         text: app.localize('PreviousYearPayments'),
         dataIndex: 'PreviousYearPayments',
         width: '10%',
         hidden: true
     }
      , {
          xtype: 'gridcolumn',
          text: app.localize('CurrentYearPayments'),
          dataIndex: 'CurrentYearPayments',
          width: '10%',
          hidden: true
      }
      , {
          xtype: 'gridcolumn',
          text: app.localize('OpenPurchaseOrders'),
          dataIndex: 'OpenPurchaseOrders',
          width: '8%',
          hidden: true
      }
       , {
           xtype: 'gridcolumn',
           text: app.localize('SSN'),
           dataIndex: 'ssnTaxId',
           width: '10%',
           hidden: true,
           filterField: {
               xtype: 'textfield',
               width: '100%',
               emptyText: app.localize('SSNSearch')
           }, editor: {
               width: '100%',
               xtype: 'textfield'
           }
       }
      , {
          xtype: 'gridcolumn',
          text: app.localize('FederalTaxID'),
          dataIndex: 'fedralTaxId',
          width: '12%',
          hidden: true,
          filterField: {
              xtype: 'textfield',
              width: '100%',
              emptyText: app.localize('FedralTaxSearch')
          }, editor: {
              width: '100%',
              xtype: 'textfield'
          }
      }
       , {
           xtype: 'gridcolumn',
           text: app.localize('Corporation'),
           dataIndex: 'typeofTax',
           width: '10%',
           hidden: true,
           filterField: {
               xtype: 'combobox',
               valueField: 'typeofTaxId',
               displayField: 'typeofTax',
               loadStoreOnCreate: true,
               forceSelection: true,
               searchProperty: 'typeofTaxId',
               isEnum: true,
               queryMode: 'local',
               width: '100%',
               store: 'utilities.TypeOfTaxListStore'
           }, editor: {
               xtype: 'combobox',
               valueField: 'typeofTaxId',
               displayField: 'typeofTax',
               queryMode: 'local',
               store: 'utilities.TypeOfTaxListStore'
           }
       }
         , {
             xtype: 'gridcolumn',
             text: app.localize('IndependentContractor'),
             dataIndex: 'isIndependentContractor',
             sortable: true,
             groupable: true,
             width: '12%',
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
         }
    ]
});
