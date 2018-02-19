'use strict';
angular.module('et.controllers').controller('adminSessionsController', ['$scope', '$state', 'sessionService', 'settingsService', 'modalService', function ($scope, $state, sessionService, settingsService, modalService) {
    $scope.setPasswordDialog = function () {
            
    }

    $scope.dialogController = function ($scope, $mdDialog, modalService) {
        $scope.modalResult = {};

        $scope.yesNoOptions = [{ name: 'TAK', value: true }, { name: 'NIE', value: false }]
        $scope.docPartOptions = ['A', 'B', 'C']

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

        $scope.isNotEqual = function (baseText, textToMatch) {
            return (baseText !== textToMatch)
        };
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

    $scope.openAdminPasswordDialog = function () {
        var modalOptions = {
            body: 'app/views/settings/sessions/admin/adminPassword/adminPasswordModal.html',
            controller: $scope.dialogController
        };

        openModal(
            modalOptions,
            function (value) {
                settingsService.setNewAdminPassword(value.OldLongPassword, value.Hasloshort, value.Haslolong).then(function (res) {
                    if (res.success) {
                        $state.reload();
                    }
                }).catch();
            }
        );
    }

    $scope.openFilesPasswordDialog = function () {
        var modalOptions = {
            body: 'app/views/settings/sessions/admin/filesPassword/filesPasswordModal.html',
            controller: $scope.dialogController
        };

        openModal(
            modalOptions,
            function (value) {
                settingsService.setNewFilesPassword(value.OldPassword, value.Hasloshort).then(function (res) {
                    if (res.success) {
                        $state.reload();
                    }
                }).catch();
            }
        );
    }


}]);