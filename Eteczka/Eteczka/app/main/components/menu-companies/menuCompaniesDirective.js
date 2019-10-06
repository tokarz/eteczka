'use strict';
angular.module('et.directives').directive('menuCompanies', function () {
    return {
        restrict: 'E',
        scope: {
            company: '='
        },
        controller: 'menuCompaniesController',
        templateUrl: 'app/main/components/menu-companies/menuCompaniesView.html'
    };

});