Ext.define('Chaching.view.financials.journals.JournalEntryFormController', {
    extend: 'Chaching.view.common.form.ChachingTransactionFormPanelController',
    alias: 'controller.financials-journals-journalentryform',
    doPreSaveOperation: function(record, values, idPropertyField) {
        record.set('typeOfAccountingDocumentId', 1);
        return record;
    },
    onHeaderCollapse:function(fieldSet, eOpts) {
        var me = this,
            view = me.getView(),
            detailContainer = view.down('fieldset[isTransactionDetailContainer=true]');
        if (detailContainer) {
            var detailGrid = detailContainer.down('gridpanel');
            if (detailGrid) {
                var gridHeight = detailGrid.getHeight();
                detailGrid.originalHeight = gridHeight;
                detailGrid.setHeight(gridHeight + (fieldSet.getHeight() - 80));
            }
        }
    },
    onHeaderExpand:function(fieldSet, eOpts) {
        var me = this,
           view = me.getView(),
           detailContainer = view.down('fieldset[isTransactionDetailContainer=true]');
        if (detailContainer) {
            var detailGrid = detailContainer.down('gridpanel');
            if (detailGrid) {
                detailGrid.setHeight(detailGrid.originalHeight);
            }
        }
    },
    getDetailsModifiedRecords: function (controller, view, detailGrid, detailsStore) {
        var modifiedRecords = detailsStore.getModifiedRecords(),
            me=this,
            records = [],
            data = [],
            modifiedRecs = { records: records, data: data },
            modelClass=detailsStore.getModel().$className,
            transactionId = view.getForm().findField('accountingDocumentId').getValue();
        if (modifiedRecords && modifiedRecords.length > 0) {
            var rowLength = modifiedRecords.length;
            for (var i = 0; i < rowLength; i++) {
                var rec = modifiedRecords[i];
                if (rec.dirty) {
                    if (rec.get('accountingDocumentId') === 0 || !rec.get('accountingDocumentId')) {
                        rec.set('accountingDocumentId', transactionId);
                    }
                    var modelRec = Ext.create(modelClass);
                    var debitCreditGroup = me.numToChar(i + 1);
                    //plain credit debit add
                    if (!rec.get('accountingItemId') && !rec.get('creditAccountingItemId')) {
                        if (rec.get('creditJobId') > 0 && rec.get('creditAccountId') > 0) {
                            //create new credit record
                            Ext.apply(modelRec.data, rec.data);
                            me.traverseValues(modelRec, rec);
                            if (rec.get('jobId') > 0 && rec.get('accountId') > 0) { //add debit and credit
                                rec.set('debitCreditGroup', debitCreditGroup);
                                modelRec.set('debitCreditGroup', debitCreditGroup);
                                records.push(rec);
                                data.push(rec.data);
                            }
                            records.push(modelRec);
                            data.push(modelRec.data);
                        } else if (rec.get('jobId') > 0 && rec.get('accountId') > 0) { //add debit
                            records.push(rec);
                            data.push(rec.data);
                        }
                    } else { //updating existing record
                        if (rec.get('accountingItemId') > 0 && rec.get('creditAccountingItemId') > 0) {//update both record
                            if (rec.get('creditJobId') > 0 && rec.get('creditAccountId') > 0 && rec.get('creditAccountingItemId')>0) {
                                //create new credit record
                                Ext.apply(modelRec.data, rec.data);
                                me.traverseValues(modelRec, rec);
                                if (rec.get('jobId') > 0 && rec.get('accountId') > 0) { //update debit and add credit
                                    rec.set('debitCreditGroup', debitCreditGroup);
                                    modelRec.set('debitCreditGroup', debitCreditGroup);
                                    rec.set('accountingItemOrigId', null);
                                    records.push(rec);
                                    data.push(rec.data);
                                }
                                records.push(modelRec);
                                data.push(modelRec.data);
                            } else if (rec.get('jobId') > 0 && rec.get('accountId') > 0) { //update debit
                                records.push(rec);
                                data.push(rec.data);
                            }
                        }
                    }
                }
            }
        }
        //return {};
        return modifiedRecs;
    },
    handleCreditDebitLines: function (record, modifiedRecs) {
        
    },
    traverseValues: function (modelRec, rec) {
        modelRec.set('accountingItemId', rec.get('creditAccountingItemId'));
        modelRec.set('accountingItemOrigId', rec.get('accountingItemId'));
        modelRec.set('amount', -(rec.get('amount')));
        modelRec.set('jobId', rec.get('creditJobId'));
        modelRec.set('jobNumber', rec.get('creditJobNumber'));
        modelRec.set('accountId', rec.get('creditAccountId'));
        modelRec.set('accountNumber', rec.get('creditAccountNumber'));
        modelRec.set('subAccountId1', rec.get('creditSubAccountId1'));
        modelRec.set('subAccountId2', rec.get('creditSubAccountId2'));
        modelRec.set('subAccountId3', rec.get('creditSubAccountId3'));
        modelRec.set('subAccountId4', rec.get('creditSubAccountId4'));
        modelRec.set('subAccountId5', rec.get('creditSubAccountId5'));
        modelRec.set('subAccountId6', rec.get('creditSubAccountId6'));
        modelRec.set('subAccountId7', rec.get('creditSubAccountId7'));
        modelRec.set('subAccountId8', rec.get('creditSubAccountId8'));
        modelRec.set('subAccountId9', rec.get('creditSubAccountId9'));
        modelRec.set('subAccountId10', rec.get('creditSubAccountId10'));
    },
    numToChar: function (number) {
        var numeric = (number - 1) % 26;
        var letter = this.chr(65 + numeric);
        var number2 = parseInt((number - 1) / 26);
        if (number2 > 0) {
            return this.numToChar(number2) + letter;
        } else {
            return letter;
        }
    },

    chr: function (codePt) {
        if (codePt > 0xFFFF) {
            codePt -= 0x10000;
            return String.fromCharCode(0xD800 + (codePt >> 10), 0xDC00 + (codePt & 0x3FF));
        }
        return String.fromCharCode(codePt);
    },
    validateDetails: function (controller, view, detailGrid, detailsStore,myMask) {
        var detailColumns = detailGrid.getColumns(),
            modifiedRecords = detailsStore.getModifiedRecords(),
            isValid = true,
            debitDataIndexes = ['jobNumber', 'accountNumber', 'subAccountNumber1', 'subAccountNumber2', 'subAccountNumber3', 'subAccountNumber4', 'subAccountNumber5', 'subAccountNumber6', 'subAccountNumber7', 'subAccountNumber8', 'subAccountNumber9', 'subAccountNumber10'],
            creditDataIndexes = ['creditJobNumber', 'creditAccountNumber', 'creditSubAccountNumber1', 'creditSubAccountNumber2', 'creditSubAccountNumber3', 'creditSubAccountNumber4', 'creditSubAccountNumber5', 'creditSubAccountNumber6', 'creditSubAccountNumber7', 'creditSubAccountNumber8', 'creditSubAccountNumber9', 'creditSubAccountNumber10'];
        if (modifiedRecords && modifiedRecords.length > 0) {
            var rowLength = modifiedRecords.length;
            for (var i = 0; i < rowLength; i++) {
                var record = modifiedRecords[i],
                    columnCount = detailColumns.length;
                if (record.dirty) {
                    for (var j = 0; j < columnCount; j++) {
                        var column = detailColumns[j],
                        dataIndex = column.dataIndex;
                        if (!dataIndex) dataIndex = column.name;
                        if (column.isMandatory) {
                            var columnValue = record.get(dataIndex);
                            var isValidRec = true;
                            if (debitDataIndexes.indexOf(dataIndex) !== -1) {
                                isValidRec = controller.validateRecord(dataIndex, debitDataIndexes, record, columnValue);
                            }else if (creditDataIndexes.indexOf(dataIndex)!==-1) {
                                isValidRec = controller.validateRecord(dataIndex, creditDataIndexes, record, columnValue);
                            }
                            if (dataIndex === "amount" && columnValue === 0) {
                                isValidRec = false;
                                columnValue = null;
                            }
                            if (columnValue === null || columnValue === undefined || columnValue === "") {
                                if (!isValidRec) {
                                    var cell = detailGrid.getView().getCell(record, column);
                                    if (cell) controller.invalidateCell(cell, column.text);
                                    isValid = false;
                                    myMask.hide();
                                    break;
                                }
                            }
                        }
                    }
                }
                if (!isValid) break;
            }
            return isValid;
        } else return true;
    },
    validateRecord: function (dataIndex, dataIndexes, record,columnValue) {
        var isValid = true, value = undefined;
        for (var i = 0; i < dataIndexes.length; i++) {
            if (dataIndexes[i] !== dataIndex) {
                value = record.get(dataIndexes[i]);
                if (value && (columnValue === null || columnValue === undefined || columnValue === "")) {
                    isValid = false;
                    break;
                } else isValid = true;
            }
        }
        return isValid;
    }
    
});
