
Ext.define('Chaching.view.imports.ImportsGrid',
{
    extend: 'Chaching.view.common.grid.ChachingGridPanel',
    xtype: 'imports',
    store: 'imports.ErrorStore',
    padding: 5,
    gridId: 28,
    requireMultiSearch: false,
    requireMultisort: false,
    multiColumnSort: true,
    scrollable: true,
    requireActionColumn: false,
    createNewMode: 'popup',
    columns: [
        {
            xtype: 'gridcolumn',
            text: app.localize('Rownumber'),
            dataIndex: 'rowNumber',
            //sortable: true,
            groupable: true,
            width: '10%',
            // simplest filter configuration
            filterField: {
                xtype: 'textfield',
                width: '100%',
                emptyText: 'Enter Row # to search'
            }

        }, {
            xtype: 'gridcolumn',
            text: app.localize('ErrorDetail'),
            dataIndex: 'errorMessage',
            //sortable: true,
            groupable: true,
            width: '89.8%'
            // as textfield is created by default
            ,
            filterField: {
                xtype: 'textfield',
                width: '100%',
                emptyText: 'Enter error detail to search'
            }
        }
    ]
});
