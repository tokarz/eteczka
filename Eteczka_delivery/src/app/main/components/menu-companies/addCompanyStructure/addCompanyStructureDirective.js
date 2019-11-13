'use strict';
angular.module('et.directives').directive('addCompanyStructure', function () {
    return {
        restrict: 'E',
        scope: {
			company: '=',
			type: '@'
        },
		controller: 'addCompanyStructureController',
        templateUrl: 'app/main/components/menu-companies/addCompanyStructure/addCompanyStructure.html'
    };
});