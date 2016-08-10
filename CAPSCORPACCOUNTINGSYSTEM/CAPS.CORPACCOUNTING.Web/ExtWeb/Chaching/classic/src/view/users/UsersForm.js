
Ext.define('Chaching.view.users.UsersForm',
{
    extend: 'Chaching.view.common.form.ChachingFormPanel',
    alias: ['widget.users.create', 'widget.users.edit'],
    requires: [
        'Chaching.view.users.UsersFormController'
    ],
    modulePermissions: {
        read: abp.auth.isGranted('Pages.Administration.Users'),
        create: abp.auth.isGranted('Pages.Administration.Users.Create'),
        edit: abp.auth.isGranted('Pages.Administration.Users.Edit'),
        destroy: abp.auth.isGranted('Pages.Administration.Users.Delete')
    },
    controller: 'users-usersform',
    name: 'Administration.Users',
    openInPopupWindow: false,
    hideDefaultButtons: false,
    layout: 'fit',
    scrollable: true,
    border: false,
    showFormTitle: false,
    displayDefaultButtonsCenter: true,
    defaults: {
        bodyStyle: { 'background-color': 'trasparent' },
        labelAlign: 'left'
    },
    defaultFocus: 'textfield#name',
    popupWndSize: {
        height: '90%',
        width: '90%'
    },
    listeners: {
        resize: 'onUserFormResize',
        afterrender: 'onFormAfterRender'
    },
    items: [
        {
            xtype: 'tabpanel',
            ui: 'submenuTabs',
            tabPosition: 'left',
            tabRotation: 2,
            items: [

                    {
                        xtype: 'tabpanel',
                        ui: 'formTabPanels',
                        title: abp.localization.localize("CreateNewUser"),
                        items: [
                            {
                                title: abp.localization.localize("UserInformations"),
                                padding: '0 10 0 10',
                                scrollable: true,
                                iconCls: 'fa fa-user',
                                defaultFocus: 'textfield#name',
                                layout: 'column',
                                defaults: {
                                    labelWidth: 100
                                },
                                items: [
                                    {
                                        xtype: 'panel',
                                        //layout:'column',
                                        columnWidth: .5,
                                        items: [
                                            {
                                                xtype: 'hiddenfield',
                                                name: 'id',
                                                value: 0
                                            },
                                            {
                                                xtype: 'textfield',
                                                name: 'name',
                                                itemId: 'name',
                                                tabIndex:1,
                                                //labelWidth: 80,
                                                fieldLabel: app.localize('Name'),
                                                width: '100%',
                                                ui: 'fieldLabelTop',
                                                emptyText: app.localize('UName')
                                            },
                                            {
                                                xtype: 'textfield',
                                                name: 'userName',
                                                tabIndex: 3,
                                                fieldLabel: app.localize('UserName'),
                                                width: '100%',
                                                ui: 'fieldLabelTop',
                                                emptyText: app.localize('UUserName')
                                            },
                                            {
                                                xtype: 'checkbox',
                                                boxLabel: app.localize('SetRandomPassword'),
                                                name: 'setRandomPassword',
                                                reference: 'isSetRandomPassword',
                                                labelAlign: 'right',
                                                inputvalue: true,
                                                uncheckedValue: false,
                                                checked: false,
                                                readOnly: true,
                                                boxLabelCls: 'checkboxLabel',
                                                listeners: {
                                                    change: 'showRandomPassword'
                                                }
                                            },
                                            {
                                                layout: 'column',
                                                margin:'-10px 0 0 0',
                                                items: [
                                                    {
                                                        columnWidth: .5,
                                                        xtype: 'textfield',
                                                        name: 'password',
                                                        labelWidth: 70,
                                                        tabIndex: 5,
                                                        reference: 'password',
                                                        inputType: 'password',
                                                        fieldLabel: app.localize('Password'),
                                                        width: '100%',
                                                        ui: 'fieldLabelTop',
                                                        emptyText: app.localize('Password'),
                                                        bind: {
                                                            hidden: '{isSetRandomPassword.checked}'
                                                        }
                                                    },
                                                    {
                                                        columnWidth: .5,
                                                        xtype: 'textfield',
                                                        padding: '0 0 0 20',
                                                        name: 'passwordRepeat',
                                                        tabIndex: 6,
                                                        reference: 'passwordRepeat',
                                                        inputType: 'password',
                                                        fieldLabel: app.localize('PasswordRepeat'),
                                                        width: '100%',
                                                        labelWidth: 120,
                                                        ui: 'fieldLabelTop',
                                                        emptyText: app.localize('PasswordRepeat'),
                                                        bind: {
                                                            hidden: '{isSetRandomPassword.checked}'
                                                        },
                                                        /*
                                                      * Custom validator implementation - checks that the value matches what was entered into
                                                      * the password1 field.
                                                      */
                                                        validator: function (value) {
                                                            var password1 = this.previousSibling('[name=password]');
                                                            return (value === password1.getValue())
                                                                ? true
                                                                : 'Passwords do not match.'
                                                        }
                                                    }
                                                ]
                                            },
                                            {
                                                xtype: 'checkbox',
                                                padding:'10 0 0 0',
                                                boxLabel: app.localize('ShouldChangePasswordOnNextLogin'),
                                                name: 'shouldChangePasswordOnNextLogin',
                                                labelAlign: 'right',
                                                inputValue: true,
                                                uncheckedValue: false,
                                                checked: false,
                                                readOnly: true,
                                                boxLabelCls: 'checkboxLabel'
                                            }
                                        ]
                                    },
                                    {
                                        xtype: 'panel',
                                        //layout:'column',
                                        columnWidth: .5,
                                        padding: '0 0 0 20',
                                        items: [
                                            {
                                                xtype: 'textfield',
                                                name: 'surname',
                                                //labelWidth: 80,
                                                tabIndex: 2,
                                                fieldLabel: app.localize('Surname'),
                                                width: '100%',
                                                ui: 'fieldLabelTop',
                                                emptyText: app.localize('USurname')
                                            },
                                             {
                                                 xtype: 'textfield',
                                                 name: 'emailAddress',
                                                 tabIndex: 4,
                                                 fieldLabel: app.localize('EmailAddress'),
                                                 width: '100%',
                                                 ui: 'fieldLabelTop',
                                                 emptyText: app.localize('UEmailAddress')
                                             },
                                            {
                                                xtype: 'checkbox',
                                                boxLabel: app.localize('SendActivationEmail'),
                                                name: 'sendActivationEmail',
                                                labelAlign: 'right',
                                                tabIndex: 7,
                                                inputValue: true,
                                                checked: true,
                                                boxLabelCls: 'checkboxLabel'
                                            }, {
                                                xtype: 'checkbox',
                                                boxLabel: app.localize('Active'),
                                                tabIndex: 8,
                                                name: 'isActive',
                                                labelAlign: 'right',
                                                inputValue: true,
                                                checked: true,
                                                boxLabelCls: 'checkboxLabel'
                                            }
                                        ]
                                    }
                                ]

                            }, {
                                title: abp.localization.localize("Roles"),
                                padding: '0 10 0 10',
                                iconCls: 'fa fa-object-group',
                                layout: 'column',
                                height: '100%',
                                items: [
                                    {
                                        columnWidth: .49,
                                        xtype: 'grid',
                                        cls: 'chaching-grid',
                                        itemId: 'rolesListGridItemId',
                                        height: '100%',
                                        scrollable: true,
                                        selType: 'checkboxmodel',
                                        columns: [
                                            { text: app.localize('RoleName'), dataIndex: 'displayName', flex: 1 },
                                            {
                                                text: app.localize('View'),
                                                itemId:'rolesViewButtonId',
                                                align: 'center',
                                                width: 80,
                                                renderer: Chaching.utilities.ChachingRenderers.viewUsersRole
                                            }
                                        ],
                                        store: Ext.create('Chaching.store.roles.RolesStore')
                                    },
                                    {
                                        columnWidth: .51,
                                        xtype: 'label',
                                        padding: '10 0 0 20',
                                        style: { color: '#cacaca', fontSize: '15px' },
                                        itemId: 'rolesMessageItemId',
                                        text: abp.localization.localize("RowClickMessage")
                                    },
                                    {
                                        columnWidth: .51,
                                        cls: 'chaching-grid',
                                        padding: '0 0 0 20',
                                        hidden: true,
                                        xtype: 'treepanel',
                                        name: 'permissions',
                                        itemId: 'permissionsListItemId',
                                        store: Ext.create('Chaching.store.roles.RolesTreeViewStore'),
                                        rootVisible: false,
                                        width: '100%',
                                        height: '100%',
                                        alwaysReload: false,
                                        scrollable: true,
                                        border: false,
                                        hideHeaders: false,
                                        columns: [
                                            {
                                                xtype: 'treecolumn',
                                                text: abp.localization.localize("Permissions"),
                                                dataIndex: 'displayName',
                                                flex: 1
                                            }
                                        ]
                                    }
                                ]
                            }, {
                                title: abp.localization.localize("LinkCompany"),
                                padding: '0 10 0 10',
                                iconCls: 'fa fa-external-link',
                                itemId: 'companyListTab',
                                disabled: true,
                                layout: 'column',
                                height: '100%',
                                items: [
                                    {
                                        columnWidth: 0.49,
                                        xtype: 'grid',
                                        cls: 'chaching-grid',
                                        itemId: 'companyListGridItemId',
                                        scrollable: true,
                                        height: '100%',
                                        selModel: {
                                            selType: 'checkboxmodel'
                                        },
                                        columns: [
                                            {
                                                text: abp.localization.localize("LinkCompanyRoles"),
                                                dataIndex: 'roleDisplayName',
                                                width: '80%'
                                            }, {
                                                text: app.localize('View'),
                                                width: '15%',
                                                itemId: 'linkCompanyRolesViewButtonId',
                                                renderer: Chaching.utilities.ChachingRenderers.viewUsersRole //addViewUsersLinkComp
                                            }
                                        ],
                                        store: Ext.create('Chaching.store.users.CompanyRoleStore'),
                                        features: [{ ftype: 'grouping' }]
                                    },
                                    {
                                        columnWidth: .51,
                                        xtype: 'label',
                                        padding: '10 0 0 20',
                                        style: { color: '#cacaca', fontSize: '15px' },
                                        itemId: 'linkCompanyMessageItemId',
                                        text: abp.localization.localize("RowClickMessage")
                                    },
                                    {
                                        columnWidth: 0.51,
                                        cls: 'chaching-grid',
                                        hidden: true,
                                        padding: '0 0 0 20',
                                        xtype: 'treepanel',
                                        name: 'permissions',
                                        itemId: 'permissionsCompanyListItemId',
                                        store: new Chaching.store.roles.RolesTreeViewStore(),
                                        rootVisible: false,
                                        width: '100%',
                                        height: '100%',
                                        alwaysReload: false,
                                        scrollable: true,
                                        border: false,
                                        hideHeaders: false,
                                        columns: [
                                            {
                                                xtype: 'treecolumn',
                                                text: abp.localization.localize("Permissions"),
                                                dataIndex: 'displayName',
                                                flex: 1
                                            }
                                        ]
                                    }
                                ]
                            }
                        ]
                    },
                    {
                        xtype: 'tabpanel',
                        ui: 'formTabPanels',
                        title: abp.localization.localize("UserSecuritySettings"),
                        itemId: 'userSecuritySettingsItemId',
                        items: [
                            {
                                title: abp.localization.localize("CorporateCoaSecurity"),
                                margin: '0 !important',
                                scrollable: true,
                                iconCls: 'fa fa-wrench',
                                defaultFocus: 'combobox#coa',
                                layout: 'column',
                                items: [
                                    {
                                        columnWidth: 1.0,
                                        xtype: 'combobox',
                                        name: 'coa',
                                        reference: 'coaCombo',
                                        padding: '5 0 0 15',
                                        fieldLabel: app.localize('ChartOfAccount'),
                                        labelWidth:120,
                                        maxWidth: 450,
                                        //anchor: '47%',
                                        ui: 'fieldLabelTop',
                                        displayField: 'description',
                                        valueField: 'coaId',
                                        emptyText: app.localize('SelectOption'),
                                        queryMode: 'local',
                                        store: Ext.create('Chaching.store.financials.accounts.ChartOfAccountStore'),
                                        listeners: {
                                            change: 'onCorporateCoaSelect'
                                        }
                                    },
                                    {
                                        columnWidth: 1.0,
                                        xtype: 'chachingGridDragDrop',
                                        itemId: 'corporateCOASecurityGridItemId',
                                        padding: '5 0 0 0',
                                        leftTitle: '',
                                        rightTitle: '',
                                        hidden:false,
                                        columns: [
                                            {
                                                xtype: 'gridcolumn',
                                                text: app.localize('AccountNumber'),
                                                dataIndex: 'accountNumber',
                                                sortable: true,
                                                groupable: true,
                                                width: '47%',
                                                filterField: {
                                                    xtype: 'textfield',
                                                    width: '100%'
                                                }
                                            }, {
                                                xtype: 'gridcolumn',
                                                text: app.localize('AccountName'),
                                                dataIndex: 'caption',
                                                sortable: true,
                                                groupable: true,
                                                width: '47%',
                                                filterField: {
                                                    xtype: 'textfield',
                                                    width: '15%'
                                                }
                                            }
                                        ],
                                        // store: 'Chaching.store.financials.accounts.ChartOfAccountStore',
                                        loadStoreOnCreate: false,
                                        leftStore: 'Chaching.store.users.AccountSecurityLeftStore',
                                        rightStore: 'Chaching.store.users.AccountSecurityRightStore',
                                        requireMultiSearch: true,
                                        rangeSelectorConfig: {
                                            entityName: '',
                                            propertyName: 'accountNumber'
                                        },
                                        selModelConfig: {
                                            selType: 'chachingCheckboxSelectionModel',
                                            injectCheckbox: "first",
                                            headerWidth: '5%',
                                            mode: 'MULTI',
                                            showHeaderCheckbox: false
                                        },
                                        doSaveOperation: function (direction, records) {
                                            var isActive = direction === "leftToRight" ? true : false,
                                                wasActive = direction === "rightToLeft" ? true : false;
                                            for (var i = 0; i < records.length; i++) {
                                                var rec = records[i];
                                                rec.set('isActive', isActive);
                                                rec.set('wasActive', wasActive);
                                            }
                                        }
                                    }
                                    
                                ]
                            },
                            {
                                title: abp.localization.localize("ProjectCoaSecurity"),
                               // padding: '0 10 0 10',
                                iconCls: 'fa fa-unlock-alt',
                                layout: 'column',
                                height: '100%',
                                items: [
                                 {
                                     columnWidth: 1.0,
                                     xtype: 'combobox',
                                     name: 'projectCoa',
                                     reference: 'projectCoaCombo',
                                     padding: '5 0 0 15',
                                     fieldLabel: app.localize('ProjectChartOfAccount'),
                                     labelWidth: 180,
                                     maxWidth: 450,
                                     //width: '50%',
                                     ui: 'fieldLabelTop',
                                     displayField: 'description',
                                     valueField: 'coaId',
                                     emptyText: app.localize('SelectOption'),
                                     queryMode: 'local',
                                     store: Ext.create('Chaching.store.projects.projectmaintenance.ProjectCoaStore'),
                                     listeners: {
                                         change: 'onProjectCoaSelect'
                                     }
                                 },
                                    {
                                        columnWidth: 1.0,
                                        xtype: 'chachingGridDragDrop',
                                        itemId: 'projectCOASecurityGridItemId',
                                        padding: '5 0 0 0',
                                        leftTitle: '',
                                        rightTitle: '',
                                       // hidden: true,
                                        columns: [
                                            {
                                                xtype: 'gridcolumn',
                                                text: app.localize('AccountNumber'),
                                                dataIndex: 'accountNumber',
                                                sortable: true,
                                                groupable: true,
                                                width: '47%',
                                                filterField: {
                                                    xtype: 'textfield',
                                                    width: '100%'
                                                }
                                            }, {
                                                xtype: 'gridcolumn',
                                                text: app.localize('AccountName'),
                                                dataIndex: 'caption',
                                                sortable: true,
                                                groupable: true,
                                                width: '47%',
                                                filterField: {
                                                    xtype: 'textfield',
                                                    width: '15%'
                                                }
                                            }
                                        ],
                                        // store: 'Chaching.store.financials.accounts.ChartOfAccountStore',
                                        loadStoreOnCreate: false,
                                        leftStore: 'Chaching.store.users.AccountSecurityLeftStore',
                                        rightStore: 'Chaching.store.users.AccountSecurityRightStore',
                                        requireMultiSearch: true,
                                        rangeSelectorConfig: {
                                            entityName: '',
                                            propertyName: 'accountNumber'
                                        },
                                        selModelConfig: {
                                            selType: 'chachingCheckboxSelectionModel',
                                            injectCheckbox: "first",
                                            headerWidth: '5%',
                                            mode: 'MULTI',
                                            showHeaderCheckbox: false
                                        },
                                        doSaveOperation: function (direction, records) {
                                            var isActive = direction === "leftToRight" ? true : false,
                                                wasActive = direction === "rightToLeft" ? true : false;
                                            for (var i = 0; i < records.length; i++) {
                                                var rec = records[i];
                                                rec.set('isActive', isActive);
                                                rec.set('wasActive', wasActive);
                                            }
                                        }
                                    }

                                ]   // End of item ProjectCoa
                            },
                            {
                                title: abp.localization.localize("ProjectSecurity"),
                               // padding: '0 10 0 10',
                                iconCls: 'fa fa-steam',
                                layout: 'column',
                                height: '100%',
                                items: [
                                    {
                                        columnWidth: 1.0,
                                        xtype: 'chachingGridDragDrop',
                                        itemId: 'projectSecurityGridItemId',
                                        padding: '5 0 0 0',
                                        leftTitle: '',
                                        rightTitle: '',
                                        columns: [
                                            {
                                                xtype: 'gridcolumn',
                                                text: app.localize('ProjectNumber'),
                                                dataIndex: 'jobNumber',
                                                sortable: true,
                                                groupable: true,
                                                width: '47%',
                                                filterField: {
                                                    xtype: 'textfield',
                                                    width: '100%'
                                                }
                                            }, {
                                                xtype: 'gridcolumn',
                                                text: app.localize('ProjectName'),
                                                dataIndex: 'caption',
                                                sortable: true,
                                                groupable: true,
                                                width: '47%',
                                                filterField: {
                                                    xtype: 'textfield',
                                                    width: '15%'
                                                }
                                            }
                                        ],
                                        // store: 'Chaching.store.financials.accounts.ChartOfAccountStore',
                                        loadStoreOnCreate: false,
                                        leftStore: 'Chaching.store.users.ProjectSecurityLeftStore',
                                        rightStore: 'Chaching.store.users.ProjectSecurityRightStore',
                                        filtersConfig:{
                                            entity: '',
                                            searchTerm: 'false',
                                            comparator: 0,
                                            dataType: 3,
                                            property: 'IsDivision'
                                        },
                                        requireMultiSearch: true,
                                        rangeSelectorConfig: {
                                            entityName: '',
                                            propertyName: 'jobNumber'
                                        },
                                        selModelConfig: {
                                            selType: 'chachingCheckboxSelectionModel',
                                            injectCheckbox: "first",
                                            headerWidth: '5%',
                                            mode: 'MULTI',
                                            showHeaderCheckbox: false
                                        },
                                        doSaveOperation: function (direction, records) {
                                            var isActive = direction === "leftToRight" ? true : false,
                                                wasActive = direction === "rightToLeft" ? true : false;
                                            for (var i = 0; i < records.length; i++) {
                                                var rec = records[i];
                                                rec.set('isActive', isActive);
                                                rec.set('wasActive', wasActive);
                                            }
                                        }
                                    }

                                ]   // End of Project Security
                            },
                             {
                                 title: abp.localization.localize("DivisionSecurity"),
                                // padding: '0 10 0 10',
                                 iconCls: 'fa fa-steam',
                                 layout: 'column',
                                 height: '100%',
                                 items: [
                                     {
                                         columnWidth: 1.0,
                                         xtype: 'chachingGridDragDrop',
                                         itemId: 'divisionSecurityGridItemId',
                                         padding: '5 0 0 0',
                                         leftTitle: '',
                                         rightTitle: '',
                                         columns: [
                                             {
                                                 xtype: 'gridcolumn',
                                                 text: app.localize('DivisionNumber'),
                                                 dataIndex: 'jobNumber',
                                                 sortable: true,
                                                 groupable: true,
                                                 width: '47%',
                                                 filterField: {
                                                     xtype: 'textfield',
                                                     width: '100%'
                                                 }
                                             }, {
                                                 xtype: 'gridcolumn',
                                                 text: app.localize('DivisionName'),
                                                 dataIndex: 'caption',
                                                 sortable: true,
                                                 groupable: true,
                                                 width: '47%',
                                                 filterField: {
                                                     xtype: 'textfield',
                                                     width: '15%'
                                                 }
                                             }
                                         ],
                                         // store: 'Chaching.store.financials.accounts.ChartOfAccountStore',
                                         loadStoreOnCreate: false,
                                         leftStore: 'Chaching.store.users.ProjectSecurityLeftStore',
                                         rightStore: 'Chaching.store.users.ProjectSecurityRightStore',
                                         filtersConfig: {
                                             entity: '',
                                             searchTerm: 'true',
                                             comparator: 0,
                                             dataType: 3,
                                             property: 'IsDivision'
                                         },
                                         requireMultiSearch: true,
                                         rangeSelectorConfig: {
                                             entityName: '',
                                             propertyName: 'jobNumber'
                                         },
                                         selModelConfig: {
                                             selType: 'chachingCheckboxSelectionModel',
                                             injectCheckbox: "first",
                                             headerWidth: '5%',
                                             mode: 'MULTI',
                                             showHeaderCheckbox: false
                                         },
                                         doSaveOperation: function (direction, records) {
                                             var isActive = direction === "leftToRight" ? true : false,
                                                 wasActive = direction === "rightToLeft" ? true : false;
                                             for (var i = 0; i < records.length; i++) {
                                                 var rec = records[i];
                                                 rec.set('isActive', isActive);
                                                 rec.set('wasActive', wasActive);
                                             }
                                         }
                                     }

                                 ]   // End of Project Security
                             },
                             {
                                 title: abp.localization.localize("CreditCardSecurity"),
                                 //padding: '0 10 0 10',
                                 iconCls: 'fa fa-credit-card',
                                 layout: 'column',
                                 height: '100%',
                                 items: [
                                     {
                                         columnWidth: 1.0,
                                         xtype: 'chachingGridDragDrop',
                                         itemId: 'creditCardSecurityGridItemId',
                                         padding: '5 0 0 0',
                                         leftTitle: '',
                                         rightTitle: '',
                                         columns: [
                                              {
                                                  xtype: 'gridcolumn',
                                                  text: app.localize('CardNumber'),
                                                  dataIndex: 'cardNumber',
                                                  sortable: true,
                                                  groupable: true,
                                                  width: '47%',
                                                  filterField: {
                                                      xtype: 'textfield',
                                                      width: '100%'
                                                  }
                                              },
                                             {
                                                 xtype: 'gridcolumn',
                                                 text: app.localize('CardHolderName'),
                                                 dataIndex: 'cardHolderName',
                                                 sortable: true,
                                                 groupable: true,
                                                 width: '47%',
                                                 filterField: {
                                                    xtype: 'textfield',
                                                    width: '15%'
                                                }
                                             }
                                         ],
                                         // store: 'Chaching.store.financials.accounts.ChartOfAccountStore',
                                         loadStoreOnCreate: false,
                                         leftStore: 'Chaching.store.users.CreditCardSecurityLeftStore',
                                         rightStore: 'Chaching.store.users.CreditCardSecurityRightStore',
                                         requireMultiSearch: true,
                                         rangeSelectorConfig: {
                                             entityName: '',
                                             propertyName: 'cardNumber'
                                         },
                                         selModelConfig: {
                                             selType: 'chachingCheckboxSelectionModel',
                                             injectCheckbox: "first",
                                             headerWidth: '5%',
                                             mode: 'MULTI',
                                             showHeaderCheckbox: false
                                         },
                                         doSaveOperation: function (direction, records) {
                                             var isActive = direction === "leftToRight" ? true : false,
                                                 wasActive = direction === "rightToLeft" ? true : false;
                                             for (var i = 0; i < records.length; i++) {
                                                 var rec = records[i];
                                                 rec.set('isActive', isActive);
                                                 rec.set('wasActive', wasActive);
                                             }
                                         }
                                     }

                                 ]   // End of Project Security
                             },
                              {
                                  title: abp.localization.localize("BankSecurity"),
                                 //padding: '0 10 0 10',
                                  iconCls: 'fa fa-bank',
                                  layout: 'column',
                                  height: '100%',
                                  items: [
                                      {
                                          columnWidth: 1.0,
                                          xtype: 'chachingGridDragDrop',
                                          itemId: 'bankSecurityGridItemId',
                                          padding: '5 0 0 0',
                                          leftTitle: '',
                                          rightTitle: '',
                                          columns: [
                                              {
                                                  xtype: 'gridcolumn',
                                                  text: app.localize('AccountName'),
                                                  dataIndex: 'accountName',
                                                  sortable: true,
                                                  groupable: true,
                                                  width: '47%',
                                                  filterField: {
                                                      xtype: 'textfield',
                                                      width: '100%'
                                                  }
                                              }, {
                                                  xtype: 'gridcolumn',
                                                  text: app.localize('BankName'),
                                                  dataIndex: 'bankName',
                                                  sortable: true,
                                                  groupable: true,
                                                  width: '47%',
                                                  filterField: {
                                                      xtype: 'textfield',
                                                      width: '15%'
                                                  }
                                              }
                                          ],
                                          // store: 'Chaching.store.financials.accounts.ChartOfAccountStore',
                                          loadStoreOnCreate: false,
                                          leftStore: 'Chaching.store.users.BankSecurityLeftStore',
                                          rightStore: 'Chaching.store.users.BankSecurityRightStore',
                                          requireMultiSearch: true,
                                          rangeSelectorConfig: {
                                              entityName: '',
                                              propertyName: 'accountName'
                                          },
                                          selModelConfig: {
                                              selType: 'chachingCheckboxSelectionModel',
                                              injectCheckbox: "first",
                                              headerWidth: '5%',
                                              mode: 'MULTI',
                                              showHeaderCheckbox: false
                                          },
                                          doSaveOperation: function (direction, records) {
                                              var isActive = direction === "leftToRight" ? true : false,
                                                  wasActive = direction === "rightToLeft" ? true : false;
                                              for (var i = 0; i < records.length; i++) {
                                                  var rec = records[i];
                                                  rec.set('isActive', isActive);
                                                  rec.set('wasActive', wasActive);
                                              }
                                          }
                                      }

                                  ]   // End of BankSecurity
                              }
                        ]
                    }

            ]
        }
    ]
});
