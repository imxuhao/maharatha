Ext.define('Chaching.view.languages.LanguagesEditFormController', {
    extend: 'Chaching.view.common.form.ChachingFormPanelController',
    alias: 'controller.languages-languageseditform',
    onSaveClicked: function (btn, e, eOpts) {
        var me = this;
        switch (btn.name) {
            case 'Previous':
                me.onSave(btn, false);
                break;
            case 'Save':
                me.onSave(btn);
                break;
            case 'SaveNext':
                me.onSave(btn, true);
                break;
        }

    },
    onCancelClicked: function (btn, e, eOpts) {
        var me = this,
            view = me.getView();
        if (view) {
            var window = view.up('window');
            Ext.destroy(window);
            return;
        }
        Ext.destroy(view);

    },
    onSave: function (btn, IsNext) {
        var me = this;
        var formPanel = btn.up('form');
        var id = formPanel.getValues().rowNumber;
        var grid = me.getView().up().parentGrid;
        if (!IsNext) {
            if (id != 0) {
                var previousRecord = {
                    sourceName: formPanel.getValues().sourceName,
                    baseValue: grid.getStore().getAt(id - 1).data.baseValue,
                    value: grid.getStore().getAt(id - 1).data.targetValue,
                    key: grid.getStore().getAt(id - 1).data.key,
                    hiddenKey: grid.getStore().getAt(id - 1).data.key,
                    targetLanguage: formPanel.getValues().hiddenTargetLanguage,
                    hiddenTargetLanguage: formPanel.getValues().hiddenTargetLanguage,
                    rowNumber: formPanel.getValues().rowNumber - 1
                };
            }
            me.getView().up().down('form').getForm().setValues(previousRecord);
            id = formPanel.getValues().rowNumber;
            if (id == 0) {
                var previousButton = formPanel.query("#BtnPrevious")[0];
                previousButton.setDisabled(true);
            }
            //formPanel.setValues(previousRecord);
        }
        else
        {
            var record = Ext.encode({
                "languageName": formPanel.getValues().hiddenTargetLanguage,
                "sourceName": formPanel.getValues().sourceName,
                "key": formPanel.getValues().hiddenKey,
                "value": formPanel.getValues().value
            });

            Ext.Ajax.request({
                url: abp.appPath + 'api/services/app/language/UpdateLanguageText',
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                jsonData: record,
                //useDefaultXhrHeader: false,
                //params: record,
                success: function (conn, response, options, eOpts) {
                    me.onSuccessOfSave(conn, response, IsNext);
                },
                failure: function (conn, response, options, eOpts) {
                    var respObj = Ext.JSON.decode(conn.responseText);
                    Ext.Msg.alert("Error", respObj.status.statusMessage);
                }
            });
        }

    },
    onSuccessOfSave: function (conn, response, IsNext) {
        var me = this,
            formPanel = me.getView(),
            grid = formPanel.up().parentGrid,
            id = formPanel.getValues().rowNumber;
        var result = Ext.JSON.decode(conn.responseText);
        if (result.success) {
            //Ext.Msg.alert('Success!', 'Stock was saved.');
            if (IsNext) {
                id=id+1;
                var nextRecord = {
                    sourceName: formPanel.getValues().sourceName,
                    baseValue: grid.getStore().getAt(id).data.baseValue,
                    value: grid.getStore().getAt(id).data.targetValue,
                    key: grid.getStore().getAt(id).data.key,
                    hiddenKey: grid.getStore().getAt(id).data.key,
                    targetLanguage: formPanel.getValues().hiddenTargetLanguage,
                    hiddenTargetLanguage: formPanel.getValues().hiddenTargetLanguage,
                    rowNumber: formPanel.getValues().rowNumber + 1
                };
                formPanel.setValues(nextRecord);
            }
            else{
                // Save and Close
                me.doPostSaveOperations(null, null, result.success);
                grid.getStore().reload();
                me.onCancelClicked();
               
            }

        } else {
            Ext.Msg.alert('Error!',result.error.message);
        }

    }

});
