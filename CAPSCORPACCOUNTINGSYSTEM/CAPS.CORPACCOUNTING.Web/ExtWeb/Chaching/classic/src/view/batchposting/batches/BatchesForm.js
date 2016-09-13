
Ext.define('Chaching.view.batchposting.batches.BatchesForm', {
    extend: 'Chaching.view.common.form.ChachingFormPanel',
    alias: ['widget.batchposting.batches.create', 'widget.batchposting.batches.edit'],
    requires: [
        'Chaching.view.batchposting.batches.BatchesFormController'
    ],

    controller: 'batchposting-batches-batchesform',
    name: 'batch',
    openInPopupWindow: false,
    hideDefaultButtons: false,
    layout: 'column',
    autoScroll: true,
    border: false,
    showFormTitle: true,
    //titleConfig: {
    //    title: abp.localization.localize("CreatingNewBatch").initCap()
    //},
    modulePermissions: {
        read: abp.auth.isGranted('Pages.BatchPosting.Batches'),
        create: abp.auth.isGranted('Pages.BatchPosting.Batches.Create'),
        edit: abp.auth.isGranted('Pages.BatchPosting.Batches.Edit'),
        destroy: abp.auth.isGranted('Pages.BatchPosting.Batches.Delete')
    },
    //bodyStyle: { 'background-color': '#F3F5F9' },   
    items: [{
        xtype: 'hiddenfield',
        name: 'batchId',
        value: 0
    }, {
        columnWidth: .5,
        padding: '20 10 0 20',
        //bodyStyle: { 'background-color': '#F3F5F9' },
        defaults: {
           // labelAlign: 'top',
            blankText: app.localize('MandatoryToolTipText')
        },
        items: [{
            xtype: 'textfield',
            name: 'description',
            itemId: 'batchName',
            allowBlank: false,
            fieldLabel: app.localize('BatchName').initCap(),
            width: '100%',
            ui: 'fieldLabelTop',
            emptyText: app.localize('MandatoryField')
        }, {
            xtype: 'textfield',
            name: 'batchOwner',
            itemId: 'batchOwner',
            readOnly: true,
            fieldLabel: app.localize('BatchOwner').initCap(),
            width: '100%',
            ui: 'fieldLabelTop'
        }, {
            xtype: 'datefield',
            name: 'postingDate',
            itemId: 'postingDate',
            fieldLabel: app.localize('PostingDate').initCap(),
            width: '100%',
            ui: 'fieldLabelTop',
            emptyText: app.localize('ToolTipPostingDate')
        }, {
            xtype: 'textfield',
            name: 'controlTotal',
            itemId: 'controlTotal',
            fieldLabel: app.localize('ControlTotal').initCap(),
            width: '100%',
            ui: 'fieldLabelTop',
            emptyText: app.localize('ToolTipControlTotal')
        }]
    }, {
        columnWidth: .5,
        padding: '20 10 0 20',
        //bodyStyle: { 'background-color': '#F3F5F9' },
        defaults: {
           // labelAlign: 'top',
            blankText: app.localize('MandatoryToolTipText')
        },
        items: [ {
            xtype: 'textfield',
            name: 'batchAmount',
            itemId: 'batchAmount',
            readOnly : true,
            fieldLabel: app.localize('BatchAmount').initCap(),
            width: '100%',
            ui: 'fieldLabelTop'
        }, {
            xtype: 'combobox',
            name: 'typeOfBatchId',
            fieldLabel: app.localize('Module').initCap(),
            width: '100%',
            ui: 'fieldLabelTop',
            emptyText: app.localize('SelectOption'),
            displayField: 'typeOfBatch',
            valueField: 'typeOfBatchId',
            queryMode : 'local',
            store: Ext.create('Chaching.store.utilities.TypeOfBatchStore')
        }, {
            xtype: 'checkbox',
            boxLabel: app.localize('RetainBatch'),
            name: 'isRetained',
            labelAlign: 'right',
            inputValue: false,
            checked: false,
            boxLabelCls: 'checkboxLabel'
        }, {
            xtype: 'checkbox',
            boxLabel: app.localize('CommunityBatch'),
            name: 'isUniversal',
            labelAlign: 'right',
            inputValue: false,
            checked: false,
            boxLabelCls: 'checkboxLabel'
        }, {
            xtype: 'checkbox',
            boxLabel: app.localize('FinalizedtoPost'),
            name: 'isBatchFinalized',
            labelAlign: 'right',
            inputValue: false,
            checked: false,
            boxLabelCls: 'checkboxLabel'
        }]
    }]
});
