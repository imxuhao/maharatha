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
		}, this, [target], 0);

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
				maskable.mask(
					'Drag and drop files here to upload',
					''
				);
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
	    if (files) {
	        var regExpress = new RegExp('(\.\)+(.xls|.xlsx)$'),
	            invalidCount = 0;
	        
	        for (var i = 0; i < files.length; i++) {
	            var fileName = files[i].name;
	            if (!regExpress.test(fileName)) {
	                var erMsg = app.localize('InvalidFileName').initCap();
	                abp.notify.warn(erMsg.format(erMsg,fileName) +
	                    ' ' +
	                    app.localize('Template_FileType_Warn').initCap(),
	                    app.localize('Error'));
                    invalidCount += 1;
                    break;
                }
            }
            if (invalidCount > 0) {
                target.unmask();
                return;
            }
        }
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
		}
		else {
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
