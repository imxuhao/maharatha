/**
 * @class Gearbox.data.file.reader.Xls
 * The XLS reader is used to read data in XLS format. This usually happens as a
 * result of loading a Store - for example we might create something like this:
 *
 *    	Ext.define('User', {
 *    		extend: 'Ext.data.Model',
 *	     	fields: ['id', 'name', 'email']
 *	    });
 *
 *     	var store = Ext.create('Ext.data.Store', {
 *      	model: 'User',
 *       	proxy: {
 *	        	type: 'ajax',
 *	         	url : 'users.xls',
 *	          		reader: {
 *	            		type: 'xls',
 *	              		record: 'user',
 *	                	root: 'users'
 *	                }
 *	        }
 *	    });
 *
 * The reader we set up is ready to read data from the server - it will accept
 * a string representing binary xls file with columns "id", "name" and "email"
 * as input.
 *
 * @extends {Gearbox.data.file.reader.Reader}
 * @alias {reader.file.xls}
 */
Ext.define('Gearbox.data.file.reader.Xls', {
	extend: 'Gearbox.data.file.reader.Reader',
	alias: 'reader.file.xls',

	/**
	 * @protected
	 * @property parserFnName
	 * The name of the function to use for parsing the input.
	 * @type {Array}
	 */
	parserFnName: 'XLS',

	/**
	 * @method read
	 * @param  {String} data   		The binary data string to read.
	 * @param  {Object} config 		Additional config.
	 * @return {Ext.data.ResultSet} The resulting dataset.
	 */
	read: function(data, config) {
		Ext.applyIf(config || {}, this.readerConfig || {});

		config = Ext.applyIf(config, {
			type: this.format || this.proxy.format || 'binary'
		});

		var me = this,
			parser = window[me.parserFnName],
			wb = parser.read(data, config),
			records = [],
			success = true;

		wb.SheetNames.forEach(function(sheetName) {
			var roa = parser.utils.sheet_to_row_object_array(
				wb.Sheets[sheetName]
			);

			if (roa.length > 0) {
				records = records.concat(roa);
			}
		});

		return this.callParent([records]);
	}
});
