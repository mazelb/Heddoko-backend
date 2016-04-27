/**
 *
 * Copyright Heddoko(TM) 2015, all rights reserved.
 *
 *
 * @brief   Angular directive for page titles.
 * @author  Francis Amankrah (frank@heddoko.com)
 * @date    November 2015
 */
angular.module('app.directives')

.directive('uiPageTitle', function() {
    return {
        restrict: 'E',
        templateUrl: 'partials/directives/ui-page-title.html',
        scope: {
            title: '=',
            icon: '=?'
        },
        controller: ['$scope', '$timeout', 'Upload', 'Rover',
            function($scope, $timeout, Upload, Rover) {

                // ...
            }
        ]
    };
});
