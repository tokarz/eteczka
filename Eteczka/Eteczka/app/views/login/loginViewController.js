'use strict';
angular.module('et.controllers').controller('loginViewController', ['$rootScope', '$scope', '$state', '$mdDialog', 'modalService', 'loginService', 'sessionService', function ($rootScope, $scope, $state, $mdDialog, modalService, loginService, sessionService) {

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
                $mdDialog.show(
                 $mdDialog.alert()
                    .clickOutsideToClose(true)
                    .title('Blad Logowania')
                    .textContent('Haslo lub nazwa Uzytkownika jest nieprawidlowa')
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Rozumiem')
                 ).then(function () {
                     $state.go('login');
                 });

            }
        },
        function (err) {
            $mdDialog.show(
                 $mdDialog.alert()
                    .clickOutsideToClose(true)
                    .title('Blad Logowania')
                    .textContent('Blad Serwera! Skontaktuj sie z Administratorem lub sprobuj ponownie za kilka minut')
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Rozumiem')
                 ).then(function () {
                     $state.go('login');
                 });
        });
    }


    

    $scope.contactAdmin = function () {
        var modalOptions = {
            body: 'app/views/login/contactAdmin/contactAdminForm.html'
        };

        modalService.showModal(modalOptions)
            .then(function (result) {
                // dodaj funkcje wyslania wiadomosci
                alert(result.Username);
                $mdDialog.show(
                    $mdDialog.confirm()
                        .clickOutsideToClose(true)
                        .title('potwierdzenie')
                        .textContent('wiadomosc przeslana do administatora')
                        .ok('OK')
                ).then(function (err) {
                    
                    alert(err);
                });
            })
            .catch(function (error) {
                alert(err);
                console.log(error)
                if (error === 'cancel' || error === 'backdrop click') {
                    return;
                }

                $mdDialog.show(
                    $mdDialog.alert()
                       .clickOutsideToClose(true)
                       .title('Blad Formularza')
                       .textContent('Blad Serwera! Skontaktuj sie z Administratorem lub sprobuj ponownie za kilka minut')
                       .ariaLabel('Alert Dialog Demo')
                       .ok('Rozumiem')
                    ).then(function () {
                        $state.go('login');
                    });
            });
    };

}]);