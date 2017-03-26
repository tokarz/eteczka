'use strict';
angular.module('et.controllers').controller('loginViewController', ['$scope', '$state', function ($scope, $state) {

    $scope.credentials = {
        username: '',
        password: ''
    };

    $scope.loginFailConstants = {
        usernameEmpty:      'Nazwa użytkownika jest wymagana',
        passwordEmpty:      'Proszę wpisać hasło',
        usernameInvalid:    'Nie znaleziono użytkownika',
        passwordInvalid:    'Niepoprawne hasło!'
    };

    $scope.loginErrorMsg;

    $scope.tryLogin = function (credentials) {
        if ($scope.errorOnLogin(credentials)) {
            $scope.displayErrorMessage();
            return;
        }

        $state.go('options');
    }

    $scope.displayErrorMessage = function () {
        //zmień!
        var errorMessage = document.getElementById('loginErrorMsg');
        errorMessage.style.display = 'block';
    }

    $scope.errorOnLogin = function (credentials) {
        var currentUser     = $scope.credentials.username;
        var currentPassword = $scope.credentials.password;

        var properPassword = true;

        return  $scope.areEmptyFields(currentUser, currentPassword)     ||
                !$scope.isUsernameRegistered(currentUser)               ||
                !$scope.isProperPassword(currentUser, currentPassword);
    };

    $scope.areEmptyFields = function (username, password) {
        if (!username) {
            $scope.loginErrorMsg = $scope.loginFailConstants.usernameEmpty;
            return true;
        }
        if (!password) {
            $scope.loginErrorMsg = $scope.loginFailConstants.passwordEmpty;
            return true;
        }
        return false;
    }

    $scope.isUsernameRegistered = function (username) {
        var usernames = $scope.getRegisteredUsers();
               
        if ($.inArray(username, usernames) == -1) {
            $scope.loginErrorMsg = $scope.loginFailConstants.usernameInvalid + ": " + username;
            return false;
        }
        return true;
    }

    
    $scope.isProperPassword = function (currentUser, currentPassword) {
        var password = $scope.getPaswordForUser(currentUser);

        if (currentPassword != password) {
            $scope.loginErrorMsg = $scope.loginFailConstants.passwordInvalid;
            return false;
        }
        return true;
    }

    //temporary function - to be replaced by the backend call
    $scope.getPaswordForUser = function (currentUser) {
        return "password";
    }


    //temporary function - to be replaced by the backend call
    $scope.getRegisteredUsers = function () {
        return ["admin", "ola", "burqin", "maksio"];
    }

}]);