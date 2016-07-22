Ext.define('Chaching.utilities.ChachingRenderers', {
    singleton: true,
    dateSearchFieldRenderer: function (value) {
        return Ext.Date.format(value, 'm/d/Y');
    },
    statusRenderer: function (val) {
        if (val) return 'YES';
        else return 'NO';
    },
    addButtonRenderer: function (value, cell) {
        var id = Ext.id();
        var widgetRec = cell.record;
        var widgetCol = cell.column;
        Ext.Function.defer(function () {
            var button = Ext.create('Ext.button.Button', {
                ui: 'actionMenuButton',
                pressed : false,
                scale: 'small',
                width: '35%',
                text: app.localize('Permissions'),
                iconCls: 'fa fa-sign-in',
                iconAlign: 'left',
                widgetRec: widgetRec,
                widgetCol: widgetCol,
                listeners: {
                    //click: gridController.ViewPermissions
                }
            });
            if (Ext.get(id)) {
                button.render(Ext.get(id));
            }
        }, 1);
        return '<div id="' + id + '"></div>';
        
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
                }
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
                height : 22,
                width: '100%',
                text: app.localize('Login'),
                iconCls: 'fa fa-sign-in',
                iconAlign: 'left',
                widgetRec: widgetRec,
                widgetCol: widgetCol,
                listeners: {
                    click: gridController.login
                }
            });
            if (Ext.get(id)) {
                button.render(Ext.get(id));
            }
        }, 1);
        return '<div id="' + id + '"></div>';

    },
    renderInMiliSeconds: function (value, cell) {
        if (value) {
            return value + " ms";
        }
        return '0 ms';
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
                width: 20,
                height : 20,
                // width: '40%',
                padding: '3px 7px 3px 7px;',
                iconCls: 'fa fa-search',
                iconAlign: 'left',
                widgetRec: widgetRec,
                widgetCol: widgetCol,
                gridControl: view
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
    renderTenant: function (value, metaData, record, rowIndex, colIndex) {
         if (record.get('connectionString') != undefined && record.get('connectionString') != null && record.get('connectionString') != "") {
             return '<div class= "fa fa-database" title ="'+ app.localize('HasOwnDatabase')+'"></div>' + " " + value;
        } 
        return value;
    },
    renderRole: function (value, metaData, record, rowIndex, colIndex) {
        return value + (record.get('isStatic') == true ?
                '<span class="staticRoleLabel" title ="' + app.localize('StaticRole_Tooltip') + '">' + app.localize('Static') + '</span>' : '')
                + (record.get('isDefault') == true ? '<span class="defaultRoleLabel" title ="' + app.localize('DefaultRole_Description') + '">' + app.localize('Default') + '</span>' : '');
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
    renderDateTimeSecondsWithoutAmPm: function (value) {
        if (value) {
            return moment(value).format(Chaching.utilities.ChachingGlobals.defaultDateTimeSecFormatWithoutAmPm);
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
    splitColumnMarkRenderer: function (val, meta, record, rowIndex) {
        if (val || record.get('isAccountingItemSplit'))
            return '<i class="fa fa-check font-green"  style="color:#32c5d2;" ></i>';
        else
            return '<i class="fa fa-unlink"  style="color:#2403a7;font-weight:600;"></i>';
    },
    languagesTextsEditIcon: function (val, meta, record, rowIndex) {
        return '<i class="fa fa-edit" ></i>';
    },
    languageIconRenderer: function (val, meta, record, rowIndex) {
        return '<i class="famfamfam-flag ' + record.get('icon') + '" style="display: inline-block;margin-right: 10px; !important" ></i><span>' + val + '</span>';
    },
    rendererHyperLink: function (val, meta, record, rowIndex, colIndex, store, view) {
        if (val) {
            var a = '<a style="padding-left:2px;text-decoration:underline !important;cursor:pointer;">' + val + '</a>';
            return a;
            ///var div = '<div class="fa fa-th" isHyperLink="true" style="overflow:hidden; text-overflow:ellipsis; color:white; cursor:pointer; width:98%; color:#3598DC !important;" title="' + val + '">' + a + '</div>';
            // return div;
        }
        return val;
    },
    renderFullAddress: function (val, meta, record, rowIndex, colIndex, store, view) {
        if (record && record._address) {
            var address = record.getAddress();
            if (address) {
                return address.get('line1') + ' ' + address.get('line2') + ' ' + address.get('line3');
            }
        }
        return val;
    },
    renderContactNumber: function (val, meta, record, rowIndex, colIndex, store, view) {
        if (record && record._address) {
            var address = record.getAddress();
            if (address) {
                return address.get('contactNumber');
            }
        }
        return val;
    },
    renderEmail: function (val, meta, record, rowIndex, colIndex, store, view) {
        if (record && record._address) {
            var address = record.getAddress();
            if (address) {
                return address.get('email');
            }
        }
        return val;
    },
    renderFirstAddress: function (val, meta, record, rowIndex, colIndex, store, view) {
        if (record && record._address) {
            var address = record.getAddress();
            if (address) {
                return address.get('line1');
            }
        }
        return val;
    },
    renderSecondAddress: function (val, meta, record, rowIndex, colIndex, store, view) {
        if (record && record._address) {
            var address = record.getAddress();
            if (address) {
                return address.get('line2');
            }
        }
        return val;
    },
    renderThirdAddress: function (val, meta, record, rowIndex, colIndex, store, view) {
        if (record && record._address) {
            var address = record.getAddress();
            if (address) {
                return address.get('line3');
            }
        }
        return val;
    },
    renderFourthAddress: function (val, meta, record, rowIndex, colIndex, store, view) {
        if (record && record._address) {
            var address = record.getAddress();
            if (address) {
                return address.get('line4');
            }
        }
        return val;
    },
    renderPhone1: function (val, meta, record, rowIndex, colIndex, store, view) {
        if (record && record._address) {
            var address = record.getAddress();
            if (address) {
                return address.get('phone1');
            }
        }
        return val;
    },
    renderCity: function (val, meta, record, rowIndex, colIndex, store, view) {
        if (record && record._address) {
            var address = record.getAddress();
            if (address) {
                return address.get('city');
            }
        }
        return val;
    },
    renderState: function (val, meta, record, rowIndex, colIndex, store, view) {
        if (record && record._address) {
                var address = record.getAddress();
                if (address) {
                    return address.get('state');
                }
            }
            return val;
        },
        renderPostalCode: function (val, meta, record, rowIndex, colIndex, store, view) {
            if (record && record._address) {
                var address = record.getAddress();
                if (address) {
                    return address.get('postalCode');
            }
        }
        return val;
    },
    renderEmployeeInnerTpl: function () {
        return '{employeeName}';
    },
    renderEmployeeDispalyTpl: function () {
        var xtemplate = Ext.create('Ext.XTemplate', [
            '<tpl for=".">',
            '{employeeName}',
            '</tpl>'
        ]);
        return xtemplate;
    },
    renderCustomerInnerTpl: function() {
        return '{name}}';
    },
    renderCustomerDispalyTpl: function () {
        var xtemplate = Ext.create('Ext.XTemplate', [
            '<tpl for=".">',
            '{name}}',
            '</tpl>'
        ]);
        return xtemplate;
    },
    renderMailToTag:function(email) {
        return '<a href="mailto:' + email + '" style="text-decoration:underline !important;color:#477EBF !important;font-size:15px;">' + email + '</a>';
    },
    amountsRenderer:function(value, meta, record, rowIndex, colIndex, store, view) {
        var isANegativeValue = false, newValue = "";
        var me = this;
        if (value) {
            value = Chaching.utilities.ChachingRenderers.unformattedNumber(value);
            if (Chaching.utilities.ChachingGlobals.displayNegAmtInBrackets === true && value < 0) {
                newValue = Math.abs(value);
                isANegativeValue = true;
                newValue = $.trim(Ext.util.Format.currency(newValue, ' '));
            } else {
                newValue = $.trim(Ext.util.Format.currency(value, ' '));
            }

            if (isANegativeValue === true) {
                newValue = Ext.String.format("(" + newValue + ")");
            }
        }
        newValue = newValue.replace(' ', '');
        return newValue;
    },
    unformattedNumber:function(value) {
        var newValue = "0.00";
        var close = ")";
        if (value != undefined) {
            value = value.toString();
            if (value !== null && value !== '') {
                newValue = value.replace("(", '-');
                newValue = newValue.replace(close, '');
                newValue = newValue.replace(/,/g, "");
            }
        }
        return newValue;
    },
    amountSummaryRenderer: function (value, summaryData, dataIndex) {
        var me = this.getView();
        var val = 0;
        var store = me.getStore();
        val = store.sum('amount');
        return '<b>' + Chaching.utilities.ChachingRenderers.amountsRenderer(val, null, null) + '</b>';
    },
    summaryTotalTextRenderer:function(value, summaryData, dataIndex) {
        return '<b>TOTAL</b>';
    }

});