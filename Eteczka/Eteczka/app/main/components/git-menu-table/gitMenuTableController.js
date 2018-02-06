'use strict';
angular.module('et.controllers').controller('gitMenuTableController', ['$scope', '$state', 'filesViewService', 'modalService', 'companiesService', function ($scope, $state, filesViewService, modalService, companiesService) {
    $scope.newrows = [];
    $scope.stagedrows = [];
    $scope.loading = false;
    $scope.filetopreview = null;
    $scope.fileDescription = {};

    $scope.$watch('company', function (value) {
        if (value) {
            $scope.loading = true;
            filesViewService.getGitStateForCompany(value).then(function (result) {
                $scope.newrows = result.newfiles;
                $scope.stagedrows = [];
                $scope.loading = false;
            });
        }
    });

    $scope.selectStagedFile = function (file) {
        if ($scope.selectedstagedfile == file) {
            $scope.selectedstagedfile = null;
            $scope.selectedfile = null;
            $scope.filetopreview = null;
        } else {
            $scope.selectedstagedfile = file;
            $scope.filetopreview = file;
            $scope.selectedfile = null;
        }
    }
    $scope.getStagedRowStyle = function (file) {
        var result = 'table-row';

        if (file === $scope.selectedstagedfile) {
            result += ' active-row';
        }

        return result;
    }
    $scope.selectFile = function (file) {
        if ($scope.selectedfile == file) {
            $scope.selectedfile = null;
            $scope.filetopreview = null;
            $scope.selectedstagedfile = null;
        } else {
            $scope.selectedfile = file;
            $scope.filetopreview = file;
            $scope.selectedstagedfile = null;
        }
    }

    $scope.getRowStyle = function (file) {
        var result = 'git-table-row';

        if (file === $scope.selectedfile) {
            result += ' active-row';
        }

        return result;
    }

    $scope.stageFile = function (row) {
        if (row && $scope.selectedfile !== null) {
            $scope.newrows.splice($scope.newrows.indexOf(row), 1);
            $scope.stagedrows.push(row);
            $scope.selectedfile = null;
            $scope.selectedstagedfile = row;
            $scope.filetopreview = row;
        }
    }

    $scope.unstageFile = function (row) {
        if (row && $scope.selectedstagedfile !== null) {
            $scope.stagedrows.splice($scope.stagedrows.indexOf(row), 1);
            $scope.newrows.push(row);
            $scope.selectedstagedfile = null;
            $scope.selectedfile = row;
            $scope.filetopreview = row;
        }
    }

    $scope.parameters = {
            company: '',
    };

    $scope.selectedStagedFile = null;
    $scope.createdMetaData = null;
    $scope.fileTypes = []
    $scope.employees = []


    var loadFileTypes = function () {
            filesViewService.getFileTypes().then(function (fileTypes) {
                    $scope.fileTypes = fileTypes.PobraneDokumenty
            })
    }
    var loadEmployees = function () {
            filesViewService.getAllEmployees().then(function (employees) {
                    $scope.employees = employees.Data.data
            })
    }

    loadFileTypes();
    loadEmployees()

    companiesService.getActiveCompany().then(function (result) {
            $scope.parameters.company = result.firma;
    });

    $scope.upsertFileDescriptionCtrl = function ($scope, $mdDialog, modalService, description, fileTypes, employees, name) {
            if (description) {
                    $scope.modalResult = description;
            }

            $scope.yesNoOptions = [{ name: 'TAK', value: true }, { name: 'NIE', value: false }]
            $scope.docPartOptions = ['A', 'B', 'C']
            $scope.modalResult.Dokwlasny = $scope.modalResult.Dokwlasny || $scope.yesNoOptions[0].value;
            $scope.modalResult.Nazwa = name;

            $scope.pracownikPesel = '';

            $scope.hide = function () {
                    $mdDialog.hide();
            };

            $scope.cancel = function () {
                    $mdDialog.cancel();
            };

            $scope.answer = function (answer, errors) {
                    console.log(errors)
                    if (!errors || Object.keys(errors).length === 0) {
                            $mdDialog.hide(answer);
                    }
            };

            $scope.isDisabled = function () {
                    return !$scope.modalResult.Typ || !$scope.isTypeWithDates($scope.modalResult.Typ.Symbol);
            }

            $scope.fillValidFromDate = function () {
                    if (!$scope.modalResult.DataPocz && $scope.modalResult.DataWytworzenia) {
                            $scope.modalResult.DataPocz = $scope.modalResult.DataWytworzenia
                    }
            }

            $scope.isTypeWithDates = function (fileSymbol) {
                    var type = fileTypes.find(function (file) {
                            return file.Symbol === fileSymbol
                    }).Typedycji

                    if (type.trim() === 'b') {
                            return true
                    }
                    else {
                            return false
                    }
            }

            var querySearch = function (arrayTosearchIn, keys, query) {
                    return query ? arrayTosearchIn.filter(createFilterFor(keys, query)) : arrayTosearchIn;
            }

            $scope.fileTypeSearch = function (query) {
                    return querySearch(fileTypes, ["Symbol", "Nazwa"], query)
            }

            $scope.employeeSearch = function (query) {
                    return querySearch(employees, ["Nazwisko"], query)
            }

            var createFilterFor = function (keys, query) {
                    var lowercaseQuery = angular.lowercase(query);

                    return function filterFn(object) {
                            return keys.some(function (key) {
                                    return (object[key].toLowerCase().indexOf(lowercaseQuery) === 0);
                            })
                    };
            }
    }

    

    $scope.commitFile = function () {
            if ($scope.createdMetaData !== null) {
                    filesViewService.commitFile($scope.createdMetaData).then(function (res) {
                            if (res.success) {
                                    modalService.alert('Zatwierdzanie pliku', 'Plik Zostal Dodany');
                                    $state.reload();
                            } else {
                                    modalService.alert('Zatwierdzanie pliku', 'Blad! Plik Nie Zostal Dodany! Zweryfikuj dane i prawa dostepu lub skontaktuj sie z Administratorem');
                            }
                    });
            }
    }

    var openModal = function (modalOptions, executor) {
            return modalService.showModal(modalOptions)
                    .then(function (result) {
                            return executor(result);
                    })
                    .catch(function (ex) {
                            if (ex !== 'cancel' && ex !== 'backdrop click') {
                                    console.error(ex);
                            }
                    });
    }

    $scope.triggerUpsertFileDescriptionDialog = function () {
            var modalOptions = {
                    body: 'app/views/files/addFile/fileDescriptionPopup/upsertFileDescription.html',
                    controller: $scope.upsertFileDescriptionCtrl,
                    locals: {
                            description: $scope.fileDescription,
                            fileTypes: $scope.fileTypes,
                            employees: $scope.employees,
                            name: $scope.selectedstagedfile ? $scope.selectedstagedfile.NazwaEad : ''
                    }
            };

            openModal(
                    modalOptions,
                    function (value) {
                            $scope.createdMetaData = value;
                            modalService.confirm('Zapisać plik?', 'Czy chcesz zapisać plik użytkownikowi ?').then(function () {
                                    $scope.commitFile();
                            });
                    }
            );
    }

}]);