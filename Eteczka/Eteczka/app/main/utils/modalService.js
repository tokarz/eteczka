'use strict';
angular.module('et.utils').factory('modalService', ['$mdDialog', function ($mdDialog) {
    var defaultUrl = 'app/main/utils/modalTemplate/modalTemplate.html';

    return {
        showModal: function (customModalOptions, initialInput) {
            //var modalDefaults = {
            //    animation: true,
            //    templateUrl: modalUrl
            //};

            //if (!modalDefaults.controller) {
            //    modalDefaults.controller = function ($scope, $uibModalInstance) {
            //        $scope.parameters = customModalOptions.parameters;
            //        $scope.modalOptions = customModalOptions;
            //        $scope.modalResult = modalInput;

            //        $scope.modalOptions.ok = function () {
            //            $uibModalInstance.close($scope.modalResult);
            //        };
            //        $scope.modalOptions.cancel = function () {
            //            $uibModalInstance.dismiss('cancel');
            //        };
            //    }
            //}

            //return $uibModal.open(modalDefaults).result;

            var mergedController = function ($scope, $mdDialog) {
                console.log(arguments)
                customModalOptions.controller.apply(this, arguments);
                ModalController.apply(this, arguments);
            }

            var dialogParams = {
                controller: customModalOptions.controller ? mergedController : 'ModalController',
                templateUrl: customModalOptions.body ? customModalOptions.body : defaultUrl,
                initialValue: initialInput,
                clickOutsideToClose: true
            }

            return $mdDialog.show(dialogParams);
        }
    };

}]);