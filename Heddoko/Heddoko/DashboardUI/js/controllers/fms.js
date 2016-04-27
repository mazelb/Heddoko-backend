/**
 * @file    fms.js
 * @brief   Controller for general functional movement screening.
 * @author  Francis Amankrah (frank@heddoko.com)
 * @date    October 2015
 */
angular.module('app.controllers')

.controller('FMSController', ['$scope', '$routeParams', 'Rover',
    function($scope, $routeParams, Rover) {

        Rover.debug('FMSController');

        $scope.params = $routeParams;

        $scope.assetVersion = '0.5.6';
    }
]);
