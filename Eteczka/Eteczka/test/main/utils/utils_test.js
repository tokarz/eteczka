'use strict';
describe('Utils UnitTest', function () {
    var sut;

    beforeEach(module('et.services'));
    beforeEach(inject(function (_utilsService_) {
        sut = _utilsService_;
    }));


    it('should return a valid object from the list', function () {
        var list = [
                { Wydzial: 'DEPARTMENT_1' },
                { Wydzial: 'SOME_DEPARTMENT' },
                { Wydzial: 'DEPARTMENT_2' }
        ];

        var result = sut.findUniqueValueInListForKey(list, 'SOME_DEPARTMENT', 'Wydzial');
        expect(result).toEqual(list[1]);
    });

    it('should return an empty object from the list when not found', function () {
        var list = [
                { Wydzial: 'DEPARTMENT_1' },
                { Wydzial: 'DEPARTMENT_2' }
        ];

        var result = sut.findUniqueValueInListForKey(list, 'SOME_DEPARTMENT', 'Wydzial');
        expect(result).toEqual({});
    });

    it('should return an empty object when bad or no parameter is given', function () {
        expect(sut.findUniqueValueInListForKey()).toEqual({});
        expect(sut.findUniqueValueInListForKey([])).toEqual({});
        expect(sut.findUniqueValueInListForKey([{ x: 'id' }], 'id')).toEqual({});
        expect(sut.findUniqueValueInListForKey(null, 'id', 'x')).toEqual({});
        expect(sut.findUniqueValueInListForKey(null, null, 'x')).toEqual({});
        expect(sut.findUniqueValueInListForKey(null, null, null)).toEqual({});
    });

});