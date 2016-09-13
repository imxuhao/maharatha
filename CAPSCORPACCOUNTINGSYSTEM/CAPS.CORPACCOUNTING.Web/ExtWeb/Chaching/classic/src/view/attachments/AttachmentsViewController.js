Ext.define('Chaching.view.attachments.AttachmentsViewController', {
    extend: 'Chaching.view.common.window.ChachingWindowPanelController',
    alias: 'controller.attachments-attachmentsview',
    onFileAttached:function(file, e) {
        var me = this,
            view = me.getView(),
            form=view.down('form').getForm(),
            grid = view.down('gridpanel'),
            gridStore = grid.getStore(),
            impFile = file.getEl().down('input[type=file]').dom.files[0];
        var newUploadFile= {
            file: impFile,
            fileName: impFile.name,
            description: impFile.name,
            fileSize: impFile.size,
            fileStatus: 0,
            fileExtension: me.getFileType(impFile.name),
            creationTime: new Date(),
            attachedDate: new Date(),
            createdUser: ChachingGlobals.loggedInUserInfo.name
        }
        newUploadFile = me.getTypeOfAttachedObject(newUploadFile.fileExtension, newUploadFile);
        if (newUploadFile.typeOfAttachedObjectId === 0) newUploadFile.fileStatus = -1;

        var typeOfObject = form.findField('typeOfObjectId').getValue();
        newUploadFile.typeOfObjectId = typeOfObject;
        var objectId = form.findField('objectId').getValue();
        newUploadFile.objectId = objectId;
        gridStore.insert(0, newUploadFile);
        var newRec = gridStore.getAt(0);
        ///TODO: Uncomment once server side is done
        if (newUploadFile.typeOfAttachedObjectId > 0)
            me.postDocument(abp.appPath + 'Attachment/UploadAttachment', newRec, gridStore);
    },
    getTypeOfAttachedObject: function (fileExt,newData) {
        switch (fileExt) {
        case "xls":
        case "xlsx":
            newData.typeOfAttachedObjectId = 1;
            newData.typeOfAttachedObject = "Excel Document";
            break;
        case "doc":
        case "docx":
        case "ppt":
            newData.typeOfAttachedObjectId = 2;
            newData.typeOfAttachedObject = "Word Document";
            break;
        case "pdf":
            newData.typeOfAttachedObjectId = 3;
            newData.typeOfAttachedObject = "PDF Document";
            break;
        case "html":
        case "htm":
        case "php":
        case "cshtml":
            newData.typeOfAttachedObjectId = 4;
            newData.typeOfAttachedObject = "Web Page";
            break;
        case "txt":
            newData.typeOfAttachedObjectId = 5;
            newData.typeOfAttachedObject = "Text Document";
            break;
        case "png":
        case "jpg":
        case "jpeg":
        case "gif":
            newData.typeOfAttachedObjectId = 7;
            newData.typeOfAttachedObject = "Image File";
            break;
        case "mpeg":
        case "mp3":
        case "mp4":
        case "zip":
        case "rar":
            newData.typeOfAttachedObjectId = 14;
            newData.typeOfAttachedObject = "Miscellaneous";
            break;
        case "exe":
        case "application":
        case "host":
        case "lnk":
        case "msi":
            newData.typeOfAttachedObjectId = 0;
            newData.typeOfAttachedObject = "Invalid File";
            break;
        default:
            newData.typeOfAttachedObjectId = 14;
            newData.typeOfAttachedObject = "Miscellaneous";
            break;
        }
        return newData;
    },
    onCancelClicked:function() {
        var me = this,
            view = me.getView();
        Ext.destroy(view);
    },
    postDocument: function (url, newUploadFile,uploadStore) {
        var xhr = new XMLHttpRequest();
        var fd = new FormData();
        //fd.append("serverTimeDiff", 0);
        xhr.open("POST", url, true);
        fd.append('typeOfAttachedObjectId', newUploadFile.get('typeOfAttachedObjectId'));
        fd.append('typeOfObjectId', newUploadFile.get('typeOfObjectId'));
        fd.append('objectId', newUploadFile.get('objectId'));
        fd.append('description', newUploadFile.get('description'));
        fd.append('fileName', newUploadFile.get('fileName'));
        fd.append('fileSize', newUploadFile.get('fileSize'));
        fd.append('fileExtension', newUploadFile.get('fileExtension'));
        fd.append('file', newUploadFile.get('file'));
        //xhr.setRequestHeader('Content-Type', 'multipart/form-data');
        //xhr.setRequestHeader("serverTimeDiff", 0);
        xhr.onreadystatechange = function () {
            try {
                if ((xhr.readyState === 4 || xhr.readyState === 2) && xhr.status === 200) {
                    //handle the answer, in order to detect any server side error
                    if (xhr.responseText && Ext.decode(xhr.responseText).success) {
                        uploadStore.reload();
                        newUploadFile.set('fileStatus', 1);
                    }
                } else if (xhr.readyState === 4 && xhr.status === 404) {
                    newUploadFile.set('fileStatus', -1);
                } else if (xhr.readyState === 2 && xhr.status === 500) {
                    newUploadFile.set('fileStatus', -1);
                } else if (xhr.status === 500) {
                    newUploadFile.set('fileStatus', -1);
                }
            } catch (e) {
                console.log(e);
            } 
           
        };
        // Initiate a multipart/form-data upload
        xhr.send(fd);
    },
    getFileType: function(filename) {
        return filename.split('.').pop();
    },
    onWindowViewResize: function (wnd, width, height) {
        var attachmentGrid = wnd.down('*[itemId=attachmentsGrid]'),
            form = wnd.down('form');
        var newHeight = height - 100;
        form.setHeight(newHeight);
        attachmentGrid.setHeight(newHeight - 50);
        form.updateLayout();
        wnd.updateLayout();
    },

    addDropZone: function (e) {
        var me = this,
            view = me.getView(),
            grid = view.down('gridpanel');
        if (!e.browserEvent.dataTransfer || Ext.Array.from(e.browserEvent.dataTransfer.types).indexOf('Files') === -1) {
            return;
        }

        e.stopEvent();

        grid.addCls('drag-over');
    },

    removeDropZone: function (e) {
        var me = this,
        view = me.getView(),
        grid = view.down('gridpanel');
        var el = e.getTarget(),
            thisEl = grid.getEl();

        e.stopEvent();


        if (el === thisEl.dom) {
            grid.removeCls('drag-over');
            return;
        }

        while (el !== thisEl.dom && el && el.parentNode) {
            el = el.parentNode;
        }

        if (el !== thisEl.dom) {
            grid.removeCls('drag-over');
        }

    },

    drop: function (e) {
        var me = this,
            view = me.getView(),
            form = view.down('form').getForm(),
            grid = view.down('gridpanel'),
            gridStore = grid.getStore();
        e.stopEvent();

        Ext.Array.forEach(Ext.Array.from(e.browserEvent.dataTransfer.files), function (file) {
            var newUploadFile = {
                file: file,
                fileName: file.name,
                description: file.name,
                fileSize: file.size,
                fileStatus:0,
                fileExtension: me.getFileType(file.name),
                creationTime: new Date(),
                attachedDate: new Date(),
                createdUser: ChachingGlobals.loggedInUserInfo.name
            };
            newUploadFile = me.getTypeOfAttachedObject(newUploadFile.fileExtension, newUploadFile);
            if (newUploadFile.typeOfAttachedObjectId === 0) newUploadFile.fileStatus = -1;

            var typeOfObject = form.findField('typeOfObjectId').getValue();
            newUploadFile.typeOfObjectId = typeOfObject;
            var objectId = form.findField('objectId').getValue();
            newUploadFile.objectId = objectId;

            gridStore.insert(0, newUploadFile);
            var newRec = gridStore.getAt(0);
            ///TODO: Uncomment once server side is done
            if (newUploadFile.typeOfAttachedObjectId > 0)
                me.postDocument(abp.appPath + 'Attachment/UploadAttachment', newRec, gridStore);
        });
        grid.removeCls('drag-over');
    },
    onAttachmentDelete: function (tableView,rowIdx,colIdx,evt,e,record,targetCell) {
        var me = this,
            view = me.getView(),
            attachGrid = view.down('gridpanel'),
            attachStore = attachGrid.getStore();
        if (record && record.get('attachedObjectId')) {
            abp.message.confirm(
              app.localize('DeleteWarningMessage'),
              function (isConfirmed) {
                  if (isConfirmed) {
                      record.set('id', record.get('attachedObjectId'));
                      //Delete record
                      if (attachStore) {
                          var operation = Ext.data.Operation({
                              params: { id: record.get('id') },
                              controller: me,
                              action: 'destroy',
                              records: [record],
                              callback: me.onOperationCompleteCallBack
                          });
                          attachStore.erase(operation);
                      }
                  }
              }
          );
        } else if(record) {
            attachStore.remove(record);
        }
    },
    onAttachmentBeforeEdit:function(editor, context, eOpts) {
        var record = context.record;
        if (record&&record.get('typeOfAttachedObjectId')>0) {
            return true;
        }
        return false;
    },
    onAttachmentEditComplete:function(editor,context,eOpts) {
        var me = this,
            view = me.getView(),
            attachGrid = view.down('gridpanel'),
            attachStore = attachGrid.getStore(),
            idPropertyField=attachStore.idPropertyField,
            record = context.record;
        if (record && record.get(idPropertyField)) {
            record.set('id', record.get(idPropertyField));
            //TODO:Remove once output dto contains typeofobject value
            if (!record.get('typeOfAttachedObjectId')) {
                var obj = {
                    typeOfAttachedObjectId: 0,
                    typeOfAttachedObject:''
                };
                var typeOfAttachObj = me.getTypeOfAttachedObject(record.get('fileExtension'), obj);
                if (typeOfAttachObj) record.set('typeOfAttachedObjectId', typeOfAttachObj.typeOfAttachedObjectId);
            }
            var operation = Ext.data.Operation({
                params: record.data,
                records: [record],
                controller: me,
                callback: me.onOperationCompleteCallBack
            });
            attachStore.update(operation);
        }

    },
    onOperationCompleteCallBack: function(records, operation, success) {
        var controller = operation.controller,
            view = controller.getView();

        if (success) {
            controller.doReloadGrid();
            abp.notify.success(app.localize('SuccessMessage'), app.localize('Success'));
        } else {
            // show error validation if any 
            ChachingGlobals.showErrorMessage(operation);
        }
    },
    doReloadGrid:function() {
        var me = this,
            view = me.getView(),
            attachGrid = view.down('gridpanel'),
            attachStore = attachGrid.getStore();
        attachStore.reload();
    },
    onDownloadFileClicked:function(tableView, rowIdx, colIdx, evt, e, record, targetCell) {
        if (record && record.get('attachedObjectId')) {
            var params= {
                attachedObjectId: record.get('attachedObjectId')
            }
            var config = {
                method: 'POST',
                url: abp.appPath + 'Attachment/GetFilesById',
                params:params
            }
            ChachingGlobals.downloadExtUtility(config);
        }
    }
});
