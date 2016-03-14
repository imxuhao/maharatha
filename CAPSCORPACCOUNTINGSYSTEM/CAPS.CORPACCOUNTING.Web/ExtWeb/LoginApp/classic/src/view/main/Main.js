/**
 * This class is the main view for the application. It is specified in app.js as the
 * "mainView" property. That setting automatically applies the "viewport"
 * plugin causing this view to become the body element (i.e., the viewport).
 *
 * TODO - Replace this content of this view to suite the needs of your application.
 */
Ext.define('LoginApp.view.main.Main', {
    extend: 'Ext.tab.Panel',
    xtype: 'app-main',

    requires: [
        'Ext.plugin.Viewport',
        'Ext.window.MessageBox',

        'LoginApp.view.main.MainController',
        'LoginApp.view.main.MainModel'
    ],

    controller: 'main',
    viewModel: 'main',
    width: 1000,
    bodyStyle: {
        'top':'50px'
    },
    ui: 'navigation',
    tabBarHeaderPosition: 1,
    titleRotation: 0,
    tabRotation: 0,
    tabBar: {
        flex: 1,
        layout: {
            pack:'right',
            align: 'center',
            overflowHandler: 'none'
        }
    },

    responsiveConfig: {
        tall: {
            headerPosition: 'left',
            iconCls: 'fa-th-list',
            title: {
                text: 'CAS'
            }
        },
        wide: {
            headerPosition: 'top',
            iconCls: 'fa-th-list',
            title: {
                text: 'Corporate Accounting System',
                flex: 0
            }
        }
    },

    defaults: {
        bodyPadding: 20,
        tabConfig: {
            plugins: 'responsive',
            responsiveConfig: {
                wide: {
                    iconAlign: 'top',
                    textAlign: 'center',
                    flex: 1
                },
                tall: {
                    iconAlign: 'left',
                    textAlign: 'center',
                    flex:1
                }
            }
        }
    },

    items: [{
        title: 'Home',
        iconCls: 'fa-home'
        // The following grid shares a store with the classic version's grid as well!
        //items: [{
        //    xtype: 'mainlist'
        //}]
    }, {
        title: 'Users',
        iconCls: 'fa-user',
        cls: 'page-md login'
        //bind: {
        //    html: '{loremIpsum}'
        //}
    }, {
        title: 'Groups',
        iconCls: 'fa-users',
        //bind: {
        //    html: '{loremIpsum}'
        //}
    }, {
        title: 'Settings',
        iconCls: 'fa-cog',
        //bind: {
        //    html: '{loremIpsum}'
        //}
    }]
});
