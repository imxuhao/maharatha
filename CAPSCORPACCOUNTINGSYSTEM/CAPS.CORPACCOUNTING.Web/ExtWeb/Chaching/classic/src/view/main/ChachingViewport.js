
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
            width: 250,
            split: false,
            reference: 'treelistContainer',
            layout: {
                type: 'vbox',
                align: 'stretch'
            },
            bodyStyle: {
                'background-color': '#F3F5F9',
                'padding': '10px 20px 20px 20px'
            },
            border: false,
            scrollable: 'y',
            items: [
                {
                    xtype: 'chachingmenu'
                }
            ]

        },
        {
            xtype: 'container',
            region: 'center',
            layout: 'fit',
            items: [
                {
                    xtype: 'gridpanel',
                    //layout: 'fit',
                    store: 'Personnel',
                    title:'Personnel',
                    bodyStyle: {
                        'background-color': '#F3F5F9'
                    },
                    plugins: [
                        {
                            ptype: 'saki-gms',
                            clearItemIconCls:'icon-settings',
                            pluginId: 'gms',
                            height:32,
                            filterOnEnter: false
                        }
                    ],
                    features:[{
                        ftype:'ux-gmsrt'
                ,displaySortOrder:true
                    }],
                    columns: [
                        {
                            text: 'Name',
                            dataIndex: 'name',
                            stateId: 'name',
                            sortable: true,
                            width: 160,
                            // simplest filter configuration
                            
                            filterField: {
                                xtype: 'textfield',
                                width:'90%',
                                plugins: [{
                                    ptype: 'saki-ficn'
                                    , iconCls: 'fa fa-info'
                                    , qtip: 'Enter name to search'
                                }]
                            }
                        }, {
                            text: 'Email',
                            dataIndex: 'email',
                            sortable: true,
                            flex: 1

                            // equivalent to filterField:true
                            // as textfield is created by default
                            ,
                            filterField: {
                                xtype: 'textfield',
                                width:'90%'
                                , plugins: [{
                                    ptype: 'saki-ficn'
                                    , iconCls: 'fa fa-info'
                                    , qtip: 'Enter Email Address to search'
                                }]
                            }
                        }, {
                            text: 'Phone',
                            dataIndex: 'phone',
                            sortable: true,
                            width: 110,
                            //align: 'right',
                            //format: '0,000',
                            filterField: {
                                xtype: 'textfield',
                                width: '90%'
                               , plugins: [{
                                   ptype: 'saki-ficn'
                                   , iconCls: 'fa fa-info'
                                   , qtip: 'Enter phone to search'
                               }]
                            }
                        }
                    ]
                }
            ]
        }
    ],
    listeners: {
        resize: 'onViewportResize'
    }

});
