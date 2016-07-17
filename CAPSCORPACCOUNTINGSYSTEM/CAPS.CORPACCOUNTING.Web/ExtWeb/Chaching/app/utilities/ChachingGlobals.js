Ext.define('Chaching.utilities.ChachingGlobals', {
    alternateClassName : ['ChachingGlobals'],
    singleton: true,
    loggedInUserInfo: {
        defaultOrganizationId: null,
        emailAddress: null,
        userId: null,
        name: null,
        profilePictureId: null,
        surname: null,
        userName: null,
        userOrganizationId: null,
        gotoMyAccount: false
    },
    defaultGridPageSize : 10,
    defaultDateFormat: 'MM/DD/YYYY',
    defaultDateTimeFormat: 'MM/DD/YYYY hh:mm a',
    defaultDateTimeSecFormat: 'MM/DD/YYYY hh:mm:ss a',
    defaultDateTimeSecFormatWithoutAmPm: 'MM/DD/YYYY hh:mm:ss',
    usersDefaultGridViewSettings: null,
    mandatoryFlag: '&nbsp;<font color=red>*</font>',
    defaultExtDateFieldFormat: 'm/d/Y',
    comboListConfig: {
        minWidth: 250,
        width: 250
    },
    displayNegAmtInBrackets: true,
    splitGroupCls: ['split-group1', 'split-group2', 'split-group3', 'split-group4', 'split-group5', 'split-group6', 'split-group7', 'split-group8'],
    getSubAccountCombo: function(valueField, displayField, isFilter) {
        var me = this;
        var beforeQuery = (isFilter ? 'emptyFunction' : 'onBeforeSubAccountQuery');
        return {
            xtype: 'chachingcombobox',
            store: new Chaching.store.utilities.autofill.SubAccountsStore(),
            valueField: valueField,
            displayField: displayField,
            queryMode: 'remote',
            minChars: 2,
            useDisplayFieldToSearch: true,
            modulePermissions: {
                read: abp.auth.isGranted('Pages.Financials.Accounts.SubAccounts'),
                create: abp.auth.isGranted('Pages.Financials.Accounts.SubAccounts.Create'),
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
            isTwoEntityPicker: false,
            listeners: {
                beforequery: beforeQuery
            }
        };
    },
    getTextField: function(emptyText) {
        return {
            xtype: 'textfield',
            emptyText: emptyText
        };

    },
    currencyCodesAndSymbols: [
         { code: 'ALL', symbol: 'Lek' },{ code: 'AFN', symbol: '؋' },{ code: 'ARS', symbol: '$' },{ code: 'AWG', symbol: 'ƒ' },
         { code: 'AUD', symbol: '$' },{ code: 'AZN', symbol: '₼' },{ code: 'BSD', symbol: '$' },{ code: 'BBD', symbol: '$' },
         { code: 'BYR', symbol: 'p.' },{ code: 'BZD', symbol: 'BZ$' },{ code: 'BMD', symbol: '$' },{ code: 'BOB', symbol: '$b' },
         { code: 'BAM', symbol: 'KM' },{ code: 'BWP', symbol: 'P' },{ code: 'BGN', symbol: 'лв' },{ code: 'BRL', symbol: 'R$' },
         { code: 'BND', symbol: '$' },{ code: 'KHR', symbol: '៛' },{ code: 'CAD', symbol: '$' },{ code: 'LRD', symbol: '$' },
         { code: 'KYD', symbol: '$' },{ code: 'CLP', symbol: '$' },{ code: 'CNY', symbol: '¥' },{ code: 'COP', symbol: '$' },
         { code: 'CRC', symbol: '₡' },{ code: 'HRK', symbol: 'kn' },{ code: 'CUP', symbol: '₱' },{ code: 'CZK', symbol: 'Kč' },
         { code: 'DKK', symbol: 'kr' },{ code: 'DOP', symbol: 'RD$' },{ code: 'XCD', symbol: '$' },{ code: 'EGP', symbol: '£' },
         { code: 'SVC', symbol: '$' },{ code: 'EEK', symbol: 'kr' },{ code: 'FKP', symbol: '£' },{ code: 'EUR', symbol: '€' },
         { code: 'FJD', symbol: '$' },{ code: 'GEL', symbol: '₾' },{ code: 'GHC', symbol: '¢' },{ code: 'GIP', symbol: '£' },
         { code: 'GTQ', symbol: 'Q' },{ code: 'GGP', symbol: '£' },{ code: 'GYD', symbol: '$' },{ code: 'HNL', symbol: 'L' },
         { code: 'HKD', symbol: '$' },{ code: 'HUF', symbol: 'Ft' },{ code: 'ISK', symbol: 'kr' },{ code: 'INR', symbol: '₹' },
         { code: 'IDR', symbol: 'Rp' },{ code: 'IRR', symbol: '﷼' },{ code: 'IMP', symbol: '£' },{ code: 'ILS', symbol: '₪' },
         { code: 'JMD', symbol: 'J$' },{ code: 'JPY', symbol: '¥' },{ code: 'JEP', symbol: '£' },{ code: 'KZT', symbol: 'лв' },
         { code: 'KPW', symbol: '₩' },{ code: 'KRW', symbol: '₩' },{ code: 'KGS', symbol: 'лв' },{ code: 'LAK', symbol: '₭' },
         { code: 'LVL', symbol: 'Ls' },{ code: 'LBP', symbol: '£' },{ code: 'LRD', symbol: '$' },{ code: 'LTL', symbol: 'Lt' },
         { code: 'MKD', symbol: 'ден' },{ code: 'MYR', symbol: 'RM' },{ code: 'MUR', symbol: '₨' },{ code: 'MXN', symbol: '$' },
         { code: 'MNT', symbol: '₮' },{ code: 'MZN', symbol: 'MT' },{ code: 'NAD', symbol: '$' },{ code: 'NPR', symbol: '₨' },
         { code: 'ANG', symbol: 'ƒ' },{ code: 'NZD', symbol: '$' },{ code: 'NIO', symbol: 'C$' },{ code: 'NGN', symbol: '₦' },
         { code: 'NOK', symbol: 'kr' },{ code: 'OMR', symbol: '﷼' },{ code: 'PKR', symbol: '₨' },{ code: 'PAB', symbol: 'B/.' },
         { code: 'PYG', symbol: 'Gs' },{ code: 'PEN', symbol: 'S/.' },{ code: 'PHP', symbol: '₱' },{ code: 'PLN', symbol: 'zł' },
         { code: 'QAR', symbol: '﷼' },{ code: 'RON', symbol: 'lei' },{ code: 'RUB', symbol: '₽' },{ code: 'SHP', symbol: '£' },
         { code: 'SAR', symbol: '﷼' },{ code: 'RSD', symbol: 'Дин.' },{ code: 'SCR', symbol: '₨' },{ code: 'SGD', symbol: '$' },
         { code: 'SBD', symbol: '$' },{ code: 'SOS', symbol: 'S' },{ code: 'ZAR', symbol: 'S' },{ code: 'LKR', symbol: '₨' },
         { code: 'SEK', symbol: 'kr' },{ code: 'CHF', symbol: 'CHF' },{ code: 'SRD', symbol: '$' },{ code: 'SYP', symbol: '£' },
         { code: 'TWD', symbol: 'NT$' },{ code: 'THB', symbol: '฿' },{ code: 'TTD', symbol: 'TT$' },{ code: 'TRL', symbol: '₺' },
         { code: 'TVD', symbol: '$' },{ code: 'UAH', symbol: '₴' },{ code: 'GBP', symbol: '£' },{ code: 'USD', symbol: '$' },
         { code: 'UYU', symbol: '$U' },{ code: 'UZS', symbol: 'лв' },{ code: 'VEF', symbol: 'Bs' },{ code: 'VND', symbol: '₫' },
         { code: 'YER', symbol: '﷼' },{ code: 'ZWD', symbol: 'Z$' }
     ]
});