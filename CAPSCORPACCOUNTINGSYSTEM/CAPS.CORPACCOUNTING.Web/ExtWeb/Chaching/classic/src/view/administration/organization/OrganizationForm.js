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
    displayDefaultButtonsCenter: true,
    titleConfig: {
        title: abp.localization.localize("CreateNewOrganization").initCap()
    },
   // layout: 'fit',
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
            allowBlank: false,
            fieldLabel: app.localize('OrganizationName').initCap(),
            width: '100%',
            ui: 'fieldLabelTop',
            emptyText: app.localize('MandatoryField')
        }
    ]
});