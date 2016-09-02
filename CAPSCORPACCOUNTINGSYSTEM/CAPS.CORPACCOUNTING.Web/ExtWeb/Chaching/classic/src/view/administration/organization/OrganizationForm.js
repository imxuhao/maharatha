Ext.define('Chaching.view.administration.organization.OrganizationForm', {
    extend: 'Chaching.view.common.form.ChachingFormPanel',
    alias: [
        'widget.organizationUnits.create', 'widget.organizationUnits.edit'
    ],
    requires: [
        'Chaching.view.administration.organization.OrganizationFormController'
    ],
    controller: 'administration-organizationunits-organizationform',
    name: 'organization',
    modulePermissions: {
        read: abp.auth.isGranted('Pages.Administration.OrganizationUnits'),
        create: abp.auth.isGranted('Pages.Administration.OrganizationUnits.ManageOrganizationTree'),
        edit: abp.auth.isGranted('Pages.Administration.OrganizationUnits.ManageOrganizationTree'),
        destroy: abp.auth.isGranted('Pages.Administration.OrganizationUnits.ManageOrganizationTree')
    },
    openInPopupWindow: true,
    hideDefaultButtons: false,
    autoScroll: false,
    border: false,
    showFormTitle: false,
    titleConfig: {
        title: abp.localization.localize("CreateNewTenantGroup")
    },
    // layout: 'fit',
    defaults : {
        labelWidth: 120
    },
   
    items: [
        {
            xtype: 'hiddenfield',
            itemId: 'organizationItemId',
            name: 'id', //organizationId
            value: 0
        },
        {
            xtype: 'textfield',
            name: 'displayName',
            itemId : 'organizationName',
            allowBlank: false,
            fieldLabel: app.localize('OrganizationName'),
            width: '100%',
            ui: 'fieldLabelTop',
            emptyText: app.localize('MandatoryField'),
            listeners: {
                change: 'OnOrganizationNameChange'
            }
        },
        {
            xtype: 'combobox',
            name: 'connectionStringId',
            fieldLabel: app.localize('SelectDatabase'),
            width: '100%',
            ui: 'fieldLabelTop',
            displayField: 'name',
            valueField: 'value',
            emptyText: app.localize('SelectDatabase'),
            queryMode: 'local',
            forceSelection: true,
            store: Ext.create('Chaching.store.administration.organization.ConnectionStringListStore')
        }
        //,
        //{
        //    xtype: 'label',
        //    padding: '10 0 0 0',
        //    style: { color: '#cacaca', fontSize: '15px' },
        //    // cls: "grayLabelText",
        //    //itemId: 'LinkCompanyMessageItemId',
        //    text: abp.localization.localize("TenantGroupMessage")
        //}
    ]
});