'use strict';
angular.module('et.utils').factory('modalServiceOld', ['$uibModal', function ($uibModal) {
    var defaultUrl = 'app/main/utils/modalTemplate/modalTemplate.html';

    return {
        showModal: function (customModalOptions, modalInput = {}, modalUrl = defaultUrl) {
            var modalDefaults = {
                animation: true,
                templateUrl: modalUrl
            };

            if (!modalDefaults.controller) {
                modalDefaults.controller = function ($scope, $uibModalInstance) {
                    $scope.parameters = customModalOptions.parameters;
                    $scope.modalOptions = customModalOptions;
                    $scope.modalResult = modalInput;

                    $scope.modalOptions.ok = function () {
                        $uibModalInstance.close($scope.modalResult);
                    };
                    $scope.modalOptions.cancel = function () {
                        $uibModalInstance.dismiss('cancel');
                    };
                }
            }

            return $uibModal.open(modalDefaults).result;
        }
    };

}]);