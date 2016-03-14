Ext.define('Chaching.view.menu.ChachingMenuModel', {
    extend: 'Ext.app.ViewModel',
    alias: 'viewmodel.menu-chachingmenu',
    data: {
        name: 'Chaching'
    },
    stores: {
        navItems: {
            type: 'tree',
            root: {
                expanded: true,
                children: [{
                    text: 'Home',
                    expanded: true,
                    iconCls: 'x-fa fa-home',
                    children: [{
                        text: 'Messages',
                        iconCls: 'x-fa fa-inbox',
                        leaf: true
                    }, {
                        text: 'Archive',
                        expanded: true,
                        iconCls: 'x-fa fa-database',
                        children: [{
                            text: 'First',
                            iconCls: 'x-fa fa-sliders',
                            leaf: true
                        }, {
                            text: 'No Icon',
                            iconCls: null,
                            leaf: true
                        }]
                    }, {
                        text: 'Music',
                        iconCls: 'x-fa fa-music',
                        leaf: true
                    }, {
                        text: 'Video',
                        iconCls: 'x-fa fa-film',
                        leaf: true
                    }]
                }, {
                    text: 'Users',
                    iconCls: 'x-fa fa-user',
                    expanded: true,
                    children: [{
                        text: 'Tagged',
                        iconCls: 'x-fa fa-tag',
                        leaf: true
                    }, {
                        text: 'Inactive',
                        iconCls: 'x-fa fa-trash',
                        leaf: true
                    }]
                }, {
                    text: 'Groups',
                    iconCls: 'x-fa fa-group',
                    leaf: true
                }, {
                    text: 'Settings',
                    iconCls: 'x-fa fa-wrench',
                    expanded: true,
                    children: [{
                        text: 'Sharing',
                        iconCls: 'x-fa fa-share-alt',
                        leaf: true
                    }, {
                        text: 'Notifications',
                        iconCls: 'x-fa fa-flag',
                        leaf: true
                    }, {
                        text: 'Network',
                        iconCls: 'x-fa fa-signal',
                        leaf: true
                    }]
                }]
            }
        }
    }

});
