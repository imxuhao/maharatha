Ext.define('Chaching.view.tenants.TenantsGridController', {
    extend: 'Chaching.view.common.grid.ChachingGridPanelController',
    alias: 'controller.tenants-tenantsgrid',
    //TODO convert this function in component(editing) so for every combo we need not to write
    onEditionChange:function(combo, newValue, oldValue, e) {
        var grid = combo.up();
        if (grid) {
            var context = grid.context,
                record = context.record;
            record.set('editionId', newValue);
        }
    },
    doAfterCreateAction:function(createMode, formView,isEdit) {
        if (formView && isEdit) {
            var form = formView.down('form').getForm();
            form.findField('tenancyName').setReadOnly(true);
            form.findField('adminEmailAddress').setReadOnly(true);
        }
    }
});
