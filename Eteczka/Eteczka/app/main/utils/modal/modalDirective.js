'use strict';
angular.module('et.utils').directive('modalView', function () {
    return {
        restrict: 'E',
        templateUrl: 'app/main/utils/modal/modalTemplate.html',
        controller: function ($scope, modalService) {

            $scope.confirmModalInput = function () {
                modalService.confirmModalInput();
            }
            $scope.closeModal = function () {
                modalService.closeModal();
            }
        }
    };
});