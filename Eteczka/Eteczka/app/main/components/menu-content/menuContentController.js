'use strict';
angular.module('et.controllers').controller('menuContentController', ['$scope', '$state', 'menuContentService', 'modalService', 'peselService', 'utilsService', 'employeesService', 'usersService', function ($scope, $state, menuContentService, modalService, peselService, utilsService, employeesService, usersService) {
	$scope.company = null;

	$scope.selectedWorkplace = {};
	$scope.workplaceParams = {
		loadingRegions: false,
		loadingDepartments: false,
		loadingSubDepartments: false,
		loadingAccouts5: false,
		regions: [],
		departments: [],
		subDepartments: [],
		accounts5: []
	};

	$scope.loadDataWithSesionId = function () {
		$scope.loadActiveCompany();
		$scope.loadRegionList();
		$scope.loadDepartmentList();
		$scope.loadAccounts5();
	};

	$scope.getRegionById = function (regionId) {
		return utilsService.findUniqueValueInListForKey($scope.workplaceParams.regions, regionId, 'Rejon');
	};

	$scope.getDepartmentById = function (departmentId) {
		return utilsService.findUniqueValueInListForKey($scope.workplaceParams.departments, departmentId, 'Wydzial');
	};

	$scope.getSubdepartmentById = function (departmentId, subDepartmentId) {
		return menuContentService.getSubDepartmets(departmentId)
			.then(function (subDepartments) {
				return utilsService.findUniqueValueInListForKey(subDepartments.PodWydzialy, subDepartmentId, 'Podwydzial');
			})
			.catch(function (error) {
				console.error(error);
				return {};
			});
	};

	$scope.getAccount5ByNumber = function (account5Number) {
		return utilsService.findUniqueValueInListForKey($scope.workplaceParams.accounts5, account5Number, 'Konto5');
	};

	$scope.loadActiveCompany = function () {
		return menuContentService.getActiveCompany()
			.then(function (activeCompany) {
				$scope.company = activeCompany;
			});
	};

	$scope.loadRegionList = function () {
		$scope.workplaceParams.loadingRegions = true;
		$scope.workplaceParams.regions = [];

		return menuContentService.getRegionsForFirm()
			.then(function (result) {
				$scope.workplaceParams.loadingRegions = false;
				$scope.workplaceParams.regions = result.Rejony;
			})
			.catch(function (error) {
				$scope.workplaceParams.loadingRegions = false;
				console.error(error);
			});
	};

	$scope.loadDepartmentList = function () {
		$scope.workplaceParams.loadingDepartments = true;
		$scope.workplaceParams.departments = [];

		return menuContentService.getDepartmentsForFirm()
			.then(function (result) {
				$scope.workplaceParams.loadingDepartments = false;
				$scope.workplaceParams.departments = result.Wydzialy;
			})
			.catch(function (error) {
				$scope.workplaceParams.loadingDepartments = false;
				console.error(error);
			});
	};

	$scope.loadAccounts5 = function () {
		$scope.workplaceParams.loadingAccouts5 = true;
		$scope.workplaceParams.accounts5 = [];

		return menuContentService.getAccounts5()
			.then(function (result) {
				$scope.workplaceParams.loadingAccouts5 = false;
				$scope.workplaceParams.accounts5 = result.pobraneKonta5;
			})
			.catch(function (error) {
				$scope.workplaceParams.loadingAccouts5 = false;
				console.error(error);
			});
	};

	$scope.selectRow = function (workplace) {
		if ($scope.selectedWorkplace === workplace) {
			$scope.selectedWorkplace = {};
		} else {
			$scope.selectedWorkplace = workplace;
		}
	};

	$scope.getRowStyle = function (workplace) {
		var result = 'details-table-row';

		if (workplace === $scope.selectedWorkplace) {
			result += ' active-row';
		}

		return result;
	};

	$scope.mapWorkplaceInput = function (workplace) {
		if (workplace) {
			if (workplace.Rejon && workplace.Rejon.Rejon) {
				workplace.Rejon = workplace.Rejon.Rejon;
			}
			if (workplace.Wydzial && workplace.Wydzial.Wydzial) {
				workplace.Wydzial = workplace.Wydzial.Wydzial;
			}
			if (workplace.Podwydzial && workplace.Podwydzial.Podwydzial) {
				workplace.Podwydzial = workplace.Podwydzial.Podwydzial;
			}
			if (workplace.Konto5 && workplace.Konto5.Konto5) {
				workplace.Konto5 = workplace.Konto5.Konto5;
			}
		}

		return workplace;
	};

	$scope.upsertEmployeeCtrl = function ($scope, $mdDialog, user) {
		if (user) {
			$scope.modalResult = user;
		}

		$scope.hide = function () {
			$mdDialog.hide();
		};

		$scope.cancel = function () {
			$mdDialog.cancel();
		};

		$scope.answer = function (answer, errors) {
			console.log(errors);
			if (!errors || Object.keys(errors).length === 0) {
				$mdDialog.hide(answer);
			}
		};

		$scope.isPeselValid = function (pesel, gender) {
			return peselService.isPeselValid(pesel, gender);
		};
		$scope.getBirthdate = function (pesel, gender) {
			return peselService.getDateFromPesel(pesel, gender);
		};

		$scope.shouldDisableByPesel = function (pesel, field) {
			var isNoPesel = pesel === null || pesel === '' || typeof pesel === 'undefined';
			var fieldHasValue = typeof field === 'string' && field.trim() !== '';

			if (fieldHasValue || !fieldHasValue && isNoPesel) {
				return false;
			}

			return true;
		};
	};

	$scope.upsertWorkplaceCtrl = function ($scope, $mdDialog, workplace, workplaceParams) {
		if (workplace) {
			$scope.modalResult = workplace;
		}

		$scope.workplaceParams = workplaceParams;

		$scope.hide = function () {
			$mdDialog.hide();
		};

		$scope.cancel = function () {
			$mdDialog.cancel();
		};

		$scope.answer = function (answer, errors) {
			console.log(errors);
			if (!errors || Object.keys(errors).length === 0) {
				$mdDialog.hide(answer);
			}
		};

		$scope.loadSubDepartmentList = function (department) {
			$scope.workplaceParams.loadingSubDepartments = true;
			$scope.workplaceParams.subDepartments = [];
			return menuContentService.getSubDepartmets(department.Wydzial)
				.then(function (result) {
					$scope.workplaceParams.loadingSubDepartments = false;
					$scope.workplaceParams.subDepartments = result.PodWydzialy;
				})
				.catch(function (error) {
					$scope.workplaceParams.loadingSubDepartments = false;
					console.error(error);
				});
		};

		$scope.validateIfProperAccount = function (account5skr) {
			var account5 = $scope.workplaceParams.accounts5.find(function (acc) {
				return acc.Kontoskr.trim() === account5skr.trim();
			});
			if (account5) {
				console.log('account5 found', account5);
			}
			else {
				console.error('account not found');
			}
		};

		$scope.querySearch = function (query) {
			var allAccounts5 = $scope.workplaceParams.accounts5;
			return query ? allAccounts5.filter(createFilterFor(query)) : allAccounts5;
		};

		var createFilterFor = function (query) {
			var lowercaseQuery = angular.lowercase(query);

			return function filterFn(account) {
				return account.Kontoskr.toLowerCase().indexOf(lowercaseQuery) === 0;
			};

		};
	};

	var openModal = function (modalOptions, executor) {
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


	$scope.triggerAddEmployeeDialog = function () {
		var userModalOptions = {
			body: 'app/views/employees/editEmployeesPopup/upsertUserModal.html',
			controller: $scope.upsertEmployeeCtrl,
			locals: {
				user: null
			}
		};
		var userWorkplaceModalOptions = {
			body: 'app/views/employees/editWorkplacesPopup/upsertWorkplaceModal.html',
			controller: $scope.upsertWorkplaceCtrl,
			locals: {
				workplace: { Firma: $scope.company.firma },
				workplaceParams: $scope.workplaceParams
			}
		};

		openModal(userModalOptions, function (user) {
			openModal(userWorkplaceModalOptions, function (workplace) {
				employeesService.addEmployeeWithWorkplace(Object.assign({}, user, $scope.mapWorkplaceInput(workplace))).then(function (res) {
					$state.reload();
					if (res.sucess.Result) {
						modalService.alert('', res.sucess.Message);
					} else {
						modalService.alert('Blad', 'Blad Dodawania Pracownika [' + res.sucess.Message + ']');
					}
				}, function (err) {
					modalService.alert('Blad', err);
				});
			});
		});
	};

	$scope.triggerEditEmployeeDialog = function () {
		var modalOptions = {
			body: 'app/views/employees/editEmployeesPopup/upsertUserModal.html',
			controller: $scope.upsertEmployeeCtrl,
			locals: {
				user: Object.assign({}, $scope.user)
			}
		};

		openModal(
			modalOptions,
			function (user) {
				employeesService.editEmployee(user).then(function (res) {
					$state.reload();
					if (res.sucess.Result) {
						modalService.alert('', 'Zapisano zmiany dla pracownika!');
					} else {
						modalService.alert('Blad', 'Blad Edycji Pracownika [' + res.sucess.Message + ']');
					}
				}, function (err) {
					modalService.alert('Blad', err);
				});
			}
		);
	};

	$scope.triggerDeleteEmployeePopup = function () {
		triggerAdminPasswordCheck()
			.then(function (isAdminPasswordCorrect) {
				if (typeof isAdminPasswordCorrect === 'undefined') {
					return;
				}

				if (isAdminPasswordCorrect === false) {
					return alert('haslo administratora niepoprawne');
				}

				var modalOptions = {
					body: 'app/views/employees/editEmployeesPopup/deleteUserModal.html',
					controller: function ($scope, $mdDialog, user) {
						if (user) {
							$scope.modalResult = user;
						}

						$scope.cancel = function () {
							$mdDialog.cancel();
						};

						$scope.answer = function (answer, errors) {
							$mdDialog.hide(answer);
						};
					},
					locals: {
						user: $scope.user
					}
				};

				openModal(
					modalOptions,
					function (value) {
						triggerShortPasswordCheck()
							.then(function (isShortPasswordCorrect) {
								if (typeof isShortPasswordCorrect === 'undefined') {
									return;
								}

								if (isShortPasswordCorrect === false) {
									return alert('haslo uzytkownika niepoprawne');
								}

								console.log('tu bedzie wywolanie funkcji usuwania pracownika', value);
							})
							.catch(console.error);
					},
					$scope.user
				);
			})
			.catch(console.error);
	};

	var triggerAdminPasswordCheck = function () {
		var modalOptions = {
			title: 'Wymagane haslo administratora',
			body: 'app/main/utils/modalTemplate/adminPasswordModal.html'
		};

		return openModal(
			modalOptions,
			function (value) {
				return menuContentService.isProperAdminPassword().then(function (result) {
					return result;
				});
			}
		);
	};

	var triggerShortPasswordCheck = function () {
		var modalOptions = {
			title: 'Wymagane potwierdzenie haslem uzytkownika',
			body: 'app/main/utils/modalTemplate/userPasswordModal.html'
		};

		return openModal(
			modalOptions,
			function (password) {
				return usersService.checkPassword(password && password.userPassword).then(function (correctPassword) {
					return correctPassword && correctPassword.success;
				});
			}
		);
	};

	$scope.triggerAddWorkplaceDialog = function () {
		var modalOptions = {
			body: 'app/views/employees/editWorkplacesPopup/upsertWorkplaceModal.html',
			controller: $scope.upsertWorkplaceCtrl,
			locals: {
				workplace: { Firma: $scope.company.firma },
				workplaceParams: $scope.workplaceParams
			}
		};

		openModal(
			modalOptions,
			function (workplace) {
				employeesService.addWorkplace(Object.assign({ NumerEad: $scope.user.Numeread }, $scope.mapWorkplaceInput(workplace))).then(function (res) {
					$state.reload();
					if (res.sucess.Result) {
						modalService.alert('', res.sucess.Message);
					} else {
						modalService.alert('Blad', 'Blad Dodawania miejsca pracy dla Pracownika: [' + res.sucess.Message + ']');
					}
				}, function (err) {
					modalService.alert('Blad', err);
				});
			}
		);
	};

	$scope.triggerEditWorkplaceDialog = function () {
		var modalOptions = {
			body: 'app/views/employees/editWorkplacesPopup/upsertWorkplaceModal.html',
			controller: $scope.upsertWorkplaceCtrl,
			locals: {
				workplace: Object.assign({}, $scope.selectedWorkplace),
				workplaceParams: $scope.workplaceParams
			}
		};

		openModal(
			modalOptions,
			function (workplace) {
				employeesService.editWorkplace(Object.assign({ NumerEad: $scope.user.Numeread }, $scope.mapWorkplaceInput(workplace))).then(function (res) {
					if (res.sucess.Result) {
						$state.reload();
						modalService.alert('', res.sucess.Message);
					} else {
						modalService.alert('Blad', 'Blad edycji miejsca pracy dla Pracownika: [' + res.sucess.Message + ']');
					}
				}, function (err) {
					modalService.alert('Blad', err);
				});
			}
		);
	};

	$scope.triggerDeleteWorkplaceDialog = function () {
		var modalOptions = {
			body: 'app/views/employees/editWorkplacesPopup/deleteWorkplace.html',
			controller: function ($scope, $mdDialog, workplace) {
				if (workplace) {
					$scope.modalResult = workplace;
				}

				$scope.cancel = function () {
					$mdDialog.cancel();
				};

				$scope.answer = function (answer, errors) {
					$mdDialog.hide(answer);
				};
			},
			locals: {
				workplace: $scope.selectedWorkplace,
				workplaceParams: $scope.workplaceParams
			}
		};

		openModal(
			modalOptions,
			function (workplace) {
				triggerShortPasswordCheck()
					.then(function (isShortPasswordCorrect) {
						if (typeof isShortPasswordCorrect === 'undefined') {
							return;
						}

						if (isShortPasswordCorrect === false) {
							return modalService.alert('Blad', 'haslo uzytkownika niepoprawne');
						}

						employeesService.removeWorkplace(Object.assign({ NumerEad: $scope.user.Numeread }, $scope.mapWorkplaceInput(workplace))).then(function (res) {
							if (res.sucess.Result) {
								$state.reload();
								modalService.alert('', res.sucess.Message);
							} else {
								modalService.alert('Blad', 'Blad usuwania miejsca pracy dla Pracownika: [' + res.sucess.Message + ']');
							}
						}, function (err) {
							modalService.alert('Blad', err);
						});
					})
					.catch(console.error);

			}
		);
	};

	$scope.$watch('user', function (value) {
		if (value && value !== {}) {
			menuContentService.getUserWorkplaces(value).then(function (result) {
				$scope.workplaces = [];

				result.MiejscaPracy.forEach(function (workplace) {
					var region = $scope.getRegionById(workplace.Rejon);
					var department = $scope.getDepartmentById(workplace.Wydzial);
					var account5 = $scope.getAccount5ByNumber(workplace.Konto5);

					if (typeof workplace.Podwydzial !== 'string' || workplace.Podwydzial.trim() === '') {
						workplace.Podwydzial = {};
					}
					else {
						$scope.getSubdepartmentById(workplace.Wydzial, workplace.Podwydzial).then(function (subdepartment) {
							workplace.Podwydzial = subdepartment;
						});
					}

					workplace.Rejon = region;
					workplace.Wydzial = department;
					workplace.Konto5 = account5;

					$scope.workplaces.push(workplace);
				});
			});
		}
	});

	utilsService.callStartupMethod($scope.loadDataWithSesionId);
}]);