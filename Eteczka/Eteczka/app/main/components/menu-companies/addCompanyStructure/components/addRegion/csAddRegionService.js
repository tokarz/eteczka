'use strict';
angular.module('et.services').factory('csAddRegionService', ['httpService', 'sessionService', function (httpService, sessionService) {
	return {
		getRegionsForCompany: function (company) {
			return httpService.get('Rejony/PobierzRejonyDlaChwilowoWybranejFirmy', {
				sessionId: sessionService.getSessionId(),
				firma: company
			});

		}
	};
}]);