
Ext.define('LoginApp.view.bord.loginDashbord',{
    extend: 'Ext.tab.Panel',

    requires: [
        'LoginApp.view.bord.loginDashbordController',
        'LoginApp.view.bord.loginDashbordModel',
        'LoginApp.view.login.LoginView'
    ],

    controller: 'bord-logindashbord',
    viewModel: {
        type: 'bord-logindashbord'
    },
    header:false,
    height: 350,
    width: 400,
    ui: 'navigation',
    renderTo: 'appDiv',
    titleRotation: 0,
    tabRotation: 0,
    tabPosition: 'left',
    //tabRotation:2,
    tabBar: {
        flex: 1,
        layout: {
            align: 'left',
            overflowHandler: 'none'
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
                    width: 140
                },
                tall: {
                    iconAlign: 'left',
                    textAlign: 'center',
                    width: 140
                }
            }
        }
    },

    items: [{
        title: 'Login',
        iconCls: 'fa-unlock',
        baseCls:'',
        bodyStyle: {
            'background-color':'transparent'
        },
        items: [{ xtype: 'loginView' }]
       
    }, {
        title: 'Register',
        iconCls: 'fa-key',
        baseCls: '',
        bodyStyle: {
            'background-color': 'transparent'
        }
    }, {
        title: 'Email Activation',
        iconCls: 'x-fa fa-inbox',
        baseCls: '',
        bodyStyle: {
            'background-color': 'transparent'
        }
    }, {
        title: 'Reset Password',
        iconCls: 'x-fa fa-lock',
        baseCls: '',
        bodyStyle: {
            'background-color': 'transparent'
        }
    }]
});
