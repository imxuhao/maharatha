(function () {
    appModule.controller('common.views.EMPTY.modal', [
        '$scope', '$modalInstance', 
        function ($scope, $modalInstance) {
            var vm = this;

            vm.saving = false;

            vm.save = function () {
            };

            vm.cancel = function () {
                $modalInstance.dismiss();
            };

            function init() {

            }

            init();
        }
    ]);
})();