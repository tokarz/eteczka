'use strict';
angular.module('et.controllers').controller('menuController', ['$rootScope', '$scope', '$mdDialog', function ($rootScope, $scope, $mdDialog) {
    $scope.userMenuVisible = false;
    $scope.valueRejected = false;
    $scope.showUserOptions = function () {
        $scope.userMenuVisible = !$scope.userMenuVisible;
    }

    $scope.$watch('firmparams.selectedfirm', function (newValue, oldValue) {
        if (!$scope.valueRejected) {
            $scope.valueRejected = false;
            if (newValue && (newValue !== oldValue)) {
                $scope.changefirm(newValue, oldValue);
            }
        } else {
            $scope.valueRejected = false;
        }
    })

    

    $scope.changefirm = function (newValue, oldValue) {
        var confirm = $mdDialog.confirm()
              .title('Czy Chcesz zmienic firme?')
              .textContent('Zmiana Firmy spowoduje przeladowanie aktualnego widoku i pobranie nowych wartosci')
              .ariaLabel('Lucky day')
              .ok('Tak')
              .cancel('Nie');

        $mdDialog.show(confirm).then(function (value) {
            $rootScope.$broadcat('MODEL_CHANGED', newValue);
        }, function () {
            $scope.firmparams.selectedfirm = oldValue;
            $scope.valueRejected = true;
        });
    }

}]);
