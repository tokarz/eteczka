angular.module('et.directives').directive('accessCheck', function () {
	return {
		restriction: 'A',
		scope: {
			allowedFor: '@',
			forbiddenFor: '@'
		},
		controller: function ($rootScope, $scope) {
			$scope.isAllowed = false;

			if ($scope.allowedFor) {
				let allowedForAll = true;
				$scope.allowedFor
					.split('[').join('')
					.split(']').join('')
					.split(',')
					.forEach(allowedRule => {
						allowedForAll &= $rootScope.ACCESS_MODEL.uprawnienia[allowedRule.toLowerCase()];
					});
				$scope.isAllowed = allowedForAll;
			}

			else if ($scope.forbiddenFor) {
				$scope.forbiddenFor
					.split('[').join('')
					.split(']').join('')
					.split(',')
					.forEach(allowedRule => {
						$scope.isAllowed &= $rootScope.ACCESS_MODEL[allowedRule];
					});
			}



			/*
				ReadOnly
				AddPracownik
				ModifyPracownik
				AddFile
				ModifyFile
				Slowniki
				SendEmail
				Raport
				RaportExport
				DoubleAkcept
			 */
		},
		link: function (scope, element) {
			$(element).addClass('disabled-field');

			scope.$watch('isAllowed', value => {
				if (value) {
					$(element).removeClass('disabled-field');
				}
			});
		}
	};
});