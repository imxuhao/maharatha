Ext.define('Chaching.view.languages.LanguagesGridController', {
    extend: 'Chaching.view.common.grid.ChachingGridPanelController',
    alias: 'controller.languages-languagesgrid',    
    changeLanguageClick: function (menu, formView, isEdit) {        
        var parentMenu = menu.parentMenu,
           widgetRec = parentMenu.widgetRecord,         
            data=widgetRec.data,
        
        languageTextView = Ext.create('Chaching.view.languages.LanguagesTextView');
        var form = languageTextView.down('form').getForm();
            form.findField('baseLanguage').setValue('en');
            form.findField('source').setValue('CORPACCOUNTING');
            form.findField('targetValue').setValue('All');
            form.findField('targetLanguage').setValue(data.name);
            languageTextView.show();
    },
    doRowSpecificEditDelete: function (button, grid) {
        if (button.menu) {
            var editActionMenu = button.menu.down('menuitem#editActionMenu');
            var deleteActionMenu = button.menu.down('menuitem#deleteActionMenu');
            var viewActionMenu = button.menu.down('menuitem#viewActionMenu'); 
            var actionMenuSeparator = button.menu.down('menuitem#actionMenuSeparator');
            if (abp.session.tenantId && editActionMenu && deleteActionMenu && viewActionMenu && actionMenuSeparator) {
                if ( button.widgetRec && button.widgetRec.get('tenantId')) {
                    editActionMenu.show();
                    deleteActionMenu.show();
                    viewActionMenu.show();
                    actionMenuSeparator.show();
                } else {
                    editActionMenu.hide();
                    deleteActionMenu.hide();
                    viewActionMenu.hide();
                    actionMenuSeparator.hide();
                }
            }
        }
    }
});
