'use strict';
angular.module('et.utils').factory('modalService', [function () {
    var defaultBodyId = 'default-modal-body';

    return {
        openModal: function (modalOptions, modalBodyId) {
            if (!modalBodyId) {
                modalBodyId = defaultBodyId;
            }
            console.log('inside modal')
            if (
                !(modalOptions === Object(modalOptions)) ||
                typeof modalOptions.title === 'undefined' ||
                typeof modalOptions.body == 'undefined'
            ) {
                throw new Error('modalOptions must contain title and body for modal!');
            }
            // To nie jest dobra praktyka angularowa, odwolanie do obiektow DOM powinno miec miejsce tylko w dyrektywach w kodzie link: function() {...}
            //Powalcz z tym jak masz czas a jak nie to mozemy wrocic do bootstrapa lub uzyc np czegos takiego:
            // https://github.com/dwmkerr/angular-modal-service
            var modalElement = $('#default-modal-body');

            //    var modalDiv = modalWindow.dialog({
            //        title: "Page",
            //        autoOpen: false,
            //        dialogClass: 'open',
            //        modal: true,
            //        height: 500,
            //        minWidth: 400,
            //        minHeight: 400,
            //        draggable: true,
            //         close: function () { $(this).remove(); },
            //        buttons: { "Ok": function () { $(this).dialog("close"); } }
            //    });
            //    $dialog.dialog('open');
            if (modalElement.hasClass('open')) {
                modalElement.removeClass('open');
            }
            modalElement.addClass('open');
            //modalElement.classList ? modalElement.classList.add('open') : modalElement.className += ' open ';

            console.log(modalElement)
        },

        confirmModalInput: function () {
            // add save data logic
            // nie wiem czy bootstrapowy sposob tu zadziala
        },

        closeModal: function () {
            // add  cancel logic
            //modalTarget.classList ? modalTarget.classList.remove('open') : modalTarget.className = modalWindow.className.replace(new RegExp('(^|\\b)' + 'open'.split(' ').join('|') + '(\\b|$)', 'gi'), ' ');
            var modalElement = $('#default-modal-body');

            //    var modalDiv = modalWindow.dialog({
            //        title: "Page",
            //        autoOpen: false,
            //        dialogClass: 'open',
            //        modal: true,
            //        height: 500,
            //        minWidth: 400,
            //        minHeight: 400,
            //        draggable: true,
            //         close: function () { $(this).remove(); },
            //        buttons: { "Ok": function () { $(this).dialog("close"); } }
            //    });
            //    $dialog.dialog('open');
            if (modalElement.hasClass('open')) {
                modalElement.removeClass('open');
            }

        }
    }
}])
