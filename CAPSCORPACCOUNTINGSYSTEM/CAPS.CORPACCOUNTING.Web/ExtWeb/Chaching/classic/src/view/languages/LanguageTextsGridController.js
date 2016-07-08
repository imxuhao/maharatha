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
            //me.createNewRecord('Languagetexts', 'popup', true, 'Edit text', record);
            var formView = Ext.create({
                xtype: 'Languagetexts.editView'
            });
            formView.show();
            var form = grid.up('form');
            //var basicForm = form.getForm();
            //var baseLanguage = basicForm.findField('baseLanguage');
            //var targetLanguage = basicForm.findField('targetLanguage');
           
            form.loadRecord(record);

        }
        
    },
    doAfterCreateAction: function (createNewMode, form, isEdit, record) {

        var me = this;
        debugger;
        // Open a window on click of Edit in Grid
        var grid = me.getView();
        

    }

});
