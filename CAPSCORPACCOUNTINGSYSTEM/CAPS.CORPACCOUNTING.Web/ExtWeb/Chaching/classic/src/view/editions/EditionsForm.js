
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
        ui: 'formTabPanels',
        //ui: 'dashboard',
        items: [
        {
            title: 'Editions',
            padding: '0 0 0 10',
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
            padding: '0 0 0 10',        
            iconCls: 'icon-home',
            items: [{
                xtype: 'textfield',
                name: 'displayName',
                itemId: 'displayFeatures',
                fieldLabel: app.localize('Name'),
                width: '100%',
                ui: 'fieldLabelTop',
                emptyText: app.localize('Edition Name')
            }
            ]
        }
        ]
    }
    ]
    
    
});
