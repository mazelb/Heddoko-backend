/**
 * @brief   Filter to convert MySQL date formats to any other format.
 * @author  Francis Amankrah (frank@heddoko.com)
 * @date    December 2015
 * @note    Use as: {{ date_expression | mysqlDate : format : timezone }}
 */
angular.module('app.filters')

.filter('mysqlDate', ['$filter', 'Utilities',
    function($filter, Utilities) {
        return function(input, format, timezone) {

            // Set a default value for the input.
            input = input || Date.now();

            // Update input string to a format supported by Angular's date filter.
            if (typeof input.match == 'function' &&
                input.match(/([0-9]{4}-[01][0-9]-[0-3][0-9] [0-2][0-9]:[0-6][0-9]:[0-6][0-9])/)) {

                // Update input with timezone.
                input = input.replace(' ', 'T') + 'Z';

                // Default format.
                format = format || 'MMMM d, yyyy (h:mm a)';
            }

            return $filter('date')(input, format, timezone);
        };
    }
]);
