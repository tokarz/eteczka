'use strict';
angular.module('et.controllers').controller('gitMenuTableController', ['$rootScope', '$scope', '$state', '$timeout', 'filesViewService', 'modalService', 'companiesService', 'addFileService', 'cacheService', function ($rootScope, $scope, $state, $timeout, filesViewService, modalService, companiesService, addFileService, cacheService) {
    $scope.newrows = [];
    $scope.stagedrows = [];
    $scope.loading = false;
    $scope.filetopreview = null;
    $scope.fileDescription = {};
    $scope.activeUser = {};
    $scope.availableUsersFolders = [];
    $scope.userChoseFolder = false;
    $scope.chosenFolder = '';

    $scope.$watch('company', function (value) {
        filesViewService.hasUserChoseFolder().then(function (res) {
            if (res && res.success) {
                $scope.userChoseFolder = true;
                $scope.chosenFolder = res.folder;
                $scope.getFilesForPath();
            }
        });
    });

    $scope.getFilesForPath = function () {
        $scope.loading = true;
        filesViewService.getGitStateForCompany().then(function (result) {
            $scope.newrows = result.pliki;
            $scope.stagedrows = [];

            $scope.stagedRowsFromCache = cacheService.getValue('stagedFiles');

            if ($scope.stagedRowsFromCache) {
                $scope.stagedrows = $scope.stagedRowsFromCache;

                $scope.newrows = _.differenceWith($scope.newrows, $scope.stagedrows, function (first, second) {
                    return first.NazwaEad === second.NazwaEad;
                });
            }

            $scope.loading = false;
        });

    }

    $('#stageFile').onchange = function () {
        var pliki = $('#stageFile')[0].files;

        addFileService.dodajPlikDoPoczekalni(pliki).then(function (success) {
            if (success) {
                modalService.alert('Dodanie pliku do poczekalni', 'Plik(i) dodano!');
            } else {
                modalService.alert('Dodanie pliku do poczekalni', 'Błąd dodawania pliku(ów)!');
            }
        });
    };

    $('#stageFile').on('change', function () {
        var pliki = $('#stageFile')[0].files;

        addFileService.dodajPlikDoPoczekalni(pliki).then(function (success) {
            if (success) {
                modalService.alert('Dodanie pliku do poczekalni', 'Plik(i) dodano!');
            } else {
                modalService.alert('Dodanie pliku do poczekalni', 'Błąd dodawania pliku(ów)!');
            }
        });
    });

    $scope.showFileDialog = function () {
        $timeout(function () {
            $('#stageFile').click();
        });
    }

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

            cacheService.addToCache('stagedFiles', $scope.stagedrows);
        }
    }

    $scope.unstageFile = function (row) {
        if (row && $scope.selectedstagedfile !== null) {
            $scope.stagedrows.splice($scope.stagedrows.indexOf(row), 1);
            $scope.newrows.push(row);
            $scope.selectedstagedfile = null;
            $scope.selectedfile = row;
            $scope.filetopreview = row;

            cacheService.addToCache('stagedFiles', $scope.stagedrows);
        }
    }

    $scope.parameters = {
        company: '',
    };

    $scope.selectedStagedFile = null;
    $scope.createdMetaData = null;
    $scope.fileTypes = []
    $scope.employees = []


    $scope.loadFileTypes = function () {
        filesViewService.getFileTypes().then(function (fileTypes) {
            $scope.fileTypes = fileTypes.PobraneDokumenty
        })
    }
    $scope.loadEmployees = function () {
        filesViewService.getAllEmployees().then(function (employees) {
            $scope.employees = employees.Data.data
        })
    }
    $scope.loadFoldersList = function () {
        filesViewService.getFoldersList().then(function (result) {
            $scope.availableUsersFolders = result.sciezkiDoFolderow
        })
    };

    $scope.loadFoldersList();
    $scope.loadFileTypes();
    $scope.loadEmployees();

    companiesService.getActiveCompany().then(function (result) {
        $scope.parameters.company = result.firma;
    });

    $scope.upsertFileDescriptionCtrl = function ($scope, $mdDialog, modalService, description, fileTypes, employees, name) {
        if (description) {
            $scope.modalResult = description;
        }

        var cachedUser = cacheService.getValue('savedGitUser');
        if (cachedUser) {

            $scope.modalResult.Pracownik = cachedUser;
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

    $scope.getUserFolderCtrl = function ($scope, $mdDialog, folderList) {
        $scope.modalResult = {}
        $scope.folderList = folderList;
        $scope.modalResult.chosenFolder = null

        $scope.setFolder = function (folder) {
            $scope.modalResult.chosenFolder = folder
        };

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
    }

    $scope.commitFile = function () {
        if ($scope.createdMetaData !== null) {
            filesViewService.commitFile($scope.createdMetaData).then(function (res) {
                if (res.success) {
                    modalService.alert('Zatwierdzanie pliku', 'Plik Zostal Dodany');
                    cacheService.addToCache('savedGitUser', $scope.createdMetaData.Pracownik);
                    var stagedFiles = cacheService.getValue('stagedFiles');
                    var updatedFiles = stagedFiles.filter(function (elm) {
                        return elm.NazwaEad !== $scope.createdMetaData.Nazwa;
                    });

                    cacheService.addToCache('stagedFiles', updatedFiles);

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
                activeEmployee: $rootScope.activeUser,
                name: $scope.selectedstagedfile ? $scope.selectedstagedfile.NazwaEad : ''
            }
        };

        openModal(
            modalOptions,
            function (value) {
                $scope.createdMetaData = value;
                $rootScope.activeUser = value.Pracownik ? value.Pracownik : {};
                modalService.confirm('Zapisać plik?', 'Czy chcesz zapisać plik użytkownikowi ?').then(function () {
                    $scope.commitFile();
                });
            }
        );
    }

    $scope.triggerGetUserFolderModal = function () {
        var modalOptions = {
            body: 'app/views/files/addFile/fileDescriptionPopup/getUserFolderModal.html',
            controller: $scope.getUserFolderCtrl,
            locals: {
                folderList: $scope.availableUsersFolders
            }
        };

        openModal(
            modalOptions,
            function (value) {
                if (value) {
                    filesViewService.setUsersFolder(value.chosenFolder).then(function (result) {
                        if (result.sucess) {
                            modalService.alert('', 'Ustawiono wybrany folder');

                            $scope.userChoseFolder = false;
                            filesViewService.hasUserChoseFolder().then(function (res) {
                                if (res && res.success) {
                                    $scope.userChoseFolder = true;
                                    $scope.getFilesForPath();
                                    $scope.chosenFolder = res.folder;
                                }
                            });
                        } else {
                            modalService.alert('', 'Błąd przy ustawianiu folderu');
                        }
                    })
                } else {
                    modalService.alert('', 'Nie wybrano żadnego folderu');
                }
            }
        );
    }

    $rootScope.$on('$stateChangeStart',
        function (event, toState, toParams, fromState, fromParams) {
            if (fromState.name !== '' && (fromState.name !== toState.name)) {
                cacheService.removeFromCache('savedGitUser');
                cacheService.removeFromCache('stagedFiles');
            }
        });

}]);