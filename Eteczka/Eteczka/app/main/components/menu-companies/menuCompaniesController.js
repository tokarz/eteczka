'use strict';
angular.module('et.controllers').controller('menuCompaniesController', ['$scope', '$state', '$mdDialog', 'settingsService', 'modalService', 'companiesService', 'usersService', '_', function ($scope, $state, $mdDialog, settingsService, modalService, companiesService, usersService, _) {
	$scope.$watch('company', function (company) {
	});

	$scope.tabs = [
		{
			title: 'Wydzial',
			type: 0
		},
		{
			title: 'Podwydzial',
			type: 1
		},
		{
			title: 'Rejon',
			type: 2
		},
		{
			title: 'Konto',
			type: 3
		}
	];

	$scope.openEditCompanyDialog = function () {
		const modalOptions = {
			body: 'app/main/components/menu-companies/addEditCompanyModal/companyModal.html',
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

	const openModal = function (modalOptions, executor) {
		return modalService.showModal(modalOptions)
			.then(function (result) {
				return executor(result);
			})
			.catch(function (error) {
				if (error !== 'cancel' && error !== 'backdrop click') {
					console.log("error found!", error);
				}
			});
	};

	$scope.openAddCompanyDialog = function () {
		const modalOptions = {
			body: 'app/main/components/menu-companies/addEditCompanyModal/companyModal.html',
			controller: $scope.addCompanyControllerFunction,
			locals: {
				isEdit: false,
				company: null
			}
		};

		openModal(
			modalOptions,
			function (value) {
				companiesService.createCompany(value).then(result => {
					if (result && result.success) {
						alert('succ');
					} else {
						alert('failed!');
					}
				});
			}
		);
	};

	$scope.triggerDeleteCompany = function (company) {
		alert('delete ' + company);
	};

	$scope.addCompanyControllerFunction = function ($scope, $mdDialog, modalService, isEdit, company) {
		$scope.isEdit = isEdit ? true : false;

		$scope.modalResult = {};
		if (company) {
			$scope.modalResult = company;
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