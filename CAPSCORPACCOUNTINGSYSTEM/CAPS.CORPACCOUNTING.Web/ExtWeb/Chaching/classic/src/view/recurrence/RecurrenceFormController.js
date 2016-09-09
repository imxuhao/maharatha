Ext.define('Chaching.view.recurrence.RecurrenceFormController', {
    extend: 'Chaching.view.common.form.ChachingFormPanelController',
    alias: 'controller.recurrence-recurrenceform',
    doPreSaveOperation: function (record, values, idPropertyField) {
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
            parentStore.each(function (rec) {
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
    },
    onTabChange: function (tabPanel, newCard, oldCard, eOpts) {
        var title = newCard.title;
        var form = newCard.up('form').getForm();
        switch (title) {
            case 'Daily':
                form.findField('IsEverySpecificDay').focus();
                break;
            case 'Weekly':
                form.findField('RecurEverySpecificWeekOn').focus();
                break;
            case 'Monthly':
                form.findField('IsDayOfEveryMonth').focus();
                break;
            case 'Yearly':
                form.findField('RecurEveryYear').focus();
                break;
        }
    },

    onEverySpecificDayChange: function (field, newValue, oldValue, eOpts) {
        var form = field.up('form').getForm();
        if (newValue == true) {
            form.findField('IsEveryWeekDay').setValue(false);
            form.findField('IsEveryWeekDay').setRawValue(false);
        }
    },

    onEveryWeekDayChange: function (field, newValue, oldValue, eOpts) {
        var form = field.up('form').getForm();
        if (newValue == true) {
            form.findField('IsEverySpecificDay').setValue(false);
            form.findField('IsEverySpecificDay').setRawValue(false);
        }
    },

    onDayOfEveryMonthChange: function (field, newValue, oldValue, eOpts) {
        var form = field.up('form').getForm();
        if (newValue == true) {
            form.findField('IsSpecificMonth').setValue(false);
            form.findField('IsSpecificMonth').setRawValue(false);
        }
    },

    onSpecificMonthChange: function (field, newValue, oldValue, eOpts) {
        var form = field.up('form').getForm();
        if (newValue == true) {
            form.findField('IsDayOfEveryMonth').setValue(false);
            form.findField('IsDayOfEveryMonth').setRawValue(false);
        }
    },

    onMonthOfYearChange: function (field, newValue, oldValue, eOpts) {
        var form = field.up('form').getForm();
        if (newValue == true) {
            form.findField('IsSpecficMonthofYear').setValue(false);
            form.findField('IsSpecficMonthofYear').setRawValue(false);
        }
    },

    onSpecificMonthOfYearChange: function (field, newValue, oldValue, eOpts) {
        var form = field.up('form').getForm();
        if (newValue == true) {
            form.findField('IsMonthOfYear').setValue(false);
            form.findField('IsMonthOfYear').setRawValue(false);
        }
    },

    onNoEndDateChange: function (field, newValue, oldValue, eOpts) {
        var form = field.up('form').getForm();
        if (newValue == true) {
            form.findField('IsEndAfter').setValue(false);
            form.findField('IsEndAfter').setRawValue(false);
            form.findField('IsRecurrenceEndDate').setValue(false);
            form.findField('IsRecurrenceEndDate').setRawValue(false);
            form.findField('EndOfOccurences').reset();
            form.findField('RecurrenceEndDate').reset();
        }
    },

    onEndAfterChange: function (field, newValue, oldValue, eOpts) {
        var form = field.up('form').getForm();
        if (newValue == true) {
            form.findField('IsNoEndDate').setValue(false);
            form.findField('IsNoEndDate').setRawValue(false);
            form.findField('IsRecurrenceEndDate').setValue(false);
            form.findField('IsRecurrenceEndDate').setRawValue(false);
        }
    },

    onEndOfOccurencesChange: function (field, newValue, oldValue, eOpts) {
        var form = field.up('form').getForm();
        if (newValue !== oldValue) {
            form.findField('IsNoEndDate').setValue(false);
            form.findField('IsNoEndDate').setRawValue(false);
            form.findField('IsRecurrenceEndDate').setValue(false);
            form.findField('IsRecurrenceEndDate').setRawValue(false);
            form.findField('IsEndAfter').setValue(true);
            form.findField('IsEndAfter').setRawValue(true);
        }
    },

    onRecurrenceEndDateSelected: function (field, newValue, oldValue, eOpts) {
        var form = field.up('form').getForm();
        if (newValue == true) {
            form.findField('IsNoEndDate').setValue(false);
            form.findField('IsNoEndDate').setRawValue(false);
            form.findField('IsEndAfter').setValue(false);
            form.findField('IsEndAfter').setRawValue(false);
        }
    },

    onRecurrenceEndDateChange: function (field, newValue, oldValue, eOpts) {
        var form = field.up('form').getForm();
        if (parseInt(form.findField('RecurrenceId').value) == "") {
            if (newValue !== oldValue) {
                form.findField('IsNoEndDate').setValue(false);
                form.findField('IsNoEndDate').setRawValue(false);
                form.findField('IsEndAfter').setValue(false);
                form.findField('IsEndAfter').setRawValue(false);
                form.findField('IsRecurrenceEndDate').setValue(true);
                form.findField('IsRecurrenceEndDate').setRawValue(true);
            }
        }
    }
});
