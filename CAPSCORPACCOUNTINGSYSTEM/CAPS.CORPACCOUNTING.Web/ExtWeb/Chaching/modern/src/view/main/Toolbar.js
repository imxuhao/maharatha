Ext.define('Chaching.view.main.Toolbar', {
    extend: 'Ext.Toolbar',
    xtype: 'maintoolbar',
   

    items: [
        {
            // This component is moved to the floating nav container by the phone profile
            xtype: 'component',
            reference: 'logo',
            userCls: 'main-logo',
            html: '<img src="'+abp.appPath+'Content/images/capslogo.png" height=30px width=80px style="margin-top:5px;" alt= ""/>'
        },
        {
            xtype: 'button',
            ui: 'header',
            iconCls: 'x-fa fa-bars',
            margin: '0 0 0 10',
            listeners: {
                tap: 'onToggleNavigationSize'
            }
        },
        '->',
        {
            xtype:'button',
            ui: 'header',
            iconCls:'x-fa fa-search',
            href: '#searchresults',
            itemId:'GlobalSearch',
            margin: '0 0 0 0',
            handler: 'toolbarButtonClick'
        },
        {
            xtype:'button',
            ui: 'header',
            scale: 'small',
            itemId:'Localization',
            iconCls: 'famfamfam-flag-gb',
            iconAlign: 'right',
            href: '#localization',
            margin: '10 0 0 0',
            handler: 'toolbarButtonClick',
            listeners: {
                painted: 'onBeforeLocalizationRender',
                scope:'controller'
            }
           
        },
        {
            xtype:'button',
            ui: 'badgeBtn',
            text: '&#xf0a2;',
            href: '#notification',
            //badgeText: '1',
            //badgeCls: 'badge',
            textAlign:'left',
            itemId:'Notifiaction',
            margin: '6 0 0 0',
            handler: 'toolbarButtonClick',
            listeners: {
                painted: 'onNotificationPainted',
                scope: 'controller'
            }
        },
        {
            xtype: 'image',
            alt: '',
            userCls: 'main-user-image small-image circular',
            alt: 'Current user image',
            href:'profile',
            src: abp.appPath + 'Profile/GetProfilePicture?t=' + new Date().getTime(),
            listeners: {
                tap: 'showProfileList',
                scope: 'controller'
            }
            //src: 'resources/images/user-profile/2.png'
        }
    ]
});
