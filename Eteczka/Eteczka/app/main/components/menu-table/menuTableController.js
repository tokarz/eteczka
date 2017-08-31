'use strict';
angular.module('et.controllers').controller('menuTableController', ['$scope', function ($scope) {
    $scope.activeTab = $scope.tabs[0];
    $scope.user = {};

    $scope.selectUser = function (user) {
        if ($scope.user === user) {
            $scope.user = {};
        } else {
            $scope.user = user;
        }
    }

    $scope.isTabActive = function (tab) {
        var result = 'tab tab-default'
        if (tab === $scope.activeTab) {
            result = 'tab tab-active';
        }

        return result;
    }
    $scope.setTabActive = function (tab) {
        $scope.activeTab = tab;
    }

    $scope.getRowStyle = function (user) {
        var result = 'table-row';

        if (user === $scope.user) {
            result += ' active-row';
        }

        return result;
    }
    

}]);