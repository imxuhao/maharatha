/**
 * @class Gearbox.data.file.Proxy
 *        Proxies are used by Stores to handle the loading and saving of
 *        {@link Ext.data.Model Model} data. Usually developers will not need to
 *        create or interact with proxies directly.
 *
 * @extends {Ext.data.proxy.Proxy}
 * @alias proxy.file
 *
 * @requires Gearbox.data.file.Packet
 * @requires Gearbox.data.file.reader.Reader
 * @requires Gearbox.data.file.reader.Csv
 * @requires Gearbox.data.file.reader.Xls
 * @requires Gearbox.data.file.reader.Xlsx
 * @requires Gearbox.data.file.writer.Writer
 * @requires Gearbox.data.file.writer.Csv
 * @requires Gearbox.data.file.writer.Xlsx
 */
Ext.define('Gearbox.data.file.Proxy', {
	extend: 'Ext.data.proxy.Memory',
	alias: 'proxy.file',

	requires: [
		'Gearbox.data.file.Packet',

		'Gearbox.data.file.reader.Reader',
		'Gearbox.data.file.reader.Csv',
		'Gearbox.data.file.reader.Xls',
		'Gearbox.data.file.reader.Xlsx',

		'Gearbox.data.file.writer.Writer',
		'Gearbox.data.file.writer.Csv',
		'Gearbox.data.file.writer.Xlsx'
	],

	/**
	 * @cfg defaultReaderType Default type for the #reader.
	 * @type {String}
	 */
	defaultReaderType: 'file',

	/**
	 * @cfg defaultWriterType Default type for the #writer.
	 * @type {String}
	 */
	defaultWriterType: 'file',

	/**
	 * @cfg reader The {@type of {@link Gearbox.data.file.reader.Reader reader} to use.
	 * @type {String}
	 */
	reader: 'file',

	/**
	 * @cfg writer The type of {@link Ext.data.writer.Writer writer} to use.
	 * @type {String}
	 */
	writer: 'file',

	/**
	 * @cfg isSynchronous
	 * @type {Boolean}
	 */
	isSynchronous: false,

	/**
	 * @cfg binary Set to true if the output is supposed to be binary data.
	 * @type {Boolean}
	 */
	binary: true,

	/**
	 * @cfg format The format for the output.
	 * @type {"binary"|"text"|"base64"}
	 */
	format: 'binary',

	packet: null,

	/**
	 * @property autoCreateModelFromHeader
	 * Automatically create model from raw header. Used by the reader.
	 * @type {Boolean}
	 */
	autoCreateModelFromHeader: false,

	/**
	 * @method read
	 *         Read data from a data source. Returns false if the reading was
	 *         not succesful. This usually means the source was neither
	 *         blob, url or data.
	 *
	 * @param  {Object}   operation The data source.
	 * @param  {Function} callback  A callback function.
	 * @param  {Object}   scope     The scope for the callback function.
	 * @return {Boolean}            True iff reading was succesful.
	 */
	read: function(operation, callback, scope) {
		var me = this;

		operation.packet = me.getPacket();

		if (operation.config) {
			if (operation.config.blob) {
				operation.blob = operation.config.blob;
			}
			if (operation.config.url) {
				operation.url = operation.config.url;
			}
			if (operation.config.data) {
				operation.data = operation.config.data;
			}
		}

		if (operation.blob) {
			return me.readBlob.apply(me, arguments);
		}
		else if (operation.url) {
			return me.readUrl.apply(me, arguments);
		}
		else if (operation.data) {
			return me.readData.apply(me, arguments);
		}
		else if (me.data) {
			operation.data = me.data;
			operation.format = me.format || 'binary';
			return me.readData.apply(me, arguments);
		}

		console.error('Read operation without blob, url or data');
		return false;
	},

	/**
	 * @method readBlob Read data from a blob.
	 * @param  {{blob: Object, packet: Gearbox.data.file.Packet}} operation The blob to read from.
	 * @return {void}
	 */
	readBlob: function(operation) {
		var me = this,
			args = arguments,
			blob = operation.blob,
			packet = operation.packet;

		delete operation.blob;

		packet.readBlob(
			blob,
			function() {
				me.readPacket.apply(me, args);
			}
		);
	},

	/**
	 * @experimental
	 * @method readUrl Not yet implemented.
	 * @param  {String} operation The URL to read the data from.
	 * @return {void} Error('Not yet implemented.');
	 */
	readUrl: function(operation) {
		throw new Error('Not yet implemented.');
	},

	/**
	 * @method readData Read
	 * @param  {{format: String, packet: Gearbox.data.file.Packet}} operation
	 *         The data source.
	 * @param  {Function} callback A callback function.
	 * @param  {Object} scope The scope for the callback function.
	 * @return {void}
	 */
	readData: function(operation, callback, scope) {
		var me = this,
			args = arguments,
			data = operation.data,
			format = operation.format,
			packet = operation.packet;

		delete operation.data;
		delete operation.format;

		packet.set(data, format);

		return me.readPacket.apply(me, args);
	},

	/**
	 * @method readPacket Read data from a {@link Gearbox.data.file.Packet packet}.
	 * @param  {Gearbox.data.file.Packet}   operation The packet to read from.
	 * @param  {Function} callback  A callback function.
	 * @param  {Object}   scope     The scope for the callback function.
	 * @return {void}
	 */
	readPacket: function(operation, callback, scope) {
		var me = this,
			reader = me.getReader();

		var resultSet = reader.readPacket(operation.packet);
		operation.setResultSet(resultSet);
		this.finishOperation(operation);

		Ext.callback(callback, scope || me, [operation]);
	},

	/**
	 * @method create Call the #writer to export data.
	 * @param  {Object}   operation The data source
	 * @param  {Function} callback  A callback function.
	 * @param  {Object}   scope     The scope of the callback function.
	 * @return {void}
	 */
	create: function(operation, callback, scope) {
		var request = new Ext.data.Request({
			operation: operation,
			callback: callback,
			scope: scope
		});

		this.getWriter().write(request);
		this.finishOperation(operation);

		Ext.callback(callback, scope || this, [operation]);
	},

	/**
	 * @method batch
	 * @inheritdoc
	 */
	batch: function(options) {
		options.batch = new Ext.data.Batch({
			proxy: this
		});

		options.batch.packet = Ext.create('Gearbox.data.file.Packet');
		options.batch.title = options.title;
		options.batch.columns = options.columns;

		return this.callParent(arguments);
	},

	update: function(operation) {
		this.finishOperation(operation);
	},

	erase: function(operation) {
		this.finishOperation(operation);
	},

	/**
	 * @accessor updateModel
	 * @param {Ext.data.Model} model The Model.
	 */
	updateModel: function(model) {
		this.callParent(arguments);
		this.getReader().setModel(model);

		/**
		 * @event modelchange
		 * @param {Gearbox.data.file.Proxy} proxy The proxy that fired the event
		 * @param {Ext.data.Model} model The new model of the proxy
		 */
		this.fireEvent('modelchange', this, this.model);
	},

	applyReader: function(reader) {
		reader = this.callParent(arguments);
		reader.setModel(this.getModel());
		return reader;
	},

	/**
	 * @accessor setReader
	 *           Sets the reader and listen for its
	 *           {@link Gearbox.data.file.reader.Reader#event-mappingchange mappingchange} event.
	 */
	updateReader: function(reader) {
		this.callParent(arguments);
		if (reader && reader.onMappingChange) {
			reader.onMappingChange = Ext.Function.createSequence(
				reader.onMappingChange, this.onMappingChange, this);
		}
	},

	/**
	 * @method onMappingChange
	 *         Handler for when the reader changes it's mapping and fires an
	 *         {@link Gearbox.data.file.reader.Reader#event-mappingchange mappingchange event}
	 *
	 * @param  {Object} mapping 				 The new {@ink Gearbox.data.file.reader.Reader#mapping mapping}
	 * @param  {Object} records    				 The records from the reader.
	 * @return {void}
	 */
	onMappingChange: function(mapping, records) {
		/**
		 * @event mappingchange fired when the mapping of the proxy's #reader changes.
		 * @param {Gearbox.data.file.Proxy} this
		 * @param {Object} mapping
		 * @param {Object} records
		 */
		this.fireEvent('mappingchange', this, mapping, records);
	},

	/**
	 * @accessor getPacket Returns a new Gearbox.data.file.Packet
	 * @return {Gearbox.data.file.Packet}
	 */
	getPacket: function() {
		if (!this.packet) {
			this.setPacket(Ext.create('Gearbox.data.file.Packet'));
		}

		return this.packet;
	},

	/**
	 * @accessor setPacket
	 * @param {Gearbox.data.file.Packet} packet #packet
	 */
	setPacket: function(packet) {
		this.packet = packet;
		return this;
	},

	getMappedRecords: function() {
		return this.getReader().getMappedRecords();
	},

	/**
	 * @accessor getMapping
	 * @return {Object} #mapping
	 */
	getMapping: function() {
		return this.getReader().getMapping();
	},

	/**
	 * @accessor setMapping
	 * @param {Object} mapping #mapping
	 * @param {Boolean=false} suppressEvent Suppress mappingchange event.
	 */
	setMapping: function(mapping, suppressEvent) {
		return this.getReader().setMapping(mapping, suppressEvent);
	},

	/**
	 * @method guessMapping
	 *         Delegate guessing the mapping of the data to the #reader.
	 *         See {@link Gearbox.data.file.reader.Reader#guessMapping}
	 *
	 * @return {Object} The guessed mapping.
	 */
	guessMapping: function() {
		return this.getReader().guessMapping();
	}
});
