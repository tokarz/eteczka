'use strict';
angular.module('et.directives').directive('addUsersView', function () {
    return {
        restrict: 'E',
        scope: {},
        controller: 'addUsersViewController',
        templateUrl: 'app/views/addUsers/addUsersView.html'
    }

});