Ext.define('Chaching.utilities.ChachingGlobals', {
    singleton: true,
    loggedInUserInfo: {
        defaultOrganizationId:null,
        emailAddress: null,
        userId: null,
        name: null,
        profilePictureId: null,
        surname: null,
        userName:null,
        userOrganizationId: null,
        gotoMyAccount:false
    },
    defaultDateFormat: 'MM/DD/YYYY',
    defaultDateTimeFormat: 'MM/DD/YYYY hh:mm a',
    defaultDateTimeSecFormat: 'MM/DD/YYYY hh:mm:ss a',
    usersDefaultGridViewSettings: null,
    mandatoryFlag: '&nbsp;<font color=red>*</font>',
    defaultExtDateFieldFormat: 'm/d/Y',
    comboListConfig: {
        minWidth: 250,
        width: 250
    },
    displayNegAmtInBrackets: true,
    splitGroupCls: ['split-group1', 'split-group2', 'split-group3', 'split-group4', 'split-group5', 'split-group6', 'split-group7', 'split-group8'],
    getSubAccountCombo: function (valueField, displayField) {
        var me = this;
        return {
            //xtype: 'combobox',
            //store: new Chaching.store.utilities.autofill.SubAccountsStore(),
            //valueField: valueField,
            //displayField: displayField,
            //queryMode: 'remote',
            //minChars: 2,
            //useDisplayFieldToSearch: true,
            //listConfig: me.comboListConfig,
            //emptyText: app.localize('SearchText')


            xtype: 'chachingcombobox',
            store: new Chaching.store.utilities.autofill.SubAccountsStore(),
            valueField: valueField,
            displayField: displayField,
            queryMode: 'remote',
            minChars: 2,
            useDisplayFieldToSearch: true,
            modulePermissions: {
                read: abp.auth.isGranted('Pages.Financials.Accounts.SubAccounts'),
                create: false,//abp.auth.isGranted('Pages.Financials.Accounts.SubAccounts.Create'),
                edit: abp.auth.isGranted('Pages.Financials.Accounts.SubAccounts.Edit'),
                destroy: abp.auth.isGranted('Pages.Financials.Accounts.SubAccounts.Delete')
            },
            primaryEntityCrudApi: {
                read: abp.appPath + 'api/services/app/list/GetSubAccountList',
                create: abp.appPath + 'api/services/app/subAccountUnit/CreateSubAccountUnit',
                update: abp.appPath + 'api/services/app/subAccountUnit/UpdateSubAccountUnit',
                destroy: abp.appPath + 'api/services/app/subAccountUnit/DeleteBankAccountUnit'
            },
            createEditEntityType: 'financials.accounts.subaccounts',
            createEditEntityGridController: 'financials-accounts-subaccountsgrid',
            entityType: 'SubAccount',
            isTwoEntityPicker: false
        };
    },
    getTextField: function (emptyText) {
        return {
            xtype: 'textfield',
            emptyText: emptyText
        };

    }
});