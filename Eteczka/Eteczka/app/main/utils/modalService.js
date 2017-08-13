'use strict';
angular.module('et.utils').factory('modalService', ['$uibModal', function ($uibModal) {
    return {
        showModal: function (customModalOptions, defaultUrl) {
            var modalDefaults = {
                animation: true,
                templateUrl: 'app/main/utils/modalTemplate/modalTemplate.html'
            };

            if (!modalDefaults.controller) {
                modalDefaults.controller = function ($scope, $uibModalInstance) {
                    $scope.parameters = customModalOptions.parameters;
                    $scope.modalOptions = customModalOptions
                    $scope.modalResult = {};

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