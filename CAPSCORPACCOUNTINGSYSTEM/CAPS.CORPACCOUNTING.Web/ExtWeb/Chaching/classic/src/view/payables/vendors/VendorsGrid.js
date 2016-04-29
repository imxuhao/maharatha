
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
        dataIndex: 'firstName',
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
        text: app.localize('Address'),
        dataIndex: 'fullAddress',
        sortable: true,
        groupable: true,
        width: '23%',        
        filterField: {
            xtype: 'textfield',
            width: '15%',
            emptyText: app.localize('AddressSearch')
        }, renderer: Chaching.utilities.ChachingRenderers.renderFullAddress
    }, {
        xtype: 'gridcolumn',
        text: app.localize('Telephone'),
        dataIndex: 'contactNumber',
        sortable: true,
        groupable: true,
        width: '10%',
        renderer: Chaching.utilities.ChachingRenderers.renderContactNumber,
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
        width: '15%',
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
    ]
});
