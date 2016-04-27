/**
 * Copyright Heddoko(TM) 2015, all rights reserved.
 *
 * @brief   This is the central controller which runs whenever the dashboard is loaded. It keeps an
 *          eye on local scope variables keeps them in sync with the local storage. It also fetches
 *          the group list when the page is loaded, and loads a group's profiles when the selected
 *          group changes.
 * @author  Maxwell Mowbray (max@heddoko.com), Francis Amankrah (frank@heddoko.com)
 */
angular.module('app.controllers')

.controller('MainController',
    ['$scope', '$timeout', 'ProfileService', 'GroupService', 'UserService', 'OnboardingService',
    'Rover', 'Utilities',
    function(
        $scope, $timeout, ProfileService, GroupService, UserService, OnboardingService,
        Rover, Utilities, isLocalEnvironment) {
        Utilities.info('MainController');

        // Setup a "global" namespace to store variables that should be inherited in child scopes.
        var role = parseInt($('meta[name="user-role"]').attr('content'));
        $scope.global =
        {
            // Information about the application.
            appVersion: $('meta[name="version"]').attr('content'),
            isLocal: Utilities.isLocal,

            // The localStorage persists across browser sessions.
            // TODO: deprecate this to save resources.
            store: Utilities.store,

            // The sessionStorage persists throughout a single browser session.
            // TODO: deprecate this to save resources.
            state: Utilities.state,

            isAdmin: role === 0,

            // The data object is ephemeral, and will reset at the end of the user session.
            data: Utilities.data,

            // Helper methods.
            getDataLength: Utilities.getDataLength,
            endSession: Rover.endSession,
            browseTo: Rover.browseTo,
            alert: Utilities.alert,

            // Onboarding messages.
            onboarding:
            {
                general: OnboardingService.general
            }
        };

        // Setup user data
        $scope.global.state.user = $scope.global.state.user || {id: 0};

        // Set some defaults.
        Utilities.store.groupId = Utilities.store.groupId || 0;
        Utilities.store.profileId = Utilities.store.profileId || 0;

        // Setup screening data.
        $scope.global.state.screening = $scope.global.state.screening || {};
        $scope.global.state.screening.list = $scope.global.state.screening.list || {length: 0};
        $scope.global.state.screening.current = $scope.global.state.screening.current || {id: 0};
        Utilities.data.isFetchingScreeningData = false;
        Utilities.data.isFetchingScreeningList = false;
        Utilities.data.isPreparingNewScreening = false;

        /**
         * Retrieves a group by ID.
         *
         * @param int
         * @return object|null
         */
        $scope.global.getGroup = function(id) {
            Utilities.info('$scope.global.getGroup is deprecated...');
            return Utilities.getData('group', id, null);
        };

        /**
         * Retrieves the currently selected group.
         *
         * @return object
         */
        $scope.global.getSelectedGroup = function() {
            return Utilities.getData('group', Utilities.store.groupId, {id: 0});
        };

        /**
         * Updates the selected group.
         *
         * @param mixed
         */
        $scope.global.selectGroup = function(group) {
            $timeout(function() {
                Utilities.store.groupId = Utilities.getId(group);
            });
        };

        /**
         * Retrieves selected profile.
         *
         * @return object
         */
        $scope.global.getSelectedProfile = function() {
            return Utilities.getData('profile', Utilities.store.profileId, {id: 0});
        };

        /**
         * Updates the selected profile.
         *
         * @param object|int profile
         * @param bool browseTo
         */
        $scope.global.selectProfile = function(profile, browseTo) {
            $timeout(function() {
                Utilities.store.profileId = Utilities.getId(profile);

                // Browse to profile page.
                if (browseTo) {
                    Rover.browseTo.profile(profile);
                }
            });
        };

        /**
         * Fetches all groups available to currently authenticated user.
         */
        $scope.fetchGroups = function() {
            Utilities.time('Fetching Groups');

            // Turn on fetching flag
            Utilities.data.isFetchingGroups = true;

            // Retrieve available groups.
    		GroupService.list(['avatarSrc', 'tags']).then(
                function(response) {
                    Utilities.timeEnd('Fetching Groups');

                    // Reset group list.
                    Utilities.resetDataNamespace('group');
                    angular.forEach(response.data, function(group) {
                        Utilities.setData('group', group.id, group);
                    });

                    if (!Utilities.data.isFetchingProfiles && response.data.length > 0)
                    {
                        // Select a default group.
                        if (Utilities.store.groupId === 0) {
                            // Utilities.store.groupId = response.data[0].id;
                            $scope.global.selectGroup(response.data[0].id);
                        }

                        else {
                            $scope.global.updateFilteredProfiles();
                        }
                    }


                    Utilities.data.isFetchingGroups = false;
        		},
                function(response) {
                    Utilities.timeEnd('Fetching Groups');
                    Utilities.error('Could not retrieve group list: ' + response.statusText);
                    Utilities.data.isFetchingGroups = false;
                }
            );
        };

        /**
         * Updates the filtered profile list.
         *
         * @param int newGroup
         * @param int oldGroup
         */
        $scope.global.updateFilteredProfiles = function(newGroup, oldGroup) {

            // Performance check.
            if (!Utilities.hasDataNamespace('profile')) {
                return;
            }

            Utilities.time('Updating Filtered Profiles');
            var isCurrentProfileIncluded = false;
            // $scope.global.state.profile.filtered = [];
            Utilities.data.filteredProfiles = [];

            // angular.forEach($scope.global.state.profile.list, function(profile) {
            angular.forEach(Utilities.getDataList('profile'), function(profile) {

                // Make sure we have a profile object.
                if (!profile || !profile.id) {
                    return;
                }

                // If no group was selected, include all profiles.
                if (newGroup === 0)
                {
                    // $scope.global.state.profile.filtered.push(profile);
                    Utilities.data.filteredProfiles.push(profile);

                    // Check if selected profile is part of newly filtered list.
                    if (profile.id == Utilities.store.profileId) {
                        isCurrentProfileIncluded = true;
                    }
                }

                // Loop through profile groups and check if any of them correspond
                // to the selected group.
                else
                {
                    newGroup = newGroup || Utilities.store.groupId;

                    if (profile.groups && profile.groups.length)
                    {
                        angular.forEach(profile.groups, function(group) {
                            if (group.id == newGroup) {
                                Utilities.data.filteredProfiles.push(profile);

                                // Check if selected profile is part of newly filtered list.
                                if (profile.id == Utilities.store.profileId) {
                                    isCurrentProfileIncluded = true;
                                }
                            }
                        });
                    }
                }
            });

            // If the selected profile is not part of the filtered list, select a new one.
            if (!isCurrentProfileIncluded)
            {
                Utilities.store.profileId = Utilities.data.filteredProfiles.length > 0 ?
                    Utilities.data.filteredProfiles[0].id : 0;
            }

            // Let app know that we've updated the filtered profiles list.
            Rover.fireEvent('updatedFilteredProfiles');
        };

        /**
         * Fetches profiles available to authenticated user.
         */
        $scope.fetchProfiles = function() {
            Utilities.time('Fetching Profiles');

            // Turn on "fetching profiles" flag.
            Utilities.data.isFetchingProfiles = true;

            // Retrieve profiles.
    		ProfileService.list(null, ['avatarSrc', 'groups', 'meta']).then(
                function(response) {
                    Utilities.timeEnd('Fetching Profiles');

                    // Reset profile list.
                    Utilities.resetDataNamespace('profile');
                    angular.forEach(response.data, function(profile) {
                        Utilities.setData('profile', profile.id, profile);
                    });

                    // Select a default profile.
                    if (Utilities.store.profileId === 0 && response.data.length > 0) {
                        $scope.global.selectProfile(response.data[0].id);
                    }

                    // Update filtered profiles list.
                    $scope.global.updateFilteredProfiles();

                    Utilities.data.isFetchingProfiles = false;
    		    },
                function(response) {
                    Utilities.timeEnd('Fetching Profiles');
                    Utilities.error('Could not retrieve profile list: ' + response.statusText);
                    Utilities.data.isFetchingProfiles = false;
                }
            );
        };

        // Fetch user details.
        if (Utilities.state.user.id === 0)
        {
            Utilities.time('Retrieving User Details');
            Utilities.data.isFetchingUser = true;

            UserService.get(Utilities.userHash).then(

                // Update user data.
                function(response) {
                    Utilities.timeEnd('Retrieving User Details');
                    Utilities.state.user = response.data;
                    Utilities.data.isFetchingUser = false;
                },
                function(response) {
                    Utilities.timeEnd('Retrieving User Details');
                    Utilities.state.user = {id: 0};
                    Utilities.error('Could not retrieve user details: ' + response.statusText);
                    Utilities.data.isFetchingUser = false;
                }
            );
        }

        // Fetch groups and profiles. We'll set a timeout for these requests, so that we don't
        // exceed the maximum # simultaneous requests on the server.
    	if (Utilities.getDataLength('profile') === 0) {
            Utilities.data.isFetchingProfiles = true;
    		$timeout($scope.fetchProfiles, 1000);
    	}

    	if (Utilities.getDataLength('group') === 0) {
            Utilities.data.isFetchingGroups = true;
    		$timeout($scope.fetchGroups, 2000);
    	}

        // Watches the selected group.
        $scope.$watch('global.store.groupId', $scope.global.updateFilteredProfiles);

        // Watches the selected profile.
        $scope.$watch('global.store.profileId', function(id, oldId) {
            Utilities.info('Selected profile: ' + id);

            // Performance check.
            if (id === 0) {
                return;
            }

            // Update the selected group.
            var profile = Utilities.getData('profile', id);
            if (profile && profile.groups && profile.groups.length &&
                profile.groups[0].id != Utilities.store.groupId) {

                // Utilities.store.groupId = profile.groups[0].id;
                $scope.global.selectGroup(profile.groups[0].id);
            }
        });
    }
]);
