'use strict';
angular.module('et.controllers').controller('menuFilesContentController', ['$rootScope', '$scope', '$state', 'filesViewService', 'shopCartService', 'modalService', 'cacheService', 'usersService', function ($rootScope, $scope, $state, filesViewService, shopCartService, modalService, cacheService, usersService) {
    $scope.selectedFile = null;
    $scope.emptyTableMessage = 'Nie zaznaczono elementu do wyświetlenia';
    $scope.noFilesMessage = 'Zaznaczona osoba nie ma przypisanych plików';
    $scope.fileListWrapperClass = 'file-list-wrapper-full';
    $scope.userFiles = [];
    $scope.selected = { row: - 1 };
    $scope.selectedUser = false;
    $scope.tLastAddedLabel = 'ostatnio dodano: ';
    $scope.tSummaryTitle = 'Teczka pracownika';

    $scope.colDefs = [{ 'type': 'numstring', 'targets': 0 }];

    $scope.endsWith234 = function (value) {
        return value.endsWith('2') || value.endsWith('3') || value.endsWith('4');
    };

    $scope.$watch('selected.row', function (row) {
        if (typeof row !== 'undefined' && row >= 0 && row < $scope.userFiles.length) {
            $scope.selectedFile = $scope.userFiles[row];
        }
    });

    $scope.loadFileTypes = function () {
        filesViewService.getFileTypes().then(function (fileTypes) {
            $scope.fileTypes = fileTypes.PobraneDokumenty;
        });
    };
    $scope.loadEmployees = function () {
        filesViewService.getAllEmployees().then(function (employees) {
            $scope.employees = employees.Data.data;
        });
    };

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
    };

    $scope.sendEmailCtrl = function ($scope, $mdDialog, selectedFiles) {
        $scope.modalResult = $scope.modalResult || {};

        $scope.hide = function () {
            $mdDialog.hide();
        };

        $scope.cancel = function () {
            $mdDialog.cancel();
        };

        $scope.answer = function (answer, errors) {
            if (!errors || Object.keys(errors).length === 0) {
                $mdDialog.hide(answer);
            }
        };

        $scope.modalResult.filesToAttach = selectedFiles;
    };

    $scope.generatePdf = function () {
        if ($scope.user) {
            filesViewService.generatePdf($scope.user).then(function () {
                modalService.alert('Raport Pdf', 'Raport wygenerowano!');
            }, function () {
                modalService.alert('Raport Pdf (Blad!) ', 'Blad przy generowaniu raportu! Sprawdz logi systemowe');
            });
        }
    };

    $scope.generateExcelReport = function () {
        if ($scope.user) {
            filesViewService.generateExcelReport($scope.user).then(function () {
                modalService.alert('Raport Excel', 'Raport wygenerowano!');
            }, function () {
                modalService.alert('Raport Excel (Blad!) ', 'Blad przy generowaniu raportu! Sprawdz logi systemowe');
            });
        }
    };

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
                var promise = (Array.isArray(value.filesToAttach) && value.filesToAttach.length > 0)
                    ? triggerZipPasswordModal()
                    : Promise.resolve({});

                promise
                    .then(function (zipPassword) {
                        var result = Object.assign({}, value, zipPassword);

                        shopCartService.sendFilesViaEmail(
                            result.recipients,
                            result.copyRecipients,
                            result.zipPassword,
                            result.subject,
                            result.content,
                            result.filesToAttach.map(function (file) { return file.PelnasciezkaEad })
                        ).then(function (res) {
                            if (res.success === true) {
                                modalService.alert('Wysylanie dokumentow', 'Wiadomosc zostala wyslana');
                                $state.reload();
                            } else {
                                modalService.alert('Blad w wysylaniu wiadomosci', 'Blad! Wiadomosc nie zostala wyslana! Zweryfikuj wprowadzone dane i prawa dostepu lub skontaktuj sie z Administratorem');
                            }
                        });
                    });
            }
        );
    };

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
                    if (!errors || Object.keys(errors).length === 0) {
                        $mdDialog.hide(answer);
                    }
                };
            }
        };

        return openModal(modalOptions, function (value) { return value; });
    };

    $scope.selectFile = function (file) {
        if ($scope.selectedFile === file) {
            $scope.selectedFile = null;
            $scope.fileListWrapperClass = 'file-list-wrapper-full';
        } else {
            $scope.selectedFile = file;
            $scope.fileListWrapperClass = '';
        }

        $scope.selectedRow = $scope.selectedFile;
    };

    $scope.getRowStyle = function (file) {
        var result = 'table-row';

        if (file === $scope.selectedFile) {
            result += ' active-row';
        }

        if (file.checked) {
            result += ' checked-row';
        }

        return result;
    };

    $scope.toggleSelectAll = function () {
        angular.forEach($scope.userFiles, function (elm) {
            elm.checked = !elm.checked;
        });
    };

    $scope.triggerDeleteEmployeePopup = function () {
        var filesToDelete = $scope.userFiles
            .filter(function (fileRow) { return (fileRow.checked || ($scope.selectedFile && fileRow.Id === $scope.selectedFile.Id)) })
            .map(function (selectedFileRow) { return selectedFileRow.Id });

        if (filesToDelete.length === 0) {
            modalService.alert('Usuwanie plików', 'Nie zaznaczono żadnego pliku!');

            return;
        }

        modalService.confirm('Usuwanie plików', 'Czy jesteś pewien, że chcesz usunąć zaznaczone pliki?').then(function () {
            modalService.promptPassword('Haslo', 'Wymagane podanie hasła (krótkiego)')
                .then(function (password) {
                    usersService.checkPassword(password && password.userPassword).then(function (correctPassword) {
                        if (correctPassword && correctPassword.success) {
                            filesViewService.deleteSelectedFiles(filesToDelete).then(function () {
                                modalService.alert('Usuwanie plików', 'Pliki usunięto!');
                                cacheService.addToCache('MFCC.user', $scope.user);

                                $state.reload();
                            }).catch(function () {
                                modalService.alert('Usuwanie plików', 'Pliki nie mogły być usunięte! Skontaktuj się z Administratorem');
                            });
                        } else {
                            modalService.alert('Usuwanie plików', 'Błędne hasło!');
                        }
                    });
                }).catch(function () {
                    modalService.alert('Usuwanie plików', 'Błąd usuwania plików!');
                });
        });
    };

    $scope.editFileDescriptionCtrl = function ($scope, $mdDialog, modalService, description, fileTypes, employees, name, user) {
        $scope.modalResult = Object.assign(
            {},
            description,
            {
                Pracownik: user,
                Typ: { Symbol: description.Symbol, Nazwa: description.OpisRodzajuDokumentu, Teczkadzial: description.TeczkaDzial },
                DataWytworzenia: new Date(description.DataDokumentuStr),
                DataPocz: new Date(description.DataPoczStr),
                DataKoniec: (typeof description.DataKoniecStr !== 'undefined') ? new Date(description.DataKoniecStr) : undefined,
                Dokwlasny: description.DokumentWlasny
            }
        );
        $scope.yesNoOptions = [{ name: 'TAK', value: true }, { name: 'NIE', value: false }];
        $scope.docPartOptions = ['A', 'B', 'C'];
        $scope.modalResult.Nazwa = name;

        $scope.pracownikPesel = '';

        $scope.hide = function () {
            $mdDialog.hide();
        };

        $scope.cancel = function () {
            $mdDialog.cancel();
        };

        $scope.answer = function (answer, errors) {
            if (!errors || Object.keys(errors).length === 0) {
                $mdDialog.hide(answer);
            }
        };

        $scope.isDisabled = function () {
            return !$scope.modalResult.Typ || !$scope.isTypeWithDates($scope.modalResult.Typ.Symbol);
        };

        $scope.fillValidFromDate = function () {
            if (!$scope.modalResult.DataPocz && $scope.modalResult.DataWytworzenia) {
                $scope.modalResult.DataPocz = $scope.modalResult.DataWytworzenia;
            }
        };

        $scope.isTypeWithDates = function (fileSymbol) {
            var type = fileTypes.find(function (file) {
                return file.Symbol === fileSymbol;
            }).Typedycji;

            if (type.trim() === 'b') {
                return true;
            }
            else {
                return false;
            }
        };

        var querySearch = function (arrayTosearchIn, keys, query) {
            return query ? arrayTosearchIn.filter(createFilterFor(keys, query)) : arrayTosearchIn;
        };

        $scope.fileTypeSearch = function (query) {
            return querySearch(fileTypes, ["Symbol", "Nazwa"], query);
        };

        $scope.employeeSearch = function (query) {
            return querySearch(employees, ["Nazwisko"], query);
        };

        var createFilterFor = function (keys, query) {
            var lowercaseQuery = angular.lowercase(query);

            return function filterFn(object) {
                return keys.some(function (key) {
                    return (object[key].toLowerCase().indexOf(lowercaseQuery) === 0);
                });
            };
        };
    };

    $scope.triggerEditFileDescriptionDialog = function () {
        if ($scope.selectedFile === null || $scope.selectedUser === null) {
            return;
        }
        var modalOptions = {
            body: 'app/views/files/addFile/fileDescriptionPopup/upsertFileDescription.html',
            controller: $scope.editFileDescriptionCtrl,
            locals: {
                description: $scope.selectedFile,
                fileTypes: $scope.fileTypes,
                employees: $scope.employees,
                activeEmployee: $rootScope.activeUser,
                name: $scope.selectedFile ? $scope.selectedFile.Nazwa : '',
                user: $scope.selectedUser
            }
        };

        openModal(
            modalOptions,
            function (value) {
                $scope.createdMetaData = value;
                $rootScope.activeUser = value.Pracownik ? value.Pracownik : {};
                modalService.confirm('Zapisać zmiany?', 'Czy chcesz zapisać zmiany w opisie pliku ?').then(function () {
                    modalService.promptPassword('Haslo', 'Wymagane podanie hasła (krótkiego)')
                        .then(function (password) {
                            usersService.checkPassword(password && password.userPassword).then(function (correctPassword) {
                                if (correctPassword && correctPassword.success) {
                                    var mappedValue = alignValueStructureToBeObject(value);
                                    filesViewService.editCommittedFile(value).then(function (res) {
                                        if (res.sucess) {
                                            modalService.alert('Zatwierdzanie zmian w pliku', 'Plik zostal zmieniony');
                                            $state.reload();
                                        } else {
                                            modalService.alert('Zatwierdzanie zmian w pliku', 'Blad! Plik nie zostal zmieniony! Zweryfikuj dane i prawa dostepu lub skontaktuj sie z Administratorem');
                                        }
                                    });
                                } else {
                                    modalService.alert('Zatwierdzanie zmian w pliku', 'Niepoprawne hasło!');
                                }
                            });
                        });
                });

                function alignValueStructureToBeObject(source) {
                    var result = Object.assign(source, {});
                    if (result.Typ) {
                        result.Typ = Object.assign({
                            SymbolEad: source.SymbolEad,
                            Dokwlasny: source.DokumentWlasny,
                            Teczkadzial: source.Teczkadzial,
                            Idoper: source.IdOper,
                            Idakcept: source.IdAkcept,
                            Datamodify: source.DataModyfikacji,
                            Dataakcept: source.DataAkcept,
                            SystemBazowy: source.Systembazowy,
                            Usuniety: source.Usuniety
                        }, result.Typ);

                    }

                    return result;
                }
            });
    };

    $scope.$watch('user', function (user) {
        $scope.userFiles = [];
        if (user && Object.keys(user).length !== 0) {
            $scope.userFiles = [];
            $scope.selectedUser = user;
            filesViewService.getFilesForUser(user).then(function (result) {
                $scope.userFiles = result.pliki;
                $scope.selectedFile = null;

                $scope.lastAddedFileType = (result && result.last) ? result.last.Symbol : '';
                $scope.lastAddedFileDate = (result && result.last) ? result.last.DataSkanuStr : '';

                $scope.filesSummaryText = $scope.userFiles.length === 1 ? 'dokument' : ($scope.endsWith234($scope.userFiles.length + '') ? 'dokumenty' : 'dokumentów');
                $scope.documentsSummary = $scope.userFiles.length + ' ' + $scope.filesSummaryText;
                $scope.user.Summary = ($scope.userFiles && $scope.userFiles.length) > 0 ? ($scope.user.Imie + ' ' + $scope.user.Nazwisko + ', ' + $scope.documentsSummary + ', ' + $scope.tLastAddedLabel + $scope.lastAddedFileType + ' ' + $scope.lastAddedFileDate) : '';
            });
        } else {
            $scope.userFiles = [];
            $scope.selectedFile = null;
            $scope.selectedUser = null;
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


    $scope.userFromCache = cacheService.getValue('MFCC.user');
    if ($scope.userFromCache) {
        $scope.user = $scope.userFromCache;
    }
}]);