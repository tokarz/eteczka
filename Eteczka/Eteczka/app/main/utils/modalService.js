'use strict';
angular.module('et.utils').factory('modalService', ['$mdDialog', function ($mdDialog) {
    var defaultUrl = 'app/main/utils/modalTemplate/modalTemplate.html';

    return {
        showModal: function (customModalOptions) {
            //var mergedController = function ($scope, $mdDialog) {
            //    arguments[2] = customModalOptions.locals;

            //    customModalOptions.controller.apply(this, arguments);
            //    ModalController.apply(this, arguments);
            //}

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
        }
    };

}]);