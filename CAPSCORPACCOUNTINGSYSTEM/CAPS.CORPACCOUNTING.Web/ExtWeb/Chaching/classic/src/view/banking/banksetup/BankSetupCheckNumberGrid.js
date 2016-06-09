
Ext.define('Chaching.view.banking.banksetup.BankSetupCheckNumberGrid', {
    extend: 'Chaching.view.common.grid.ChachingGridPanel',
    xtype: 'widget.banking.banksetup.checknumbergrid',
    modulePermissions: {
        read: abp.auth.isGranted('Pages.Banking.BankSetup'),
        create: abp.auth.isGranted('Pages.Banking.BankSetup.Create'),
        edit: abp.auth.isGranted('Pages.Banking.BankSetup.Edit'),
        destroy: abp.auth.isGranted('Pages.Banking.BankSetup.Delete')
    },
    padding: 5,
    itemId: 'checkNumberGrid',
    controller: 'banking.banksetup.checknumbergrid',
    store: 'banking.banksetup.BankCheckNumberStore',
    headerButtonsConfig: [{
        xtype: 'displayfield',
        value: app.localize('CheckNumber'),
        ui: 'headerTitle'
    }, '->', {
        xtype: 'button',
        scale: 'small',
        ui: 'actionButton',
        action: 'create',
        text: abp.localization.localize("Add").toUpperCase(),
        tooltip: app.localize('AddCheckNumber'),
        checkPermission: true,
        iconCls: 'fa fa-plus',
        //routeName: '',
        iconAlign: 'left'
    }],
    requireExport: false,
    requireMultiSearch: false,
    requireMultisort: false,
    isEditable: true,
    editingMode: 'cell',
    columnLines: true,
    multiColumnSort: false,
    createNewMode: 'inline',
    isSubMenuItemTab: false,
    showPagingToolbar: false,
    autoScroll: true,
    columns: [{
        text: 'Bank Account Id',
        dataIndex: 'bankAccountId',
        hidden: true,
        hideable : false
    }, {
        text: 'bankAccountPaymentRangeId',
        dataIndex: 'bankAccountPaymentRangeId',
        hidden: true,
        hideable: false
    }, {
        text: 'Organization Unit Id',
        dataIndex: 'organizationUnitId',
        hidden: true,
        hideable: false
    }, {
        text: app.localize('StartingCheckNumber').initCap(),
        dataIndex: 'startingPaymentNumber',
        sortable: false,
        groupable: false,
        width: '15%',
        editor: {
            xtype: 'textfield',
            allowBlank : false
        }
    }, {
        text: app.localize('EndingCheckNumber').initCap(),
        dataIndex: 'endingPaymentNumber',
        sortable: false,
        groupable: false,
        width: '15%',
        editor: {
            xtype: 'textfield',
            allowBlank : false
        }
    }]
});
