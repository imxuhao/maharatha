
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
    listeners: {
        resize: 'onRolesFormResize'
    },
    items: {
        xtype: 'tabpanel',
        width:'100%',
        ui: 'formTabPanels',
        //ui: 'dashboard',       
        items: [
        {
            title: app.localize('Roles'),                            
            padding: '0 0 0 10',
            iconCls: 'fa fa-object-group',
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
                name: 'isDefault',
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
            }
            ]         
        },
        {
            title: app.localize('Permissions'),
            padding: '0 0 0 10',
            layout : 'fit',
            xtype: 'treepanel',
            name: 'permissions',
            itemId: 'permissionsItemId',
            cls: 'chaching-grid',
            iconCls:'fa fa-ticket',
            //store: 'roles.RolesTreeStore',
            store: new Chaching.store.roles.RolesTreeStore(),
            rootVisible: false,
            width:'100%',
            alwaysReload: false,
            scrollable: true,
            hideHeaders: true,
            columns: [{
                xtype: 'treecolumn',
                dataIndex: 'displayName',
                flex: 1
            }
            ],
            listeners: {
                checkchange: 'onTreeCheckChange'
            }
        }
        
        ]
    }

});
