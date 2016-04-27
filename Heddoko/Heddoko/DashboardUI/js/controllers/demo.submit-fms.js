/**
 * @file    submit-fms-demo.js
 * @brief   Temporary controller for submitting FMS tests.
 * @author  Francis Amankrah (frank@heddoko.com)
 * @date    October 2015
 * @notes   This controller should probably combined with FMSControler sometime
            in the future.
 */
angular.module('app.controllers')

.controller('SubmitFMSDemoController', ['$scope', '$routeParams', 'Rover', 'assetVersion',
    function($scope, $routeParams, Rover, assetVersion) {

        Rover.debug('SubmitFMSDemoController');

        $scope.params = $routeParams;

        $scope.assetVersion = assetVersion;
    }
]);
