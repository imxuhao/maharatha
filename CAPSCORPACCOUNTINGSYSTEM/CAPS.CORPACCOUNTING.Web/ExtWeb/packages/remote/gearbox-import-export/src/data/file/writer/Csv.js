/**
 * @class Gearbox.data.file.writer.Csv
 *        A writer for CSV files.
 *
 * @extends Gearbox.data.file.writer.Writer
 * @alias writer.file.csv
 */
Ext.define('Gearbox.data.file.writer.Csv', {
	extend: 'Gearbox.data.file.writer.Writer',

	alias: 'writer.file.csv',

	/**
	 * @method  writerRecords Write records to the CSV file.
	 * @param  {Object} request
	 * @param  {Ext.data.Operation} [request.operation]
	 * @param  {Ext.data.Batch} [request.operation.batch] The batch to write to
	 * @param  {Object[]} data The records to write.
	 * @return {Object} request.
	 */
	writeRecords: function(request, data) {
		var operation = request.operation || request.getOperation(),
			batch = operation.batch || operation.getBatch(),
			packet = batch.packet,
			title = (batch.title || 'Data') + '.csv',
			result = window.CSV.encode(data, {
				header: true
			});

		this.setTitle(title);
		batch.packet.setText(result);
		batch.packet.setName(title);

		return request;
	},

	/**
	 * @method writeValue
	 *         Write a value to a particular field in a data set.
	 * @param  {Object} data  	The data to write to the field.
	 * @param  {Ext.data.Model} field The field to write to
	 * @return {void}
	 */
	writeValue: function(data, field) {
		var name = field[this.nameProperty];
		if (name === null) {
			name = field.name;
		}

		if (typeof data[name] === 'undefined' || data[name] === null) {
			data[name] = '';
		}
	}
});
