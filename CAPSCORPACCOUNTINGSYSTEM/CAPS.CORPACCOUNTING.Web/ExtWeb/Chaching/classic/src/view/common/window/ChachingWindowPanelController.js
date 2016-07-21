Ext.define('Chaching.view.common.window.ChachingWindowPanelController', {
    extend: 'Ext.app.ViewController',
    alias: 'controller.common-window-chachingwindowpanel',
    onWindowViewResize: function (window, width, height) {
        window.updateLayout();
    }
    
});
