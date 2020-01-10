'use strict';
angular.module('et.services').factory('sessionService', ['httpService', function (httpService) {
	var sesja = '';
	var detale = '';
	return {
		createSession: function (sessionDetails) {
			detale = sessionDetails;
			sesja = sessionDetails.IdSesji;
		},
		getSessionAccessList: function () {
			return httpService.get('Sesja/PobierzUprawnienia', { sessionid: sesja });
		},
		getSessionId: function () {
			if (sesja) {
				httpService.get('Sesja/OdnowSesje', { sessionid: sesja });
			}

			return sesja;
		},
		killGivenSession: function (sessionId) {
			return httpService.get('Sesja/ZamknijSesje', {
				sessionId: sesja,
				toKill: sessionId
			});
		},
		killSession: function () {
			return httpService.get('Sesja/ZamknijSesje',
				{
					sessionId: sesja,
					toKill: sesja
				});
		}
	};
}]);