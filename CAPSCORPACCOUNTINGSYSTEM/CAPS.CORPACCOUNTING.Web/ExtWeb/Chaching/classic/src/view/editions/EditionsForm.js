
Ext.define('Chaching.view.editions.EditionsForm',{
    extend: 'Chaching.view.common.form.ChachingFormPanel',
  

    alias:['host.edition.create','host.edition.edit'],
    requires: [
        'Chaching.view.editions.EditionsFormController'
    ],

    controller: 'editions-editionsform',
    name: 'Editions',
    openInPopupWindow: true,
    hideDefaultButtons: false,
    layout: 'vbox',
    defaults: {
        bodyStyle: { 'background-color': 'trasparent' },
        labelAlign: 'top'
    },
    defaultFocus: 'textfield#displayName',
 
    items: [{
        xtype: 'tabpanel',
        region: 'center',      
        ui: 'dashboard',       
        items: [
        {
            title: 'Editions',                
            iconCls: 'icon-grid',
            items: [{
                xtype: 'hiddenfield',
                name: 'id',
                value:0
            }, {
                xtype: 'textfield',
                name: 'displayName',
                itemId: 'displayName',
                fieldLabel: app.localize('Name'),
                width: '100%',
                ui:'fieldLabelTop',
                emptyText: app.localize('Edition Name')
            }
            ]
        },
        {
            title: 'Features',
            ui: 'dashboard',          
            iconCls: 'icon-home'
        }
        ]
    }
    ]
    
    
});
