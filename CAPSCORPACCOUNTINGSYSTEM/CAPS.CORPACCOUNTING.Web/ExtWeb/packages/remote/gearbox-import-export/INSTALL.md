INSTALL
=======

Install package
---------------

1. Terminal:

    ```bash
    sencha -sdk ext421 generate app ImEx import-export
    cd import-export
    ```

2. `app.json`:

    ```json
    // ...
    "requires": [
        "gearbox-import-export"
    ]
    // ...
    ```

3. Terminal:

    ```bash
    sencha app refresh
    sencha web start
    ```

4. Go to [localhost:1841](http://localhost:1841)


Example
-------

1. Generate model:

    ```bash
    sencha generate model Example id:int,name:string,value:string
    ```


2. Create store `store/Example.js`:

    ```javascript
    Ext.define('ImEx.store.Example', {
        extend: 'Gearbox.data.file.Store',
        storeId: 'Example',
        model: 'ImEx.model.Example'
    });
    ```


3. Add store to `Application.js`:
    
    ```javascript
    stores: [
        'Example'
    ]
    ```

4. Replace view `view/Main.js`:
    
    ```javascript
    Ext.define('ImEx.view.Main', {
        extend: 'Ext.grid.Panel',
        xtype: 'app-main',
        store: 'Example',
        title: 'Import/export example',

        columns: [{
            dataIndex: 'name',
            text: 'Name'
        }, {
            dataIndex: 'value',
            text: 'Value'
        }],

        buttons: [{
            text: 'Guess mapping',
            handler: function(btn) {
                btn.up('grid').getStore().guessMapping();
            }
        }, {
            text: 'Save as XLSX',
            handler: function(btn) {
                var grid = btn.up('grid'),
                    store = grid.getStore();

                store.getProxy().setWriter('file.xlsx');
                store.sync({
                    title: grid.title,
                    columns: grid.columns,
                    callback: function(batch) {
                        batch.packet.save();
                    }
                });
            }
        }],

        afterRender: function() {
            this.callParent(arguments);
            this.getStore().bindDrop(this);
        }
    });
    ```
