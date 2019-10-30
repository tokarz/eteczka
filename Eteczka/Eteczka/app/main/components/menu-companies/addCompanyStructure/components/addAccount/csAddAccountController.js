'use strict';
angular.module('et.controllers').controller('csAddAccountController', ['$scope', 'csAddAccountService', function ($scope, csAddAccountService) {
	$scope.allAccounts = [];

	$scope.$watch('company', function (selectedCompany) {
		if (selectedCompany) {
			csAddAccountService.getAccountsForCompany(selectedCompany.Firma).then(result => {
				$scope.allAccounts = result.Konta;
			}).catch(err => {
				console.error('Err!' + err);
			});
		}
	});
}]);