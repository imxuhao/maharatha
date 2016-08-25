# Import

To Import data into a grid, only 4 steps are needed:

*Load the Gearbox import/export package using Sencha CMD*

```bash
$ sencha package install gearbox-import-export
```

```javascript
// app.json:
{
    "name": "MyApp",
    "requires": [
        "gearbox-import-export"
    ],
    "id": "6f844633-8dee-4968-a5bd-17a2e1e2f9cf"
}
```

*Create a store that extends 'Gearbox.data.file.Store'*

```javascript
Ext.define('MyApp.store.Example', {
    extend: 'Gearbox.data.file.Store',
    model: 'MyApp.model.Example',
    storeId: 'ImportExport',
    proxy: {
        type: 'file'
    }
});
```

*Set the proxy to the type of file it should expect (XLS, XLSX or CSV) and manually load the file or attach drag and drop behaviour to any ExtJS component*

```javascript
// Manual Import
var store = Ext.getStore('ImportExport');
store.getProxy().setReader('file.xls');
store.loadFile(myExcelFile);
 
// OR Drag and drop import
store.bindDrop(grid);
```

*Import/Export Gear (only import/export)*

```javascript
// Manually map Excel columns to Model fields
store.setMapping({
    firstName: {
        source: "First Name"
    },
    lastName: {
        source: "Last Name"
    }
});
```
 
```javascript
// OR use automatic mapping
store.guessMapping();
```

Done!