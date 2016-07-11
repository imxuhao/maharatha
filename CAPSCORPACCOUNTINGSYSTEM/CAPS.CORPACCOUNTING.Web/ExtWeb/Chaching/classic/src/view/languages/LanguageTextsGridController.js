Ext.define('Chaching.view.languages.LanguageTextsGridController', {
    extend: 'Chaching.view.common.grid.ChachingGridPanelController',
    alias: 'controller.languages-languagetextsgrid',
    doAfterCreateAction: function (createMode, formView, isEdit) {      
        if (formView && isEdit) {
            var form = formView.down('form').getForm();
            form.findField('name').setReadOnly(true);
        }
    },
    onLanguageCellClick: function (view, td, cellIndex, record, tr, rowIndex, e, eOpts) {
        var me = this;
        // Open a window on click of Edit in Grid
        var grid = me.getView();
        var columnName = grid.columns[cellIndex].dataIndex;
        if (columnName == 'isEdit') {
            //var formView = Ext.create({
            //    xtype: 'Languagetexts.editView'
            //});
            var formView = Ext.widget('Languagetexts.editView');
            formView.parentGrid = grid;
            formView.show();
            var languageForm = grid.up('form'),
                basicForm = languageForm.getForm(),
                languageRecord = basicForm.getValues(),
                baseLaguage = basicForm.findField('baseLanguage').getRawValue(),
                targetLanguage = basicForm.findField('targetLanguage').getRawValue();
            var newRecord = {
                sourceName: languageRecord.source,
                baseValue: record.get('baseValue'),
                value: record.get('targetValue'),
                key: record.get('key'),
                hiddenKey: record.get('key'),
                targetLanguage: targetLanguage,
                hiddenTargetLanguage: basicForm.findField('targetLanguage').getValue(),
                rowNumber: rowIndex
            }
            var languageEditForm = formView.down('form').getForm();
            languageEditForm.setValues(newRecord);
            languageEditForm.findField('sourceLanguage').setHtml('<i class="famfamfam-flag famfamfam-flag-' + languageRecord.baseLanguage + '">' + baseLaguage + '</i>');
            var previousButton = formView.query("#BtnPrevious")[0];
            if (rowIndex == 0) {
                previousButton.setDisabled(true);
            }
        }
        
    },
    doBeforeInlineAddUpdate: function (record) {
        var me = this,
            view = me.getView(),
            gridStore = view.getStore(),
            storeProxy = gridStore.getProxy();
        record.set('languageName', storeProxy.extraParams.TargetLanguageName);
        record.set('value', record.get('targetValue'));
        return record;
    },

});
