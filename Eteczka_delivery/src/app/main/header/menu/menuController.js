'use strict';
angular.module('et.controllers').controller('menuController', ['$rootScope', '$scope', '$mdDialog', '$state', 'companiesService', 'cacheService', function ($rootScope, $scope, $mdDialog, $state, companiesService, cacheService) {
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
            companiesService.setActiveCompany(newValue).then(function (res) {
                if (res && res.success) {
                    console.log('Ustawiono firme: [' + newValue + ']');

                    cacheService.clearCache();

                    $state.reload();
                }
            },
            function (err) {
                console.error('Blad ustawiania firmy [' + newValue + ']', err);
                $scope.firmparams.selectedfirm = oldValue;
            });
        }, function () {
            $scope.firmparams.selectedfirm = oldValue;
            $scope.valueRejected = true;
        });
    }

}]);
