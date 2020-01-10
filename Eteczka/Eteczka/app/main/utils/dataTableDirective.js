'use strict';

angular.module('et.directives').directive('etDataTable', ['$timeout', function ($timeout) {
	function getOrSetDefault(value, defaultValue) {
		var result = defaultValue;
		if (typeof value !== 'undefined') {
			result = value;
		}

		return result;
	}

	function resize(parentContainer, headerHeight) {
		var initParentSize = parentContainer.height();

		var initTbodyHeight = initParentSize - headerHeight - 15;
		_.debounce(function () {
			$('div.dataTables_scrollBody').height(initTbodyHeight);
		}, 100)();
	}

	return {
		restrict: 'A',
		scope: {
			haskeys: '@',
			scrolly: '@',
			scrollx: '@',
			reload: '=',
			selectedrow: '=',
			colDefs: '='
		},
		link: function (scope, element) {
			//{keys:true, fixedHeader:true, paging: false, scrollY:'50vh', scrollCollapse: true, scrollX:false, info: false}

			$timeout(function () {
				var parentContainer = $(element).parent();
				var initParentSize = parentContainer.height();
				var headerHeight = $('thead', element).height();

				var initTbodyHeight = initParentSize - headerHeight - 17;

				scope.table = $(element).DataTable({
					keys: getOrSetDefault(scope.haskeys, false),
					fixedHeader: true,
					paging: false,
					scrollY: getOrSetDefault(scope.scrolly, initTbodyHeight),
					info: false,
					scrollX: getOrSetDefault(scope.scrollx, true),
					searching: false,
					language: {
						emptyTable: 'Brak danych do wyświetlenia w tabeli'
					},
					columnDefs: scope.colDefs ? scope.colDefs : [
						{
							orderable: false,
							targets: -1
						}
					]
				});

				scope.$watch('reload', function () {
					resize(parentContainer, headerHeight);
					$timeout(function () {
						scope.table.columns.adjust().draw();
					});
				});

				scope.table
					.on('key-focus', function (e, datatable, cell) {
						scope.selectedrow = cell.index().row;
					});

				$(window).resize(function () {
					resize(parentContainer, headerHeight);
				});

				resize(parentContainer, headerHeight);
			});

			scope.$on('$destroy', function () {
				if (scope.table) {
					scope.table.destroy();
				}
			});

			jQuery.fn.dataTableExt.oSort['numstring-asc'] = function (x, y) {
				//sorting without first letter
				return sortNumString(x, y);
			};

			jQuery.fn.dataTableExt.oSort['numstring-desc'] = function (x, y) {
				//sorting without first letter
				return sortNumString(y, x);
			};

			function sortNumString(x, y) {
				const endsWithLetter = /[A-Za-z]$/g;
				const isAnyLetter = /[A-Za-z]/g;

				const xNumberValue = createNumber(x);
				const yNumberValue = createNumber(y);

				const xLetter = x[0].toLowerCase();
				const yLetter = y[0].toLowerCase();

				if (xLetter === yLetter) {
					return xNumberValue - yNumberValue;
				} else {
					return compareAlphabetically(xLetter, yLetter);
				}

				function createNumber(stringValue) {
					const specialCharactersToEscape = ['-', '_', ',', '*', '.', '\\', '/', ':'];
					let valueToMap = stringValue;
					specialCharactersToEscape.forEach(character => {
						const splittedStringByCharacter = stringValue.split(character);

						if (splittedStringByCharacter.length > 1) {

							valueToMap = splittedStringByCharacter[0];

							if (stringValue.match(endsWithLetter)) {
								valueToMap += '.' + stringValue.charAt(stringValue.length - 1);
							}
						}
					});

					let mappedValue = null;

					if (stringValue.match(endsWithLetter)) {
						mappedValue = Number(valueToMap.replace(isAnyLetter, ''));
						mappedValue += '.' + stringValue.charCodeAt(stringValue.length - 1);
					} else {
						mappedValue = Number(valueToMap.replace(isAnyLetter, ''));
					}

					return mappedValue;
				}

				function compareAlphabetically(a, b) {
					if (a > b) {
						return 1;
					}
					if (b > a) {
						return -1;
					}

					return 0;
				}
			}


		}
	};
}]);
