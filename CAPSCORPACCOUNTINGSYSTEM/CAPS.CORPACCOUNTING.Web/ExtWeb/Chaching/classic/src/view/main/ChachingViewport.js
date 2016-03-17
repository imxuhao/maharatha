
Ext.define('Chaching.view.main.ChachingViewport', {
    extend: 'Ext.container.Viewport',

    requires: [
        'Chaching.view.main.ChachingViewportController',
        'Chaching.view.main.ChachingViewportModel',
        'Chaching.view.menu.ChachingMenu',
        'Chaching.view.header.ChachingHeader'
    ],

    controller: 'main-chachingviewport',
    viewModel: {
        type: 'main-chachingviewport'
    },

    layout: 'border',
    defaults: {
        bodyStyle: {
            'background-color': '#F3F5F9'
        }
    },
    //height: '100%',
    //width: '100%',
    alias: 'widget.chachingviewport',

    items: [
        {
            region: 'north',
            height: 70,
            items: [
                {
                    xtype: 'chachingheader',
                    height: 70
                }
            ]
        },
        {
            region: 'west',
            width: 300,
            split: false,
            reference: 'treelistContainer',
            layout: {
                type: 'vbox',
                align: 'stretch'
            },
            bodyStyle: {
                'background-color': '#F3F5F9',
                'padding':'10px 20px 20px 20px'
            },
            border: false,
            scrollable: 'y',
            items: [
                {
                    xtype: 'chachingmenu'
                }
            ]

        }
    ],
    listeners: {
        resize:'onViewportResize'
    }
  
});
