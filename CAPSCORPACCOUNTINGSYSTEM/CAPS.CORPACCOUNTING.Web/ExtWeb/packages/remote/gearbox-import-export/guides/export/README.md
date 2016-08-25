### Export
Like the Import, no server code is required to be able to export Grid data to an excel sheet. The steps needed to get an XLSX file containing all data from a grid are:

```javascript
var grid = Ext.getCmp('MyGrid'); // Never use getCmp in practice!
var store = grid.getStore();
store.getProxy().setWriter('file.xlsx');
store.sync({
    title: 'My Excel sheet title',
    columns: grid.columns,
    callback: function(batch) {
        batch.packet.save();
    }
});
```

An XLSX download is started immediately.
