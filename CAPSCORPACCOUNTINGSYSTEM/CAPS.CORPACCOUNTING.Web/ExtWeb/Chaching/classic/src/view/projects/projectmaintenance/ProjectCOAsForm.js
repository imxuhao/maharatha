
Ext.define('Chaching.view.projects.projectmaintenance.ProjectCOAsForm',{
    extend: 'Chaching.view.common.form.ChachingFormPanel',
    alias:['widget.projects.projectmaintenance.projectcoas.create', 'widget.projects.projectmaintenance.projectcoas.edit'],
    requires: [
        'Chaching.view.projects.projectmaintenance.ProjectCOAsFormController'        
    ],
    controller: 'projects-projectmaintenance-projectcoasform',
    name: 'projectcoa',
    openInPopupWindow: false,
    hideDefaultButtons: false,
    layout: 'column',
    autoScroll: true,
    border: false,
    showFormTitle: true,
    displayDefaultButtonsCenter: true,
    titleConfig: {
        title: abp.localization.localize("CreatingNewProjectCOA").initCap()
    },
    modulePermissions: {
        read: abp.auth.isGranted('Pages.Projects.ProjectMaintenance.ProjectCOAs'),
        create: abp.auth.isGranted('Pages.Projects.ProjectMaintenance.ProjectCOAs.Create'),
        edit: abp.auth.isGranted('Pages.Projects.ProjectMaintenance.ProjectCOAs.Edit'),
        destroy: abp.auth.isGranted('Pages.Projects.ProjectMaintenance.ProjectCOAs.Delete')
    },
    items: [{
        xtype: 'hiddenfield',
        name: 'coaId',
        value: 0
    }, {
        columnWidth: .5,
        padding: '20 10 0 20',
        //bodyStyle: { 'background-color': '#F3F5F9' },
        defaults: {
           // labelAlign: 'top',
            blankText: app.localize('MandatoryToolTipText')
        },
        items: [{
            xtype: 'textfield',
            name: 'caption',
            itemId: 'caption',
            allowBlank: false,
            fieldLabel: app.localize('Caption').initCap(),
            width: '100%',
            ui: 'fieldLabelTop',
            emptyText: app.localize('MandatoryField')
        }, {
            xtype: 'textfield',
            name: 'description',
            itemId: 'description',
            allowBlank: false,
            fieldLabel: app.localize('description').initCap() ,
            width: '100%',
            ui: 'fieldLabelTop',
            emptyText: app.localize('MandatoryField')
        }, {
            xtype: 'checkbox',
            boxLabel: app.localize('IsApproved'),
            name: 'isApproved',
            labelAlign: 'right',
            inputValue: true,
            checked: true,
            boxLabelCls: 'checkboxLabel',
            hidden: false
        }, {
            xtype: 'checkbox',
            boxLabel: app.localize('IsPrivate'),
            name: 'isPrivate',
            labelAlign: 'right',
            inputValue: true,
            checked: true,
            boxLabelCls: 'checkboxLabel',
            hidden: false
        }]
    }, {
        columnWidth: .5,
        padding: '20 10 0 20',
        //bodyStyle: { 'background-color': '#F3F5F9' },
        defaults: {
            //labelAlign: 'top',
            labelWidth : 160,
            blankText: app.localize('MandatoryToolTipText')
        },
        items: [{
            xtype: 'combobox',
            name: 'rollupDivisionId',
            fieldLabel: app.localize('DefaultRollupJob').initCap(),
            width: '100%',
            ui: 'fieldLabelTop',
            emptyText: app.localize('DefaultRollupJob'),
            displayField: 'rollupDivision',
            valueField: 'rollupDivisionId',
            bind: {
                store: '{rollupDivisionList}'
            }
        }, {
            xtype: 'combobox',
            name: 'rollupAccountId',
            fieldLabel: app.localize('DefaultRollupAccount').initCap(),
            width: '100%',
            ui: 'fieldLabelTop',
            emptyText: app.localize('DefaultRollupAccount'),
            displayField: 'name',
            valueField: 'value',
            bind: {
                store: '{genericRollupAccountList}'
            }
        }, {
            xtype: 'checkbox',
            boxLabel: app.localize('IsCorporate'),
            name: 'isCorporate',
            labelAlign: 'right',
            inputValue: true,
            checked: false,
            readOnly: true,
            boxLabelCls: 'checkboxLabel'
        }, {
            xtype: 'checkbox',
            boxLabel: app.localize('IsNumeric'),
            name: 'isNumeric',
            labelAlign: 'right',
            inputValue: true,
            checked: true,
            boxLabelCls: 'checkboxLabel'
        }]
    }]
   
});
