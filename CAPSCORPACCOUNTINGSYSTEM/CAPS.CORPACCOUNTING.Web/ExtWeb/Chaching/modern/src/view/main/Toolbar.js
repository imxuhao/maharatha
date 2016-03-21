Ext.define('Chaching.view.main.Toolbar', {
    extend: 'Ext.Toolbar',
    xtype: 'maintoolbar',

   

    items: [
        {
            // This component is moved to the floating nav container by the phone profile
            xtype: 'component',
            reference: 'logo',
            userCls: 'main-logo',
            html: '<img src="'+abp.appPath+'Content/images/capslogo.png" height=30px width=80px style="margin-top:5px;"/>'
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
            handler: 'toolbarButtonClick'
           
        },
        {
            xtype:'button',
            ui: 'badgeBtn',
            //iconCls: 'badge',
            //iconAlign: 'right',
            text: '&#xf0a2;',
            //textAlign: 'left',
            href: '#notification',
            margin: '6 0 0 0',
            handler: 'toolbarButtonClick'
        },
        {
            xtype: 'image',
            userCls: 'main-user-image small-image circular',
            alt: 'Current user image',
            src: abp.appPath + 'Profile/GetProfilePicture?t=' + new Date().getTime()
            //src: 'resources/images/user-profile/2.png'
        }
    ]
});
