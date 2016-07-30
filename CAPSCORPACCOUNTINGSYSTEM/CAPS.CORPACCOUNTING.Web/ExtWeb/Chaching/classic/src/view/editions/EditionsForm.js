
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
    layout: 'fit',
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
                
            title: app.localize('Features'),
            padding: '0 0 0 10',
            iconCls: 'icon-home',
            layout: 'fit',
            xtype: 'treepanel',
            name: 'features',
            itemId: 'features',
            cls: 'chaching-grid',
            store: 'editions.EditionsTreeStore',
            rootVisible: false,
            width: '100%',
            alwaysReload: true,
            hideHeaders: true,
            columns: [{
                xtype: 'treecolumn',
                dataIndex: 'displayName',
                flex: 1
            }
            ],
            plugins: [
                Ext.create('Ext.grid.plugin.CellEditing', {
                    clicksToEdit: 2
                })
            ]

        }
        ]
    }
    ]
    
    
});
