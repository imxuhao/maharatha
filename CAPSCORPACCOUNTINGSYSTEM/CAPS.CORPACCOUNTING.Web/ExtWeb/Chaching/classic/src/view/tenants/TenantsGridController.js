Ext.define('Chaching.view.tenants.TenantsGridController', {
    extend: 'Chaching.view.common.grid.ChachingGridPanelController',
    alias: 'controller.tenants-tenantsgrid',
    //TODO convert this function in component(editing) so for every combo we need not to write
    onEditionChange: function (combo, newValue, oldValue, e) {
        var grid = combo.up();
        if (grid) {
            var context = grid.context,
                record = context.record;
            record.set('editionId', newValue);
        }
    },
    doAfterCreateAction: function (createMode, formView, isEdit) {
        var me = this,
         form = formView.down('form').getForm();
        if (formView && isEdit) {
            form.findField('tenancyName').setReadOnly(true);
            form.findField('isUseHostDatabase').setHidden(true);
            form.findField('connectionString').setHidden(true);
            form.findField('isSetRandomPassword').setHidden(true);
            form.findField('adminPassword').setHidden(true);
            form.findField('adminPasswordRepeat').setHidden(true);
            form.findField('adminEmailAddress').setReadOnly(true);
            form.findField('organizationId').setReadOnly(true);
        }
        var organizationStore = form.findField('organizationId').getStore();
        organizationStore.load();
        var viewModel = formView.down('form').getViewModel();
        var editionStore = viewModel.getStore('editionsForComboBox');
        editionStore.load();
    }
});
