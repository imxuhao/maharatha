Ext.define('Chaching.view.administration.organization.MembersForm', {
    extend: 'Chaching.view.common.form.ChachingFormPanel',
    alias: ['widget.administration.organizationunits.members'],
    requires: [
       // 'Chaching.view.administration.organization.MembersFormController'
    ],
   // controller: 'administration-organization-membersform',
    modulePermissions: {
        read: abp.auth.isGranted('Pages.Administration.OrganizationUnits'),
        create: abp.auth.isGranted('Pages.Administration.OrganizationUnits.Create'),
        edit: abp.auth.isGranted('Pages.Administration.OrganizationUnits.Edit'),
        destroy: abp.auth.isGranted('Pages.Administration.OrganizationUnits.Delete')
    },
    name: 'members',
    openInPopupWindow: false,
    hideDefaultButtons: true,
    autoScroll: true,
    border: false,
    showFormTitle: false,
    displayDefaultButtonsCenter: true,
    titleConfig: {
        title: abp.localization.localize("Members").initCap()
    },
    items: [{
        xtype: 'panel',
        html : 'Members will come to this place.'
    }]
});