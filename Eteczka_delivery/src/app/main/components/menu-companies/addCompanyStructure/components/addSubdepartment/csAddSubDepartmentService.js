'use strict';
angular.module('et.services').factory('csAddSubDepartmentService', ['httpService', 'sessionService', function (httpService, sessionService) {
	return {
		getSubDepartmentsForCompany: function (company, department) {
			return httpService.get('PodWydzial/PobierzWszystkiePodwydzialyDoStruktury', {
				sessionId: sessionService.getSessionId(),
				firma: company,
				wydzial: department
			});

		}
	};
}]);