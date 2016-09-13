Ext.define('Chaching.view.imports.ImportsFormController', {
    extend: 'Chaching.view.common.form.ChachingFormPanelController',
    alias: 'controller.imports-importsform',
    parentViewObj : null,
    parentController: null,
    entityName: null,
    onFileChange: function (file, e) {
        var me = this,
           view = me.getView(),
           importConfig = me.importConfig;
        if (file.value === "") {
            return;
        }
        var newvalue = file.value.replace(/^c:\\fakepath\\/i, '');
        file.setRawValue(newvalue);
        //var regExpress = new RegExp('([a-zA-Z0-9\s_\\.\-:])+(.xls|.xlsx)$');
        var regExpress = new RegExp('(\.\)+(.xls|.xlsx)$');
        regExpress.test(file.value);
        if (file.value && !(regExpress.test(file.value))) {
            abp.message.error(app.localize('Template_FileType_Warn').initCap(), app.localize('Error'));
            return;
        } else {
            var importGrid = view.down('gridpanel'),
                impStore = importGrid.getStore();
            impStore.getProxy().setReader('file.xls');
            impStore.loadFile(file.getEl().down('input[type=file]').dom.files[0]);
        } 
    },
    uploadTemplateFile: function () {
        var me = this,
            view = me.getView(),
            wnd = view.up('window'),
            form = view.getForm(),
            requestParamsObj = null,
            importGrid = view.down('gridpanel'),
            importStore = importGrid.getStore(),
            importConfig = me.importConfig,
            parentGridController = me.parentController,
            bulkListInputName = importConfig.bulkListInputName;

        if (!importConfig.targetUrl) {
            abp.notify.info('Import functionality is not available yet.');
            return;
        }

        var uploadMask = new Ext.LoadMask({
            msg: 'Please wait...',
            target: wnd
        });
        uploadMask.show();
        //wnd.suspendEvent();
        Ext.defer(function() {
                var records = me.validateImport(importGrid, importStore, uploadMask);
                if (records && records.length > 0) {
                    if (parentGridController && parentGridController.doBeforeDataImportSaveOperation(records, me.parentViewObj)) {
                        var params = {};
                        params[bulkListInputName] = records;
                        Ext.Ajax.request({
                            url: importConfig.targetUrl,
                            timeout: 60000000,
                            jsonData: Ext.encode(params),
                            success: function(response, opts) {
                                var result = Ext.decode(response.responseText);
                                if (result && result.success) {
                                    var invalidRecs = result.result;
                                    if (invalidRecs && invalidRecs.length > 0) {
                                        importStore.loadData(invalidRecs);
                                        abp.notify.error(app.localize('UploadFailedMessage'), app.localize('Error'));
                                    } else {
                                        abp.notify.success(app.localize('SuccessMessage'), app.localize('Success'));
                                        var parentGrid = me.parentViewObj;//parentGridController.getView();
                                        var gridStore = parentGrid.getStore();
                                        gridStore.loadPage(1);
                                        var wnd = view.up('window');
                                        if (wnd) {
                                            Ext.destroy(wnd);
                                        } else {
                                            Ext.destroy(view);
                                        }
                                    }
                                } else {
                                    ChachingGlobals.showPageSpecificErrors(response);
                                }
                                uploadMask.hide(true);
                            },
                            failure: function(response, eOpts) {
                                uploadMask.hide();
                                abp.message.error(app.localize('Failed'), app.localize('Error'));
                            }
                        });
                    } else {
                        uploadMask.hide(true);
                    }
                } else {
                    uploadMask.hide(true);
                }
            },
            100);

        //wnd.resumeEvent();
    },
    validateImport: function (importGrid, importStore, uploadMask) {
        var isValid = true,
            me = this,
            duplicateCount = 0;
        if (importGrid && importStore) {
            var records = importStore.getRange(),
                errorCount = 0,
                columnManager = importGrid.getVisibleColumnManager(),
                data = [];
            isValid = records.length > 0;
            if (!isValid) {
                uploadMask.hide(true);
                abp.notify.error(app.localize('FileNotImported'), app.localize('Error'));
                return isValid;
            }
            var fields = importStore.getProxy().getModel().getFields();
            importStore.each(function(record) {
                record.set('errorMessage', null);
                try {
                    Ext.each(fields,
                        function(field) {
                            var mandatory = field.mandatory,
                                duplicate = field.duplicate,
                                err = undefined,
                                column = undefined;
                            if (mandatory && field.name !== "errorMessage") {
                                isValid = me.checkMandatory(record, field.name, field.type);
                                if (!isValid) {
                                    errorCount += 1;
                                    var exError = record.get('errorMessage');
                                    if (exError)
                                        err = exError + '; ' + app.localize('MandatoryCellValue');
                                    else
                                        err = app.localize('MandatoryCellValue');
                                    column = columnManager.getHeaderByDataIndex(field.name);
                                    if (column)
                                        record.set('errorMessage', err.format(err, column.text));
                                    else
                                        record.set('errorMessage', err);
                                }
                            }
                            if (duplicate) {
                                isValid = me.checkDuplicate(importStore, record, field.name);
                                if (!isValid) {
                                    errorCount += 1;
                                    var exiErr = record.get('errorMessage');
                                    if (exiErr)
                                        err = exiErr + '; ' + record.get(field.name) +' '+ field.name+' is been duplicated.';//app.localize('DuplicateRecord');
                                    else
                                        err = record.get(field.name) + ' ' + field.name + ' is been duplicated.';

                                    record.set('errorMessage', err);
                                }
                            }
                            if (field.type == 'int' && field.name.indexOf('Id')>=0) {
                                var comboColumnName = field.name.replace('Id','');
                                if (record.get(comboColumnName)) {
                                    isValid = (record.get(field.name)) == null ? false : true;
                                    if (!isValid) {
                                        errorCount += 1;
                                        var exError = record.get('errorMessage');
                                        column = columnManager.getHeaderByDataIndex(comboColumnName);
                                        if (column) {
                                            comboColumnName = column.text;
                                        }
                                        if (exError)
                                            err = exError + '; ' + comboColumnName + app.localize('ImportValueNotExists');
                                        else
                                            err = comboColumnName + app.localize('ImportValueNotExists');

                                        record.set('errorMessage', err);
                                    }
                                }
                            }
                        });
                    data.push(record.data);
                } catch (e) {
                    console.log(e);
                }
            });
            if (errorCount > 0) {
                uploadMask.hide(true);
                abp.message.error(app.localize('InvalidDataUploaded'), app.localize('Error'));
                return false;
            } else {
                return data;
            }
        }
        return false;
    },
    checkDuplicate: function (importStore, rec, fieldName) {
        var value = rec.get(fieldName);
        var recordIndex = importStore.findBy(
            function(record, id) {
                if (record.get(fieldName) === value && record.internalId !== rec.internalId && value) {
                    return true; // a record with this data exists
                }
                return false; // there is no record in the store with this data
            }
        );

        if (recordIndex !== -1) {
            return false;
        }
        return true;
    },
    checkMandatory:function(record, fieldName,fieldType) {
        var value = record.get(fieldName), isValid = true;
        switch (fieldType) {
            case "string":
                if (!value) isValid = false;
                break;
            case"int":
            case "number":
            case "float":
                if (value <= 0) isValid = false;
                break;
            case "bool":
            case "boolean":
                isValid = value;
                break;
            default:
                break;
        }
        return isValid;
    }
    
});
