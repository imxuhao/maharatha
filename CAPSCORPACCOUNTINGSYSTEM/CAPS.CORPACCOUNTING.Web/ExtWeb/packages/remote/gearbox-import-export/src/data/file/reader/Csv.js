/**
 * @class Gearbox.data.file.reader.Csv
 * The CSV reader is used to read data in CSV format. This usually happens as a
 * result of loading a Store - for example we might create something like this:
 *
 *    	var store = this.getStore();
 *
 *		Ext.Msg.prompt('Load CSV', 'Paste CSV file here', function(btn, data) {
 *			if (btn !== 'ok') {
 *				return;
 *			}
 *
 *			// set store reader to csv
 *			store.getProxy().setReader('file.csv');
 *
 *			// and load the new data
 *			store.loadRawData(data);
 *		}, this, true, store.rawData);
 *
 * @extends {Gearbox.data.file.reader.Reader}
 * @alias reader.file.csv
 */
Ext.define('Gearbox.data.file.reader.Csv', {
	extend: 'Gearbox.data.file.reader.Reader',
	alias: 'reader.file.csv',


	format: 'text',

	/**
	 * @private
	 * @property lineEnding
	 * The lineEnding the reader will use to split the data into lines
	 * @type {String}
	 */
	lineEnding: null,

	/**
	 * @private
	 * @property delimiter
	 * The delimiter the reader will use to split the lines into fields
	 * @type {String}
	 */
	delimiter: null,

	/**
	 * @property lineEndings
	 * Possible values for the line endings, they are tried in-order by {@link #guessLineEnding}.
	 * @type {Array}
	 */
	lineEndings: ['\r\n', '\r', '\n'],

	/**
	 * @property delimiters
	 * Possible values for the delimiters, they are tried in-order by {@link #guessLineEnding}.
	 * @type {Array}
	 */
	delimiters: [',', ';', '\t'],

	/**
	 * @method read
	 * Read raw csv data into a {@link Ext.data.ResultSet result set }
	 *
	 * @param  {string} data Raw data to read.
	 * @return {Ext.data.ResultSet} The resulting dataset
	 */
	read: function(data) {
		var lineEnding = this.lineEnding || this.guessLineEnding(data),
			delimiter = this.delimiter || this.guessDelimiter(data, lineEnding),
			parser,
			error;

		data = this.alwaysTrailingLineEnding(data, lineEnding);

		try {
			data = window.CSV.parse(data, {
				header: true,
				line: lineEnding,
				delimiter: delimiter
			});

			return this.callParent(arguments);
		}
		catch (e) {
			error = new Ext.data.ResultSet({
				total  : 0,
				count  : 0,
				records: [],
				success: false,
				message: e.message
			});

			this.fireEvent('exception', this, data, error);

			console.error('Unable to parse the provided CSV: ' + e.message);

			return error;
		}
	},

	/**
	 * @method guessLineEnding
	 * Guess the line ending character for the supplied data.
	 *
	 * @param  {string} data Raw data to guess from.
	 * @return {string} The guessed line ending.
	 */
	guessLineEnding: function(data) {
		var lineEndings = this.lineEndings,
			lineEndingsRegExp = new RegExp('(' + lineEndings.join('|') + ')', 'g'),
			matches = data.match(lineEndingsRegExp),
			maxIndex = 0,
			max = 0,
			lineEndingsCounts = Ext.Array.map(lineEndings, function(lineEnding, idx) {
				var count = 0;
				Ext.Array.forEach(matches, function(lineEndingMatch) {
					if (lineEnding === lineEndingMatch) {
						count++;
					}
				});
				if (count > max) {
					maxIndex = idx;
					max = count;
				}
				return count;
			});

		return lineEndings[maxIndex];
	},

	/**
	 * @method guessDelimiter
	 * Guess the delimiter character for the supplied data.
	 *
	 * @param  {string} data Raw data to guess from.
	 * @return {string} The guessed delimiter.
	 */
	guessDelimiter: function(data, lineEnding) {
		var delimiters = this.delimiters,
			delimiterRegExps = Ext.Array.map(delimiters, function(delimiter) {
				return new RegExp(delimiter, 'g');
			}),
			prevDelimiterCount = [],
			lines = data.split(lineEnding);

		Ext.each(lines, function(line) {
			var delimiterCount = Ext.Array.map(delimiterRegExps,
				function(delimiterRegExp) {
					return (line.match(delimiterRegExp) || []).length;
				}
			);

			if (Ext.Array.max(delimiterCount) > 0 &&
				Ext.Array.equals(prevDelimiterCount, delimiterCount)) {
				return false;
			}

			prevDelimiterCount = delimiterCount;
		});

		var max = Ext.Array.max(prevDelimiterCount),
			maxIndex = Ext.Array.indexOf(prevDelimiterCount, max),
			delimiter = delimiters[maxIndex];

		return delimiter;
	},

	/**
	 * @method alwaysTrailingLineEnding
	 * Ensure the data ends with a trailing line ending. This method doesn't
	 * append an extra line ending if the data already ends in the line ending.
	 *
	 * @param  {string} data Raw data to read.
	 * @return {string} Raw data ending with line ending.
	 */
	alwaysTrailingLineEnding: function(data, lineEnding) {
		var len = lineEnding.length;

		if (data.slice(-len) !== lineEnding) {
			data += lineEnding;
		}

		return data;
	}
});
