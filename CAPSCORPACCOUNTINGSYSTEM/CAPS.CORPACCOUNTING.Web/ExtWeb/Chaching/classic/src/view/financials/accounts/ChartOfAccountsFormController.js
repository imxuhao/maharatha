Ext.define('Chaching.view.financials.accounts.ChartOfAccountsFormController', {
    extend: 'Chaching.view.common.form.ChachingFormPanelController',
    alias: 'controller.financials-accounts-chartofaccountsform',
    onTypeOfChartChange:function(typeOdChart, newValue, oldValue) {
        var me = this,
            view = me.getView(),
            form = view.getForm(),
            newAccCoa = form.findField('linkChartOfAccountID'),
            mappingStore = newAccCoa.getStore();
        switch (newValue) {
            case "1":
                if (mappingStore.getCount() > 0 && !newAccCoa.getValue()) {
                    Ext.each(mappingStore.getRange(),
                        function(rec) {
                            if (rec.get('typeOfChartId') === 4) {
                                newAccCoa.setValue(rec.get('coaId'));
                                return false;
                            }
                        });

                } else if (mappingStore.isLoading() && !newAccCoa.getValue()) {
                    mappingStore.on('load',
                        function (st,records) {
                            Ext.each(records,
                                function(rec) {
                                    if (rec.get('typeOfChartId') === 4) {
                                        newAccCoa.setValue(rec.get('coaId'));
                                        return false;
                                    }
                                });
                        });

                } else {
                    newAccCoa.setValue(null);
                }
                newAccCoa.hide(true);
            break;
            case "4":
                newAccCoa.setValue(null);
                newAccCoa.hide(true);
                break;
            default:
                newAccCoa.setValue(null);
                newAccCoa.show(true);
                break;
        }
    }
    
});
