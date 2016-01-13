(function () {
    appModule.controller('common.views.profile.changePassword', [
        '$scope', 'appSession', '$modalInstance', 'abp.services.app.profile',
        function ($scope, appSession, $modalInstance, profileService) {
            var vm = this;

            vm.saving = false;
            vm.passwordInfo = null;

            vm.save = function () {
                vm.saving = true;
                profileService.changePassword(vm.passwordInfo)
                    .success(function () {
                        abp.notify.info(app.localize('YourPasswordHasChangedSuccessfully'));
                        $modalInstance.close();
                    }).finally(function () {
                        vm.saving = false;
                    });
            };

            vm.cancel = function () {
                $modalInstance.dismiss();
            };
        }
    ]);
})();