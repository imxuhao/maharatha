Ext.define('Chaching.model.notes.NotesModel', {
    extend: 'Chaching.model.base.BaseModel',

    fields: [
        { name: 'notedObjectId', type: 'int', isPrimryKey: true },
        { name: 'id', type: 'int', mapping: 'notedObjectId' },
        { name: 'typeOfObjectId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'objectId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'notes', type: 'string' },
        {
            name: 'notedOn',
            type: 'string',
            convert: function (value, record) {
                if (!record.get('creationTime')) record.set('creationTime', new Date());
                return Chaching.utilities.ChachingRenderers.renderDateTimeWithFromNow(record.get('creationTime'));
            }
        }

    ]
});
