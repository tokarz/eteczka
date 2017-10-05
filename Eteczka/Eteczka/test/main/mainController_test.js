'use strict';
describe('ut', function () {
    var $controller, $window, $rootScope, $scope, $state, $q, sessionService;

    beforeEach(module('et.controllers'));
    beforeEach(inject(function (_$rootScope_, _$controller_, _$window_, _$q_) {
        $rootScope = _$rootScope_;
        $controller = _$controller_;
        $window = _$window_;
        $q = _$q_;
    }));

    beforeEach(function () {
        var stateMock = { go: function () { }, current: { name: 'someStateName' } },
        sessionService = {
            killSession: function () {
                var deferred = $q.defer();

                deferred.resolve(true);

                return deferred.promise;
            }
        };

        $scope = $rootScope.$new();

        $controller('mainController', { '$window': $window, '$rootScope': $rootScope, '$scope': $scope, '$state': stateMock, 'sessionService': sessionService });
    });

    it('should set up a startup state', function () {
        expect($scope.title).toEqual('ETeczka');
        expect($scope.isLoaded).toBe(false);
        expect($scope.selectedUser).toEqual('');
        expect($scope.selectedFirm).toEqual('');
        expect($scope.isLoggedIn).toBe(false);
        expect($scope.startupContext).toEqual({
            title: 'EAd',
            version: '1.0'
        });

        expect($scope.currentState).toEqual({
            state: 'someStateName'
        });
    });


});