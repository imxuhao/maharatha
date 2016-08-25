/**
 * This class is the main view for the application. It is specified in app.js as the
 * "autoCreateViewport" property. That setting automatically applies the "viewport"
 * plugin to promote that instance of this class to the body element.
 *
 * TODO - Replace this content of this view to suite the needs of your application.
 */
Ext.define('ExampleGrid.view.main.MainController', {
    extend: 'Ext.app.ViewController',

    requires: [
        'Ext.window.MessageBox'
    ],

    alias: 'controller.main',

    onAfterRender: function() {
        var grid = this.getView(),
            store = this.getViewModel().getStore('Example');

        store.bindDrop(grid);
    },

    loadCsv: function(btn) {
        var store = this.getViewModel().getStore('Example');

        Ext.Msg.prompt('Load CSV', 'Paste CSV file here', function(btn, data) {
            if (btn !== 'ok') {
                return;
            }

            // sets store reader to csv
            store.getProxy().setReader('file.csv');

            // and loads the new data
            store.loadRawData(data);
        }, this, true, store.rawData);
    },

    saveCsv: function(btn) {
        var grid = this.getView(),
            store = this.getViewModel().getStore('Example');

        store.getProxy().setWriter('file.csv');
        store.sync({
            title: grid.title,
            columns: grid.columns,
            callback: function(batch) {
                batch.packet.save();
            }
        });
    },

    saveXlsx: function(btn) {
        var grid = this.getView(),
            store = this.getViewModel().getStore('Example');

        store.getProxy().setWriter('file.xlsx');
        store.sync({
            title: grid.title,
            columns: grid.columns,
            callback: function(batch) {
                batch.packet.save();
            }
        });
    }
});