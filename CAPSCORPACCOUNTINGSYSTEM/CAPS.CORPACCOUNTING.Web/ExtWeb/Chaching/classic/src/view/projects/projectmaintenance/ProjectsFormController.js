Ext.define('Chaching.view.projects.projectmaintenance.ProjectsFormController', {
    extend: 'Chaching.view.common.form.ChachingFormPanelController',
    alias: 'controller.projects-projectmaintenance-projectsform',
    onAgencyChange:function(combo, newValue, oldValue, eOpts) {
        var me = this,
            view = me.getView();
        var comboStore = combo.getStore();
        if (comboStore) {
            var record = comboStore.findRecord('customerId', newValue);
            if (record) {
                var addresses = record.getAddresses();
                if (addresses) {
                    var form = view.getForm(),
                        agencyEmailField = form.findField('agencyEmail'),
                        agencyEmailDisplay = form.findField('agencyEmailDisplay'),
                        agencyAddressField = form.findField('agencyAddress'),
                        agencyPhoneField = form.findField('agencyPhone');
                    agencyEmailField.setValue(addresses.get('email'));
                    agencyEmailDisplay.setValue(Chaching.utilities.ChachingRenderers.renderMailToTag(addresses.get('email')));
                    agencyAddressField.setValue(me.getAgencyAddress(addresses));
                    agencyPhoneField.setValue(addresses.get('phone1Extension') + addresses.get('phone1'));

                }
            }
            
        }
       

    },
    getAgencyAddress:function(addresses) {
        var fullAddress = '';
        if (addresses.get('line1')) {
            fullAddress = addresses.get('line1') + ' ';
        }
        if (addresses.get('line2')) fullAddress += addresses.get('line2') + ' ';
        if (addresses.get('line3')) fullAddress += addresses.get('line3') + ' ';
        if (addresses.get('line4')) fullAddress += addresses.get('line4') + ' ';
        if (addresses.get('city')) fullAddress += ','+addresses.get('city') + ' ';
        if (addresses.get('state')) fullAddress +=','+ addresses.get('state') + ' ';
        if (addresses.get('postalCode')) fullAddress += ','+addresses.get('postalCode');
        return fullAddress;
    },
    onProjectSetupSave:function() {
        var me = this,
            view = me.getView();
        ///TODO: do save operations for project setup tab along with line#
    },
    onProjectDetailsSave:function() {
        var me = this,
            view = me.getView();
        ///TODO: do save operations for project details tab along with locations data
    }
});
