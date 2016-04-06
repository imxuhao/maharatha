Ext.define('Chaching.view.editions.EditionsFormModel', {
    extend: 'Chaching.view.common.form.ChachingFormPanelModel',
    alias: 'viewmodel.editions-editionsform',
    data: {
        name: 'Chaching'
    },
    //stores: {
    //    editionsForComboBox: {
    //        fields: [{ name: 'displayText' }, { name: 'value' }, {
    //            name: 'editionDisplayName', convert: function (value, record) {
    //                return record.get('displayText');
    //            }
    //        }, {
    //            name: 'editionId', convert: function (value, record) {
    //                return record.get('value');
    //            }
    //        }],
    //        xtype: 'ajax',
    //        autoLoad: true,
    //        proxy: {
    //            actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
    //            type: 'chachingProxy',
    //            url: abp.appPath + 'api/services/app/edition/GetEditionComboboxItems',
    //            reader: {
    //                type: 'json',
    //                rootProperty: 'result',
    //                totalProperty: 'result.totalCount'
    //            }
    //        }
    //    }
    //}

});
