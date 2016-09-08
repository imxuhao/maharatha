Ext.define('Chaching.view.financials.accounts.AccountsMapCoaWindowController', {
    extend: 'Chaching.view.common.window.ChachingWindowPanelController',
    alias: 'controller.financials-accounts-accountsmapcoawindow',
    onMapSaveClicked: function (btn, e, eOpts) {
        
    },
    onCancelClicked: function (btn, e, eOpts) {

        var me = this,
            view = me.getView();
        if (view && view.openInPopupWindow) {
            var wnd = view.up('window');
            Ext.destroy(wnd);
            return;
        }
        Ext.destroy(view);
    }
});
