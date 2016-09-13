Ext.define('Chaching.view.purchaseorders.entry.PurchaseOrderFormController', {
    extend: 'Chaching.view.common.form.ChachingTransactionFormPanelController',
    alias: 'controller.purchaseorders-entry-purchaseorderform',
    onCloseCheckChange:function(field, newValue, oldValue) {
        var me = this,
            view = me.getView(),
            form = view.getForm(),
            closeDate = form.findField('closeDate'),
            documentId = form.findField('accountingDocumentId').getValue();
        if (newValue&&(!documentId || documentId === "0" || documentId == 0)) {
            
            field.setValue(false);
            //abp.notify.info('Cannot close po.','Invalid Operation');
            return false;
        }
        if (newValue&&parseInt(documentId)>0) {
            closeDate.show();
            closeDate.allowBlank = false;
        } else {
            closeDate.hide();
            closeDate.allowBlank = true;
        }
    },
    onFormResize: function (formPanel, width, height, oldWidth, oldHeight, eOpts) {
        if (formPanel) {
            var transactionDetailContainer = formPanel.down('*[itemId=transactionDetails]');
            if (transactionDetailContainer) {
                var heightForDetailGrid = height - (170 + 80);
                transactionDetailContainer.down('gridpanel[isTransactionDetailGrid=true]').setHeight(heightForDetailGrid);
                transactionDetailContainer.down('gridpanel[isHistoryGrid=true]').setHeight(heightForDetailGrid);
            }
            formPanel.updateLayout();
        }
    },
    changeCurrency: function (field, newValue, oldValue) {
        var me = this,
            view = me.getView(),
            form = view.getForm(),
            poOriginalAmount = form.findField('poOriginalAmount'),
            controlTotal = form.findField('controlTotal'),
            remBalance = form.findField('remainingBalance');
        ///TODO: change based on currency code
        poOriginalAmount.setCurrency('INR');
        controlTotal.setCurrency('EUR');
        remBalance.setCurrency('KGS');
    }
    
});
