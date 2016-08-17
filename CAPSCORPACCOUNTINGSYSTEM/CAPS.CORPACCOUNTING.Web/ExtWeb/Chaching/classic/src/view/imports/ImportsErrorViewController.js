Ext.define('Chaching.view.imports.ImportsErrorViewController', {
    extend: 'Chaching.view.common.window.ChachingWindowPanelController',
    alias: 'controller.window-importsView',
    ErrorList:null,
    onFormShow: function (form, eOpts) {
        var me = this, panelStore, panelTpl, panelItems, 
            win= me.getView(),
            formView = win.down('form').getController().getView(),
            gridPanel = formView.down('gridpanel[itemId=errorgridPanelItemId]');
        //me.ErrorList
        if (gridPanel) {
            gridPanel.getStore().loadData(me.ErrorList);
        }
        
    }
});