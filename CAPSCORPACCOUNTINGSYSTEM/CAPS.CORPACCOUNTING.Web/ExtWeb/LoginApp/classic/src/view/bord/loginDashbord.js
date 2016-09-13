
Ext.define('LoginApp.view.bord.loginDashbord',{
    extend: 'Ext.tab.Panel',

    requires: [
        'LoginApp.view.bord.loginDashbordController',
        'LoginApp.view.bord.loginDashbordModel',
        'LoginApp.view.login.LoginView',
        'LoginApp.view.resetpwd.ResetPassword',
        'LoginApp.view.email.EmailActivation'
    ],

    controller: 'bord-logindashbord',
    viewModel: {
        type: 'bord-logindashbord'
    },
    header:false,
    height: 390,
    width: 400,
    ui: 'navigation',
    renderTo: 'appDiv',
    titleRotation: 0,
    tabRotation: 0,
    tabPosition: 'bottom',
    //tabRotation:2,
    tabBar: {
        flex: 1,
        layout: {
            align: 'center',
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
                    flex: 1
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
        title: abp.localization.localize("LogIn"),
        iconCls: 'fa-unlock',
        baseCls:'',
        bodyStyle: {
            'background-color':'transparent'
        },
        items: [{ xtype: 'loginView' }]
       
    }, {
        title: abp.localization.localize("EmailActivation"),
        iconCls: 'x-fa fa-inbox',
        baseCls: '',
        bodyStyle: {
            'background-color': 'transparent'
        },
        items: [{ xtype: 'emailactivationView' }]
    }, {
        title: abp.localization.localize("ForgotPassword"),
        iconCls: 'x-fa fa-lock',
        baseCls: '',
        bodyStyle: {
            'background-color': 'transparent'
        },
        items: [{ xtype: 'resetpwdView' }]
    }]
});
