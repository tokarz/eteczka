'use strict';
angular.module('et.controllers').controller('menuFilesContentController', ['$rootScope', '$scope', 'filesViewService', 'shopCartService', 'modalService', function ($rootScope, $scope, filesViewService, shopCartService, modalService) {
    $scope.selectedFile = null;
    $scope.emptyTableMessage = 'Nie zaznaczono elementu do wyswietlenia';
    $scope.userFiles = [];

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

    $scope.loadFileTypes();
    $scope.loadEmployees();

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
    }

    $scope.sendEmailCtrl = function ($scope, $mdDialog, selectedFiles) {
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

        $scope.filesToAttach = selectedFiles;
    }

    $scope.generatePdf = function () {
        filesViewService.generatePdf($scope.user).then(function () {
            modalService.alert('Raport Pdf', 'Raport wygenerowano!');
        }, function () {
            modalService.alert('Raport Pdf (Blad!) ', 'Blad przy generowaniu raportu! Sprawdz logi systemowe');
        });

    }

    $scope.openSendEmailDialog = function () {
        var modalOptions = {
            body: 'app/views/shopcart/shopCartModals/sendEmailModal.html',
            controller: $scope.sendEmailCtrl,
            locals: {
                selectedFiles: $scope.userFiles.filter(function (elm) {
                    if (elm.checked) {
                        return elm;
                    }
                })
            }
        };

        openModal(
            modalOptions,
            function (value) {
                triggerZipPasswordModal()
                    .then(function (zipPassword) {
                        var result = Object.assign({}, value, zipPassword)
                        // add be function to send email
                    });
            }
        )
    }

    var triggerZipPasswordModal = function () {
        var modalOptions = {
            body: 'app/views/shopcart/shopCartModals/zipPasswordModal.html',
            controller: function ($scope, $mdDialog) {
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
        }

        return openModal(modalOptions, function (value) { return value })
    }


    $scope.selectFile = function (file) {
        if ($scope.selectedFile === file) {
            $scope.selectedFile = null;
        } else {
            $scope.selectedFile = file;
        }
    }

    $scope.getRowStyle = function (file) {
        var result = 'table-row';

        if (file === $scope.selectedFile) {
            result += ' active-row';
        }

        if (file.checked) {
            result += ' checked-row';
        }

        return result;
    }

    $scope.toggleSelectAll = function () {
        angular.forEach($scope.userFiles, function (elm) {
            elm.checked = !elm.checked;
        });
    }

    $scope.triggerDeleteEmployeePopup = function () {
        var filesToDelete = [];
        angular.forEach($scope.userFiles, function (elm) {
            if (elm.checked) {
                filesToDelete.push(elm);
            }
        });


    }

    $scope.editFileDescriptionCtrl = function ($scope, $mdDialog, modalService, description, fileTypes, employees, name) {
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

    $scope.triggerEditFileDescriptionDialog = function () {
        var modalOptions = {
            body: 'app/views/files/addFile/fileDescriptionPopup/upsertFileDescription.html',
            controller: $scope.editFileDescriptionCtrl,
            locals: {
                description: $scope.selectedFile,
                fileTypes: $scope.fileTypes,
                employees: $scope.employees,
                activeEmployee: $rootScope.activeUser,
                name: $scope.selectedFile ? $scope.selectedFile.Nazwa : ''
            }
        };

        openModal(
            modalOptions,
            function (value) {
                $scope.createdMetaData = value;
                $rootScope.activeUser = value.Pracownik ? value.Pracownik : {};
                modalService.confirm('Zapisać zmiany?', 'Czy chcesz zapisać zmiany w opisie pliku ?').then(function () {
                    filesViewService.editCommittedFile().then(function (res) {
                        if (res.success) {
                            modalService.alert('Zatwierdzanie zmian w pliku', 'Plik zostal zmieniony');
                            // $state.reload(); - przeladowac stan kiedy edytujemy?
                        } else {
                            modalService.alert('Zatwierdzanie zmian w pliku', 'Blad! Plik nie zostal zmieniony! Zweryfikuj dane i prawa dostepu lub skontaktuj sie z Administratorem');
                        }
                });
            }
        );

    $scope.$watch('user', function (user) {
        if (user) {
            filesViewService.getFilesForUser(user).then(function (result) {
                $scope.userFiles = result.pliki;
                $scope.selectedFile = null;
            });
        } else {
            $scope.selectedFile = null;
        }
    });

    $scope.triggeraddtocart = function () {
        var elementsToAdd = [];

        angular.forEach($scope.userFiles, function (file) {
            if (file.checked) {
                elementsToAdd.push(file.Id);
            }
        });

        if (elementsToAdd.length > 0) {
            shopCartService.addFilesToCart(elementsToAdd).then(function (res) {
                if (res.success) {
                    modalService.alert('', 'Dodano Pliki!');
                } else {
                    modalService.alert('', 'Blad, pliki znajduja sie juz w koszyku!');
                }

                $rootScope.$broadcast('RECALCULATE_CART');
            });

        } else {
            modalService.alert('', 'Zaznacz plik(i) do dodania do koszyka!');
        }

    };

    $scope.$watch('rows', function (value) {
        if (value) {
            $scope.userFiles = value;
        }
    });
}]);