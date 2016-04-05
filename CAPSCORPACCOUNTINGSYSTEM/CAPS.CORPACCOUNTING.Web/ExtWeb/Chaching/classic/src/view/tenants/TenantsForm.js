
Ext.define('Chaching.view.tenants.TenantsForm',{
    extend: 'Chaching.view.common.form.ChachingFormPanel',
    alias: ['host.tenants.create', 'host.tenants.edit'],
    requires: [
        'Chaching.view.tenants.TenantsFormController',
        'Chaching.view.tenants.TenantsFormModel'
    ],

    controller: 'tenants-tenantsform',
    viewModel: {
        type: 'tenants-tenantsform'
    },
    name: 'Tenants',
    openInPopupWindow: true,
    hideDefaultButtons: false,
    layout: 'vbox',
    defaults: {
        bodyStyle: { 'background-color': 'trasparent' },
        labelAlign: 'top',
        blankText: app.localize('MandatoryToolTipText')
    },
    defaultFocus:'textfield#tenancyName',
    items: [{
        xtype: 'hiddenfield',
        name: 'id',
        value:0
    }, {
        xtype: 'textfield',
        name: 'tenancyName',
        itemId: 'tenancyName',
        allowBlank:false,
        fieldLabel: app.localize('TenancyName').initCap(),
        width: '100%',
        ui:'fieldLabelTop',
        emptyText: app.localize('TTenancyCodeName')
    }, {
        xtype: 'textfield',
        name: 'name',
        allowBlank: false,
        fieldLabel: app.localize('Name').initCap(),
        width: '100%',
        ui: 'fieldLabelTop',
        emptyText: app.localize('TName')
    }, {
        xtype: 'textfield',
        name: 'adminEmailAddress',
        fieldLabel: app.localize('AdminEmailAddress').initCap(),
        width: '100%',
        allowBlank: false,
        ui: 'fieldLabelTop',
        emptyText: app.localize('TAdminEmailAddress')
    }, {
        xtype: 'combobox',
        name: 'editionId',
        fieldLabel: app.localize('Edition').initCap(),
        width: '100%',
        ui: 'fieldLabelTop',
        emptyText: app.localize('TEdition'),
        displayField: 'editionDisplayName',
        valueField: 'editionId',
        bind: {
            store: '{editionsForComboBox}'
        }
    },{
        xtype: 'checkbox',
        boxLabel: app.localize('ShouldChangePasswordOnNextLogin'),
        name: 'shouldChangePasswordOnNextLogin',
        labelAlign: 'right',
        inputValue: true,
        checked: true,
        boxLabelCls: 'checkboxLabel'
    }, {
        xtype: 'checkbox',
        boxLabel: app.localize('SendActivationEmail'),
        name: 'sendActivationEmail',
        labelAlign: 'right',
        inputValue: true,
        checked: true,
        boxLabelCls: 'checkboxLabel'
    }, {
        xtype: 'checkbox',
        boxLabel: app.localize('Active'),
        name: 'isActive',
        labelAlign: 'right',
        inputValue: true,
        checked: true,
        boxLabelCls: 'checkboxLabel'
    }]
    
});
