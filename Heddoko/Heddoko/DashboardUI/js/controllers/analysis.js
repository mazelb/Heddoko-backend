/**
 * Copyright Heddoko(TM) 2016, all rights reserved.
 *
 * @brief   Controller for analysis.
 * @author  Francis Amankrah (frank@heddoko.com)
 * @date    January 2016
 */
angular.module('app.controllers')

.controller('AnalysisController', ['$scope', '$routeParams', 'MovementService', 'Rover', 'Utilities',
    function($scope, $routeParams, MovementService, Rover, Utilities) {
        Utilities.info('AnalysisController');

        // Setup controller.
        $scope.movement = null;
        $scope.profile = null;
        $scope.layout = 'emphasis';
        Utilities.data.isFetchingAnalysisMovement = Utilities.data.isFetchingAnalysisMovement || true;

        /**
         * Retrieves movement data.
         */
        $scope.fetchMovement = function() {
            Utilities.time('Retrieving Movement Data.');

            // Turn on "fetching movement data" flag.
            Utilities.data.isFetchingAnalysisMovement = true;

            MovementService.get($routeParams.movementId, ['frames', 'meta', 'markers', 'events', 'tags']).then(
                function(response) {
                    Utilities.timeEnd('Retrieving Movement Data.');

                    // Save movement data.
                    $scope.movement = response.data;

                    // Retrieve associated profile.
                    $scope.profile = Utilities.getData('profile', response.data.profileId);

                    Utilities.data.isFetchingAnalysisMovement = false;
                },

                function(response) {
                    Utilities.timeEnd('Retrieving Movement Data.');

                    // Notify user of error.
                    Utilities.error('Could not retrieve movement data: ' + response.statusText);
                    Utilities.alert('Could not retrieve movement data. Please try again later.');
                    Utilities.data.isFetchingAnalysisMovement = false;
                }
            );
        };

        /**
         * Saves movement details through the uiEditableListContainer directive.
         */
        $scope.saveMovementDetails = function() {
            return MovementService.update($scope.movement.id, $scope.movement, ['frames', 'meta', 'markers', 'events', 'tags']);
        };

        /**
         * Callback for uiEditableListContainer directive.
         *
         * @param bool saved
         */
        $scope.saveMovementDetailsCallback = function(saved) {

            // Update movement data.
            if (saved) {
                $scope.movement = this;
            }

            //
            else {
                Utilities.alert('Could not save movement details. Please try again later.');
            }
        };

        Rover.waitForFlag('isFetchingProfiles', false, $scope, $scope.fetchMovement);
    }
]);
