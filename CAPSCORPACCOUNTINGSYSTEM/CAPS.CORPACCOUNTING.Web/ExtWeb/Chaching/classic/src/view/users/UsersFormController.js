Ext.define('Chaching.view.users.UsersFormController', {
    extend: 'Chaching.view.common.form.ChachingFormPanelController',
    alias: 'controller.users-usersform',
    showRandomPassword: function () {
        var me = this;
        setRandomPassword = me.lookupReference('setRandomPassword');
        password = me.lookupReference('password');
        passwordRepeat = me.lookupReference('passwordRepeat');
        if (setRandomPassword.value) {
            password.hide();
            passwordRepeat.hide();
        }
        else {
            password.show();
            passwordRepeat.show();
        }
        password.reset();
        passwordRepeat.reset();
    },

});
