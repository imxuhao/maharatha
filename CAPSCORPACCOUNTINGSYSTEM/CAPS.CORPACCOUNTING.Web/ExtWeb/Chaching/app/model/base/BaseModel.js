/**
 * A Base Model or Entity represents some object that your application manages. For example, one
 * might define a Model for Users, Products, Cars, or other real-world object that we want
 * to model in the system. Models are used by {@link Ext.data.Store stores}, which are in
 * turn used by many of the data-bound components in Ext.
 *
 * # Fields
 *
 * Models are defined as a set of fields and any arbitrary methods and properties relevant
 * to the model. For example:
 *
 *     Ext.define('User', {
 *         extend: 'Chaching.model.base.BaseModel',
 *         fields: [
 *             {name: 'name',  type: 'string'},
 *             {name: 'age',   type: 'int', convert: null},
 *             {name: 'phone', type: 'string'},
 *             {name: 'alive', type: 'boolean', defaultValue: true, convert: null}
 *         ],
 *
 *         changeName: function() {
 *             var oldName = this.get('name'),
 *                 newName = oldName + " The Barbarian";
 *
 *             this.set('name', newName);
 *         }
 *     });
 *
 * Now we can create instances of our User model and call any model logic we defined:
 *
 *     var user = Ext.create('User', {
 *         id   : 'ABCD12345',
 *         name : 'Conan',
 *         age  : 24,
 *         phone: '555-555-5555'
 *     });
 *
 *     user.changeName();
 *     user.get('name'); //returns "Conan The Barbarian"
 *
 * By default, the built in field types such as number and boolean coerce string values
 * in the raw data by virtue of their {@link Ext.data.field.Field#method-convert} method.
 * When the server can be relied upon to send data in a format that does not need to be
 * converted, disabling this can improve performance. The {@link Ext.data.reader.Json Json}
 * and {@link Ext.data.reader.Array Array} readers are likely candidates for this
 * optimization. To disable field conversions you simply specify `null` for the field's
 * {@link Ext.data.field.Field#cfg-convert convert config}.
 *
 * ## The "id" Field and `idProperty`
 *
 * A Model definition always has an *identifying field* which should yield a unique key
 * for each instance. By default, a field named "id" will be created with a
 * {@link Ext.data.Field#mapping mapping} of "id". This happens because of the default
 * {@link #idProperty} provided in Model definitions.
 *
 * To alter which field is the identifying field, use the {@link #idProperty} config.
 *
 * # Validators
 *
 * Models have built-in support for field validators. Validators are added to models as in
 * the follow example:
 *
 *     Ext.define('User', {
 *         extend: 'Chaching.model.base.BaseModel',
 *         fields: [
 *             { name: 'name',     type: 'string' },
 *             { name: 'age',      type: 'int' },
 *             { name: 'phone',    type: 'string' },
 *             { name: 'gender',   type: 'string' },
 *             { name: 'username', type: 'string' },
 *             { name: 'alive',    type: 'boolean', defaultValue: true }
 *         ],
 *
 *         validators: {
 *             age: 'presence',
 *             name: { type: 'length', min: 2 },
 *             gender: { type: 'inclusion', list: ['Male', 'Female'] },
 *             username: [
 *                 { type: 'exclusion', list: ['Admin', 'Operator'] },
 *                 { type: 'format', matcher: /([a-z]+)[0-9]{2,3}/i }
 *             ]
 *         }
 *     });
 *
 * The derived type of `Ext.data.field.Field` can also provide validation. If `validators`
 * need to be duplicated on multiple fields, instead consider creating a custom field type.
 *
 * ## Validation
 *
 * The results of the validators can be retrieved via the "associated" validation record:
 *
 *     var instance = Ext.create('User', {
 *         name: 'Ed',
 *         gender: 'Male',
 *         username: 'edspencer'
 *     });
 *
 *     var validation = instance.getValidation();
 *
 * The returned object is an instance of `Ext.data.Validation` and has as its fields the
 * result of the field `validators`. The validation object is "dirty" if there are one or
 * more validation errors present.
 *
 * This record is also available when using data binding as a "pseudo-association" called
 * "validation". This pseudo-association can be hidden by an explicitly declared
 * association by the same name (for compatibility reasons), but doing so is not
 * recommended.
 *
 * The `{@link Ext.Component#modelValidation}` config can be used to enable automatic
 * binding from the "validation" of a record to the form fields that may be bound to its
 * values.
 *
 * # Associations
 *
 * Models often have associations with other Models. These associations can be defined by
 * fields (often called "foreign keys") or by other data such as a many-to-many (or "matrix").
 *
 * ## Foreign-Key Associations - One-to-Many
 *
 * The simplest way to define an association from one Model to another is to add a
 * {@link Ext.data.field.Field#cfg-reference reference config} to the appropriate field.
 *
 *      Ext.define('Post', {
 *          extend: 'Chaching.model.base.BaseModel',
 *
 *          fields: [
 *              { name: 'user_id', reference: 'User' }
 *          ]
 *      });
 *
 *      Ext.define('Comment', {
 *          extend: 'Chaching.model.base.BaseModel',
 *
 *          fields: [
 *              { name: 'user_id', reference: 'User' },
 *              { name: 'post_id', reference: 'Post' }
 *          ]
 *      });
 *
 *      Ext.define('User', {
 *          extend: 'Ext.data.Model',
 *
 *          fields: [
 *              'name'
 *          ]
 *      });
 *
 * The placement of `reference` on the appropriate fields tells the Model which field has
 * the foreign-key and the type of Model it identifies. That is, the value of these fields
 * is set to value of the `idProperty` field of the target Model.
 *
 * ### One-to-Many Without Foreign-Keys
 *
 * To define an association without a foreign-key field, you will need to use either the
 * `{@link #cfg-hasMany}` or `{@link #cfg-belongsTo}`.
 *
 *      Ext.define('Post', {
 *          extend: 'Chaching.model.base.BaseModel',
 *
 *          belongsTo: 'User'
 *      });
 *
 *      Ext.define('Comment', {
 *          extend: 'Ext.data.Model',
 *
 *          belongsTo: [ 'Post', 'User' ]
 *      });
 *
 *      // User is as above
 *
 * These declarations have changed slightly from previous releases. In previous releases
 * both "sides" of an association had to declare their particular roles. This is now only
 * required if the defaults assumed for names are not satisfactory.
 *
 * ## Foreign-Key Associations - One-to-One
 *
 * A special case of one-to-many associations is the one-to-one case. This is defined as
 * a `{@link Ext.data.field.Field#reference unique reference}`.
 *
 *      Ext.define('Address', {
 *          extend: 'Chaching.model.base.BaseModel',
 *
 *          fields: [
 *              'address',
 *              'city',
 *              'state'
 *          ]
 *      });
 *
 *      Ext.define('User', {
 *          extend: 'Chaching.model.base.BaseModel',
 *
 *          fields: [{
 *              name: 'addressId',
 *              reference: 'Address',
 *              unique: true
 *          }]
 *      });
 *
 * ## Many-to-Many
 *
 * The classic use case for many-to-many is a User and Group. Users can belong to many
 * Groups and Groups can contain many Users. This association is declared using the
 * `{@link #cfg-manyToMany}` config like so:
 *
 *
 *      Ext.define('User', {
 *          extend: 'Chaching.model.base.BaseModel',
 *
 *          fields: [
 *              'name'
 *          ],
 *
 *          manyToMany: 'Group'
 *      });
 *
 *      Ext.define('Group', {
 *          extend: 'Chaching.model.base.BaseModel',
 *
 *          fields: [
 *              'name'
 *          ],
 *
 *          manyToMany: 'User'
 *      });
 *
 * As with other associations, only one "side" needs to be declared.
 *
 * To manage the relationship between a `manyToMany` relationship, a {@link Ext.data.Session}
 * must be used.
 *
 * # Using a Proxy
 *
 * Models are great for representing types of data and relationships, but sooner or later we're going to want to load or
 * save that data somewhere. All loading and saving of data is handled via a {@link Ext.data.proxy.Proxy Proxy}, which
 * can be set directly on the Model:
 *
 *     Ext.define('User', {
 *         extend: 'Chaching.model.base.BaseModel',
 *         fields: ['id', 'name', 'email'],
 *
 *         proxy: {
 *             type: 'rest',
 *             url : '/users'
 *         }
 *     });
 *
 * Here we've set up a {@link Ext.data.proxy.Rest Rest Proxy}, which knows how to load and save data to and from a
 * RESTful backend. Let's see how this works:
 *
 *     var user = Ext.create('User', {name: 'Ed Spencer', email: 'ed@sencha.com'});
 *
 *     user.save(); //POST /users
 *
 * Calling {@link #save} on the new Model instance tells the configured RestProxy that we wish to persist this Model's
 * data onto our server. RestProxy figures out that this Model hasn't been saved before because it doesn't have an id,
 * and performs the appropriate action - in this case issuing a POST request to the url we configured (/users). We
 * configure any Proxy on any Model and always follow this API - see {@link Ext.data.proxy.Proxy} for a full list.
 *
 * Loading data via the Proxy is accomplished with the static `load` method:
 *
 *     //Uses the configured RestProxy to make a GET request to /users/123
 *     User.load(123, {
 *         success: function(user) {
 *             console.log(user.getId()); //logs 123
 *         }
 *     });
 *
 * Models can also be updated and destroyed easily:
 *
 *     //the user Model we loaded in the last snippet:
 *     user.set('name', 'Edward Spencer');
 *
 *     //tells the Proxy to save the Model. In this case it will perform a PUT request to /users/123 as this Model already has an id
 *     user.save({
 *         success: function() {
 *             console.log('The User was updated');
 *         }
 *     });
 *
 *     //tells the Proxy to destroy the Model. Performs a DELETE request to /users/123
 *     user.erase({
 *         success: function() {
 *             console.log('The User was destroyed!');
 *         }
 *     });
 * 
 * # HTTP Parameter names when using a {@link Ext.data.proxy.Ajax Ajax proxy}
 *
 * By default, the model ID is specified in an HTTP parameter named `id`. To change the
 * name of this parameter use the Proxy's {@link Ext.data.proxy.Ajax#idParam idParam}
 * configuration.
 *
 * Parameters for other commonly passed values such as
 * {@link Ext.data.proxy.Ajax#pageParam page number} or
 * {@link Ext.data.proxy.Ajax#startParam start row} may also be configured.
 *
 * # Usage in Stores
 *
 * It is very common to want to load a set of Model instances to be displayed and manipulated in the UI. We do this by
 * creating a {@link Ext.data.Store Store}:
 *
 *     var store = Ext.create('Ext.data.Store', {
 *         model: 'User'
 *     });
 *
 *     //uses the Proxy we set up on Model to load the Store data
 *     store.load();
 *
 * A Store is just a collection of Model instances - usually loaded from a server somewhere. Store can also maintain a
 * set of added, updated and removed Model instances to be synchronized with the server via the Proxy. See the {@link
 * Ext.data.Store Store docs} for more information on Stores.
 */
Ext.define('Chaching.model.base.BaseModel', {
    extend: 'Ext.data.Model',
    schema: {
        namespace: 'Chaching.model'
    },
    config: {
        searchEntityName: ''
    },
    fields: [
        //common fields in all entities
        { name: 'tenantId', type: 'int' },
        { name: 'organizationUnitId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'isDeleted', type: 'boolean' },
        { name: 'deletionTime', type: 'date' },
        { name: 'deleterUserId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'lastModificationTime', type: 'date', dateFormat: 'c' },
        { name: 'lastModifierUserId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'creationTime', type: 'date', dateFormat: 'c' },
        { name: 'creatorUserId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'createdUser', type: 'string' },

        //custom fields required for all entities
        { name: 'allowEdit', type: 'boolean', defaultValue: true },
        { name: 'allowDelete', type: 'boolean', defaultValue: true },
        { name: 'isRestricted', type: 'boolean', defaultValue: true },

        //local pass edit/delete action
        { name: 'passEdit', type: 'boolean', defaultValue: false },
        { name: 'passDelete', type: 'boolean', defaultValue: false }

    ]
});
