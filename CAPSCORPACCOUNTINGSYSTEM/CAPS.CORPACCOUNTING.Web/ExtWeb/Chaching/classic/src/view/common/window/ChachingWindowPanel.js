/**
 * This class is created as a base window.
 * Author: Krishna Garad
 * Date Created: 12/05/2016
 */
/**
 * @class Chaching.view.common.window.ChachingWindowPanel
 * Base Window class.
 */
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
    monitorResize : true,
    modal: true,
    padding:5,
    closeAction: 'destroy',
    ui:'chachingWindow',
    name: null,
    border: false,
    frame: false,
    initComponent: function () {
        var me = this,
            controller = me.getController();
        me.callParent(arguments);
        me.mon(Ext.getBody(), 'click', function (el, e) {
            me.close(me.closeAction);
        }, me, { delegate: '.x-mask' });

        me.on('resize', controller.onWindowViewResize, me);
    }
});
