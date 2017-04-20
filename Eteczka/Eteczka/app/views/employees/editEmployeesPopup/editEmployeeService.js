angular.module('et.services').service('editEmployeeService', ['$uibModal',
    function ($uibModal) {
        return {
            showModal: function (customModalDefaults, customModalOptions) {
                if (!customModalDefaults) customModalDefaults = {};
                customModalDefaults.backdrop = 'static';

                return this.show(customModalDefaults, customModalOptions);
            },
            show: function (customModalDefaults, customModalOptions) {
                var tempModalDefaults = {
                    animation: true,
                    templateUrl: 'app/views/employees/editEmployeesPopup/addEmployeePopup.html',
                    ariaLabelledBy: 'modal-title',
                    ariaDescribedBy: 'modal-body'
                };
                var tempModalOptions = {};

                return $uibModal.open(tempModalDefaults).result;
            }
        };
    }]);