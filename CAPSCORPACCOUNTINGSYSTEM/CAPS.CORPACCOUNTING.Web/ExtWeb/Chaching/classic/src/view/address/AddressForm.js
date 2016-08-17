/**
 * This class is created as form for addresses add/edit purpose. {@link Chaching.component.form.Panel}
 * Author: Krishna Garad
 * Date Created: 08/16/2016
 */
Ext.define('Chaching.view.address.AddressForm',
{
    extend: 'Chaching.view.common.form.ChachingFormPanel',
    alias: ['widget.address.create', 'widget.address.edit'],
    requires: [
        'Chaching.view.address.AddressFormController'
    ],
    openInPopupWindow: true,
    hideDefaultButtons: false,
    border: false,
    displayDefaultButtonsCenter: true,
    scrollable: true,
    controller: 'address-addressform',
    modulePermissions: {
        read: true,
        create: true,
        edit: true,
        destroy: true
    },
    layout: {
        type: 'fit'
    },
    items: [
        {
            xtype: 'container',
            layout: {
                type: 'column'
            },
            items: [
                {
                    columnWidth: .5,
                    padding: '10 10 0 10',
                    defaults: {
                        labelWidth: 80,
                        width: '100%',
                        ui: 'fieldLabelTop',
                        blankText: app.localize('MandatoryToolTipText')
                    },
                    items: [
                        {
                            xtype: 'hiddenfield',
                            name: 'addressId'
                        }, {
                            xtype: 'combobox',
                            valueField: 'addressTypeId',
                            displayField: 'addressType',
                            queryMode: 'local',
                            name: 'addressTypeId',
                            store: 'utilities.TypeOfAddressListStore',
                            allowBlank: false,
                            fieldLabel: app.localize('Type').initCap()
                        }, {
                            xtype: 'textfield',
                            name: 'line1',
                            allowBlank: false,
                            fieldLabel: app.localize('Address1').initCap(),
                            emptyText: app.localize('MandatoryField')
                        }, {
                            xtype: 'textfield',
                            name: 'line2',
                            fieldLabel: app.localize('Address2').initCap()
                        }, {
                            xtype: 'textfield',
                            name: 'line3',
                            fieldLabel: app.localize('Address3').initCap()
                        }, {
                            xtype: 'combobox',
                            name: 'country',
                            reference: 'countryCombo',
                            fieldLabel: app.localize('Country').initCap(),
                            displayField: 'description',
                            valueField: 'country',
                            emptyText: app.localize('SelectOption'),
                            queryMode: 'local',
                            store: Ext.create('Chaching.store.utilities.CountryListStore')
                        }, {
                            xtype: 'textfield',
                            name: 'postalCode',
                            fieldLabel: app.localize('PostalCode').initCap(),
                            width: '100%',
                            ui: 'fieldLabelTop'
                            //listeners: {
                            //    change: 'onPostalCodeChange'
                            //}
                        }
                    ]
                }, {
                    columnWidth: .5,
                    padding: '10 10 0 10',
                    defaults: {
                        labelWidth: 80,
                        width: '100%',
                        ui: 'fieldLabelTop',
                        blankText: app.localize('MandatoryToolTipText')
                    },
                    items: [
                        {
                            xtype: 'textfield',
                            name: 'city',
                            fieldLabel: app.localize('City').initCap(),
                            width: '100%',
                            ui: 'fieldLabelTop'
                        }, {
                            xtype: 'combobox',
                            name: 'state',
                            reference: 'stateCombo',
                            fieldLabel: app.localize('CompanyState').initCap(),
                            width: '100%',
                            ui: 'fieldLabelTop',
                            displayField: 'description',
                            valueField: 'state',
                            emptyText: app.localize('SelectOption'),
                            queryMode: 'local',
                            store: Ext.create('Chaching.store.utilities.StateOrRegionListStore')
                        }, {
                            xtype: 'textfield',
                            name: 'contactNumber',
                            fieldLabel: app.localize('Contact').initCap()
                        }, {
                            xtype: 'textfield',
                            name: 'phone1',
                            itemId: 'phone1',
                            fieldLabel: app.localize('Telephone').initCap(),
                            width: '100%',
                            ui: 'fieldLabelTop'
                        }, {
                            xtype: 'textfield',
                            name: 'email',
                            fieldLabel: app.localize('Email').initCap(),
                            vtype: 'email',
                            width: '100%',
                            ui: 'fieldLabelTop'
                        }, {
                            xtype: 'checkbox',
                            ui: 'default',
                            labelAlign: 'right',
                            inputValue: true,
                            uncheckedValue: false,
                            boxLabelCls: 'checkboxLabel',
                            name: 'isPrimary',
                            boxLabel: app.localize('Primary')
                        }
                    ]
                }
            ]
        }
    ]
});
