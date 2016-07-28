Ext.define('Chaching.view.roles.RolesGridController', {
    extend: 'Chaching.view.common.grid.ChachingGridPanelController',
    alias: 'controller.roles-rolesgrid',
    doAfterCreateAction: function (createNewMode, form, isEdit, record) {       
        var me = this;
        if (form.down('treepanel')) {
            var treeStore = form.down('treepanel').getStore();
            if (isEdit) {
                treeStore.getProxy().setExtraParams({ id: record.get('id') });
            } else {
                treeStore.getProxy().setExtraParams({});
            }
            treeStore.load();
        }
        if (isEdit) {
            if (record.data.displayName.toLowerCase() == 'admin') {
                if (form.down('checkbox')) {
                    var chkbox = form.down('checkbox[name=isDefault]');
                    chkbox.checked = false;
                    chkbox.setDisabled(true);
                }
            }
        }
    },
    doRowSpecificEditDelete: function (button, grid) {
        if (button.menu) {
            var deleteActionMenu = button.menu.down('menuitem#deleteActionMenu');
            if (deleteActionMenu && button.widgetRec && button.widgetRec.get('isStatic')) {
                deleteActionMenu.hide();
            } else {
                deleteActionMenu.show();
            }
        }
    }

});
