'use strict';
angular.module('et.services').factory('csAddDepartmentService', ['httpService', 'sessionService', function (httpService, sessionService) {
	return {
		getDepartmentsForCompany: function (company) {
			return httpService.get('Wydzialy/PobierzWydzialyDlaFirmy', {
				sessionId: sessionService.getSessionId(),
				firma: company
			});

		}
	};
}]);