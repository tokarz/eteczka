'use strict';
angular.module('et.directives').directive('csAddRegion', function () {
	return {
		restrict: 'E',
		scope: {
			company: '='
		},
		controller: 'csAddRegionController',
		templateUrl: 'app/main/components/menu-companies/addCompanyStructure/components/addRegion/csAddRegion.html'
	};
});