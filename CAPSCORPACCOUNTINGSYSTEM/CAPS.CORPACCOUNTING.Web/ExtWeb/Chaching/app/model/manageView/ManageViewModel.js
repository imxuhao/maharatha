/**
 * DataModel to represent entity schema for User View Settings.
 */
Ext.define('Chaching.model.manageView.ManageViewModel', {
    extend: 'Chaching.model.base.BaseModel',
    config: {
        searchEntityName: 'UserViewSettings'
    },
    fields: [
        { name: 'userViewId', type: 'int', isPrimaryKey: true },
        { name: 'viewId', type: 'int' },
        { name: 'userId', type: 'int' },
        { name: 'viewSettings', type: 'string' },
        { name: 'isDefault', type: 'boolean',defaultValue:false },
        { name: 'grid_Name', type: 'string' },
        { name: 'grid_Description', type: 'string' },
        { name: 'viewName', type: 'string' },
        { name: 'ColumnIndex', type: 'int' }
    ]
});
