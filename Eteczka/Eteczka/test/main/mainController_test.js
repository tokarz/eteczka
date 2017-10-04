'use strict';
describe('ut', function () {
    var $controller, $window, $rootScope, $scope, $state, sessionService;

    beforeEach(module('et.controllers'));
    beforeEach(inject(function (_$rootScope_, _$controller_, _$window_, _$state_) {
        $rootScope = _$rootScope_;
        $controller = __$controller_;
        $window = _$window_;
        $state = _$state_;
    }));

    beforeEach(function () {
        $scope = $rootScope.$new();

        $controller('mainController', { '$window': $window, '$rootScope': $rootScope, '$scope': $scope, '$state': $state, 'sessionService': sessionService });
    });

    it('should set up a startup state', function () {
        expect($scope.title).toEqual('ETeczka');
        expect($scope.isLoaded).toBe(false);
        expect($scope.selectedUser).toEqual('');
        expect($scope.selectedFirm).toEqual('');
        expect($scope.isLoggedIn).toBe(false);
        expect($scope.startupContext).toEqual();
        //To Do: Mock
        //expect($scope.currentState).toEqual($state.current.name);
    });


});