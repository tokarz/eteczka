'use strict';
angular.module('et.controllers').controller('csAddRegionController', ['$scope', 'csAddRegionService', function ($scope, csAddRegionService) {
	$scope.allRegions = [];

	$scope.$watch('company', function (selectedCompany) {
		if (selectedCompany) {
			csAddRegionService.getRegionsForCompany(selectedCompany.Firma).then(result => {
				$scope.allRegions = result.Rejony;
			}).catch(err => {
				console.error('Err!' + err);
			});
		}
	});
}]);