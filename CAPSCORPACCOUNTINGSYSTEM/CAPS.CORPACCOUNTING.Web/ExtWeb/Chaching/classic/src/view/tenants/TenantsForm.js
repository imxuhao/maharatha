
Ext.define('Chaching.view.tenants.TenantsForm',{
    extend: 'Chaching.view.common.form.ChachingFormPanel',
    alias: ['host.tenants.create', 'host.tenants.edit'],
    requires: [
        'Chaching.view.tenants.TenantsFormController'
    ],

    controller: 'tenants-tenantsform',
    name: 'Tenants',
    openInPopupWindow: true,
    hideDefaultButtons: false,

    scrollable: true,
    border: false,
    showFormTitle: false,
    displayDefaultButtonsCenter: true,
    defaults: {
        bodyStyle: { 'background-color': 'trasparent' },
      //  labelAlign: 'top',
        blankText: app.localize('MandatoryToolTipText')
    },
   
    items: [{
        xtype: 'tabpanel',
        ui: 'formTabPanels',
        items : [{
            title: abp.localization.localize("GeneralInformation"),
            padding: '0 0 0 10',
            scrollable: true,
            iconCls: 'fa fa-gear',
            defaults: {
                labelWidth: 140
            },
            defaultFocus: 'combobox#organizationId',
            items: [{
                xtype: 'hiddenfield',
                name: 'id',
                value: 0
            },
            {
                xtype: 'combobox',
                name: 'organizationUnitId',
                itemId : 'organizationId',
                fieldLabel: app.localize('TenantGroupName'),
                width: '100%',
                ui: 'fieldLabelTop',
                emptyText: app.localize('SelectOption'),
                queryMode: 'local',
                displayField: 'name',
                valueField: 'value',
                allowBlank: false,
               // forceSelection : true,
                store: Ext.create('Chaching.store.administration.organization.OrganizationListStore'),
                listeners: {
                    select : 'onOrganizationSelect'
                }
            },
            {
                xtype: 'textfield',
                name: 'tenancyName',
                itemId: 'tenancyName',
                allowBlank: false,
                fieldLabel: app.localize('TenancyName'),
                width: '100%',
                ui: 'fieldLabelTop',
                emptyText: app.localize('TTenancyCodeName'),
                listeners: {
                    change : 'onTenancyNameEnter'
                }
            }, {
                xtype: 'textfield',
                name: 'name',
                hidden : true,
                allowBlank: false,
                fieldLabel: app.localize('Name'),
                width: '100%',
                ui: 'fieldLabelTop',
                emptyText: app.localize('TName')
            },
            {
                xtype: 'textfield',
                name: 'adminEmailAddress',
                fieldLabel: app.localize('AdminEmailAddress').initCap(),
                width: '100%',
                allowBlank: false,
                ui: 'fieldLabelTop',
                emptyText: app.localize('TAdminEmailAddress')
            }, {
                xtype: 'checkbox',
                boxLabel: app.localize('SetRandomPassword'),
                name: 'isSetRandomPassword',
                reference: 'isSetRandomPassword',
                labelAlign: 'right',
                inputValue: true,
                checked: true,
                boxLabelCls: 'checkboxLabel',
                listeners: {
                    change: 'showRandomPassword'
                }
            }, {
                xtype: 'textfield',
                name: 'adminPassword',
                reference : 'adminPassword',
                fieldLabel: app.localize('AdminPassword'),
                width: '100%',
                // allowBlank: false,
                ui: 'fieldLabelTop',
                inputType: 'password',
                bind: {
                    hidden: '{isSetRandomPassword.checked}'
                },
                emptyText: app.localize('AdminPassword')
            }, {
                xtype: 'textfield',
                name: 'adminPasswordRepeat',
                reference: 'adminPasswordRepeat',
                submitValue: false,
                fieldLabel: app.localize('AdminPasswordRepeat'),
                width: '100%',
                // allowBlank: false,
                ui: 'fieldLabelTop',
                inputType: 'password',
                bind: {
                    hidden: '{isSetRandomPassword.checked}'
                },
                emptyText: app.localize('AdminPasswordRepeat'),
                /*
                * Custom validator implementation - checks that the value matches what was entered into
                * the password1 field.
                */
                validator: function (value) {
                    var password1 = this.previousSibling('[name=adminPassword]');
                    return (value === password1.getValue()) ? true : 'Passwords do not match.';
                }
            }, {
                xtype: 'combobox',
                name: 'editionId',
                fieldLabel: app.localize('Edition'),
                width: '100%',
                ui: 'fieldLabelTop',
                emptyText: app.localize('TEdition'),
                displayField: 'editionDisplayName',
                valueField: 'editionId',
                queryMode : 'local',
                bind: {
                    store: '{editionsForComboBox}'
                }
            }, {
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
        }, {
            title: abp.localization.localize("CopyFromTenants"),
            padding: '0 0 0 10',
            iconCls: 'fa fa-gear',
            disabled : true,
            xtype: 'grid',
            cls: 'chaching-grid',
            itemId: 'moduleListGridItemId',
            width:'100%',
            //height: 400,
            scrollable: true,
            selType: 'checkboxmodel',
            columns: [
               { text: 'Module Name', dataIndex: 'displayName', flex: 1 }
            ],
            dockedItems : [{
                xtype : 'toolbar',
                dock : 'top',
                items : [ {
                    xtype: 'combobox',
                    itemId : 'tenantItemId',
                    valueField : 'tenantId',
                    displayField : 'tenantName',
                    fieldLabel: app.localize('OrganizationTenants'),
                    submitValue : false,
                    width: '100%',
                    maxWidth : 300,
                    ui: 'fieldLabelTop',
                    queryMode: 'local',
                    labelWidth : 60,
                    forceSelection : true,
                    emptyText: app.localize('SelectOrganizationTenant'),
                    store: Ext.create('Chaching.store.administration.organization.TenantListStore'),
                    listeners: {
                        select: 'onTenantSelect'
                    }

                }]
            }],
            store: {
                fields: ['name','displayName'],
                data : []
            }
        }]
    }]

    
    
});
