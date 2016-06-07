
Ext.define('Chaching.view.roles.RolesForm',{
    extend: 'Chaching.view.common.form.ChachingFormPanel',
    alias: ['host.roles.create', 'host.roles.edit'],

    requires: [
        'Chaching.view.roles.RolesFormController'       
    ],

    controller: 'roles-rolesform',
  

    name: 'Administration.Roles',
    openInPopupWindow: true,
    hideDefaultButtons: false,
    layout: 'vbox',
    defaults: {
        bodyStyle: { 'background-color': 'trasparent' },
        labelAlign: 'top',
        blankText: app.localize('MandatoryToolTipText')
    },
    defaultFocus: 'textfield#displayName',
    items: {
        xtype: 'tabpanel',
        width:'100%',
        //region: 'center',      
        ui: 'dashboard',       
        items: [
        {
            title: 'Roles',                            
            items: [{
                xtype: 'hiddenfield',
                name: 'id',
                value:0
            }, {
                xtype: 'textfield',
                name: 'displayName',
                itemId: 'displayName',
                fieldLabel: app.localize('RoleName'),
                width: '100%',
                ui:'fieldLabelTop',
                emptyText: app.localize('RoleName')
            },
            ,{
                xtype: 'checkbox',
                boxLabel: app.localize('Default'),
                name: 'default',
                labelAlign: 'right',
                inputValue: true,
                checked: true,
                boxLabelCls: 'checkboxLabel'
            },
            {
                xtype: 'label',             
                text: app.localize('DefaultRole_Description').initCap(),
                width: '100%',
                reference: "lablepassword"
            },
            ],          
        },
        {
            title:'Permissions',
            xtype: 'treepanel',
            name: 'permissions',
            store: 'roles.RolesTreeStore',
            rootVisible: true,
            width: 400,
            height: 500
        }
        
        ]
    }

});
