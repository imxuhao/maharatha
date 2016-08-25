Ext.define('ExampleGrid.model.Example', {
	extend: 'Ext.data.Model',

	schema: {
		namespace: 'ExampleGrid.model'
	},

	fields: [{
		name: 'id',
		persist: false
	}, {
		name: 'name',
		type: 'string',
		mapping: 'Name'
	}, {
		name: 'value',
		type: 'string',
		mapping: 'Value'
	}, {
		name: 'hobby',
		type: 'string',
		mapping: 'Hobby'
	}]
});
