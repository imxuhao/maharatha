
Ext.define('Chaching.view.linkedaccounts.LinkedAccountsForm', {
    extend: 'Chaching.view.common.form.ChachingFormPanel',
  

    alias: ['host.linkedaccounts.create', 'host.linkedaccounts.edit'],
    requires: [
        'Chaching.view.linkedaccounts.LinkedAccountsFormController',
        'Chaching.view.linkedaccounts.LinkedAccountsFormModel'
    ],

    controller: 'linkedaccounts-linkedaccountsform',
    viewModel: {
        type: 'linkedaccounts-linkedaccountsform'
    },
    name: 'LinkedAccounts',
    openInPopupWindow: true,
    hideDefaultButtons: false,
    layout: 'vbox',
    defaults: {
        bodyStyle: { 'background-color': 'trasparent' },
        labelAlign: 'top'
    },
    //defaultFocus: 'textfield#displayName',
 
    items: [{
        xtype: 'tabpanel',
       
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
            iconCls: 'icon-home',
            items: [{
                xtype: 'treelist',
                title: 'Simple Tree',             
                expanded: true,
                children: [
                    { text: 'detention', leaf: true },
                    { text: 'homework', expanded: true, children: [
                        { text: 'book report', leaf: true },
                        { text: 'algebra', leaf: true}
                    ] },
                    { text: 'buy lottery tickets', leaf: true }
                ],
                rootVisible: false,
            }

            ]
        }
        ]
    }
    ]
    
    
});
