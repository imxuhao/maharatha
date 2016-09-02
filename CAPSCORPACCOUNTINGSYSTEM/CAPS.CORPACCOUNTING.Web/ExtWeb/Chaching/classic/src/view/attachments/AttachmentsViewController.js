Ext.define('Chaching.view.attachments.AttachmentsViewController', {
    extend: 'Chaching.view.common.window.ChachingWindowPanelController',
    alias: 'controller.attachments-attachmentsview',
    onFileAttached:function(file, e) {
        var me = this,
            view = me.getView(),
            grid = view.down('gridpanel'),
            gridStore = grid.getStore(),
            impFile = file.getEl().down('input[type=file]').dom.files[0];

        gridStore.add({
            file: impFile,
            filename: impFile.name,
            description: impFile.name,
            filetype:me.getFileType(impFile.name),
            creationTime: new Date(),
            createdUser: ChachingGlobals.loggedInUserInfo.name
        });
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
            grid = view.down('gridpanel'),
            gridStore = grid.getStore();
        e.stopEvent();

        Ext.Array.forEach(Ext.Array.from(e.browserEvent.dataTransfer.files), function (file) {
            gridStore.add({
                file: file,
                filename: file.name,
                description: file.name,
                filetype: me.getFileType(file.name),
                creationTime: new Date(),
                createdUser: ChachingGlobals.loggedInUserInfo.name
            });
            console.log(file);
        });

        grid.removeCls('drag-over');
    }

    
});
