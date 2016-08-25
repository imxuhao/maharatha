/**
 * This class is the view model for the Main view of the application.
 */
Ext.define('ExampleGrid.view.main.MainModel', {
    extend: 'Ext.app.ViewModel',

    alias: 'viewmodel.main',

    requires: [
        'ExampleGrid.model.Example',
        'Gearbox.data.file.Store'
    ],

    data: {
        name: 'ExampleGrid'
    },

    //TODO - add data, formulas and/or methods to support your view
    stores: {
        Example: {
            xclass: 'Gearbox.data.file.Store',
            model: 'ExampleGrid.model.Example',
            storeId: 'Example',

            proxy: {
                type: 'file',
                reader: 'file.csv',
                writer: 'file.xlsx'
            },

            rawData: [
                'Name,Value,Hobby',
                'name1,value1,hobby1',
                'name2,value2,hobby2',
                'name3,value3,hobby3',
                'name4,value4,hobby4',
                'name5,value5,hobby5',
                'name6,value6,hobby6',
                'name7,value7,hobby7',
                'name8,value8,hobby8',
                'name9,value9,hobby9'
            ].join('\n')
        }
    }
});