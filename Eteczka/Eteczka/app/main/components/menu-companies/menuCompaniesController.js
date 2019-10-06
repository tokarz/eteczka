'use strict';
angular.module('et.controllers').controller('menuCompaniesController', ['$scope', '$state', '$mdDialog', 'settingsService', 'modalService', 'companiesService', 'usersService', '_', function ($scope, $state, $mdDialog, settingsService, modalService, companiesService, usersService, _) {
	$scope.$watch('company', function (company) {
	});

	$scope.openEditCompanyDialog = function () {
		var modalOptions = {
			body: 'app/main/components/menu-users-content/addEditCompanyModal/companyModal.html',
			controller: $scope.addCompanyControllerFunction,
			locals: {
				isEdit: true,
				company: $scope.company
			}
		};

		openModal(
			modalOptions,
			function (value) {
				settingsService.editUser(value).then(function (res) {
					if (res.success) {
						$state.reload();
					}
				}).catch();
			}
		);
	};

	$scope.openAddCompanyDialog = function () {
		var modalOptions = {
			body: 'app/main/components/menu-users-content/addEditUserModal/userModal.html',
			controller: $scope.addCompanyControllerFunction,
			locals: {
				isEdit: false,
				company: null
			}
		};

		openModal(
			modalOptions,
			function (value) {
				settingsService.addNewUser(value).then(function (res) {
					if (res.success) {
						$state.reload();
					}
				}).catch();
			}
		);
	};

	$scope.triggerDeleteCompany = function (company) {
		alert('delete ' + company);
	};

	$scope.addCompanyControllerFunction = function ($scope, $mdDialog, modalService, isEdit, user) {
		$scope.isEdit = isEdit ? true : false;

		$scope.modalResult = {};

		if (user) {
			$scope.modalResult = user;
		}

		$scope.yesNoOptions = [{ name: 'TAK', value: true }, { name: 'NIE', value: false }];

		$scope.hide = function () {
			$mdDialog.hide();
		};

		$scope.cancel = function () {
			$mdDialog.cancel();
		};

		$scope.isEditable = function () {
			let result = '';
			if (isEdit) {
				result = 'disabled-field';
			}

			return result;
		};

		$scope.answer = function (answer, errors) {
			console.log(errors);
			if (!errors || Object.keys(errors).length === 0) {
				$mdDialog.hide(answer);
			}
		};

		$scope.isNotEqual = function (baseText, textToMatch) {
			return baseText !== textToMatch;
		};
	};
}]);