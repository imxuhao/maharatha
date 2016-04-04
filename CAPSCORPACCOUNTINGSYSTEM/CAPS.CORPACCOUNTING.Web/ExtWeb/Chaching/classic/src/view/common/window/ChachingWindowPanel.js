
Ext.define('Chaching.view.common.window.ChachingWindowPanel',{
    extend: 'Ext.window.Window',

    requires: [
        'Chaching.view.common.window.ChachingWindowPanelController',
        'Chaching.view.common.window.ChachingWindowPanelModel'
    ],

    controller: 'common-window-chachingwindowpanel',
    viewModel: {
        type: 'common-window-chachingwindowpanel'
    },
    modal: true,
    padding:5,
    closeAction: 'destroy',
    ui:'chachingWindow',
    name: null,
    border: false,
    frame: false,
});
