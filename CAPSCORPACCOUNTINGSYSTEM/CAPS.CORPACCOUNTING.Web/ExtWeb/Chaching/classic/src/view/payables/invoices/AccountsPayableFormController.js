Ext.define('Chaching.view.payables.invoices.AccountsPayableFormController',
{
    extend: 'Chaching.view.common.form.ChachingTransactionFormPanelController',
    alias: 'controller.payables-invoices-accountspayableform',
    onFormResize: function(formPanel, width, height, oldWidth, oldHeight, eOpts) {
        if (formPanel) {
            var transactionDetailContainer = formPanel.down('*[itemId=transactionDetails]');
            if (transactionDetailContainer) {
                var heightForDetailGrid = height - (170 + 80);
                transactionDetailContainer.down('gridpanel').setHeight(heightForDetailGrid);
            }
            formPanel.updateLayout();
        }
    },
    onInvoiceTypeChange: function(field, newValue, oldValue) {
        var me = this,
            view = me.getView(),
            form = view.getForm(),
            quickPayCol = view.down('*[itemId=quickPaySection]');

        if (quickPayCol && newValue && newValue.typeOfInvoiceId) {
            var bank = form.findField('bankAccountId'),
                checkDate = form.findField('paymentDate'),
                payType = form.findField('payType'),
                checkNumber = form.findField('paymentNumber'),
                invoiceTotal = form.findField('controlTotal'),
                checkGroup = form.findField('typeOfCheckGroupId'),
                adjustInv = form.findField('adjustInvoice');
            switch (newValue.typeOfInvoiceId) {
            case "1":
                invoiceTotal.setFieldLabel(app.localize('InvoiceTotal'));
                bank.allowBlank = true;
                bank.hide();
                bank.reset();
                checkDate.allowBlank = true;
                checkDate.hide();
                checkDate.reset();
                payType.allowBlank = true;
                payType.hide();
                payType.reset();
                checkNumber.allowBlank = true;
                checkNumber.hide();
                checkNumber.reset();
                checkGroup.setDisabled(false);
                adjustInv.hide();
                break;
            case "2":
                invoiceTotal.setFieldLabel(app.localize('CreditTotal'));
                bank.allowBlank = true;
                bank.hide();
                bank.reset();
                checkDate.allowBlank = true;
                checkDate.hide();
                checkDate.reset();
                payType.allowBlank = true;
                payType.hide();
                payType.reset();
                checkNumber.allowBlank = true;
                checkNumber.hide();
                checkNumber.reset();
                checkGroup.setDisabled(false);
                adjustInv.show();
                break;
            case "3":
                invoiceTotal.setFieldLabel(app.localize('InvoiceTotal'));
                bank.show();
                bank.allowBlank = false;
                checkDate.show();
                checkDate.allowBlank = false;
                payType.show();
                payType.allowBlank = false;
                checkNumber.show();
                checkNumber.allowBlank = false;
                checkGroup.setDisabled(true);
                adjustInv.hide();
                break;
            default:
                break;
            }
            form.clearInvalid();
        }

    },
    onQuickPayFieldChanged: function(field, newValue, oldValue) {
        var me = this,
            view = me.getView(),
            form = view.getForm(),
            bank = form.findField('bankAccountId').getValue(),
            checkDate = form.findField('paymentDate').getValue(),
            payType = form.findField('payType').getValue(),
            checkNumber = form.findField('paymentNumber');
        var printCheck = checkNumber.getTrigger('printCheck');
        if (bank && checkDate && payType && checkNumber.getValue()) {
            printCheck.getEl()
                .removeCls('x-form-trigger x-form-trigger-fieldLabelTop printTriggerClsInactive printTriggerClsInactive-fieldLabelTop');
            printCheck.getEl()
                .setCls('x-form-trigger x-form-trigger-fieldLabelTop printTriggerClsActive printTriggerClsActive-fieldLabelTop');
            checkNumber.updateLayout();
            checkNumber.allowAction = true;
        } else {
            printCheck.getEl()
                .removeCls('x-form-trigger x-form-trigger-fieldLabelTop printTriggerClsActive printTriggerClsActive-fieldLabelTop');
            printCheck.getEl()
                .setCls('x-form-trigger x-form-trigger-fieldLabelTop printTriggerClsInactive printTriggerClsInactive-fieldLabelTop');
            checkNumber.updateLayout();
            checkNumber.allowAction = false;
        }
    },
    changeCurrency: function(field, newValue, oldValue) {
        var me = this,
            view = me.getView(),
            form = view.getForm(),
            controlTotal = form.findField('controlTotal');
        ///TODO: change based on currency code
        controlTotal.setCurrency('INR');
    },
    onProcessPoClicked: function() {
        var me = this,
            view = me.getView(),
            recentPoGrid = view.down('gridpanel[itemId=recentPos]'),
            recentPoStore = recentPoGrid.getStore();
        if (recentPoStore && recentPoStore.getTotalCount() > 0) {
            ///TODO: if selected then process only selected else show po relief screen for selected vendor
            abp.message.confirm(app.localize('ProcessPos'),
                app.localize('AreYouSure'),
                function(isConfirmed) {
                    if (isConfirmed) {
                        abp.notify.info(app.localize('POProcessedSuccessfully'),app.localize('Success'));
                    }
                });
        }
    }

});
