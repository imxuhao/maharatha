Ext.define('Chaching.view.payables.vendors.VendorsForm', {
    extend: 'Chaching.view.common.form.ChachingFormPanel',
    alias: ['widget.payables.vendors.create', 'widget.payables.vendors.edit'],
    requires: [
        'Chaching.view.payables.vendors.VendorsFormController'
    ],

    controller: 'payables-vendors-vendorsform',
    name: 'vendors',
    openInPopupWindow: false,
    hideDefaultButtons: false,
  
    autoScroll: true,
    border: false,
    showFormTitle: true,
    displayDefaultButtonsCenter: true,
    titleConfig: {
        title: abp.localization.localize("CreatingNewVendors").initCap()
    },
    items: {
        xtype: 'tabpanel',
        width: '100%',
        //region: 'center',      
        ui: 'dashboard',
        items: [
        {
            title: abp.localization.localize("General").initCap(),
            layout: 'column',
            items: [
        {
            columnWidth: .5,
            padding: '20 10 0 20',
            defaults: {
                labelAlign: 'top',
                blankText: app.localize('MandatoryToolTipText')
            },
            items: [
                {
                    xtype: 'textfield',
                    name: 'lastName',
                    itemId: 'lastName',
                    allowBlank: false,
                    fieldLabel: app.localize('Company/LastName').initCap() + Chaching.utilities.ChachingGlobals.mandatoryFlag,
                    width: '100%',
                    ui: 'fieldLabelTop',
                    emptyText: app.localize('MandatoryField')
                },
            {
                xtype: 'textfield',
                name: 'firstName',
                itemId: 'firstName',
                fieldLabel: app.localize('firstName').initCap(),
                width: '100%',
                ui: 'fieldLabelTop'
            },
            {
                xtype: 'textfield',
                name: 'firstName',
                itemId: 'firstName',
                fieldLabel: app.localize('PayToName').initCap(),
                width: '100%',
                ui: 'fieldLabelTop'
            },
            {
                xtype: 'combobox',
                name: 'typeofVendorId',
                fieldLabel: app.localize('Type').initCap(),
                width: '100%',
                ui: 'fieldLabelTop',
                displayField: 'typeofvendor',
                valueField: 'typeofvendorId',
                bind: {
                    store: '{vendorTypeList}'
                }
            },
            {
                xtype: 'textfield',
                name: 'vendorNumber',
                itemId: 'vendorNumber',
                fieldLabel: app.localize('VendorNumber').initCap(),
                width: '100%',
                ui: 'fieldLabelTop'
            }
            ]
        },
        {
            columnWidth: .5,
            padding: '20 10 0 20',
            defaults: {
                labelAlign: 'top',
                blankText: app.localize('MandatoryToolTipText')
            },
            items: [{
                xtype: 'textfield',
                name: 'ssnTaxId',
                itemId: 'ssnTaxId',
                fieldLabel: app.localize('SSN/TaxID').initCap(),
                width: '100%',
                ui: 'fieldLabelTop'
            },
            {
                xtype: 'textfield',
                name: 'fedralTaxId',
                itemId: 'fedralTaxId',
                fieldLabel: app.localize('FedTaxID').initCap(),
                width: '100%',
                ui: 'fieldLabelTop'
            },
            {
                xtype: 'checkbox',
                boxLabel: app.localize('Corporation'),
                name: 'isCorporation',
                labelAlign: 'right',
                inputValue: true,
                checked: false,
                boxLabelCls: 'checkboxLabel'
            }
            ,
            {
                xtype: 'textfield',
                name: 'dbaName',
                itemId: 'dbaName',
                fieldLabel: app.localize('LegalName').initCap(),
                width: '100%',
                ui: 'fieldLabelTop'
            }
             ,
             {
                 layout: 'column',
                 items: [{
                     columnWidth: .5,
                     padding: '20 10 0 20',
                     defaults: {
                         labelAlign: 'top',
                         blankText: app.localize('MandatoryToolTipText')
                     },
                     items: [{
                         xtype: 'checkbox',
                         boxLabel: app.localize('1099s'),
                         name: 'is1099',
                         labelAlign: 'right',
                         inputValue: true,
                         checked: false,
                         boxLabelCls: 'checkboxLabel'
                     }]
                 },{
                     columnWidth: .5,
                     padding: '20 10 0 20',
                     defaults: {
                         labelAlign: 'top',
                         blankText: app.localize('MandatoryToolTipText')
                     },
                     items: [{
                         xtype: 'combobox',
                         name: 'typeof1099BoxId',
                         fieldLabel: app.localize('1099s').initCap(),
                         width: '100%',
                         ui: 'fieldLabelTop',
                         displayField: 'typeof1099Box',
                         valueField: 'typeof1099BoxId',
                         bind: {
                             store: '{typeof1099BoxList}'
                         }
                     }]
                 }]}
           ,{
                xtype: 'checkbox',
                boxLabel: app.localize('W9OnFile'),
                name: 'isw9OnFile',
                labelAlign: 'right',
                inputValue: true,
                checked: false,
                boxLabelCls: 'checkboxLabel'
           }
            ,
            {
                xtype: 'textfield',
                name: 'creditLimit',
                itemId: 'creditLimit',
                fieldLabel: app.localize('CreditLimit').initCap(),
                width: '100%',
                ui: 'fieldLabelTop'
            }
            ]
        }]
            
            },

        {
            title: abp.localization.localize("Contact").initCap(),
            items: [{
                xtype: 'address',
                layout: 'fit',
                width: '100%'
            }]
        }
        ,
        {
            title: abp.localization.localize("Other").initCap(),
            items: [{
                xtype: 'textfield',
                name: 'lastName',
                itemId: 'lastName',
                allowBlank: false,
                fieldLabel: app.localize('Company/LastName').initCap() + Chaching.utilities.ChachingGlobals.mandatoryFlag,
                width: '100%',
                ui: 'fieldLabelTop',
                emptyText: app.localize('MandatoryField')
            }]
        }
        ]
    }
}
);