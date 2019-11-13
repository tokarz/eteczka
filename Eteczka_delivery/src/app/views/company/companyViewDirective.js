'use strict';

angular.module('et.directives').directive('companyView', function ($timeout) {
    return {
        restrict: 'E',
        scope: {},
        controller: 'companyViewController',
        templateUrl: 'app/views/company/companyView.html'
    }

});