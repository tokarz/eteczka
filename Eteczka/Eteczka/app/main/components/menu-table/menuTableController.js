'use strict';
angular.module('et.controllers').controller('menuTableController', ['$scope', function ($scope) {
    $scope.selectedrow = {};

    $scope.isTabActive = function (tab) {
        var result = 'tab tab-default';
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

    $scope.goRowUp = function () {
        if ($scope.selectedrow) {
            var indexOfRow = $scope.rows.indexOf($scope.selectedrow);
            if (indexOfRow !== -1 && indexOfRow !== $scope.selectedrow.length - 1) {
                $scope.selectedrow = $scope.rows[indexOfRow + 1];
                $scope.$apply();
            }
        }
    }

    $scope.goRowDown = function () {
        if ($scope.selectedrow) {
            var indexOfRow = $scope.rows.indexOf($scope.selectedrow);
            if (indexOfRow !== -1 && indexOfRow !== 0) {
                $scope.selectedrow = $scope.rows[indexOfRow - 1];
                $scope.$apply();
            }
        }
    }

    $scope.getRowStyle = function (user) {
        var result = 'table-row';

        if (user && $scope.selectedrow && user.Numeread && $scope.selectedrow.Numeread && user.Numeread === $scope.selectedrow.Numeread) {
            result += ' active-row';
        }

        return result;
    }


}]);