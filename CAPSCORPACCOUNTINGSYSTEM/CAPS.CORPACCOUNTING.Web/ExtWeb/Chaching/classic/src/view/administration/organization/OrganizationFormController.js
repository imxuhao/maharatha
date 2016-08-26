Ext.define('Chaching.view.administration.organization.OrganizationFormController', {
    extend: 'Chaching.view.common.form.ChachingFormPanelController',
    alias: 'controller.administration-organizationunits-organizationform',
    OnOrganizationNameChange: function (field, newValue, oldValue, eOpts) {
        field.setValue(newValue.toUpperCase());
    }
});
