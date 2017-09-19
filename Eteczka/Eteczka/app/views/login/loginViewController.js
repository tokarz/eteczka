'use strict';
angular.module('et.controllers').controller('loginViewController', ['$rootScope', '$scope', '$state', 'modalService', 'loginService', 'sessionService', function ($rootScope, $scope, $state, modalService, loginService, sessionService) {

    $scope.credentials = {
        username: '',
        password: ''
    };

    $scope.tryLogin = function (credentials) {
        $state.go('processing');
        $scope.firmChoices = [];
        loginService.authenticate(credentials.username, credentials.password).then(function (result) {
            if (result && result.success) {
                $rootScope.SESSIONID = result.sesja;
                sessionService.createSession(result.sesja);
                $scope.fetchedUser = {
                    companies: result.firms,
                    userdetails: result.userdetails
                };

                if (result.isadmin) {
                    $rootScope.$broadcast('USER_LOGGED_IN_EV', $scope.fetchedUser);
                    $state.go('admin');
                } else {
                    $rootScope.$broadcast('USER_LOGGED_IN_EV', $scope.fetchedUser);
                    $state.go('options');
                }
            }

            else {
                $state.go('login');
                alert('Login failed!');
            }
        },
        function (err) {
            console.error('LOGIN FAILED!' + err);
            $state.go('login');
        });
    }

    $scope.contactAdmin = function () {
        var modalOptions = {
            title: 'Kontakt z Adminem',
            body: 'app/views/login/contactAdmin/contactAdminForm.html'
        };

        modalService.showModal(modalOptions).then(function (result) {
            loginService.sendMessageToAdmin(result).then(function (success) {
                if (success) {
                    //show message - sent
                }
            });
        }).catch(function (error) {
            alert("error found!");
        });
    };



}]);