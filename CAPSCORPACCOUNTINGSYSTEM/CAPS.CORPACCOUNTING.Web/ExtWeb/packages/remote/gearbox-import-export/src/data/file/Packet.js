/**
 * @class Gearbox.data.file.Packet
 *        The Packet class read data from binary, plaintext and Base64 
 *        sources and allows retrieving the data in any of the other formats. 
 *
 * @mixins Ext.util.Observable
 */
Ext.define('Gearbox.data.file.Packet', {
	mixins: {
		observable: 'Ext.util.Observable'
	},

	//

	config: {
		
		/**
		 * @cfg format 
		 *      The format of the input data. Will be guessed if none is provided.
		 *      Valid values are 'binary', 'text', or 'base64'.
		 * @type {"binary"|"text"|"base64"}
		 */
		format: 'binary',
		
		/**
		 * @cfg data 
		 *      The input data.
		 * @type {String}
		 */
		data: null,
		
		/**
		 * @private
		 * @cfg binary 
		 *      The binary value of the input data.
		 * @type {String}
		 */
		binary: null,

		/**
		 * @cfg name 
		 *      The name of the file.
		 * @type {String}
		 */
		name: '',
		
		/**
		 * @cfg extension 
		 *      The extension of the file.
		 * @type {String}
		 */
		extension: '',

		/**
		 * @cfg type 
		 *      The mime type of the file.
		 * @type {String}
		 */
		type: '',

		/**
		 * @cfg size 
		 *      The size of the binary data.
		 * @type {String}
		 */
		size: null
	},

	//
	/**
	 * @method constructor
	 * @param  {Object} config
	 * @return {Gearbox.data.file.Packet}
	 */
	constructor: function(config) {
		this.initConfig(config);

		if (this.hasData()) {
			this.set(this.getData(), this.getFormat());
		}

		this.mixins.observable.constructor.call(this, config);

		return this.callParent(arguments);
	},

	//
	/**
	 * @method reset
	 *         Reset the #binary, #type, #size and #name properties.
	 * @return {void} 
	 */
	reset: function() {
		this.setBinary(null);
		this.setType(null);
		this.setSize(0);
		this.setName('');
	},

	/**
	 * @method getReader Returns a new FileReader
	 * @return {FileReader}
	 */
	getReader: function() {
		var me = this,
			reader = new FileReader();

		return reader;
	},

	/**
	 * @method read 
	 *         Reads data from a blob and triggers a callback with the read data.
	 * @param  {Object}   operation 	The blob.
	 * @param  {Function} cb        
	 *         A callback function to call with the binary read data as the first argument.
	 * @return {void}
	 */
	read: function(operation, cb) {
		return this.readBlob(operation, cb);
	},

	/**
	 * @method readBlob
	 *         Read data from a Blob and store it in #binary.
	 *         If cb is set, call it with the binary data as its argument.
	 * @param  {Object}   blob 	The blob to read
	 * @param  {Function} cb    
	 *         Callback function for when te reader is done loading the file.
	 * @return {void}
	 */
	readBlob: function(blob, cb) {
		this.reset();

		var me = this,
			reader = this.getReader();

		reader.onload = function(e) {
			var result = e.target.result;

			me.setBinary(result);

			if (typeof cb === 'function') {
				cb(result);
			}
		};

		reader.readAsArrayBuffer(blob);
	},

	/**
	 * @accessor
	 * @param {Object} data 	The binary data.
	 */
	setBinary: function(data) {
		var len = 0;

		if (data) {
			if (!this.isBinary(data)) {
				data = new Uint8Array(data);
			}

			len = data.length;
		}

		this.setSize(len);
		this.binary = data;

		return this;
	},

	/**
	 * @accessor
	 * @param {Object} data 	Data in text format.
	 * @param {String} type 	The mime type of the read file.
	 */
	setText: function(data, type) {
		if (type) {
			this.setType(type);
		}

		var i = 0,
			len = data.length,
			buf = new ArrayBuffer(len),
			bufView = new Uint8Array(buf);

		for (; i < len; i++) {
			bufView[i] = data.charCodeAt(i);
		}

		this.setBinary(bufView);
		return this;
	},

	/**
	 * @accessor
	 * @param {Object} data 	Data in Base64 format.
	 * @param {String} type 	The mime type of the read file.
	 */
	setBase64: function(data, type) {
		if (type) {
			this.setType(type);
		}

		var i = 0,
			bytes = window.atob(data),
			len = bytes.length,
			buf = new ArrayBuffer(len),
			bufView = new Uint8Array(buf);

		for (; i < len; i++) {
			bufView[i] = bytes.charCodeAt(i);
		}

		this.setBinary(bufView);
		return this;
	},

	/**
	 * @method set
	 *         Process data, according to the given format and mime type. If no 
	 *         format is given, try to {@link #guessFormat guess} it. 
	 * @param {Object} data 	Data in text format.
	 * @param {"binary"|"text"|"base64"} format 	The data format.
	 * @param {String} type 	The mime type of the read file.
	 */
	set: function(data, format, type) {
		if (!format) {
			format = this.guessFormat(data);
		}

		this.setFormat(format);

		if (type) {
			this.setType(type);
		}

		switch (format) {
			case 'text':
				return this.setText(data);
			case 'base64':
				return this.setBase64(data);
			default:
				return this.setBinary(data);
		}
	},

	/**
	 * @method getText Returns the previously read data as plaintext.
	 * @return {String} {@link #binary binary} in plaintext format.
	 */
	getText: function() {
		var i = 0,
			data = this.getBinary(),
			len = data.length,
			text = '';

		for (; i < len; i++) {
			text += String.fromCharCode(data[i]);
		}

		return text;
	},

	/**
	 * @method getBase64 Returns the previously read data as a Base64 string.
	 * @return {String} {@link #binary binary} as Base64 String.
	 */
	getBase64: function() {
		return btoa(this.getText());
	},


	/**
	 * @method getBinary Returns the previously read data as binary data.
	 * @return {Mixed} {@link #binary binary}
	 */
	getBinary: function() {
		return this.binary;
	},


	/**
	 * @method getBlob Returns the previously read data as a Blob.
	 * @return {Object} {@link #binary binary} as Blob.
	 */
	getBlob: function(type) {
		type = type || this.getType();

		var binary = this.getBinary();

		return new Blob([binary.buffer], {
			type: type
		});
	},

	/**
	 * @method save 
	 *         Make the user download the #binary data.
	 * @return {void}
	 */
	save: function() {
		this.saveAs(this.getName());
	},

	/**
	 * @method saveAs 
	 *         Make the user download the #binary data, overriding the filename.
	 * @return {void}
	 */
	saveAs: function(name) {
		saveAs(this.getBlob(), name);
	},


	/**
	 * @method get 
	 *         Get the #binary data in a different format or type. 
	 * @return {Object|String}
	 */
	get: function(format, type) {
		var me = this,
			data = me.data;

		format = format || me.guessFormat(data);
		type = type || me.getType();

		switch (format) {
			case 'base64':
				return me.getBase64();
			case 'binary':
				return me.getBinary(type);
			default:
				return me.getText();
		}
	},

	/**
	 * @accessor
	 * @return {String} The mime #type.
	 */
	getType: function() {
		var type = this.type;
		if (type) {
			return type;
		}

		var extension = this.extension;
		switch (extension) {
			case 'csv':
				return 'text/csv';
			case 'xls':
				return 'application/vnd.ms-excel';
			case 'xlsx':
				return 'application/vnd.openxmlformats-' +
					'officedocument.spreadsheetml.sheet';
			default:
				return 'text/plain';
		}
	},


	/**
	 * @method isText 
	 *         Test whether the given data is in plaintext format.
	 * @param  {Object}  data The data to test
	 * @return {Boolean}
	 */
	isText: function(data) {
		return !this.isBinary(data) && !this.isBase64(data);
	},

	
	/**
	 * @method isBinary 
	 *         Test whether the given data is in binary format.
	 * @param  {Object}  data The data to test
	 * @return {Boolean}
	 */
	isBinary: function(data) {
		return (data instanceof Uint8Array || data instanceof Uint16Array);
	},

	
	/**
	 * @method isBase64 
	 *         Test whether the given data is in Base64 format.
	 *         Returns true if the data complies to the following regex:
	 *             '^(?:[A-Za-z0-9+/]{4})*(?:[A-Za-z0-9+/]{2}==|[A-Za-z0-9+/]{3}=)?$'
	 * @param  {Object}  data The data to test
	 * @return {Boolean}
	 */
	isBase64: function(data) {
		var r = new RegExp(
			'^(?:[A-Za-z0-9+/]{4})*(?:[A-Za-z0-9+/]{2}==|[A-Za-z0-9+/]{3}=)?$'
		);

		return r.test(data);
	},


	/**
	 * @method hasData 
	 *         Returns false if no data has been read.
	 * @return {Boolean}
	 */
	hasData: function() {
		return (this.data && this.data.length);
	},


	/**
	 * @method guessFormat
	 *         Attempts to guess the format of the data using #isBinary, #isBase64 and #isText.
	 * @param  {Object} data The data to inspect.
	 * @return {"binary"|"text"|"base64"}      
	 */
	guessFormat: function(data) {
		if (this.isBinary(data)) {
			return 'binary';
		}
		else if (this.isBase64(data)) {
			return 'base64';
		}

		return 'text';
	}
});