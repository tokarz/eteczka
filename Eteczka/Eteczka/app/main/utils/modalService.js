'use strict';
angular.module('et.utils').factory('modalService', ['$mdDialog', function ($mdDialog) {
    var defaultUrl = 'app/main/utils/modalTemplate/modalTemplate.html';

    return {
        showModal: function (customModalOptions) {
            var dialogParams = {
                templateUrl: customModalOptions.body ? customModalOptions.body : defaultUrl,
                controller: customModalOptions.controller ? customModalOptions.controller : 'ModalController',
                locals: customModalOptions.locals,
                clickOutsideToClose: true
            }

            return $mdDialog.show(dialogParams);
        },
        alert: function (title, content, ok) {
            if (!ok) {
                ok = 'Ok';
            }

            return $mdDialog.show(
                    $mdDialog.confirm()
                        .clickOutsideToClose(true)
                        .title(title)
                        .textContent(content)
                        .ok(ok)
                );
        },
        confirm: function (title, message) {
            var confirm = $mdDialog.confirm()
                .clickOutsideToClose(false)
                .title(title)
                .textContent(message)
                .ariaLabel('Lucky day')
                .ok('Tak')
                .cancel('Nie');

            return $mdDialog.show(confirm);
        }
        
    };

}]);