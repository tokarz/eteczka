'use strict';
angular.module('et.controllers').controller('adminViewController', ['$scope', '$timeout', function ($scope, $timeout) {
    $scope.sideMenuOptions = [
        {
            icon: 'fa fa-id-badge',
            label: 'Uzytkownicy',
            index: 0
        },
        {
            icon: 'fa fa-user-secret',
            label: 'Uprawnienia',
            index: 1
        },
        {
            icon: 'fa fa-file-text',
            label: 'Logi',
            index: 1
        },
        {
            icon: 'fa fa-gear',
            label: 'Ustawienia',
            index: 2
        }
    ];

    $scope.quickNote = {
        files: {
            icon: 'fa fa-gear',
            title: 'Tit1',
            value: 200,
            trend: '200'
        },
        users: {
            icon: 'fa fa-gear',
            title: 'Tit1',
            value: 200,
            trend: '+200'
        },
        imports: {
            icon: 'fa fa-gear',
            title: 'Tit1',
            value: 200,
            trend: '-200'
        },
        exports: {
            icon: 'fa fa-gear',
            title: 'Tit1',
            value: 200,
            trend: '-200'
        }
    };


    $scope.detailsVisible = false;

    $(document).mouseup(function (e) {
        var elm = $("#mySidenav");

        // if the target of the click isn't the container nor a descendant of the container
        if (!elm.is(e.target) && elm.has(e.target).length === 0) {
            $scope.detailsVisible = false;
            elm.width("60px");
            elm.find('.option-label').addClass('option-hidden');
            elm.unbind('click', $(document));
        }
    });

    $scope.toggleDetails = function () {
        $scope.detailsVisible = !$scope.detailsVisible;
        var elm = $("#mySidenav");

        if ($scope.detailsVisible) {
            elm.width("150px");
            $timeout(function () {
                elm.find('.option-label').removeClass('option-hidden');
            }, 700);
        } else {
            elm.width("60px");
            
            elm.find('.option-label').addClass('option-hidden');
            

            elm.unbind('click', $(document));
        }
    }

    $scope.showPanelMoveDirection = function () {
        var result = '';
        if ($scope.detailsVisible) {
            result = 'detailsButton fa fa-chevron-left';
        } else {
            result = 'detailsButton fa fa-chevron-right';
        }

        return result;
    }

}]);