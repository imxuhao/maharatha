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
        var gridController = this.getController(),
           view = gridController.getView();
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
                listeners: {
                    click: gridController.login
                },
            });
            if (Ext.get(id)) {
                button.render(Ext.get(id));
            }
        }, 1);
        return '<div id="' + id + '"></div>';

    },
    auditLogView: function (value, cell) {
        var gridController = this.getController(),
            view = gridController.getView();
        var id = Ext.id();
        var widgetRec = cell.record;
        var widgetCol = cell.column;
        Ext.Function.defer(function () {
            var button = Ext.create('Ext.button.Button', {
                scale: 'small',
                width: '40%',
                iconCls: 'fa fa-search',
                iconAlign: 'left',
                widgetRec: widgetRec,
                widgetCol: widgetCol,
                gridControl: view,
                //listeners: {
                //    click: gridController.auditLogView
                //},
            });
            if (Ext.get(id)) {
                button.render(Ext.get(id));
            }
        }, 1);
        return '<div id="' + id + '"></div>';

    },
    auditLogExceptionIcon: function (val, meta, record, rowIndex) {
        if (Ext.isEmpty(Ext.util.Format.trim(record.get('exception'))))
            return '<i class="fa fa-check-circle font-green"  style="color:#32c5d2" ></i>';
        else
            return '<i class="fa fa-warning font-yellow-gold"  style="color:#32c5d2"></i>';
    },// hh:mm:ss
    renderDateOnly: function (value) {
        if (value) {
            return moment(value).format(Chaching.utilities.ChachingGlobals.defaultDateFormat);
        }
        return '';
    },
    renderDateTime: function (value) {
        if (value) {
            return moment(value).format(Chaching.utilities.ChachingGlobals.defaultDateTimeFormat);
        }
        return '';
    },
    renderDateTimeSeconds: function (value) {
        if (value) {
            return moment(value).format(Chaching.utilities.ChachingGlobals.defaultDateTimeSecFormat);
        }
        return '';
    },
    renderDateTimeWithFromNow: function (value) {
        if (value) {
            return moment(value).fromNow() + ' (' + moment(value).format(Chaching.utilities.ChachingGlobals.defaultDateTimeSecFormat) + ')';
        }
        return '';
    },
    rightWrongMarkRenderer: function (val, meta, record, rowIndex) {
        if (val)
            return '<i class="fa fa-check font-green"  style="color:#32c5d2" ></i>';
        else
            return '<i class="fa fa-close font-yellow-gold"  style="color:#E00353"></i>';
    },
    languagesTextsEditIcon: function (val, meta, record, rowIndex) {
        return '<i class="fa fa-edit" ></i>';
    },
    languageIconRenderer: function (val, meta, record, rowIndex) {
        return '<i class="famfamfam-flag ' + record.get('icon') + '" style="display: inline-block;margin-right: 10px; !important" ></i><span>' + val + '</span>';
    },
    rendererHyperLink: function (val, meta, record, rowIndex, colIndex, store, view) {
        if (val) {
            var a = '<a style="color:white !important; padding-left:10px;">' + val + '</a>';
            var div = '<div class="fa fa-th" isHyperLink="true" style="overflow:hidden; color:white; cursor:pointer; height:25px; width:100%; color:white !important; background-color:#3598DC !important; padding-top:6px; padding-left:6px; border-radius:2px;" title="' + val + '">' + a + '</div>';
            return div;
        }
        return val;
    },
    renderFullAddress: function (val, meta, record, rowIndex, colIndex, store, view) {
        if (record) {
            var address = record.getAddress();
            if (address) {
                return address.get('line1') + ' ' + address.get('line2') + ' ' + address.get('line3');
            }
        }
        return value;
    },
    renderContactNumber: function (val, meta, record, rowIndex, colIndex, store, view) {
        if (record) {
            var address = record.getAddress();
            if (address) {
                return address.get('contactNumber');
            }
        }
        return value;
    },
    renderEmail: function (val, meta, record, rowIndex, colIndex, store, view) {
        if (record) {
            var address = record.getAddress();
            if (address) {
                return address.get('email');
            }
        }
        return value;
    },
    renderFirstAddress: function (val, meta, record, rowIndex, colIndex, store, view) {
        if (record) {
            var address = record.getAddress();
            if (address) {
                return address.get('line1');
            }
        }
        return value;
    },
    renderSecondAddress: function (val, meta, record, rowIndex, colIndex, store, view) {
        if (record) {
            var address = record.getAddress();
            if (address) {
                return address.get('line2');
            }
        }
        return value;
    },
    renderThirdAddress: function (val, meta, record, rowIndex, colIndex, store, view) {
        if (record) {
            var address = record.getAddress();
            if (address) {
                return address.get('line3');
            }
        }
        return value;
    },
    renderFourthAddress: function (val, meta, record, rowIndex, colIndex, store, view) {
        if (record) {
            var address = record.getAddress();
            if (address) {
                return address.get('line4');
            }
        }
        return value;
    },
    renderPhone1: function (val, meta, record, rowIndex, colIndex, store, view) {
        if (record) {
            var address = record.getAddress();
            if (address) {
                return address.get('phone1');
            }
        }
        return value;
    },
    renderCity: function (val, meta, record, rowIndex, colIndex, store, view) {
        if (record) {
            var address = record.getAddress();
            if (address) {
                return address.get('city');
            }
        }
        return value;
    },
    renderState: function (val, meta, record, rowIndex, colIndex, store, view) {
            if (record) {
                var address = record.getAddress();
                if (address) {
                    return address.get('state');
                }
            }
            return value;
        },
        renderPostalCode: function (val, meta, record, rowIndex, colIndex, store, view) {
            if (record) {
                var address = record.getAddress();
                if (address) {
                    return address.get('postalCode');
            }
        }
        return value;
    },
    renderEmployeeInnerTpl: function () {
        return '{firstName} {lastName}';
    },
    renderEmployeeDispalyTpl: function () {
        var xtemplate = Ext.create('Ext.XTemplate', [
            '<tpl for=".">',
            '{firstName} {lastName}',
            '</tpl>'
        ]);
        return xtemplate;
    },
    renderCustomerInnerTpl: function() {
        return '{firstName} {lastName} {{customerNumber}}';
    },
    renderCustomerDispalyTpl: function () {
        var xtemplate = Ext.create('Ext.XTemplate', [
            '<tpl for=".">',
            '{firstName} {lastName} {{customerNumber}}',
            '</tpl>'
        ]);
        return xtemplate;
    },
    renderMailToTag:function(email) {
        return '<a href="mailto:' + email + '" style="text-decoration:underline !important;color:#477EBF !important;font-size:15px;">' + email + '</a>';
    }

});