Ext.define('Chaching.view.financials.accounts.ClassificationsGrid', {
    extend: 'Chaching.view.common.grid.ChachingGridPanel',
    xtype: 'financials.accounts.classifications',
    require: ['Chaching.view.financials.accounts.ClassificationsGridController'],
    controller: 'financials-accounts-classificationsgrid',
    modulePermissions: {
        read: abp.auth.isGranted('Pages.Financials.Accounts.TypeofClassification'),
        create: abp.auth.isGranted('Pages.Financials.Accounts.TypeofClassification.Create'),
        edit: abp.auth.isGranted('Pages.Financials.Accounts.TypeofClassification.Edit'),
        destroy: abp.auth.isGranted('Pages.Financials.Accounts.TypeofClassification.Delete')
    },
    store: 'financials.accounts.ClassificationsStore',
    padding: 5,
    gridId: 33,
    name: 'Financials.Accounts.TypeofClassification',
    headerButtonsConfig: [
      {
          xtype: 'displayfield',
          value: abp.localization.localize("Classifications"),
          ui: 'headerTitle'
      }, '->', {
          xtype: 'button',
          scale: 'small',
          ui: 'actionButton',
          action: 'create',
          text: abp.localization.localize("Add").toUpperCase(),
          tooltip: app.localize('CreateNewClassification'),
          checkPermission: true,
          iconCls: 'fa fa-plus',
          iconAlign: 'left'
      }],
    requireMultiSearch: true,
    requireMultisort: true,
    isEditable: true,
    editingMode: 'row',
    columnLines: true,
    multiColumnSort: true,
    editWndTitleConfig: {
        title: app.localize('EditClassification'),
        iconCls: 'fa fa-pencil'
    },
    createWndTitleConfig: {
        title: app.localize('CreateNewClassification'),
        iconCls: 'fa fa-plus'
    },
    viewWndTitleConfig: {
        title: app.localize('ViewClassification'),
        iconCls: 'fa fa-th'
    },
    createNewMode: 'inline',
    columns: [
        {
            xtype: 'gridcolumn',
            name: 'classificationId',
            dataIndex: 'classificationId',
            sortable: true,
            groupable: true,
            flex: 1,
            allowBlank: false,
            hidden:true,
            text: app.localize('ClassificationId')
        }, {
            xtype: 'gridcolumn',
            name: 'description',
            dataIndex: 'description',
            sortable: true,
            groupable: true,
            flex: 1,
            allowBlank: false,
            text: app.localize('Description'),
            filterField: {
                xtype: 'textfield',
                width: '100%'
            }, editor: {
                xtype: 'textfield'
            }
        }, {
            xtype: 'gridcolumn',
            name: 'caption',
            dataIndex: 'caption',
            sortable: true,
            groupable: true,
            flex: 1,
            allowBlank: false,
            text: app.localize('Caption'),
            filterField: {
                xtype: 'textfield',
                width: '100%'
            }, editor: {
                xtype: 'textfield'
            }
        }
    ]
});