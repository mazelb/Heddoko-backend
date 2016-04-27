/**
 * Copyright Heddoko(TM) 2015, all rights reserved.
 *
 * @brief   Controller for movement screenings.
 * @author  Francis Amankrah (frank@heddoko.com)
 * @date    December 2015
 */
angular.module('app.controllers')

.controller('ScreeningResultsController', ['$scope', '$routeParams', 'ScreeningService', 'Rover', 'Utilities',
    function($scope, $routeParams, ScreeningService, Rover, Utilities) {
        Utilities.debug('ScreeningResultsController');

        // Initial setup.
        $scope.global.state.screening = $scope.global.state.screening || {};
        $scope.global.state.screening.list = $scope.global.state.screening.list || {length: 0};
        $scope.global.state.screening.current = $scope.global.state.screening.current || {id: 0};
        $scope.global.data.isFetchingScreeningData = false;
        $scope.global.data.isPreparingNewScreening = false;

        // Shortcut to global objects.
        $scope.screening = $scope.global.state.screening.current;
        if ($scope.screening.id > 0 && $scope.screening.profileId) {
            $scope.screeningProfile =
                $scope.global.state.profile.list[$scope.screening.profileId] || {id: 0};
        } else {
            $scope.screeningProfile = {id: 0};
        }

        /**
         * Updates the selected screening.
         *
         * @param int|object screening
         */
        $scope.selectScreening = function(screening) {

            // Performance check.
            if ($scope.global.state.screening.list.length < 1) {
                return;
            }

            // Update selected screening.
            if (typeof screening == 'object') {
                $scope.global.state.screening.current = screening;
                Rover.browseTo.path('/screenings/current');
            }

            // Find the screening by ID.
            else
            {
                var test;
                for (test in $scope.global.state.screening.list)
                {
                    if (test.id == screening) {
                        $scope.global.state.screening.current = test;
                        Rover.browseTo.path('/screenings/current');
                        break;
                    }
                }
            }
        };

        /**
         * Shortcut to create a standard FMS.
         */
        $scope.createFunctionalMovementScreening = function() {
            Utilities.debug('Creating standard FMS...');

            $scope.global.data.isPreparingNewScreening = true;

            ScreeningService.create($scope.global.getSelectedProfile().id,
            {
                title: 'Functional Movement Screening',
                scoreMax: 3,
                movements: [
                    {title: 'Deep Squat'},
                    {title: 'Hurdle Step - Left'},
                    {title: 'Hurdle Step - Right'},
                    {title: 'Inline Lunge - Left'},
                    {title: 'Inline Lunge - Right'},
                    {title: 'Shoulder Mobility - Left'},
                    {title: 'Shoulder Mobility - Right'},
                    {title: 'Impingment Test - Left', meta: {
                        params: {
                            isClearanceTest: true
                        }
                    }},
                    {title: 'Impingment Test - Right', meta: {
                        params: {
                            isClearanceTest: true
                        }
                    }},
                    {title: 'Active Straight-Leg Raise - Left'},
                    {title: 'Active Straight-Leg Raise - Right'},
                    {title: 'Trunk Stability Push-Up'},
                    {title: 'Spinal Extension', meta: {
                        params: {
                            isClearanceTest: true
                        }
                    }},
                    {title: 'Rotary Stability - Left'},
                    {title: 'Rotary Stability - Right'},
                    {title: 'Posterior Rocking', meta: {
                        params: {
                            isClearanceTest: true
                        }
                    }},
                ]
            }).then(
                function(response) {
                    Utilities.debug('Screening created.');

                    // Save screening data.
                    $scope.global.state.screening.current = $scope.formatScreening(response.data);
                    $scope.global.state.screening.list.unshift($scope.global.state.screening.current);
                    $scope.global.data.isPreparingNewScreening = false;
                },
                function(response) {
                    $scope.global.data.isPreparingNewScreening = false;
                    Utilities.alert('Could not create screening. Please try again later.');
                }
            );
        };

        /**
         * Retrieves a screening by ID.
         */
        $scope.fetchScreening = function(id) {
            Utilities.debug('Retrieving screening #' + id + '...');

            $scope.global.data.isFetchingScreeningData = true;

            ScreeningService.get(id).then(
                function(response) {
                    Utilities.debug('Received ' + response.data.results.length + ' results.');

                    // Save screening data.
                    $scope.global.state.screening.current = response.data;
                    $scope.global.data.isFetchingScreeningData = false;
                },
                function(response) {
                    Utilities.debug('Could not retrieve screening data. Please try again later.');
                    $scope.global.data.isFetchingScreeningData = false;
                }
            );
        };

        /**
         * Retrieves screenings based on search parameters
         */
        $scope.fetchScreenings = function() {
            Utilities.debug('Retrieving screenings...');

            $scope.global.data.isFetchingScreeningData = true;

            ScreeningService.search({
                query: '',
                profileId: null,
                offset: 0,
                limit: 20,
                orderBy: 'createdAt',
                orderDir: 'desc'
            }).then(
                function(response) {
                    Utilities.debug('Received ' + response.data.results.length + ' results.');

                    // Save screening data.
                    $scope.global.state.screening.list =
                        response.data.results.map($scope.formatScreening);
                    $scope.global.data.isFetchingScreeningData = false;
                },
                function(response) {
                    Utilities.debug('Could not retrieve screening data. Please try again later.');
                    $scope.global.data.isFetchingScreeningData = false;
                }
            );
        };

        /**
         * Adds profile data to a screening.
         *
         * @param object screening
         */
        $scope.formatScreening = function(screening) {

            // Defaults.
            screening.profile = {
                firstName: '',
                lastName: ''
            };

            // Performance check.
            if (!screening || !Rover.state.profile.list[screening.profileId]) {
                return screening;
            }

            screening.profile.firstName =
                $scope.global.state.profile.list[screening.profileId].firstName;
            screening.profile.lastName =
                $scope.global.state.profile.list[screening.profileId].lastName;

            return screening;
        };

        // If a screening ID was provided, try to fetch its contents.
        if ($routeParams.screeningId) {
        }

        // Retrieve recent screenings.
        if ($scope.global.state.screening.list.length === 0) {
            $scope.fetchScreenings();
        }

        // Watch selected screening.
        $scope.$watch('global.store.screeningId', function(id) {

        });

        // Watch current screening object.
        $scope.$watch('global.state.screening.current', function(newScreening) {

            // Update current screening.
            $scope.screening = newScreening.id > 0 ? newScreening : {id: 0};

            // Update screening profile.
            if (newScreening.id > 0 && newScreening.profileId) {
                $scope.screeningProfile =
                    $scope.global.state.profile.list[newScreening.profileId] || {id: 0};
            }
        });
    }
]);
