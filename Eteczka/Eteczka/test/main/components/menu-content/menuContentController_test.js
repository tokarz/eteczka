'use strict';
describe('MenuContent UT', function () {
    var $controller, $q, $rootScope, $scope, menuContentService, modalService, peselService, utilsService;

    beforeEach(module('et.controllers'));
    beforeEach(inject(function (_$controller_, _$rootScope_, _$q_) {
        $controller = _$controller_;
        $rootScope = _$rootScope_;
        $q = _$q_;
    }));

    beforeEach(function () {
        $scope = $rootScope.$new();

        menuContentService = {
            getActiveCompany: function () {
                var deferred = $q.defer();

                deferred.resolve('AFM_ALBO_JAGROL');

                return deferred.promise;
            },
            getRegionsForFirm: function () {

            },
            getDepartmentsForFirm: function () {

            },
            getAccounts5: function () {

            },
            getSubDepartmets: function () {

            },
            getRegionsForFirms: function () {

            }
        };
        modalService = {};
        peselService = {};
        utilsService = {
            callStartupMethod: function () { },
            findUniqueValueInListForKey: function () { }
        };


        $controller('menuContentController', { '$scope': $scope, 'menuContentService': menuContentService, 'modalService': modalService, 'peselService': peselService, 'utilsService': utilsService });
    })

    it('should test a startup state', function () {
        spyOn(utilsService, 'callStartupMethod');

        $controller('menuContentController', { '$scope': $scope, 'menuContentService': menuContentService, 'modalService': modalService, 'peselService': peselService, 'utilsService': utilsService });

        expect($scope.company).toBe(null);
        expect($scope.selectedWorkplace).toEqual({});
        expect($scope.workplaceParams).toEqual({
            loadingRegions: false,
            loadingDepartments: false,
            loadingSubDepartments: false,
            loadingAccouts5: false,
            regions: [],
            departments: [],
            subDepartments: [],
            accounts5: []
        });

        expect(utilsService.callStartupMethod).toHaveBeenCalledWith($scope.loadDataWithSesionId);
    });

    it('should load all menu contents on startup', function () {
        spyOn($scope, 'loadActiveCompany');
        spyOn($scope, 'loadRegionList');
        spyOn($scope, 'loadDepartmentList');
        spyOn($scope, 'loadAccounts5');

        expect($scope.loadActiveCompany).not.toHaveBeenCalled();
        expect($scope.loadRegionList).not.toHaveBeenCalled();
        expect($scope.loadDepartmentList).not.toHaveBeenCalled();
        expect($scope.loadAccounts5).not.toHaveBeenCalled();

        $scope.loadDataWithSesionId();

        expect($scope.loadActiveCompany).toHaveBeenCalled();
        expect($scope.loadRegionList).toHaveBeenCalled();
        expect($scope.loadDepartmentList).toHaveBeenCalled();
        expect($scope.loadAccounts5).toHaveBeenCalled();
    });

    it('should get a departmend by Id from BE', function () {
        $scope.workplaceParams = {
            departments: ['some', 'list']
        };

        spyOn(utilsService, 'findUniqueValueInListForKey').and.returnValue('someValue');

        var result = $scope.getDepartmentById('SOME_DEPARTMENT');

        expect(result).toEqual('someValue');
        expect(utilsService.findUniqueValueInListForKey).toHaveBeenCalledWith(['some', 'list'], 'SOME_DEPARTMENT', 'Wydzial');
    });

    it('should fetch all subdepartments for a given department by id', function () {
        var departments = [
                { Podwydzial: 'SUBDEPARTMENT_1' },
                { Podwydzial: 'OUR_SEARCH_ID' },
                { Podwydzial: 'DEPARTMENT_2' }
        ]
        spyOn(utilsService, 'findUniqueValueInListForKey').and.returnValue('someValue');
        spyOn(menuContentService, 'getSubDepartmets').and.callFake(function () {
            var deferred = $q.defer();

            deferred.resolve({ PodWydzialy: departments });

            return deferred.promise;
        });

        $scope.getSubdepartmentById('WYDZIAL1', 'OUR_SEARCH_ID').then(function (result) {
            expect(result).toEqual('someValue');
            expect(utilsService.findUniqueValueInListForKey).toHaveBeenCalledWith(departments, 'OUR_SEARCH_ID', 'Podwydzial');
        });
        $rootScope.$apply();
    });

    it('should return an empty for wrong when BE fails', function () {
        var departments = [
                { Podwydzial: 'SUBDEPARTMENT_1' },
                { Podwydzial: 'DEPARTMENT_2' }
        ]

        spyOn(menuContentService, 'getSubDepartmets').and.callFake(function () {
            var deferred = $q.defer();

            deferred.reject({ PodWydzialy: departments });

            return deferred.promise;
        });

        $scope.getSubdepartmentById('WYDZIAL1', 'OUR_SEARCH_ID').then(function (result) {
            expect(result).toEqual({});
        });
        $rootScope.$apply();
    });

    it('should fetch account5 from BE', function () {
        $scope.workplaceParams = {
            accounts5: ['some', 'list']
        };

        spyOn(utilsService, 'findUniqueValueInListForKey').and.returnValue('someValue');

        var result = $scope.getAccount5ByNumber('SOME_DEPARTMENT');

        expect(result).toEqual('someValue');
        expect(utilsService.findUniqueValueInListForKey).toHaveBeenCalledWith(['some', 'list'], 'SOME_DEPARTMENT', 'Konto5');
    });


    it('should fetch all Regions for a given id', function () {
        $scope.workplaceParams = {
            regions: ['some', 'list']
        };

        spyOn(utilsService, 'findUniqueValueInListForKey').and.returnValue('someValue');

        var result = $scope.getRegionById('SOME_DEPARTMENT');

        expect(result).toEqual('someValue');
        expect(utilsService.findUniqueValueInListForKey).toHaveBeenCalledWith(['some', 'list'], 'SOME_DEPARTMENT', 'Rejon');
    });


});