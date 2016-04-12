Ext.define('Chaching.utilities.ChachingRenderers', {
    singleton: true,
    dateSearchFieldRenderer: function (value) {
        return Ext.Date.format(value, 'm/d/Y');
    },
    statusRenderer: function (val) {
        if (val) return 'YES';
        else return 'NO';
    },
});