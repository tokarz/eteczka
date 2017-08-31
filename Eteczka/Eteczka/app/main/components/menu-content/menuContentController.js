'use strict';
angular.module('et.controllers').controller('menuContentController', ['$scope', 'menuContentService', function ($scope, menuContentService) {

    $scope.$watch('user', function (value) {
        if (value && value !== {}) {
            menuContentService.getUserWorkplaces(value).then(function (result) {
                $scope.workplaces = result.MiejscaPracy;
            });
        }
    });
}]);