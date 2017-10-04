'use strict';

angular.module('et.utils').factory('peselService', [function () {
    return {
        isPeselValid: function (pesel) {
            if (pesel === null || typeof pesel === 'undefined') {
                return false
            }

            pesel = pesel.trim()

            if (pesel === '' || pesel.length !== 11) {
                return false
            }

            var peselControlDigit = parseInt(pesel[10], 10)
            var expectedControlDigit = 0
            var controlDigitNumerator = {
                2: 3,
                6: 3,
                1: 7,
                5: 7,
                9: 7,
                0: 9,
                4: 9,
                8: 9,  
                default: 1
            }

            pesel.split('').forEach(function(peselDigit, index) {
                if (index === 10) {
                    return
                }

                expectedControlDigit += parseInt(peselDigit, 10) * (controlDigitNumerator[index] || controlDigitNumerator['default']) 
            })

            if (peselControlDigit !== (expectedControlDigit % 10)) {
                return false
            }

            return true
        },
        getDateFromPesel: function (pesel) {
            if (!this.isPeselValid(pesel)) {
                console.log('invalid pesel')
                return null
            }


            pesel = pesel.trim()

            var month = 0
            var century = 0
            var centuryControlPart = parseInt(pesel.substr(2, 2), 10)

            if (centuryControlPart > 80) {
                century = 18
                month = centuryControlPart - 80
            } else if (centuryControlPart > 12) {
                century = 20
                month = centuryControlPart - 20
            } else {
                century = 19
                month = centuryControlPart
            }

            var monthString = month.toString()

            if (monthString.length === 1) {
                monthString = '0' + monthString
            }

            return century.toString() + pesel.substr(0, 2) + '-' + monthString + '-' + pesel.substr(4, 2)
        }
    }
}])
