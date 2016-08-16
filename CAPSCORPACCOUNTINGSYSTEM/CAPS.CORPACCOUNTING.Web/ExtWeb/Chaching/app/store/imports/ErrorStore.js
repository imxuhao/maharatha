/**
 * DataStore to get erorrs in Imports.
 */
Ext.define('Chaching.store.imports.ErrorStore', {
    extend: 'Chaching.store.base.BaseStore',
    model: 'Chaching.model.imports.ErrorModel',
    idPropertyField: 'id'//important to set for add/update of records
});