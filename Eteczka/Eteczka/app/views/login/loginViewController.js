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
                sessionService.createSession().then(function (sessionId) {
                    $rootScope.SESSIONID = sessionId;
                    $scope.fetchedUser = result.user;


                    if ($scope.fetchedUser && $scope.fetchedUser.isAdmin) {
                        $state.go('admin');
                    } else {
                        $rootScope.$broadcast('USER_LOGGED_IN_EV', $scope.fetchedUser);
                        $state.go('options');
                    }
                }, function (err) {
                    alert('Blad Sesji! ' + err);
                    $state.go('login');
                });
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