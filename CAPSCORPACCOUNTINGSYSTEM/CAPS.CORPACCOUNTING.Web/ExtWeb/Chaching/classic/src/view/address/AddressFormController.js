Ext.define('Chaching.view.address.AddressFormController', {
    extend: 'Chaching.view.common.form.ChachingFormPanelController',
    alias: 'controller.address-addressform',
    doPreSaveOperation: function(record, values, idPropertyField) {
        var me = this,
            view = me.getView(),
            form = view.getForm(),
            parentGrid = view.parentGrid,
            parentStore = parentGrid.getStore();
        if (values) {
            values.addressType = form.findField('addressTypeId').getRawValue();
        }
        if (!form.isValid()) {
            abp.notify.warn(app.localize('InvalidFormMessage'), app.localize('ValidationFailed'));
            return false;
        }
        if (values.isPrimary) {
            parentStore.each(function(rec) {
                rec.set('isPrimary', false);
            });
        }
        if (record && record.get(idPropertyField) > 0) {
            record = parentStore.findRecord(idPropertyField, record.get(idPropertyField));
            Ext.apply(record.data, values);
            parentGrid.getView().refresh();
            record.dirty = true;
        } else {
            Ext.apply(record.data, values);
            record.dirty = true;
            parentStore.add(record);
        }
        if (view.openInPopupWindow) {
            Ext.destroy(view.up('window'));
        } else Ext.destroy(view);
        return false;
    }
});
