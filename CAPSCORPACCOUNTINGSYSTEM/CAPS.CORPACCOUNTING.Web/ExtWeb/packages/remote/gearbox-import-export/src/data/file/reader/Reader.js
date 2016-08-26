/*global FuzzySet*/
/**
 * @class Gearbox.data.file.reader.Reader
 *        The Gearbox Import Reader is a specialized {@link Ext.data.reader.Reader Ext Reader}
 *        for reading files. It can also {@link #guessMapping attempt to map} the field names of the data in
 *        the file to those in a given {@link Ext.data.Model}.
 * @alias reader.file
 *
 *
 * @extends {Ext.data.reader.Reader}
 * @alternateClassName Gearbox.data.file.Reader
 *
 */
Ext.define('Gearbox.data.file.reader.Reader', {
	extend: 'Ext.data.reader.Reader',
	alternateClassName: [
		'Gearbox.data.file.Reader'
	],
	alias: 'reader.file',

	idProperty: null,
	totalProperty: null,
	successProperty: null,

	/**
	 * @cfg model
	 * The model to use for this reader. This config is only required if the
	 * reader is being used without a proxy, otherwise the proxy will automatically set the model.
	 * @type {Ext.data.Model}
	 */

	format: 'binary',

	/**
	 * @evented
	 * @cfg mapping
	 *      The ({@link #guessMapping guessed}) mapping from
	 *      {@link #rawHeaders headers} to model fields.
	 *
	 * for example:
	 *    {
	 *	  	'name': {source: 'Name'},
	 *	   	'value': {value: 'Value'}
	 *	  }
	 *
	 * @type {Object}
	 */
	mapping: null,

	/**
	 * @property rawData The raw data the Reader is currently holding.
	 * @type {Object|String}
	 */
	rawData: null,

	/**
	 * @property rawHeaders The headers as they were read from the data source.
	 * @type {Array.<String>}
	 */
	rawHeaders: null,

	/**
	 * @property rawHeaders The headers as they were read in a FuzzySet.
	 * @type {FuzzySet}
	 */
	rawHeadersFuzzySet: null,

	/**
	 * @property matchScore
	 * The rating for how confident the reader is that its mapping is correct
	 * @type {Number}
	 */
	matchScore: 0.5,

	/**
	 * @property autoCreateModelFromHeader
	 * Automatically create model from raw header.
	 * @type {Boolean}
	 */
	autoCreateModelFromHeader: false,

	/**
	 * @cfg autoGuessMapping Set to false to disable automatically guessing the
	 *      				 mapping of loaded files. Defaults to true.
	 * @type {Boolean=true}
	 */
	autoGuessMapping: true,

	/**
	 * @method setModel Applies a model to the Reader.
	 * @param {Ext.data.Model} model The new model.
	 * @param {Boolean=} setOnProxy Set to true to propagate the setting to
	 *                              the #model. Defaults to false.
	 * @param {Boolean=} dontSetRawMapping
	 *        			 Set to false to prevent the Reader changing its
	 *        			 mapping accordingly. Defaults to true.
	 * @return {void}
	 */
	setModel: function(model, setOnProxy, dontSetRawMapping) {
		this.callParent(arguments);

		if (!dontSetRawMapping) {
			this.setMapping(this.getRawMapping());
		}
	},

	/**
	 * @method  getRawMapping Create a crude mapping from the #model and return it.
	 * @return {Object} {@link #mapping mapping}
	 */
	getRawMapping: function() {
		var me = this,
			model = me.getModel() || {},
			modelProto = model.prototype || {},
			fieldsProto = modelProto.fields || {},
			fields = fieldsProto.items || [],
			mapping = {};

		Ext.Array.each(fields, function(field, key) {
			var name = field.name,
				fieldMapping = field.importMapping || field.mapping || null;

			if (Ext.Array.indexOf([
					'id'
				], name) !== -1) {
				return;
			}

			if (Ext.Array.indexOf(['hasMany'], field.type.type) !== -1) {
				return;
			}

			if (!field.persist) {
				return;
			}

			mapping[name] = fieldMapping;
		});

		return mapping;
	},

	/**
	 * @accessor getMapping Returns the mapping for this Reader.
	 * @return {Object} The mapping.
	 */
	getMapping: function() {
		return this.mapping;
	},

	/**
	 * @protected
	 * @chainable
	 * @fires mappingchange
	 * @accessor Sets the mapping for data fields to model fields.
	 * @param {Object} mapping The mapping.
	 * @param {Boolean=false} suppressEvent Suppress mappingchange event.
	 */
	setMapping: function(mapping, suppressEvent) {
		var me = this,
			model = this.getModel(),
			fieldCls,
			newMapping = me.mapping || (me.mapping = {});

		if (Ext.Object.equals(mapping, newMapping)) {
			return;
		}

		Ext.Object.each(mapping, function(key, item) {
			fieldCls = me.findField(model, key);
			if (!fieldCls) {
				throw new Error('Can\'t set mapping for unknown field: ' + key);
			}

			if (typeof item === 'string') {
				item = {
					source: item
				};
			}
			else if (item === null) {
				item = {
					source: fieldCls.mapping || fieldCls.name
				};
			}

			newMapping[key] = item;
		});

		this.mapping = newMapping;

		/**
		 * @event mappingchange Fired when the mapping changes.
		 * @param {Gearbox.data.file.reader.Reader} reader The reader that fired the event.
		 * @param {Object} mapping The new {@link #mapping mapping}.
		 * @param {Object} data The raw data of the reader.
		 */
		if (!suppressEvent) {
			var records = this.getMappedRecords();
			this.onMappingChange(mapping, records);
		}

		return this.mapping;
	},

	onMappingChange: function(mapping, records) {
		this.fireEvent('mappingchange', this, mapping, records);
	},

	/**
	 * @method findField
	 *         Find a {@link Ext.data.Field field} in a {@link Ext.data.Model model}. <br/>
	 *         Returns undefined if the field could not be found.
	 * @param  {Ext.data.Model} model     	The model to search in.
	 * @param  {String} dataIndex 			The name of the field.
	 * @return {Ext.data.Field|undefined}   The field or undefined.
	 */
	findField: function(model, dataIndex) {
		return model.getField(dataIndex);
	},

	/**
	 * @template
	 * @method guessMapping
	 *         Try to guess the mapping from the raw data to the model.
	 * @param  {Boolean} dontSave
	 *         Set to false to just return the mapping,
	 *         instead of saving it with {@link #setMapping setMapping}.
	 * @return {Object} The guessed mapping.
	 */
	guessMapping: function(dontSave, suppressEvent) {
		// Steps:
		// 1. Get current (raw) mapping from the model
		// 2. Map the current (raw) mapping to new mapping
		// 2.1. If currently mapped as {value: %}, goto 3
		// 2.2. If mapped as string or {source: %}, use for finding target
		// 2.3. Look in field class: name, (import)Mapping, header?,
		//      if match goto 3
		// 2.4. If defaultValue: {value: defaultValue}, goto 3
		// 3. Save the new mapping if asked with .setMapping, save the result
		//    as the new mapping
		// 4. Return the new mapping

		// 1
		var me = this,
			mapping = me.getRawMapping(),
			newMapping = {},
			model = me.getModel(),
			importStore = me.importStore,
			fuzzySet = me.rawHeadersFuzzySet;

		// 2
		Ext.Object.each(mapping, function(dataIndex, item) {
			var fieldMapping;

			// 2.1
			if (item !== null && typeof item === 'object' && (item.value || item.source)) {
				fieldMapping = item;
			} 
			else {
				// 2.2
				var mappingSource;
				if (typeof item === 'string') {
					mappingSource = item;
				}
				else if (item !== null && typeof item === 'object' && item.source) {
					mappingSource = item.source;
				}

			    var displayName = dataIndex;//Gearbox.l10n('node', model.entityName, 'field', dataIndex);

				// 2.3
				var fieldCls = me.findField(model, dataIndex),
					fieldHeader = me.getModelHeader && me.getModelHeader(dataIndex),
					candidates = Ext.Array.filter(
						[displayName, dataIndex, mappingSource, fieldHeader],
						function (item) { return item !== null && Ext.isDefined(item); }),
					matches = [];

				if (fuzzySet) {
					Ext.each(candidates, function(candidate) {
						if (candidate) {
							var result = fuzzySet.get(candidate);
							if (result) {
								matches = matches.concat(result);
							}
						}
					});
				}

				Ext.Array.sort(
					matches,
					function (a, b) {
						var sa = a[0], sb = b[0];
						return sa < sb ? -1 : (sb < sa ? 1 : 0);
					}
				);
				var match = matches.pop();

				// TODO only map each source column to *one* target property

				if (match && match.length === 2) {
					var score = match[0],
						value = match[1];

					if (score > me.matchScore) {
						fieldMapping = {
							source: value
						};
					}
				}

				// 2.4
				if (!fieldMapping && fieldCls.defaultValue) {
					fieldMapping = {
						value: fieldCls.defaultValue
					};
				}
			}

			newMapping[dataIndex] = fieldMapping;
		});

		// 3
		if (!dontSave) {
			newMapping = me.setMapping(newMapping, suppressEvent);
		}

		// 4
		return newMapping;
	},

	/**
	 * @method getMappedRecords [description]

	 * @return {void}
	 */
	getMappedRecords: function() {
		var me = this,
			rawData = me.rawData,
			data = me.getData(rawData),
			root = me.getRoot(data),
			records = [];

		if (root) {
			records = me.extractData(root);
		}

		return records;
	},

	/**
	 * @accessor buildExtractors
	 * @hide
	 */
	buildExtractors: Ext.emptyFn,

	/**
	 * @accessor getIdProperty
	 * @hide
	 */
	getIdProperty: Ext.emptyFn,

	/**
	 * @accessor getId
	 * @hide
	 */
	getId: Ext.emptyFn,

	/**
	 * @accessor buildRecordDataExtractor
	 * @hide
	 */
	buildRecordDataExtractor: Ext.emptyFn,

	/**
	 * @accessor
	 * @hide
	 */
	createFieldAccessExpression: Ext.emptyFn,

	/**
	 * @accessor returns whether reading was successful.
	 * @param  {Object} root The root.
	 * @return {Boolean} success
	 */
	getSuccess: function(root) {
		return root.length > 0;
	},

	/**
	 * @accessor getCount returns the number of records read
	 * @param  {Object} root The raw data read.
	 * @return {Number} root.length
	 */
	getCount: function(root) {
		return root.length;
	},

	/**
	 * @accessor getTotal returns the number of records read
	 * @alias getCount
	 * @param  {Object} root The raw data read.
	 * @return {Number} root.length
	 */
	getTotal: function(root) {
		return root.length;
	},

	/**
	 * @protected
	 * @template
	 * @abstract
	 * @method read Read data from a file.
	 */

	/**
	 * @method convertRecordData
	 *         Reads data from a source object, and writes the converted data to dest.
	 *
	 * @param  {Object} dest
	 *         The object to write the converted data to.
	 * @param  {Object} source
	 *         The object to read the data from.
	 * @param  {Ext.data.Model} record
	 *         The data object containing the Model as read so far by the Reader.
	 *         Note that the Model may not be fully populated at this point as
	 *         the fields are read in the order that they are defined in your fields array.
	 *
	 * @return {void}
	 */
	convertRecordData: function(dest, source, record) {
		var me = this,
			fields = me.getModel().getFields();

		Ext.Array.each(fields, function(field, idx) {
			var name = field.name;
			if (name === 'id') {
				return;
			}

			dest[name] = this.readField(field, record, name);
		}, this);
	},

	/**
	 * @method readRecords
	 * @param  {Object} data The raw data to read records from
	 * @return {Ext.data.ResultSet}      The records read.
	 */
	readRecords: function(data) {
		var record = Ext.Array.from(data)[0] || {},
			proxy = this.proxy || {},
			store = proxy.store || {};

		this.rawData = data;
		this.rawHeaders = Ext.Object.getKeys(record);
		this.rawHeadersFuzzySet = new window.FuzzySet(this.rawHeaders);

		if (this.autoCreateModelFromHeader || proxy.autoCreateModelFromHeader) {
			var fields = Ext.Array.map(this.rawHeaders, function(header) {
				return {
					name: header
				};
			});

			var model = Ext.define('Ext.data.Store.ImplicitModel-' + Ext.id(), {
				extend: 'Ext.data.Model',
				fields: fields
			});

			this.model = model;
			proxy.model = model;
			if (proxy.store) {
				proxy.store.model = model;
			}
		}

		if (this.autoGuessMapping || (
				typeof this.autoGuessMapping === 'undefined' &&
				store.autoGuessMapping)) {
			this.guessMapping();
		}

		return this.callParent(arguments);
	},

	/**
	 * @method readPacket
	 *         Reads a packet of data.
	 * @param  {String|Object} packet 	A binary packet to read.
	 * @return {Object} 				The data read from the packet.
	 */
	readPacket: function(packet) {
		return this.read(packet.getText(), {
			type: 'binary'
		});
	},

	defaultRecordCreator: function() {
		var record = this.callParent(arguments);
		delete record.data.id;
		return record;
	},

	extractModelData: function(raw, fieldExtractorInfo) {
		var me = this,
			mapping = this.getMapping(),
			Model = this.getModel(),
			fields = Model.getFields(),
			data = {};

		Ext.Array.each(fields, function(field) {
			var name = field.name;
			data[name] = me.readField(field, raw, name);
		});

		return data;
	},

	readField: function(field, record, name) {
		name = name || field.name;

		var mapping = this.getMapping(),
			fieldMapping = mapping[name],
			value;

		if (fieldMapping && typeof fieldMapping !== 'object') {
			fieldMapping = {
				source: fieldMapping
			};
		}

		var mappingType = Ext.Object.getKeys(fieldMapping)[0],
			mappingValue = Ext.Object.getValues(fieldMapping)[0];

		if (mappingType === 'value') {
			value = mappingValue;
		}
		else {
			value = record[mappingValue || name];
		}

		if (typeof value === 'undefined' || value === null) {
			if (field.defaultValue) {
				value = field.defaultValue;
			}
		}

		return value;
	}
});
