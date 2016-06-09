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
            xtype: 'combobox',
            store: new Chaching.store.utilities.autofill.SubAccountsStore(),
            valueField: valueField,
            displayField: displayField,
            queryMode: 'remote',
            minChars: 2,
            useDisplayFieldToSearch: true,
            listConfig: me.comboListConfig,
            emptyText: app.localize('SearchText')
        };
    },
    getTextField: function (emptyText) {
        return {
            xtype: 'textfield',
            emptyText: emptyText
        };

    }
});