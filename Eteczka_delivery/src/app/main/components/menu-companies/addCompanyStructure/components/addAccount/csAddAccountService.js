'use strict';
angular.module('et.services').factory('csAddAccountService', ['httpService', 'sessionService', function (httpService, sessionService) {
	return {
		getAccountsForCompany: function (company) {
			return httpService.get('Konto5/PobierzKonta5DlaFirmy', {
				sessionId: sessionService.getSessionId(),
				firma: company
			});
		}
	};
}]);