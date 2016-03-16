
Ext.define('Chaching.view.header.ChachingHeader', {
    extend: 'Ext.toolbar.Toolbar',

    requires: [
        'Chaching.view.header.ChachingHeaderController',
        'Chaching.view.header.ChachingHeaderModel'
    ],
    alias: 'widget.chachingheader',
    controller: 'header-chachingheader',
    viewModel: {
        type: 'header-chachingheader'
    },

    bodyStyle: {
        'background-color': '#F3F5F9'
    },

    layout: {
        type: 'hbox'
    },
    defaults: {
        bodyStyle: {
            'background-color': 'transparent',
            'border-color': 'transparent',
            'border-style': 'transparent'
        }
    },
    items: [
        {
            xtype: 'image',
            height: 30,
            itemId: 'CapsLogo',
            width: 110,
            src: abp.appPath + 'Content/images/capslogo.png',
            margin: '2px'
        }, {
            xtype: 'button',
            text: '',
            scale: 'medium',
            iconCls: 'x-fa fa-list',
            iconAlign: 'right',
            width: 120,
            baseCls: '',
            listeners: {
                click: 'onToggleClick'
            }
        }, '->',
        {
            xtype: 'button',
            scale: 'small',
            ui: 'countryMenu',
            width: 120,
            iconCls: 'famfamfam-flag-gb',
            iconAlign: 'left',
            text: 'English' + ' &#xf107;',
            baseCls: '',
            itemId:'LocalizationBtn',
            contextMenu:undefined,
            listeners: {
                mouseover: 'onLocalizationHover',
                beforerender:'onBeforeLocalizationRender'
            }
        },
         {
             scale: 'small',
             ui: 'badgeBtn',
             width: 50,
             iconCls: 'badge',
             iconAlign: 'right',
             text: '&#xf0a2;',
             textAlign: 'left',
             baseCls: '',
             itemId: 'NotificationBtn',
             contextMenu: undefined,
             menu: undefined,
             listeners: {
                 mouseover: 'onNotificationHover',
                 boxready: 'onNotificationReady'
             }
         },
        {
            scale: 'small',
            ui: 'badgeBtnBack',
            width: 100,
            //iconCls: 'badge',
            iconAlign: 'left',
            text: '&#xf112 .\\admin',
            textAlign: 'left',
            baseCls: '',
            itemId: 'AccountBtn',
            contextMenu: undefined,
            menu: undefined,
            listeners: {
                //mouseover: 'onAccountsHover',
                //boxready: 'onAccountsReady'
            }
        }
         //{
         //    xtype: 'button',
         //    scale: 'small',
         //    ui: 'countryMenu',
         //    width: 40,
         //    //iconCls: 'fa fa-angle-down',
         //    //iconAlign: 'left',
         //    text: '&#xf0a2;',
         //    textAlign: 'center',
         //    baseCls: '',
         //    itemId: 'NotificationBtn',
         //    contextMenu: undefined,
         //    bodyStyle: {
         //        'background-color': 'transparent',
         //        'border-color': 'transparent',
         //        'border-style': 'transparent'
         //    },
         //    listeners: {
         //        mouseover: 'onNotificationHover'
         //    }
         //}
        //{
        //    xtype: 'combo',
        //    width: 120,
        //    ui: 'country',
        //    bind: {
        //        store: '{languageStore}'
        //    },
        //    valueField: 'name',
        //    displayField: 'displayName',
        //    fieldLabel: '',
        //    iconClsField: 'icon',
        //    editable: false,
        //    typeAhead: false,
        //    listeners: {
        //        collapse: function(combo) {
        //            return false;
        //        }
        //    },
        //    listConfig: {
        //        minWidth: 200,
        //        getInnerTpl: function() {
        //            var tpl = '<tpl for=".">' + //'<div class="x-combo-list-item">' +
        //                '<p class="{icon}" style="width:16px; height:11px; display: inline-block;"/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<p style="display: inline-block;height:11px;">&nbsp;&nbsp;{displayName}</p>' +
        //                '</tpl>';
        //            return tpl;
        //        }
        //    }
        //}
    ]
});
