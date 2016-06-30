Ext.define('Chaching.view.administration.organization.CompanySetupGridController', {
    extend: 'Chaching.view.common.grid.ChachingGridPanelController',
    alias: 'controller.administration-organization-companysetupgrid',
    doAfterCreateAction: function (createMode, formPanel, isEdit, record) {
        var form = formPanel.getForm();
        if (isEdit) {
            form.findField('addressId').setValue(record._address.get('addressId'));
        }
        //load default combo
        var defaultBankCombo = form.findField('defaultBank');
        defaultBankCombo.getStore().load();
    }
});
