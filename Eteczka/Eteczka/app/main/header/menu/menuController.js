'use strict';
angular.module('et.controllers').controller('menuController', ['$rootScope', '$scope', '$mdDialog', function ($rootScope, $scope, $mdDialog) {
    $scope.userMenuVisible = false;
    $scope.showUserOptions = function () {
        $scope.userMenuVisible = !$scope.userMenuVisible;
    }

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
            $scope.selectedfirm = oldValue;
        });
    }

}]);
