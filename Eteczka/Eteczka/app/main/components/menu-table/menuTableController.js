'use strict';
angular.module('et.controllers').controller('menuTableController', ['$scope', function ($scope) {
    $scope.selectedrow = {};

    $scope.isTabActive = function (tab) {
        var result = 'tab tab-default'
        if (tab === $scope.activetab) {
            result = 'tab tab-active';
        }

        return result;
    }
    $scope.setTabActive = function (tab) {
        $scope.activetab = tab;
    }

    $scope.selectRow = function (user) {
        if ($scope.selectedrow === user) {
            $scope.selectedrow = {};
        } else {
            $scope.selectedrow = user;
        }
    }

    $scope.getRowStyle = function (user) {
        var result = 'table-row';

        if (user === $scope.selectedrow) {
            result += ' active-row';
        }

        return result;
    }
    

}]);