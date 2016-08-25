/**
 * @class Gearbox.data.file.writer.Writer
 *        A specialized writer for working with CSV and xlsx files.
 *
 * @extends { Ext.data.writer.Writer}
 * @alternateClassName gearbox.data.file.Writer
 * @alias writer.file
 */
Ext.define('Gearbox.data.file.writer.Writer', {
	extend: 'Ext.data.writer.Writer',
	alternateClassName: [
		'Gearbox.data.file.Writer'
	],
	alias: 'writer.file',

	config: {
		nameProperty: 'mapping',
		writeAllFields: true,

		/**
		 * @cfg writeRecordId Set to true to include the records id field in the output.
		 * @type {Boolean=}
		 */
		writeRecordId: false
	},

	//
	/**
	 * @cfg model The {@link Ext.data.Model} to use.
	 * @type {Ext.data.Model}
	 */
	model: null,

	/**
	 * @cfg title The title of the file
	 * @type {String}
	 */
	title: null,

	/**
	 * @accessor Set the title of the file.
	 * @param {String} title
	 */
	setTitle: function(title) {
		this.title = title.substring(0, title.lastIndexOf('.'));
	},

	/**
	 * @accessor Returns the title of the file.
	 * @return {String}
	 */
	getTitle: function() {
		return this.title || '';
	},

	/**
	 * @method getRawColumns
	 *         Returns the columns of a set of data.
	 * @param  {Object} data The data to retrieve the columns from.
	 * @return {{dataIndex: String, text: String}}
	 */
	getRawColumns: function(data) {
		var head = Ext.isArray(data) && data.length > 0 ? data[0] : null,
			columns = [];

		if (head) {
			Ext.Array.each(head, function(item, key) {
				columns.push({
					dataIndex: key,
					text: key
				});
			});
		}

		return columns;
	},

	write: function() {
		return this.callParent(arguments);
	},

	getRecordData: function(record, operation) {
		var data = Ext.clone(record.data);
		delete data.id;
		return data;
	}
});
