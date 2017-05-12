angular.module('et.services').service('editEmployeeService', ['$uibModal',
    function ($uibModal) {
        return {
            showModal: function (customModalOptions, parentScope) {           

                var modalDefaults = {
                    animation: true,
                    templateUrl: 'app/views/employees/editEmployeesPopup/addEmployeePopup.html'
                };

                if (!modalDefaults.controller) {
                    modalDefaults.controller = function ($scope, $uibModalInstance) {
                        $scope.parameters = customModalOptions.parameters;
                        $scope.modalOptions = customModalOptions
                        $scope.modalResult = {}
                        alert($scope.parameters[0].jrwa);

                        $scope.modalOptions.ok = function () {
                            $uibModalInstance.close($scope.modalResult);
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