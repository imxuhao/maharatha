Ext.define('Chaching.view.address.AddressGrid', {
    extend: 'Chaching.view.common.grid.ChachingGridPanel',

    requires: [
        'Chaching.view.address.AddressGridController'
    ],
    controller: 'address-addressgrid',
    xtype: 'address',
    store: 'address.AddressStore',
    name: 'address',
    modulePermissions: {
        read: true,
        create: true,
        edit: true,
        destroy: true
    },
    padding: 5,
    gridId: 17,
    headerButtonsConfig: ['->', {
        xtype: 'button',
        scale: 'small',
        ui: 'actionButton',
        action: 'create',
        text: abp.localization.localize("CreatingNewAddress").toUpperCase(),
        checkPermission: false,
        iconCls: 'fa fa-plus',
        iconAlign: 'left'
    }],
    requireExport: false,
    requireMultiSearch: false,
    requireMultisort: false,
    isEditable: true,
    editingMode: 'cell',
    createNewMode: 'inline',
    columnLines: true,
    multiColumnSort: false,
    manageViewSetting: false,
    showPagingToolbar: false,
    hideClearFilter: true,
    columns: [

    {
        text: app.localize('Apply'),
        xtype: 'checkcolumn', dataIndex: 'isPrimary'
    }
        ,
         {
             text: app.localize('FirstAddress'),
             dataIndex: 'line1',
             xtype: 'gridcolumn',
             sortable: false,
             groupable: false,
             //flex:1,
             width: '10%',
             editor: {
                 xtype: 'textfield',
                 name: 'line1'
             }
         },
          {
              text: app.localize('SecondAddress'),
              dataIndex: 'line2',
              xtype: 'gridcolumn',
              sortable: false,
              groupable: false,
              //flex:1,
              width: '13%',
              editor: {
                  xtype: 'textfield',
                  name: 'line2'
              }
          }
          ,
          {
              text: app.localize('ThirdAddress'),
              dataIndex: 'line3',
              xtype: 'gridcolumn',
              sortable: false,
              groupable: false,
              //flex:1,
              width: '13%',
              editor: {
                  xtype: 'textfield',
                  name: 'line3'
              }
          },
          {
              text: app.localize('FourthAddress'),
              dataIndex: 'line4',
              xtype: 'gridcolumn',
              sortable: false,
              groupable: false,
              //flex:1,
              width: '13%',
              editor: {
                  xtype: 'textfield',
                  name: 'line4'
              }
          }
          ,
          {
              text: app.localize('City'),
              dataIndex: 'city',
              xtype: 'gridcolumn',
              sortable: false,
              groupable: false,
              //flex:1,
              width: '10%',
              editor: {
                  xtype: 'textfield',
                  name: 'city'
              }
          }
          , {
              xtype: 'gridcolumn',
              text: app.localize('Country'),
              dataIndex: 'country',
              sortable: true,
              groupable: true,
              width: '8%',
              editor: {
                  xtype: 'combobox',
                  valueField: 'countryId',
                  displayField: 'country',
                  bind: {
                      store: '{GetCountryList}'
                  }
              }
          }
           , {
               xtype: 'gridcolumn',
               text: app.localize('State/Region'),
               dataIndex: 'state',
               sortable: true,
               groupable: true,
               width: '12%',
               editor: {
                   xtype: 'combobox',
                   valueField: 'stateId',
                   displayField: 'state',
                   bind: {
                       store: '{getStateOrRegionList}'
                   }
               }
           },
           {
               text: abp.localization.localize("PostalCode"),
               dataIndex: 'postalCode',
               xtype: 'gridcolumn',
               sortable: false,
               groupable: false,
               //flex:1,
               width: '10%',
               editor: {
                   xtype: 'textfield',
                   name: 'postalCode'
               }
           }
           ,
           {
               text: abp.localization.localize("Telephone"),
               dataIndex: 'phone1',
               xtype: 'gridcolumn',
               sortable: false,
               groupable: false,
               //flex:1,
               width: '10%',
               editor: {
                   xtype: 'textfield',
                   name: 'phone1'
               }
           }
           ,
           {
               text: abp.localization.localize("Email"),
               dataIndex: 'email',
               xtype: 'gridcolumn',
               sortable: false,
               groupable: false,
               //flex:1,
               width: '10%',
               editor: {
                   xtype: 'textfield',
                   name: 'email'
               }
           }
           ,
           {
               text: abp.localization.localize("Website"),
               dataIndex: 'website',
               xtype: 'gridcolumn',
               sortable: false,
               groupable: false,
               //flex:1,
               width: '10%',
               editor: {
                   xtype: 'textfield',
                   name: 'website'
               }
           }
           , {
               text: abp.localization.localize("TypeOfAddress"),
               xtype: 'gridcolumn',
               dataIndex: 'typeofAddressId',
               sortable: true,
               groupable: true,
               width: '8%',
               editor: {
                   xtype: 'combobox',
                   valueField: 'typeofAddressId',
                   displayField: 'typeofAddress',
                   bind: {
                       store: '{typeofAddressList}'
                   }
               }
           }
    ]
});
