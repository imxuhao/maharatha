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
            view = this.getCmp().getView(),
            maxRowIdx = view.dataSource.getCount() - 1,
            maxColIdx = view.getVisibleColumnManager().getColumns().length - 1,
            navModel = view.getNavigationModel(),
            destination = navModel.getPosition(),
            dataIndex,
            destinationStartColumn,
            dataObject = {},
            me = this;

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
            if (invalidCells.length>0) {
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
         invalidCell.set({ 'data-errorqtip': '' });

         //add again back
         invalidCell.addCls("x-invalid-cell-value");
         invalidCell.set({ 'data-errorqtip': 'Invalid value for ' + dataIndex.initCap() });
     }
});