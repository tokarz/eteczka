'use strict';
angular.module('et.controllers').controller('chooseFirmController', ['$rootScope', '$scope', '$state', 'sessionService', function ($rootScope, $scope, $state, sessionService) {
    $scope.firmChoices = $state.params.choices;
    $scope.firm = $scope.firmChoices[0];
    $scope.chooseFirm = function () {
        $rootScope.SELECTED_FIRM = $scope.firm;
        $state.go('home');
    }


}]);