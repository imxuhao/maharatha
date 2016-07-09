Ext.define('Chaching.view.tenants.TenantsGridController', {
    extend: 'Chaching.view.common.grid.ChachingGridPanelController',
    alias: 'controller.tenants-tenantsgrid',
    //TODO convert this function in component(editing) so for every combo we need not to write
    //onEditionChange: function (combo, newValue, oldValue, e) {
    //    var grid = combo.up();
    //    if (grid) {
    //        var context = grid.context,
    //            record = context.record;
    //        record.set('editionId', newValue);
    //    }
    //},
    doAfterCreateAction: function (createMode, formView, isEdit, record) {
        var me = this,
         form = formView.down('form').getForm();
        var copyFromTenantsTab = formView.down('*[itemId=moduleListGridItemId]');
        if (formView && isEdit) {
            form.findField('tenancyName').setReadOnly(true);
            form.findField('isUseHostDatabase').setHidden(true);
            form.findField('connectionString').setHidden(true);
            form.findField('isSetRandomPassword').setHidden(true);
            form.findField('adminPassword').setHidden(true);
            form.findField('adminPasswordRepeat').setHidden(true);
            form.findField('adminEmailAddress').setReadOnly(true);
            form.findField('organizationUnitId').setReadOnly(true);
            form.findField('adminEmailAddress').setHidden(true);
            form.findField('shouldChangePasswordOnNextLogin').setHidden(true);
            form.findField('sendActivationEmail').setHidden(true);
            if (copyFromTenantsTab) {
                copyFromTenantsTab.setDisabled(true);
            }
            if (record.get('editionId') == null) {
                record.set('editionId', "null");
            }
        } else {
            //if (copyFromTenantsTab) {
            //    copyFromTenantsTab.setDisabled(false);
            //}
        }
        var organizationStore = form.findField('organizationUnitId').getStore();
        organizationStore.load();
        var viewModel = formView.down('form').getViewModel();
        var editionStore = viewModel.getStore('editionsForComboBox');
        editionStore.load();
       
    }
});
