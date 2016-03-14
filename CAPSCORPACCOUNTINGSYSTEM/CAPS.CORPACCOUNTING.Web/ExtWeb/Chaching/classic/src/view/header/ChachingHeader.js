
Ext.define('Chaching.view.header.ChachingHeader',{
    extend: 'Ext.toolbar.Toolbar',

    requires: [
        'Chaching.view.header.ChachingHeaderController',
        'Chaching.view.header.ChachingHeaderModel'
    ],
    alias:'widget.chachingheader',
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
    items:[
    {
        xtype: 'image',
        height: 30,
        width:110,
        src: abp.appPath + 'Content/images/capslogo.png',
        margin: '2px'
    }, {
        xtype: 'button',
        text: '',
        scale: 'medium',
        iconCls: 'x-fa fa-list',
        iconAlign: 'right',
        width:120,
        baseCls:'',
        bodyStyle: {
            'background-color': 'transparent',
            'border-color': 'transparent',
            'border-style': 'transparent'
        },
        listeners: {
            click:'onToggleClick'
        }
    }]
});
