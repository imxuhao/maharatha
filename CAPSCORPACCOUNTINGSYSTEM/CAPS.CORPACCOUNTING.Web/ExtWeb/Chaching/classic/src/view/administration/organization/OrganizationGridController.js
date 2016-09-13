Ext.define('Chaching.view.administration.organization.OrganizationGridController', {
    extend: 'Chaching.view.common.grid.ChachingGridPanelController',
    alias: 'controller.administration-organization-organizationgrid',
    doAfterCreateAction: function (createMode, formPanel, isEdit, record) {
        var me = this,
            form = formPanel.down('form'),
            basicForm = null;
        
        if (form) {
            basicForm = form.getForm();
            var connectionStringCombo = basicForm.findField('connectionStringId');
            if (connectionStringCombo) {
                if (isEdit) {
                    connectionStringCombo.setReadOnly(true);
                } else {
                    connectionStringCombo.setReadOnly(false);
                }
                var connectionStore = connectionStringCombo.getStore();
                connectionStore.load();
            }
        }
    },
    OnOrganizationNameChange: function (field, newValue, oldValue, eOpts) {
        field.setValue(newValue.toUpperCase());
    }
});
