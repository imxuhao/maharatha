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
    headerButtonsConfig: [{
        xtype: 'displayfield',
        value: abp.localization.localize("Address"),
        ui: 'headerTitle'
    }, '->', {
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
        text: app.localize('Primary'),
        xtype: 'checkcolumn', dataIndex: 'isPrimary',
        listeners: {
            checkchange: function (column, recordIndex, checked) {
                var store = this.up('grid').getStore();
                Ext.each(store, function (record) {
                    for (var i = 0; i < record.count() ; i++) {
                        record.getAt(i).set('isPrimary', false);
                    }
                    record.getAt(recordIndex).set('isPrimary', true);
                });
            }
        }
    },
     {
         text: abp.localization.localize("Type"),
         xtype: 'gridcolumn',
         dataIndex: 'addressType',
         sortable: true,
         groupable: true,
         width: '15%',
         editor: {
             xtype: 'combobox',
             valueField: 'addressTypeId',
             displayField: 'addressType',
             bind: {
                 store: {
                     fields: [{ name: 'name' }, { name: 'value' }, {
                         name: 'addressType', convert: function (value, record) {
                             return record.get('name');
                         }
                     }, {
                         name: 'addressTypeId', convert: function (value, record) {
                             return record.get('value');
                         }
                     }],
                     xtype: 'ajax',
                     proxy: {
                         actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
                         type: 'chachingProxy',
                         url: abp.appPath + 'api/services/app/vendorUnit/GetTypeofAddressList',
                         reader: {
                             type: 'json',
                             rootProperty: 'result'
                         }
                     }
                 }
             }
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
                 store: {
                     fields: [{ name: 'name' }, { name: 'value' }, {
                         name: 'country', convert: function (value, record) {
                             return record.get('name');
                         }
                     }, {
                         name: 'countryId', convert: function (value, record) {
                             return record.get('value');
                         }
                     }],
                     xtype: 'ajax',
                     proxy: {
                         actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
                         type: 'chachingProxy',
                         url: abp.appPath + 'api/services/app/vendorUnit/GetCountryList',
                         reader: {
                             type: 'json',
                             rootProperty: 'result'
                         }
                     }
                 }
             }
         }
     }
        ,
         {
             text: app.localize('Address1'),
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
              text: app.localize('Address2'),
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
              text: app.localize('Address3'),
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
               text: app.localize('Contact'),
               dataIndex: 'contactNumber',
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
              text: abp.localization.localize("ZipCode"),
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
               text: app.localize('State/Region'),
               dataIndex: 'state',
               sortable: true,
               groupable: true,
               width: '12%',
               editor: {
                   xtype: 'combobox',
                   valueField: 'stateId',
                   displayField: 'state',
                   store: {
                       fields: [{ name: 'name' }, { name: 'value' }, {
                           name: 'state', convert: function (value, record) {
                               return record.get('name');
                           }
                       }, {
                           name: 'stateId', convert: function (value, record) {
                               return record.get('value');
                           }
                       }],
                       xtype: 'ajax',
                       proxy: {
                           actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
                           type: 'chachingProxy',
                           url: abp.appPath + 'api/services/app/vendorUnit/GetRegionList',
                           reader: {
                               type: 'json',
                               rootProperty: 'result'
                           }
                       }
                   }
               }


           },

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
           //,
           //{
           //    text: abp.localization.localize("Website"),
           //    dataIndex: 'website',
           //    xtype: 'gridcolumn',
           //    sortable: false,
           //    groupable: false,
           //    //flex:1,
           //    width: '10%',
           //    editor: {
           //        xtype: 'textfield',
           //        name: 'website'
           //    }
           //}

    ]
});
