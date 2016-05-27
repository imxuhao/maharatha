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
    displayNegAmtInBrackets:true,
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