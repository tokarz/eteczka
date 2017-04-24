angular.module('et.services').service('editEmployeeService', ['$uibModal',
    function ($uibModal) {
        return {
            showModal: function (customModalOptions) {           

                var modalDefaults = {
                    animation: true,
                    templateUrl: 'app/views/employees/editEmployeesPopup/addEmployeePopup.html'
                };

                if (!modalDefaults.controller) {
                    modalDefaults.controller = function ($scope, $uibModalInstance) {
                        $scope.modalOptions = customModalOptions
                        $scope.employee = {}

                        $scope.modalOptions.ok = function () {
                            $uibModalInstance.close($scope.employee);
                        };
                        $scope.modalOptions.cancel = function () {
                            $uibModalInstance.dismiss('cancel');
                        };
                    }
                }

                return $uibModal.open(modalDefaults).result;
            }
        };
    }]);