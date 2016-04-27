/**
 * Copyright Heddoko(TM) 2015, all rights reserved.
 *
 * @brief   Controller for movement screenings.
 * @author  Francis Amankrah (frank@heddoko.com)
 * @date    December 2015
 */
angular.module('app.controllers')

.controller('ScreeningController', ['$scope', '$routeParams', 'ScreeningService', 'Rover', 'Utilities',
    function($scope, $routeParams, ScreeningService, Rover, Utilities) {
        Utilities.info('ScreeningController');

        /**
         * Retrieves screening data.
         */
        $scope.fetchScreening = function() {
            Utilities.time('Retrieving Selected Screening Data');

            // Turn on "fetching selected screening" flag.
            Utilities.data.isFetchingSelectedScreening = true;

            ScreeningService.get($routeParams.screeningId, 'movements').then(
                function(response) {
                    Utilities.timeEnd('Retrieving Selected Screening Data');

                    // Store screening data.
                    $scope.screening = Utilities.data.selectedScreening = response.data;

                    // Retrieve screening profile.
                    $scope.profile = Utilities.getData('profile', response.data.profileId);

                    // Turn off flag.
                    Utilities.data.isFetchingSelectedScreening = false;
                },
                function(response) {
                    Utilities.timeEnd('Retrieving Selected Screening Data');

                    // Reset screening data and turn off flag.
                    $scope.resetScreening();

                    // Alert user of error.
                    Utilities.alert('Could not retrieve screening. Please try again later.');
                }
            );
        };

        /**
         * Resets screening data.
         */
        $scope.resetScreening = function() {
            $scope.screening = Utilities.data.selectedScreening = {id: 0};
            $scope.profile = null;
            Utilities.data.isFetchingSelectedScreening = false;
        };

        /**
         * Updates the "live" screening.
         *
         * @param int|object screening
         * @param bool browseTo
         */
        $scope.editScreening = function(screening) {

            Utilities.data.liveScreening = null;
            Utilities.store.liveScreeningId = Utilities.getId(screening);
            Rover.browseTo.path('/screenings/live');
        };

        /**
         * Retrieves screenings based on search parameters
         */
        $scope.fetchScreeningList = function() {
            Utilities.time('Retrieving Screenings');

            Utilities.data.isFetchingScreeningData = true;

            ScreeningService.search({
                query: '',
                profileId: null,
                offset: 0,
                limit: 20,
                orderBy: 'createdAt',
                orderDir: 'desc'
            }).then(
                function(response) {
                    Utilities.timeEnd('Retrieving Screenings');
                    Utilities.log('Received ' + response.data.results.length + ' results.');

                    // Reset screening list.
                    Utilities.resetDataNamespace('screening');
                    angular.forEach(response.data.results, function(screening) {
                        Utilities.setData('screening', screening.id, $scope.formatScreening(screening));
                    });

                    // Select latest screening by default.
                    if (Utilities.data.screeningId === 0 && response.data.results.length > 0) {
                        Utilities.data.screeningId = response.data.results[0].id;
                    }

                    Utilities.data.isFetchingScreeningData = false;
                },
                function(response) {
                    Utilities.timeEnd('Retrieving Screenings');
                    Utilities.alert('Could not retrieve screening data. Please try again later.');
                    Utilities.data.isFetchingScreeningData = false;
                }
            );
        };

        /**
         * Adds profile data to a screening.
         *
         * @param object screening
         */
        $scope.formatScreening = function(screening) {

            // Performance check.
            if (!screening || !Utilities.hasData('profile', screening.profileId)) {
                return screening;
            }

            screening.profile = {
                firstName: Utilities.getData('profile', screening.profileId).firstName || '',
                lastName: Utilities.getData('profile', screening.profileId).lastName || ''
            };

            return screening;
        };

        // Retrieve recent screenings.
        if (Utilities.getDataLength('screening') === 0) {
            Utilities.data.isFetchingScreeningData = true;
            Rover.waitForFlag('isFetchingProfiles', false, $scope, $scope.fetchScreeningList);
        }

        // Retrieve data for current screening.
        if ($routeParams.screeningId > 0)
        {
            // From the API, if it hasn't already been loaded.
            if (!Utilities.data.selectedScreening || Utilities.data.selectedScreening.id != $routeParams.screeningId) {
                Utilities.data.isFetchingSelectedScreening = true;
                Rover.waitForFlag('isFetchingProfiles', false, $scope, $scope.fetchScreening);
            }

            // Or from the ephemeral storage.
            else {
                $scope.screening = Utilities.data.selectedScreening;
                $scope.profile = Utilities.data.selectedScreening.id > 0 ?
                    Utilities.getData('profile', Utilities.data.selectedScreening.profileId) : null;
            }
        }
    }
]);
