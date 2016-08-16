Ext.define('Chaching.view.imports.ImportsFormController', {
    extend: 'Chaching.view.common.form.ChachingFormPanelController',
    alias: 'controller.imports-importsform',
    parentController: null,
    entityName: null,
    onFileChange: function (file, e) {
        if (file.value === "") {
            return;
        }
        var newvalue = file.value.replace(/^c:\\fakepath\\/i, '');
        file.setRawValue(newvalue);
        var regExpress = new RegExp('([a-zA-Z0-9\s_\\.\-:])+(.xls|.xlsx)$');
        regExpress.test(file.value);
        //if (file.value && !(regExpress.test(file.value))) {
        //    abp.message.error(app.localize('Template_FileType_Warn').initCap(), app.localize('Error'));
        //    return;
        //};
    },
    uploadTemplateFile: function () {
        var me = this,
            view = me.getView(),
            form = view.getForm(),
            requestParamsObj = null;
        if (me.entityName === "FinancialAccounts") {
            requestParamsObj = {
                'entityName': me.entityName,
                'coaId': me.parentController.getView().coaId
            };
        } else {
            requestParamsObj = {
                'entityName': me.entityName
            };
        }
        if (form.isValid()) {
            form.submit({
                url: abp.appPath + 'FileUpload/UploadExcelFile',
                params: requestParamsObj,
                success: function (fp, response) {
                    if (response.result) {
                        var data = response.result.result;
                        if (data.success) {
                            if (response.success) {
                                var wnd = view.up('window');
                                if (wnd) {
                                    Ext.destroy(wnd);
                                } else {
                                    Ext.destroy(view);
                                }
                                abp.notify.success(app.localize('SuccessMessage'), app.localize('Success'));
                                var parentGrid = me.parentController.getView();
                                var gridStore = parentGrid.getStore();
                                gridStore.loadPage(1);
                            } else {
                                ChachingGlobals.showPageSpecificErrors(response);
                            }
                        }
                        else {
                            var wnd = view.up('window');
                            if (wnd) {
                                Ext.destroy(wnd);
                            } else {
                                Ext.destroy(view);
                            }
                            var win = Ext.create('Chaching.view.imports.ImportsErrorView');
                            var winController = win.getController();
                            winController.ErrorList = data.errorMessageList;
                            win.show();
                        }
                    }
                },
                failure: function (fp, action) {
                    abp.message.error(app.localize('Failed'), app.localize('Error'));
                }
            });
        }

    }
    
});
