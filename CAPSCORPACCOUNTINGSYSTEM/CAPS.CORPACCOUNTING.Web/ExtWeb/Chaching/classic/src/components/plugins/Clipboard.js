/**
 * This {@link Ext.grid.Panel grid} plugin adds clipboard support to a grid.
 *
 * *Note that the grid must use the {@link Ext.grid.selection.SpreadsheetModel spreadsheet selection model} to utilize this plugin.*
 *
 * This class supports the following `{@link Ext.plugin.AbstractClipboard#formats formats}`
 * for grid data:
 *
 *  * `cell` - Complete field data that can be matched to other grids using the same
 *    {@link Ext.data.Model model} regardless of column order.
 *  * `text` - Cell content stripped of HTML tags.
 *  * `html` - Complete cell content, including any rendered HTML tags.
 *  * `raw` - Underlying field values based on `dataIndex`.
 *
 * The `cell` format is not valid for the `{@link Ext.plugin.AbstractClipboard#system system}`
 * clipboard format.
 */
Ext.define('Chaching.components.plugins.Clipboard', {
    extend: 'Ext.grid.plugin.Clipboard',

    alias: 'plugin.chachingClipboard',
    noofRequestsFired:0,
    getCellData: function (format, erase) {
        var cmp = this.getCmp(),
            selModel = cmp.getSelectionModel(),
            ret = [],
            isRaw = format === 'raw',
            isText = format === 'text',
            viewNode,
            cell, data, dataIndex, lastRecord, column, record, row, view;

        selModel.getSelected().eachCell(function (cellContext) {
            column = cellContext.column,
            view = cellContext.column.getView();
            record = cellContext.record;

            // Do not copy the check column or row numberer column
            if (column.ignoreExport) {
                return;
            }

            if (lastRecord !== record) {
                lastRecord = record;
                ret.push(row = []);
            }

            dataIndex = column.dataIndex;

            if (isRaw) {
                data = record.data[dataIndex];
            } else {
                // Try to access the view node.
                viewNode = view.all.item(cellContext.rowIdx);
                // If we could not, it's because it's outside of the rendered block - recreate it.
                if (!viewNode) {
                    viewNode = Ext.fly(view.createRowElement(record, cellContext.rowIdx));
                }
                cell = viewNode.down(column.getCellInnerSelector());
                data = cell.dom.innerHTML;
                if (isText) {
                    data = Ext.util.Format.stripTags(data);
                    //if is empty cell do not copy space html chars &nbsp
                    if (!record.get(dataIndex)) {
                        data = "";
                    }
                    if (dataIndex==="amount") {
                        data = Chaching.utilities.ChachingRenderers.unformattedNumber(data);
                    }
                }
            }

            row.push(data);

            if (erase && dataIndex) {
                record.set(dataIndex, null);
            }
        });

        return Ext.util.TSV.encode(ret);
    },
    putCellData: function (data, format) {
        var values = Ext.util.TSV.decode(data),
            row,
            recCount = values.length,
            colCount = recCount ? values[0].length : 0,
            sourceRowIdx,
            sourceColIdx,
            grid=this.getCmp(),
            view = grid.getView(),
            maxRowIdx = view.dataSource.getCount() - 1,
            visibleColumns = view.getVisibleColumnManager().getColumns(),
            maxColIdx = visibleColumns.length - 1,
            navModel = view.getNavigationModel(),
            destination = navModel.getPosition(),
            dataIndex,
            destinationStartColumn,
            dataObject = {},
            me = this,
            cols = [];
        grid.mask('Please wait!... While we are validating your inputs.');
        if (recCount && colCount) {
            if (colCount > 1) {
                var startFrom = destination.colIdx;
                cols.push(visibleColumns[startFrom]);
                for (var i = startFrom; i < maxColIdx; i++) {
                    if (i <= colCount)
                        cols.push(visibleColumns[i + 1]);
                    else
                        break;
                }
            } else {
                cols.push(destination.column);
            }
        }
        var promise = me.loadRequiredDataForKeyValuePairColumns(cols, values);
        var runner = new Ext.util.TaskRunner(),
            task = undefined;

        task = runner.newTask({
            run: function () {
                if (promise && promise.owner.completed) {
                    task.stop();
                    grid.unmask();
                    me.noofRequestsFired = 0;
                    if (promise.owner.completionAction === "fulfill"||promise.owner.completionAction === "reject") {
                        // If the view is not focused, use the first cell of the selection as the destination.
                        if (!destination) {
                            view.getSelectionModel().getSelected().eachCell(function (c) {
                                destination = c;
                                return false;
                            });
                        }

                        if (destination) {
                            // Create a new Context based upon the outermost View.
                            // NavigationModel works on local views. TODO: remove this step when NavModel is fixed to use outermost view in locked grid.
                            // At that point, we can use navModel.getPosition()
                            destination = new Ext.grid.CellContext(view).setPosition(destination.record, destination.column);
                        } else {
                            destination = new Ext.grid.CellContext(view).setPosition(0, 0);
                        }
                        destinationStartColumn = destination.colIdx;

                        for (sourceRowIdx = 0; sourceRowIdx < recCount; sourceRowIdx++) {
                            row = values[sourceRowIdx];
                            var invalidCells = [];
                            // Collect new values in dataObject
                            for (sourceColIdx = 0; sourceColIdx < colCount; sourceColIdx++) {
                                dataIndex = destination.column.dataIndex;
                                var columnValueField = destination.column.valueField,
                                    remoteData,
                                    cell;

                                if (dataIndex) {
                                    switch (format) {
                                        // Raw field values
                                        case 'raw':
                                            dataObject[dataIndex] = row[sourceColIdx];
                                            if (columnValueField && dataObject[dataIndex]) {
                                                remoteData = me.getValueByText(destination.column.remoteData, dataObject[dataIndex], dataIndex);
                                                if (remoteData) {
                                                    dataObject[columnValueField] = remoteData[columnValueField];
                                                } else {
                                                    cell = view.getCell(destination.record, destination.column);
                                                    if (cell) {
                                                        invalidCells.push({ cell: cell, dataIndex: destination.column.text });
                                                    }
                                                }
                                            }
                                            break;

                                            // Textual data with HTML tags stripped    
                                        case 'text':
                                            dataObject[dataIndex] = row[sourceColIdx];
                                            if (columnValueField && dataObject[dataIndex]) {
                                                remoteData = me.getValueByText(destination.column.remoteData, dataObject[dataIndex], dataIndex);
                                                if (remoteData) {
                                                    dataObject[columnValueField] = remoteData[columnValueField];
                                                } else {
                                                    cell = view.getCell(destination.record, destination.column);
                                                    if (cell) {
                                                        invalidCells.push({ cell: cell, dataIndex: destination.column.text });
                                                    }
                                                }
                                            }
                                            break;

                                            // innerHTML from the cell inner
                                        case 'html':
                                            break;
                                    }
                                }
                                // If we are at the end of the destination row, break the column loop.
                                if (destination.colIdx === maxColIdx) {
                                    break;
                                }
                                destination.setColumn(destination.colIdx + 1);
                            }

                            // Update the record in one go.
                            destination.record.set(dataObject);
                            if (invalidCells.length > 0) {
                                for (var c = 0; c < invalidCells.length; c++) {
                                    me.applyCssOnInvalidCells(invalidCells[c].cell, invalidCells[c].dataIndex);
                                }
                            }

                            // If we are at the end of the destination store, break the row loop.
                            if (destination.rowIdx === maxRowIdx) {
                                break;
                            }

                            // Jump to next row in destination
                            destination.setPosition(destination.rowIdx + 1, destinationStartColumn);
                        }
                    } 
                }
            },
            interval: 1000
        });

        task.start();
    },
     getValueByText: function(arr, value, property) {
        if (!arr)return undefined;
        for (var i = 0, iLen = arr.length; i < iLen; i++) {

            if (arr[i][property] === value) return arr[i];
        }
        return undefined;
     },
     applyCssOnInvalidCells: function (invalidCell, dataIndex) {
         //remove first if existing classes has been applied. Needs to remove else multiple times cls-class and tooltip will get added
         invalidCell.removeCls("x-invalid-cell-value");
         invalidCell.removeCls("x-mandatory-cell-value");
         invalidCell.set({ 'data-errorqtip': '' });

         //add again back
         invalidCell.addCls("x-invalid-cell-value");
         invalidCell.set({ 'data-errorqtip': 'Invalid value for ' + dataIndex.initCap() });
     },
    loadRequiredDataForKeyValuePairColumns:function(columns, values) {
        var deferred = new Ext.Deferred(), me = this;
        var columnLength = columns.length,
            rowLength = values.length,
            entityValueCollection = undefined;
        if (columns) {
            for (var i = 0; i < columnLength; i++) {
                var column = columns[i];
                if (column.valueField) {
                    if (!column.entityType)continue;
                    var nameValueCollection = [];
                    for (var j = 0; j < rowLength; j++) {
                        var obj = {
                            name: values[j].length>1?values[j][i]:values[j][0]
                        };
                        if (!me.getValueByText(nameValueCollection, obj.name, 'name') && obj.name)
                            nameValueCollection.push(obj);
                    }
                    if (nameValueCollection.length > 0) {
                        entityValueCollection = {
                            type: column.entityType,
                            nameValueList: nameValueCollection,
                            organizationUnitId: Chaching.utilities.ChachingGlobals.loggedInUserInfo.userOrganizationId
                        };
                        me.fetchData(column, deferred, entityValueCollection);
                    }
                }
            }
        }
        if (me.noofRequestsFired <= 0) {
            deferred.resolve('{success:true}');
        }
        return deferred.promise;
    },
    fetchData: function (column, deferred, params) {
        var me = this;
        me.noofRequestsFired += 1;
            
        Ext.Ajax.request({
            method: 'POST',
            headers: {
                'Accept': 'application/json'
            },
            url: abp.appPath + 'api/services/app/list/GetListByNames',
            jsonData:Ext.encode(params),
            success: function (response, opts) {
                me.noofRequestsFired -= 1;
                var obj = Ext.decode(response.responseText);
                if (obj && obj.success) {
                    var valueField = column.valueField,
                        displayField = column.dataIndex,
                        result = obj.result;
                    if (result.length>0) {
                        var remoteData = [];
                        for (var i = 0; i < result.length; i++) {
                            var rData = {};
                            rData[valueField] = result[i].value;
                            rData[displayField] = result[i].name;
                            remoteData.push(rData);
                        }
                        column.remoteData = remoteData;
                    }
                }
                if (me.noofRequestsFired<=0) {
                    deferred.resolve('{success:true}');
                }
            },
            failure: function (response, opts) {
                var res = Ext.decode(response.responseText);
                Ext.toast(res.exceptionMessage);
                console.log(response);
                me.noofRequestsFired -= 1;
                if (me.noofRequestsFired <= 0) {
                    deferred.resolve('{success:true}');
                }
            }
        });
    }
});