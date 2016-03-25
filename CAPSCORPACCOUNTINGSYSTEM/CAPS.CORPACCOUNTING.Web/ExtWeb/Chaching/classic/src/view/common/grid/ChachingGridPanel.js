
Ext.define('Chaching.view.common.grid.ChachingGridPanel',{
    extend: 'Ext.panel.Panel',

    requires: [
        'Chaching.view.common.grid.ChachingGridPanelController',
        'Chaching.view.common.grid.ChachingGridPanelModel'
    ],

    controller: 'common-grid-chachinggridpanel',
    viewModel: {
        type: 'common-grid-chachinggridpanel'
    },

    html: 'Hello, World!!'
});
