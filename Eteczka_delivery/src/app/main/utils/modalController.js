'use strict';
angular.module('et.utils').controller('ModalController', ModalController);

function ModalController($scope, $mdDialog) {
    $scope.hide = function () {
        $mdDialog.hide();
    };

    $scope.cancel = function () {
        $mdDialog.cancel();
    };

    $scope.answer = function (answer, errors) {
        console.log(errors)
        if (!errors || Object.keys(errors).length === 0) {
            $mdDialog.hide(answer);
        }
    };
}