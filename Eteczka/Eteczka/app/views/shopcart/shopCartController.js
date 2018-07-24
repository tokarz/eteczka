'use strict';
angular.module('et.controllers').controller('shopCartController', ['$scope', '$state', 'shopCartService', 'modalService', function ($scope, $state, shopCartService, modalService) {
    $scope.files = {
        rows: [],
        selectedRow: {}
    };
    $scope.emptyBasketMsg = 'Koszyk jest pusty!';
    $scope.printSelectedOptions = function () {

    }

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
        $scope.modalResult = $scope.modalResult || {};

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

        $scope.modalResult.filesToAttach = selectedFiles
    }

    $scope.openSendEmailDialog = function () {
        var modalOptions = {
            body: 'app/views/shopcart/shopCartModals/sendEmailModal.html',
            controller: $scope.sendEmailCtrl,
            locals: {
                selectedFiles: $scope.files.rows.filter(function (elm) {
                    if (($scope.files.selectedRow && $scope.files.selectedRow.Id === elm.Id) || elm.checked) {
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

                        shopCartService.sendFilesViaEmail(
                            result.recipients,
                            result.copyRecipients,
                            result.zipPassword,
                            result.subject,
                            result.content,
                            result.filesToAttach.map(function (file) { return file.PelnasciezkaEad })
                        ).then(function (res) {
                            if (res.success === true) {
                                modalService.alert('Wysylanie dokumentow z koszyka', 'Wiadomosc zostala wyslana');
                                $state.reload();
                            } else {
                                modalService.alert('Blad w wysylaniu wiadomosci', 'Blad! Wiadomosc nie zostala wyslana! Zweryfikuj wprowadzone dane i prawa dostepu lub skontaktuj sie z Administratorem');
                            }
                        });
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

    $scope.toggleSelectAll = function () {
        angular.forEach($scope.files.rows, function (elm) {
            if (elm) {
                elm.checked = !elm.checked;
            }
        });
    }

    $scope.deleteSelectedFromCart = function () {
        var elementsToDelete = [];

        angular.forEach($scope.files.rows, function (file) {
            if (file && (($scope.files.selectedRow && $scope.files.selectedRow.Id === file.Id) || (file.checked))) {
                elementsToDelete.push(file.Id);
            }
        });

        shopCartService.deleteSelectedCartElements(elementsToDelete).then(function (result) {
            $state.reload();
            if (result && result.success) {
                modalService.alert('', 'Pliki usunieto!');
            } else {
                modalService.alert('', 'Pliki nie mogly zostac usuniete!');
            }
        });
    }

    $scope.toolbar = [
        {
            action: $scope.openSendEmailDialog,
            itemClass: 'toolbar-option option-two fa fa-envelope-open-o',
        },
        {
            action: $scope.downloadFiles,
            itemClass: 'not-yet-available toolbar-option option-two fa fa-download',
        },
        {
            action: $scope.deleteSelectedFromCart,
            itemClass: 'toolbar-option option-three fa fa-trash-o',
        }
    ];

    shopCartService.getShoppingCartForUser().then(function (result) {
        if (result) {
            $scope.files = {
                rows: result.pliki,
                selectedRow: {}
            }
        }
    });

}]);