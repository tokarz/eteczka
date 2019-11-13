'use strict';
angular.module('et.controllers').controller('companyViewController', ['$scope', '$state', 'companyService', 'modalService', function ($scope, $state, companyService, modalService) {
    var tabs = []
    $scope.parameters = {
        tab: { Id: 0, Name: 'Lista Firm' },
        company: {},
        searchTerm: '',
        companies: [],
        loading: false
    };

    $scope.startProcessing = function () {
        $scope.parameters.loading = true;
        $scope.parameters.companies = [];
    }

    $scope.getAll = function () {
        companyService.getAll().then(function (result) {
            $scope.parameters.loading = false;
            $scope.parameters.companies = result.Firmy;
        }, function (err) {
            $scope.parameters.loading = false;
            console.error(err);
        });
    };

    $scope.$watch('parameters.searchTerm', function (value) {
        if (value && value.trim() !== '' && value.trim().length > 1) {
            $scope.startProcessing();

            companyService.searchByText(value).then(function (result) {
                $scope.parameters.loading = false;
                $scope.parameters.companies = result.pracownicy;
            }, function (err) {
                $scope.parameters.loading = false;
                console.error(err);
            });
        } else {
            if (value === '') {
                $scope.getAll();
            }
        }
    });


}]);
