
Ext.define('Chaching.view.projects.projectmaintenance.LineNumbersForm',{
    extend: 'Chaching.view.common.form.ChachingFormPanel',
    alias: ['widget.projects.projectmaintenance.linenumbers.create', 'widget.projects.projectmaintenance.linenumbers.edit'],
    //requires: [
    //    'Chaching.view.projects.projectmaintenance.LineNumbersFormController'       
    //],

    //controller: 'projects-projectmaintenance-linenumbersform',   

    name: 'accounts',
    openInPopupWindow: false,
    hideDefaultButtons: false,
    layout: 'column',
    autoScroll: true,
    border: false,
    showFormTitle: true,
    displayDefaultButtonsCenter: true,
    popupWndSize: {
        height: '70%',
        width: '70%'
    },
    modulePermissions: {
        read: abp.auth.isGranted('Pages.Financials.Accounts.Accounts'),
        create: abp.auth.isGranted('Pages.Financials.Accounts.Accounts.Create'),
        edit: abp.auth.isGranted('Pages.Financials.Accounts.Accounts.Edit'),
        destroy: abp.auth.isGranted('Pages.Financials.Accounts.Accounts.Delete')
    },
    items: [{
        xtype: 'hiddenfield',
        name: 'accountId',
        value: 0
    }, {
        xtype: 'hiddenfield',
        name: 'chartOfAccountId',
        value: 0
    }, {
        columnWidth: .5,
        padding: '20 10 0 20',
        //defaults: {
        //    labelAlign: 'top',
        //    blankText: app.localize('MandatoryToolTipText')
        //},
        items: [{
            xtype: 'textfield',
            name: 'accountNumber',
            itemId: 'accountNumber',
            allowBlank: false,
            fieldLabel: app.localize('LineNumber').initCap(),
            width: '100%',
            ui: 'fieldLabelTop',
            emptyText: app.localize('MandatoryField')
        }, {
            xtype: 'combobox',
            name: 'typeOfAccountId',
            fieldLabel: app.localize('Classification').initCap(),
            width: '100%',            
            ui: 'fieldLabelTop',          
            displayField: 'typeOfAccount',
            valueField: 'typeOfAccountId',
            queryMode : 'local',
            bind: {
                store: '{typeOfAccountList}'
            }
        }
        , {

            xtype: 'chachingcombobox',
            store: new Chaching.store.utilities.autofill.RollupAccountListStore(),
            fieldLabel: app.localize('RollUpAccount'),
            ui: 'fieldLabelTop',
            width: '100%',
            name: 'rollupAccountId',
            valueField: 'accountId',
            displayField: 'accountNumber',
            queryMode: 'remote',
            minChars: 2,
            useDisplayFieldToSearch: true,
            modulePermissions: {
                read: abp.auth.isGranted('Pages.Financials.Accounts.Accounts'),
                create: abp.auth.isGranted('Pages.Financials.Accounts.Accounts.Create'),
                edit: abp.auth.isGranted('Pages.Financials.Accounts.Accounts.Edit'),
                destroy: abp.auth.isGranted('Pages.Financials.Accounts.Accounts.Delete')
            },
            primaryEntityCrudApi: {
                read: abp.appPath + 'api/services/app/accountUnit/GetRollupAccountsList',
                create: abp.appPath + 'api/services/app/accountUnit/CreateAccountUnit',
                update: abp.appPath + 'api/services/app/accountUnit/UpdateAccountUnit',
                destroy: abp.appPath + 'api/services/app/accountUnit/DeleteAccountUnit'
            },
            createEditEntityType: 'financials.accounts.accounts',
            createEditEntityGridController: 'financials-accounts-accountsgrid',
            entityType: 'Account',
            isTwoEntityPicker: false
        }
        , {
            xtype: 'combobox',
            name: 'typeOfCurrencyId',
            fieldLabel: app.localize('Currency').initCap(),
            width: '100%',
            ui: 'fieldLabelTop',
            displayField: 'typeOfCurrency',
            valueField: 'typeOfCurrencyId',
            queryMode: 'local',
            bind: {
                store: '{typeOfCurrencyList}'
            }
        }        
        ]
    }, {
        columnWidth: .5,
        padding: '20 10 0 20',
        //defaults: {
        //    labelAlign: 'top',
        //    blankText: app.localize('MandatoryToolTipText')
        //},
        items: [{
            xtype: 'textfield',
            name: 'caption',
            itemId: 'caption',
            allowBlank: false,
            fieldLabel: app.localize('Caption').initCap(),
            width: '100%',
            ui: 'fieldLabelTop',
            emptyText: app.localize('MandatoryField')
        }, {
            xtype: 'combobox',
            name: 'typeofConsolidationId',
            fieldLabel: app.localize('Consolidation').initCap(),
            width: '100%',
            ui: 'fieldLabelTop',            
            displayField: 'typeofConsolidation',
            valueField: 'typeofConsolidationId',
            queryMode: 'local',
            bind: {
                store: '{typeofConsolidationList}'
            }
        }, {
            xtype: 'chachingcombobox',
            store: new Chaching.store.utilities.autofill.DivisionListStore(),
            fieldLabel: app.localize('RollUpDivision'),
            ui: 'fieldLabelTop',
            width: '100%',
            name: 'rollupJobId',
            valueField: 'rollupJobId',
            displayField: 'jobNumber',
            queryMode: 'remote',
            minChars: 2,
            modulePermissions: {
                read: abp.auth.isGranted('Pages.Financials.Accounts.Divisions'),
                create: abp.auth.isGranted('Pages.Financials.Accounts.Divisions.Create'),
                edit: abp.auth.isGranted('Pages.Financials.Accounts.Divisions.Edit'),
                destroy: abp.auth.isGranted('Pages.Financials.Accounts.Divisions.Delete')
            },
            primaryEntityCrudApi: {
                read: abp.appPath + 'api/services/app/jobUnit/GetDivisionList',
                create: abp.appPath + 'api/services/app/divisionUnit/CreateDivisionUnit',
                update: abp.appPath + 'api/services/app/divisionUnit/UpdateDivisionUnit',
                destroy: abp.appPath + 'api/services/app/divisionUnit/DeleteDivisionUnit'
            },
            createEditEntityType: 'financials.accounts.divisions',
            createEditEntityGridController: 'financials-accounts-divisionsgrid',
            entityType: 'Division',
            useDisplayFieldToSearch: true,
            identificationKey: 'isDivision'

        }
        ,
        {
            xtype: 'checkbox',
            boxLabel: app.localize('JournalsAllowed'),
            name: 'isEnterable',
            labelAlign: 'right',
            inputValue: true,
            checked: false,
            boxLabelCls: 'checkboxLabel'
        }
        ]
    }]
});
