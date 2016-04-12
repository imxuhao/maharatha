Ext.define('Chaching.utilities.ChachingRenderers', {
    singleton: true,
    dateSearchFieldRenderer: function (value) {
        return Ext.Date.format(value, 'm/d/Y');
    },
    statusRenderer: function (val) {
        if (val) return 'YES';
        else return 'NO';
    },
    unlinkedaccount: function (value, cell) {       
        var gridController = this.getController(),
            view = gridController.getView();
        var id = Ext.id();
        var widgetRec = cell.record;
        var widgetCol = cell.column;
        Ext.Function.defer(function () {
            var button = Ext.create('Ext.button.Button', {
                scale: 'small',
                width: '100%',
                iconCls: 'fa fa-trash',
                iconAlign: 'left',
                widgetRec: widgetRec,
                widgetCol: widgetCol,
                gridControl: view,
                listeners: {
                    click: gridController.unlinkUser
                },
            });
            if (Ext.get(id)) {
                button.render(Ext.get(id));
            }
        }, 1);
        return '<div id="' + id + '"></div>';

    },
    loginaccount: function (value, cell) {
        var id = Ext.id();
        var widgetRec = cell.record;
        var widgetCol = cell.column;
        Ext.Function.defer(function () {
            var button = Ext.create('Ext.button.Button', {
                ui: 'actionMenuButton',
                scale: 'small',
                width: '100%',
                text: app.localize('Login'),
                iconCls: 'fa fa-sign-in',
                iconAlign: 'left',
                widgetRec: widgetRec,
                widgetCol: widgetCol,
                //controller: gridController

            });
            if (Ext.get(id)) {
                button.render(Ext.get(id));
            }
        }, 1);
        return '<div id="' + id + '"></div>';

    },
});