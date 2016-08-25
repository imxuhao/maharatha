Ext.define('ExampleGrid.view.main.Main', {
    extend: 'Ext.grid.Panel',
    xtype: 'app-main',

    requires: [
        'ExampleGrid.view.main.MainController',
        'ExampleGrid.view.main.MainModel'
    ],

    controller: 'main',
    viewModel: {
        type: 'main'
    },

    plugins: [{
        ptype: 'cellediting'
    }],

    listeners: {
        afterrender: 'onAfterRender'
    },

    bind: {
        store: '{Example}'
    },

    title: 'Import Export Gear Example',

    columns: [{
        dataIndex: 'name',
        flex: 1,
        text: 'Name',
        editor: 'textfield'
    }, {
        dataIndex: 'value',
        flex: 1,
        text: 'Value',
        editor: 'textfield'
    }, {
        dataIndex: 'hobby',
        flex: 1,
        text: 'Hobby',
        editor: 'textfield'
    }],

    bbar: [{
        text: 'Load CSV',
        handler: 'loadCsv'
    }, '->', {
        text: 'Save CSV',
        handler: 'saveCsv'
    }, {
        text: 'Save XLSX',
        handler: 'saveXlsx'
    }]
});