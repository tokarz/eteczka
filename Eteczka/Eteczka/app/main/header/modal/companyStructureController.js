'use strict';
angular.module('et.controllers').controller('companyStructureController', ['$scope', 'modalService', function ($scope, modalService) {

    $scope.addFiletypeCtrl = function ($scope, $mdDialog) {
        $scope.yesNoOptions = [{ name: 'TAK', value: true }, { name: 'NIE', value: false }]
        $scope.editOptions = [{ name: 'TAK', value: 'b' }, { name: 'NIE', value: 'a' }]
        $scope.docPartOptions = ['A', 'B', 'C']
        $scope.modalResult.Dokwlasny = $scope.modalResult.Dokwlasny || $scope.yesNoOptions[0].value;
        
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

    $scope.triggerAddFileTypeDialog = function () {
        var modalOptions = {
            body: 'app/main/components/company-structure/addFileTypeModal.html',
            controller: $scope.addFiletypeCtrl,
        };

        openModal(
            modalOptions,
            function (value) {
                //TODO: add executor
            }
        );
    }
}])