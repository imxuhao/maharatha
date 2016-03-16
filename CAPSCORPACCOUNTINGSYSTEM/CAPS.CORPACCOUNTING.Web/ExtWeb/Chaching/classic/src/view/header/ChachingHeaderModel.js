Ext.define('Chaching.view.header.ChachingHeaderModel', {
    extend: 'Ext.app.ViewModel',
    alias: 'viewmodel.header-chachingheader',
    data: {
        name: 'Chaching'
    },
    stores: {
        languageStore: {
            xtype: 'array',
            autoLoad: true,
            fields: ['displayName', 'icon', 'isDefault', 'name'],
            data: abp.localization.languages
        }
    }

});
