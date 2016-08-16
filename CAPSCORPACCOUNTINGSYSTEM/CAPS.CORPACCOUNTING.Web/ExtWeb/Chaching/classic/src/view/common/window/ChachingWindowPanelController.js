Ext.define('Chaching.view.common.window.ChachingWindowPanelController',
{
    extend: 'Ext.app.ViewController',
    alias: 'controller.common-window-chachingwindowpanel',
    onWindowViewResize: function(wnd, width, height) {
        Ext.defer(function() {
                wnd.updateLayout();
                wnd.center();
            },500);
    }

});
