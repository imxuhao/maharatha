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
        return new Blob([
            binary.buffer
        ], {
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
                return 'application/vnd.openxmlformats-' + 'officedocument.spreadsheetml.sheet';
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
        var r = new RegExp('^(?:[A-Za-z0-9+/]{4})*(?:[A-Za-z0-9+/]{2}==|[A-Za-z0-9+/]{3}=)?$');
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
        } else if (this.isBase64(data)) {
            return 'base64';
        }
        return 'text';
    }
});

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
            if (Ext.Array.indexOf([
                'hasMany'
            ], field.type.type) !== -1) {
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
            } else if (item === null) {
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
            if (item !== null && typeof item === 'object' && item.value) {
                fieldMapping = item;
            } else {
                // 2.2
                var mappingSource;
                if (typeof item === 'string') {
                    mappingSource = item;
                } else if (item !== null && typeof item === 'object' && item.source) {
                    mappingSource = item.source;
                }
                var displayName = Gearbox.l10n('node', model.entityName, 'field', dataIndex);
                // 2.3
                var fieldCls = me.findField(model, dataIndex),
                    fieldHeader = me.getModelHeader && me.getModelHeader(dataIndex),
                    candidates = Ext.Array.filter([
                        displayName,
                        dataIndex,
                        mappingSource,
                        fieldHeader
                    ], function(item) {
                        return item !== null && Ext.isDefined(item);
                    }),
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
                Ext.Array.sort(matches, function(a, b) {
                    var sa = a[0],
                        sb = b[0];
                    return sa < sb ? -1 : (sb < sa ? 1 : 0);
                });
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
        if (this.autoGuessMapping || (typeof this.autoGuessMapping === 'undefined' && store.autoGuessMapping)) {
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
        } else {
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
    lineEndings: [
        '\r\n',
        '\r',
        '\n'
    ],
    /**
	 * @property delimiters
	 * Possible values for the delimiters, they are tried in-order by {@link #guessLineEnding}.
	 * @type {Array}
	 */
    delimiters: [
        ',',
        ';',
        '\t'
    ],
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
            parser, error;
        data = this.alwaysTrailingLineEnding(data, lineEnding);
        try {
            data = window.CSV.parse(data, {
                header: true,
                line: lineEnding,
                delimiter: delimiter
            });
            return this.callParent(arguments);
        } catch (e) {
            error = new Ext.data.ResultSet({
                total: 0,
                count: 0,
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
            var delimiterCount = Ext.Array.map(delimiterRegExps, function(delimiterRegExp) {
                    return (line.match(delimiterRegExp) || []).length;
                });
            if (Ext.Array.max(delimiterCount) > 0 && Ext.Array.equals(prevDelimiterCount, delimiterCount)) {
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
            var roa = parser.utils.sheet_to_row_object_array(wb.Sheets[sheetName]);
            if (roa.length > 0) {
                records = records.concat(roa);
            }
        });
        return this.callParent([
            records
        ]);
    }
});

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
            SheetNames: [
                this.getTitle()
            ],
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
        if (column.xtype !== 'checkcolumn' && renderer && !Ext.isDate(value)) {
            var metaData = {
                    tdClass: ''
                },
                view = column.ownerCt.view;
            value = renderer.call(column, value, metaData, record, rowIdx, colIdx, view ? view.dataSource : null, view);
        }
        if (column.xtype === 'templatecolumn') {
            value = String(value).replace(/<\/?[^>]+\>/g, '');
        }
        var cell = {
                v: value
            };
        if (typeof cell.v === 'number') {
            cell.t = 'n';
        } else if (typeof cell.v === 'boolean') {
            cell.t = 'b';
        } else if (Ext.isDate(value)) {
            cell.t = 'n';
            cell.z = window.XLSX.SSF._table[14];
            cell.v = this.convertDate(cell.v);
        } else {
            cell.t = 's';
            if (cell.v === null) {
                cell.v = '';
            } else {
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
            if (!column.hidden && column.dataIndex && column.xtype !== 'actioncolumn' && column.xtype !== 'widgetcolumn' && column.xtype !== 'rownumberer') {
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
        } else if (operation.url) {
            return me.readUrl.apply(me, arguments);
        } else if (operation.data) {
            return me.readData.apply(me, arguments);
        } else if (me.data) {
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
        packet.readBlob(blob, function() {
            me.readPacket.apply(me, args);
        });
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
        Ext.callback(callback, scope || me, [
            operation
        ]);
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
        Ext.callback(callback, scope || this, [
            operation
        ]);
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
            reader.onMappingChange = Ext.Function.createSequence(reader.onMappingChange, this.onMappingChange, this);
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

/**
 * @class Gearbox.data.file.Store
 * @extends {Ext.data.Store}
 * @requires Gearbox.data.file.Proxy
 *
 * @mixin Ext.data.Store
 * @alias store.file
 *
 * A specialized {@link Ext.data.Store store} for working with data from files.
 *
 */
Ext.define('Gearbox.data.file.Store', {
    extend: 'Ext.data.Store',
    alias: 'store.file',
    mixins: [
        'Ext.data.Store'
    ],
    requires: [
        'Gearbox.data.file.Proxy'
    ],
    /**
	 * @cfg defaultProxyType The default type of proxy to use.
	 * @type {String}
	 */
    defaultProxyType: 'file',
    /**
	 * @cfg proxy The type of proxy to use.
	 * @type {String}
	 */
    proxy: 'file',
    /**
	 * @cfg mapping The mapping to use to map the file data onto the #model.
	 * @type {Object}
	 */
    mapping: null,
    /**
	 * @cfg autoGuessMapping Set to false to disable automatically guessing the
	 *      				 mapping of loaded files. Defaults to true.
	 * @type {Boolean=true}
	 */
    autoGuessMapping: true,
    /**
	 * @method constructor
	 * @param {Object} config
	 * @param {Ext.data.Model} [config.model] The model to use for this stores data.
	 * @param {Ext.data.Model[]} [config.data] Initial data to load.
	 * @param {Mixed} [config.rawData] Initial raw data to load.
	 * @return {Gearbox.data.file.Store}
	 */
    constructor: function(config) {
        config = config || {};
        this.callParent(arguments);
        if (config.mapping) {
            this.getProxy().setMapping(config.model);
        }
        if (config.model) {
            this.setModel(config.model);
        }
        if (config.data) {
            this.loadRawData(config.data, false, config.format || 'binary');
        }
        if (this.rawData) {
            this.loadRawData(this.rawData, false, config.format || 'binary');
        }
    },
    /**
	 * @method loadRawData
	 * @param {Object[]} data The CSV data you'd like to load into the Data store.
	 * @param  {Boolean} append Set to true if the data should be appended to the
	 *                          data already in the store.
	 * @param  {"binary"|"text"|"base64"} format The format of the data to read.
	 * @return {void}
	 */
    loadRawData: function(data, append, format) {
        this.getProxy().getReader().format = format;
        this.rawData = data;
        return this.callParent(arguments);
    },
    /**
	 * @method getNewRecords Returns the new records.
	 * @return {Ext.data.Model[]}
	 */
    getNewRecords: function() {
        return this.data.items;
    },
    /**
	 * @accessor setProxy Set the {@link Gearbox.data.file.Proxy proxy} for this store.
	 * @param {Object} proxy A config for the proxy.
	 */
    updateProxy: function(proxy) {
        if (proxy) {
            proxy.on('mappingchange', this.onMappingChange, this);
        }
    },
    /**
	 * @accessor updateModel
	 * @param {Ext.data.Model} model
	 * @param {Boolean} dontSetOnProxy {@link Gearbox.data.file.Proxy#setModel Don't propagate} the setting to the #proxy.
	 */
    updateModel: function(model, dontSetOnProxy) {
        this.model = model;
        if (this.getProxy()) {
            this.getProxy().setModel(model);
        }
        this.fireEvent('modelchange', this, model);
    },
    /**
	 * @accessor getMappedRecords
	 * @return {Ext.data.Model[]} Get records with mapped fields
	 */
    getMappedRecords: function() {
        return this.getProxy().getMappedRecords();
    },
    /**
	 * @accessor getMapping
	 * @return {Object} {@link #mapping mapping}
	 */
    getMapping: function() {
        var proxy = this.getProxy();
        return proxy.getMapping.apply(proxy, arguments);
    },
    /**
	 * @accessor setMapping
	 * @param {Object} mapping
	 * @param {Boolean=false} suppressEvent Suppress mappingchange event.
	 */
    setMapping: function(mapping, suppressEvent) {
        var result = this.getProxy().setMapping(mapping, suppressEvent);
        if (suppressEvent) {
            var mappedRecords = this.getMappedRecords();
            this.loadData(mappedRecords);
        }
        return result;
    },
    /**
	 * @method guessMapping
	 * @return {Object} Tries to guess the mapping from the data to the model.
	 *                  See {@link Gearbox.data.file.reader.Reader#guessMapping}
	 */
    guessMapping: function() {
        var proxy = this.getProxy();
        return proxy.guessMapping.apply(proxy, arguments);
    },
    /**
	 * @method onMappingChange Handler for when the proxy's mapping changes.
	 * @param  {Object} mapping The new mapping
	 * @param  {Ext.data.Model[]} records The data according to the new mapping
	 * @return {void}
	 */
    onMappingChange: function(proxy, mapping, records) {
        this.loadData(records);
        this.fireEvent('mappingchange', this, mapping, records);
    },
    /**
	 * @method bindDrop Handler for dropping files.
	 * @param  {HTMLElement} target The DOM element to listen to.
	 * @return {void}
	 */
    bindDrop: function(target) {
        var me = this,
            domElement = target.getEl().dom;
        domElement.ondrop = Ext.bind(function(target, event) {
            event.preventDefault();
            me.onDrop(target, event.dataTransfer.files);
        }, this, [
            target
        ], 0);
        this.onDragBind(target.getMaskTarget());
    },
    /**
	 * @method onDragBind Handler for when a file is dragged over an element.
	 * @param  {HTMLElement} maskable The HTML element to watch
	 * @return {void}
	 */
    onDragBind: function(maskable) {
        var curElement = null;
        document.ondragenter = function(event) {
            curElement = event.toElement;
            return false;
        }.bind(this);
        document.ondragleave = function(event) {
            if (curElement !== null) {
                curElement = null;
                return;
            }
            if (maskable.isMasked()) {
                maskable.unmask();
            }
            return false;
        }.bind(this);
        document.ondragover = function() {
            if (!maskable.isMasked()) {
                maskable.mask('Drag and drop files here to upload', '');
            }
            return false;
        }.bind(this);
        maskable.on('destroy', this.onDragUnbind, this);
    },
    /**
	 * @method  onDragUnbind Stop listening for drag events.
	 * @return {void}
	 */
    onDragUnbind: function() {
        document.ondragenter = null;
        document.ondragleave = null;
        document.ondragover = null;
    },
    /**
	 * @method destroy See #onDragUnbind.
	 * @return {void}
	 */
    destroy: function() {
        this.onDragUnbind();
    },
    /**
	 * @method onDrop Handler to process file drops.
	 * @param  {HTMLElement} target The HTML element the file was dropped on.
	 * @param  {Mixed[]} files The dropped files.
	 * @return {void}
	 */
    onDrop: function(target, files) {
        this.loadFiles(files);
        target.unmask();
    },
    /**
	 * @method loadFiles Loads the contents of an array of files into records.
	 *         			 See #loadFile
	 * @param  {Mixed} files The files to load.
	 * @return {void}
	 */
    loadFiles: function(files) {
        if (files.length) {
            this.loadFile(files[0]);
        }
    },
    /**
	 * @method loadFile Loads the contents of a file into records.
	 * @param  {Mixed} file The file to load.
	 * @return {void}
	 */
    loadFile: function(file) {
        var name = file.name,
            ext = 'csv',
            extIdx = name.lastIndexOf('.');
        if (extIdx !== -1) {
            ext = name.substring(extIdx + 1);
        }
        this.getProxy().setReader('file.' + ext);
        this.load({
            blob: file,
            format: 'binary'
        });
    },
    /**
	 * @method exportFile Exports a grid to a file
	 * @param  {Ext.grid.Panel} grid The grid to extract the data from
	 * @param  {String=} name The filename to export to, without extension. Optional.
	 * @param  {("csv"|"xlsx")=} ext The type of file to export. Defaults to "csv".
	 * @return {void}
	 */
    exportFile: function(grid, name, ext) {
        if (typeof ext !== 'undefined') {
            ext = 'file.' + ext;
        } else {
            ext = this.getProxy().getReader().type;
        }
        name = name || grid.title;
        this.getProxy().setWriter(ext);
        this.sync({
            title: name,
            columns: grid.columns,
            callback: function(batch) {
                batch.packet.save();
            }
        });
    }
});

