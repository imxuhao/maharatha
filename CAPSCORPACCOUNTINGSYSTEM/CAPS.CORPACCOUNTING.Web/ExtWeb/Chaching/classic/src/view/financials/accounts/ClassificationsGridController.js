Ext.define('Chaching.view.financials.accounts.ClassificationsGridController', {
    extend: 'Chaching.view.common.grid.ChachingGridPanelController',
    alias: 'controller.financials-accounts-classificationsgrid',
    doAfterCreateAction: function (createMode, formView, isEdit) {
    },
    doRowSpecificEditDelete: function (button, grid) {
        if (button.menu) {
            var quickEditButton = button.menu.down('menuitem#quickEditActionMenu');
            var deleteButton = button.menu.down('menuitem#deleteActionMenu');
            if (!button.widgetRec.data.allowDelete && !button.widgetRec.data.allowEdit) {
                button.menu.hide();
            }
            else {
                if ((quickEditButton && button.widgetRec)) {
                    if (!button.widgetRec.data.allowEdit)
                        quickEditButton.hide();
                }
                if ((deleteButton && button.widgetRec)) {
                    if (!button.widgetRec.data.allowDelete)
                        deleteButton.hide();
                }
            }
        }
    }
});
