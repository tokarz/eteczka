'use strict';
angular.module('et.controllers').controller('menuFilesContentController', ['$rootScope', '$scope', 'filesViewService', 'shopCartService', 'modalService', function ($rootScope, $scope, filesViewService, shopCartService, modalService) {
    $scope.selectedFile = null;

    $scope.userFiles = [];

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

        $scope.filesToAttach = selectedFiles
    }

    $scope.openSendEmailDialog = function () {
        var modalOptions = {
            body: 'app/views/shopcart/shopCartModals/sendEmailModal.html',
            controller: $scope.sendEmailCtrl,
            locals: {
                selectedFiles: $scope.userFiles.map(function (elm) {
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

    $scope.$watch('user', function (user) {
        if (user) {
            filesViewService.getFilesForUser(user).then(function (result) {
                $scope.userFiles = result.pliki;
            });
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