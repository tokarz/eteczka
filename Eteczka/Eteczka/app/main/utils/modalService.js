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

            if (customModalOptions.controller) {
                Object.assign(customModalOptions, ModalController);
            }

            var dialogParams = {
                templateUrl: customModalOptions.body ? customModalOptions.body : defaultUrl,
                controller: customModalOptions.controller ? customModalOptions.controller : 'ModalController',
                locals: customModalOptions.locals,
                clickOutsideToClose: true
            }

            return $mdDialog.show(dialogParams);
        }
    };

}]);