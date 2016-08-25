/**
 * @class Gearbox.data.file.writer.Xlsx
 */
Ext.define('Gearbox.data.file.writer.Xlsx', {
	extend: 'Gearbox.data.file.writer.Writer',

	alias: 'writer.file.xlsx',

	/**
	 * @method  writerRecords Write records to the xlsx file.
	 * @param  {Object} request
	 * @param  {Ext.data.Operation} [request.operation]
	 * @param  {Ext.data.Batch} [request.operation.batch] The batch to write to
	 * @param  {Object[]} data The records to write.
	 * @return {Object} request.
	 */
	writeRecords: function(request, data) {
		var operation = request.operation || request.getOperation(),
			batch = operation.batch || operation.getBatch(),
			wb,
			title = (batch.title || 'Data') + '.xlsx',
			columns = batch.columns || this.getRawColumns(data);

		this.setTitle(title);

		wb = {
			SheetNames: [this.getTitle()],
			Sheets: {}
		};
		wb.Sheets[this.getTitle()] = this.createSheet(columns, data);

		var binary = window.XLSX.write(wb, {
			type: 'binary'
		});

		batch.packet.setText(binary);
		batch.packet.setName(title);

		return request;
	},

	/**
	 * @method writeValue
	 *         Write a value to a particular field in a data set.
	 * @param  {Object} value  	The value to write to the field.
	 * @return {void}
	 */
	writeValue: function(value, column, record, rowIdx, colIdx) {
		var renderer = column.renderer;

		if (column.xtype !== 'checkcolumn' &&
			renderer &&
			!Ext.isDate(value)
		) {
			var metaData = {
					tdClass: ''
				},
				view = column.ownerCt.view;

			value = renderer.call(
				column,
				value,
				metaData,
				record,
				rowIdx,
				colIdx,
				view ? view.dataSource : null,
				view);
		}

		if (column.xtype === 'templatecolumn') {
			value = String(value).replace(/<\/?[^>]+\>/g, '');
		}

		var cell = {
			v: value
		};

		if (typeof cell.v === 'number') {
			cell.t = 'n';
		}
		else if (typeof cell.v === 'boolean') {
			cell.t = 'b';
		}
		else if (Ext.isDate(value)) {
			cell.t = 'n';
			cell.z = window.XLSX.SSF._table[14];
			cell.v = this.convertDate(cell.v);
		}
		else {
			cell.t = 's';
			if (cell.v === null) {
				cell.v = '';
			}
			else {
				cell.v = String(cell.v);
			}
		}

		return cell;
	},

	createSheet: function(columns, data) {
		var me = this,
			ws = {};

		var xlsColumns = [];
		Ext.Array.each(columns, function(column) {
			if (!column.hidden &&
				column.dataIndex &&
				column.xtype !== 'actioncolumn' &&
				column.xtype !== 'widgetcolumn' &&
				column.xtype !== 'rownumberer'
			) {
				xlsColumns[column.getVisibleIndex()] = column;
			}
		});
		xlsColumns = Ext.Object.getValues(xlsColumns);

		Ext.Array.each(xlsColumns, function(column, colIdx) {
			ws[window.XLSX.utils.encode_cell({
				c: colIdx,
				r: 0
			})] = {
				t: 's',
				v: column.text
			};
		});

		Ext.Array.each(data, function(record, rowIdx) {
			Ext.Array.each(xlsColumns, function(column, colIdx) {
				var value = record.get(column.dataIndex);

				ws[window.XLSX.utils.encode_cell({
					c: colIdx,
					r: rowIdx + 1
				})] = me.writeValue(value, column, record, rowIdx, colIdx);
			});
		});

		ws['!ref'] = window.XLSX.utils.encode_range({
			s: {
				c: 0,
				r: 0
			},
			e: {
				c: xlsColumns.length - 1,
				r: data.length
			}
		});

		return ws;
	},

	convertDate: function(v, date1904) {
		if (date1904) {
			v += 1462;
		}
		var epoch = Date.parse(v);
		return (epoch - new Date(Date.UTC(1899, 11, 30))) / (24 * 60 * 60 * 1000);
	},

	getRecordData: function(record, operation) {
		return record;
	}
});
