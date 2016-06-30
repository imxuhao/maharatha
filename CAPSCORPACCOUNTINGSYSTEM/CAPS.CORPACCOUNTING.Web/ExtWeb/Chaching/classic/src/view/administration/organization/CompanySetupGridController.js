Ext.define('Chaching.view.administration.organization.CompanySetupGridController', {
    extend: 'Chaching.view.common.grid.ChachingGridPanelController',
    alias: 'controller.administration-organization-companysetupgrid',
    doAfterCreateAction: function (createMode, formPanel, isEdit, record) {
        var form = formPanel.getForm();
        var membersTab = formPanel.down('*[itemId=membersTab]');
        if (isEdit) {
            if (record._address)
            form.findField('addressId').setValue(record._address.get('addressId'));
            //enable tabs
            var companyPreferencesTab = formPanel.down('*[itemId=companyPreferencesTab]');
            if (companyPreferencesTab) {
                companyPreferencesTab.setDisabled(false);
            }
            if (membersTab) {
                membersTab.setDisabled(false);
            }
        } else {

        }
        //load default combo
        var defaultBankCombo = form.findField('defaultBank');
        defaultBankCombo.getStore().load();
    }
});
