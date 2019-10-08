'use strict';
angular.module('et.controllers').controller('companyViewController', ['$scope', '$state', 'companyService', 'modalService', function ($scope, $state, companyService, modalService) {
    $scope.options = [
        { Nazwa: 'Wydzialy', target: 'departments' },
        { Nazwa: 'Podwydzialy', target: 'subdepartments' },
        { Nazwa: 'Rejony' },
        { Nazwa: 'Konta5' }
    ];
    $scope.parameters = {
        options: $scope.options,
        selectedOption: {},
        targets: [],
        target: {},
        searchTerm: '',
        loading: false
    };

    $scope.showTarget = function () {
        return ($scope.parameters.targets.length !== 0)
    }
    $scope.showDepartment = function () {
        console.log('showDepartment check')
        return ($scope.parameters.selectedOption.Nazwa === $scope.options[0].Nazwa)
    }

    $scope.getAllDepartments = function () {
        companyService.getAllDepartments().then(function (result) {
            $scope.parameters.loading = false;
            $scope.departments = result.Wydzialy;
        }, function (err) {
            $scope.parameters.loading = false;
            console.error(err);
        });
    };

    $scope.getAllDepartments();

    $scope.$watch('parameters.selectedOption', function (val, oldVal) {
        console.log('value', val)
        if (val) {
            if (typeof val.Nazwa !== 'string') {
                $scope.parameters.targets = [];

                return
            }

            var chosen = $scope.options.find(function (option) {
                return option.Nazwa === val.Nazwa
            })

            if (typeof chosen !== 'object') {
                console.error('chosen unknown option')

                return
            }

            $scope.parameters.targets = $scope[chosen.target] || []
        }
    });

    $scope.$watch('parameters.target', function (val, oldVal) {
        if (val) {
            if (typeof val.Wydzial === 'string') {
                companyService.getSubdepartments(val.Wydzial.trim()).then(function (subdepartments) {
                    console.log('sub', subdepartments)
                })
            }
        }
    });
}]);
