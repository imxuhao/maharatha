
Ext.define('Chaching.view.tenants.TenantsForm',{
    extend: 'Chaching.view.common.form.ChachingFormPanel',
    alias:['host.tenant.create','host.tenant.edit'],
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
        labelAlign: 'top'
    },
    defaultFocus:'textfield#tenancyName',
    items: [{
        xtype: 'hiddenfield',
        name: 'id',
        value:0
    }, {
        xtype: 'textfield',
        name: 'tenancyName',
        itemId:'tenancyName',
        fieldLabel: app.localize('TenancyName'),        
        width: '100%',
        ui:'fieldLabelTop',
        emptyText: app.localize('TTenancyCodeName')
    }, {
        xtype: 'textfield',
        name: 'name',
        fieldLabel: app.localize('Name'),
        width: '100%',
        ui: 'fieldLabelTop',
        emptyText: app.localize('TName')
    }, {
        xtype: 'textfield',
        name: 'adminEmailAddress',
        fieldLabel: app.localize('AdminEmailAddress'),
        width: '100%',
        ui: 'fieldLabelTop',
        emptyText: app.localize('TAdminEmailAddress')
    }, {
        xtype: 'combobox',
        name: 'editionId',
        fieldLabel: app.localize('Edition'),
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
