/**
 * Copyright Heddoko(TM) 2015, all rights reserved.
 *
 * @brief   Controller for movement screenings.
 * @author  Francis Amankrah (frank@heddoko.com)
 * @date    December 2015
 */
angular.module('app.controllers')

.controller('LiveScreeningController', ['$scope', 'ScreeningService', 'MovementService', 'Rover', 'Utilities',
    function($scope, ScreeningService, MovementService, Rover, Utilities) {
        Utilities.info('LiveScreeningController');

        // Setup controller.
        Utilities.data.isFetchingLiveScreening = true;
        Utilities.data.isRecordingLiveScreening = false;
        Utilities.data.isSavingLiveScreeningScore = false;      // demo

        /**
         * Retrieves screening data.
         */
        $scope.fetchScreening = function() {
            Utilities.time('Retrieving Live Screening Data');

            // Turn on "fetching live screening" flag.
            Utilities.data.isFetchingLiveScreening = true;

            ScreeningService.get(Utilities.store.liveScreeningId, ['movements.meta']).then(
                function(response) {
                    Utilities.timeEnd('Retrieving Live Screening Data');

                    // Setup screening.
                    $scope.setupScreening(response.data);

                    // Turn off flag.
                    Utilities.data.isFetchingLiveScreening = false;
                },
                function(response) {
                    Utilities.timeEnd('Retrieving Live Screening Data');

                    // Reset screening data and turn off flag.
                    $scope.resetScreening();

                    // Alert user of error.
                    Utilities.alert('Could not retrieve screening. Please try again later.');
                }
            );
        };

        /**
         * Sets up the screening to be started or continued.
         *
         * @param object screening
         */
        $scope.setupScreening = function(screening) {

            screening = screening || Utilities.data.liveScreening;
            if (!screening) {
                return;
            }

            // Create local reference to screening.
            $scope.screening = Utilities.data.liveScreening = screening;

            // Retrieve screening profile.
            $scope.profile = Utilities.getData('profile', screening.profileId);

            // Set current movement in screening.
            for (var i = 0; i < screening.movements.length; i++)
            {
                if (screening.movements[i].meta.score !== null) {
                    continue;
                }

                else {
                    $scope.screeningMovement = screening.movements[i];
                    break;
                }
            }
            $scope.screeningMovement = $scope.screeningMovement ?
                $scope.screeningMovement : screening.movements[0];

            // Turn off flag.
            Utilities.data.isFetchingLiveScreening = false;
        };

        /**
         *
         */
        $scope.setScreeningMovement = function(movement) {
            $scope.screeningMovement = movement;
        };

        /**
         * Resets screening data.
         */
        $scope.resetScreening = function() {
            Utilities.store.liveScreeningId = 0;
            $scope.screening = Utilities.data.liveScreening = {id: 0};
            $scope.profile = null;
            Utilities.data.isFetchingLiveScreening = false;
        };

        /**
         * Creates a standard FMS.
         */
        $scope.createFunctionalMovementScreening = function() {
            Utilities.time('Creating Standard FMS');

            Utilities.data.isPreparingNewScreening = true;

            ScreeningService.create($scope.global.getSelectedProfile().id,
            {
                title: '',
                scoreMin: 0,
                scoreMax: 3,
                movements: [
                    {title: 'Deep Squat'},
                    {title: 'Hurdle Step - L'},
                    {title: 'Hurdle Step - R'},
                    {title: 'Inline Lunge - L'},
                    {title: 'Inline Lunge - R'},
                    {title: 'Shoulder Mobility - L'},
                    {title: 'Shoulder Mobility - R'},
                    {title: 'Impingment Test - L', meta: {
                        data: {
                            isClearanceTest: true
                        }
                    }},
                    {title: 'Impingment Test - R', meta: {
                        data: {
                            isClearanceTest: true
                        }
                    }},
                    {title: 'Active Straight-Leg Raise - L'},
                    {title: 'Active Straight-Leg Raise - R'},
                    {title: 'Trunk Stability Push-Up'},
                    {title: 'Spinal Extension', meta: {
                        data: {
                            isClearanceTest: true
                        }
                    }},
                    {title: 'Rotary Stability - L'},
                    {title: 'Rotary Stability - R'},
                    {title: 'Posterior Rocking', meta: {
                        data: {
                            isClearanceTest: true
                        }
                    }},
                ]
            }, ['movements']).then(
                function(response) {
                    Utilities.timeEnd('Creating Standard FMS');

                    // Save screening data.
                    Utilities.store.liveScreeningId = response.data.id;
                    $scope.screening = Utilities.data.liveScreening = response.data;
                    $scope.profile = Utilities.getData('profile', response.data.profileId);

                    // Update list of screenings.
                    if (Utilities.hasDataNamespace('screening'))
                    {
                        response.data.profile = {
                            firstName: Utilities.getData('profile',  response.data.profileId).firstName || '',
                            lastName: Utilities.getData('profile',  response.data.profileId).lastName || ''
                        };
                        Utilities.setData('screening', response.data.id, response.data);
                    }

                    // Turn off flag.
                    Utilities.data.isPreparingNewScreening = false;
                },
                function(response) {
                    Utilities.timeEnd('Creating Standard FMS');
                    Utilities.data.isPreparingNewScreening = false;
                    Utilities.alert('Could not create screening. Please try again later.');
                }
            );
        };

        /**
         * Saves the details of the screening through the uiEditableStandaloneField directive.
         *
         * @return $http
         */
        $scope.saveScreening = function() {
            return ScreeningService.update(this.id, this, ['movements']);
        };

        /**
         * Callback uiEditableStandaloneField directive.
         *
         * @param bool screeningSaved
         */
        $scope.saveScreeningCallback = function(screeningSaved) {
            if (screeningSaved)
            {
                // Add profile data to screening.
                if (Utilities.hasData('profile', this.profileId))
                {
                    this.profile = {
                        firstName: Utilities.getData('profile', this.profileId).firstName || '',
                        lastName: Utilities.getData('profile', this.profileId).lastName || ''
                    };
                }

                // Update screening in storage.
                Utilities.setData('screening', this.id, this);
            }
        };

        /**
         * Saves the details of the screening movement through the
         * uiEditableStandaloneField directive.
         *
         * @return $http
         */
        $scope.saveScreeningMovement = function() {
            return MovementService.update($scope.screeningMovement.id, $scope.screeningMovement);
        };

        /**
         * Callback uiEditableStandaloneField directive.
         *
         * @param bool screeningSaved
         */
        $scope.saveScreeningMovementCallback = function(movementSaved) {
            // ...
        };

        /**
         * Saves the score for the screening movement.
         * TODO: this can be combined with $scope.saveScreeningMovement
         */
        $scope.saveScreeningScore = function() {
            Utilities.time('Saving Screening Score');

            Utilities.data.isSavingLiveScreeningScore = true;

            MovementService.update($scope.screeningMovement.id, $scope.screeningMovement).then(
                function(response) {
                    Utilities.timeEnd('Saving Screening Score');

                    // Turn off flag.
                    Utilities.data.isSavingLiveScreeningScore = false;
                },

                function(response) {
                    Utilities.timeEnd('Saving Screening Score');

                    // Turn off flag.
                    Utilities.data.isSavingLiveScreeningScore = false;

                }
            );
        };

        /**
         * Starts a live recording
         */
        $scope.startLiveRecording = function() {

            Utilities.info('Live recording is not yet supported.');

            Utilities.data.isRecordingLiveScreening = true;
        };

        /**
         * Stops a live recording
         */
        $scope.pauseLiveRecording = function() {

            Utilities.data.isRecordingLiveScreening = false;
        };

        /**
         * Cycles to the previous screening movement.
         */
        $scope.previousMovement = function() {
            // ...
        };

        /**
         * Cycles to the next screening movement.
         */
        $scope.nextMovement = function() {
            // ...
        };

        /**
         * Sets the score of a screening movement.
         *
         * @param object movement
         * @param int score
         */
        $scope.setScore = function(movement, score) {

            // Save the score in the movement meta.
            movement.meta = movement.meta || {};
            movement.meta.score = score;

            // Update the screening score.
            $scope.screening.score = 0;
            angular.forEach($scope.screening.movements, function(test) {
                $scope.screening.score += parseInt(test.meta.score);
            });
        };

        // Retrieve current screening.
        if (Utilities.store.liveScreeningId > 0)
        {
            // From the API, if it hasn't already been loaded.
            if (!Utilities.data.liveScreening) {
                Rover.waitForFlag('isFetchingProfiles', false, $scope, $scope.fetchScreening);
            }

            // Or from the ephemeral storage.
            else {
                $scope.setupScreening();
            }
        }

        else {
            $scope.resetScreening();
        }
    }
]);
