/**
 * @class Gearbox.data.file.reader.Xlsx
 * @extends {Gearbox.data.file.reader.Xls}
 * @alias reader.file.xlsx
 *
 * 
 */
Ext.define('Gearbox.data.file.reader.Xlsx', {
	extend: 'Gearbox.data.file.reader.Xls',
	alias: 'reader.file.xlsx',

	/**
	 * @protected
	 * @property {String} parserFnName
	 * The name of the function to use for parsing the input.
	 */
	parserFnName: 'XLSX'
});