'use strict';
angular.module('et.services').factory('companiesService', ['httpService', 'sessionService', function (httpService, sessionService) {
	return {
		getAll: function () {
			return httpService.get('Firmy/PobierzWszystkieFirmy', {
				sessionId: sessionService.getSessionId()
			});
		},
		importMissing: function () {
			return httpService.get('FilesImport/ImportujFirmy', {
				sessionId: sessionService.getSessionId(),
				nadpisz: false
			});
		},
		setActiveCompany: function (company) {
			return httpService.get('Firmy/UstawAktywnaFirme', {
				sessionId: sessionService.getSessionId(),
				company: company
			});
		},
		getActiveCompany: function () {
			return httpService.get('Firmy/PobierzAktywnaFirme', {
				sessionId: sessionService.getSessionId()
			});
		},
		createCompany: function (companyData) {
			return httpService.post('Firmy/DodajFirme', {
				sessionId: sessionService.getSessionId(),
				firmaDoDodania: companyData
			});
		}
	};
}]);