/**
 * Copyright Heddoko(TM) 2015, all rights reserved.
 *
 * @brief   Controller for the main search input.
 * @author  Francis Amankrah (frank@heddoko.com)
 * @date    November 2015
 */
angular.module('app.controllers')

.controller('SearchController', ['$scope', '$timeout', 'Rover', 'Utilities',
    function($scope, $timeout, Rover, Utilities) {
        Utilities.info('SearchController');

        // Available search filters.
        $scope.filters = [
            {
                name: 'profile',
                label: 'Find an athlete',
                placeholder: 'search for athletes...',
                icon: 'user'
            },
            {
                name: 'group',
                label: 'Find a team',
                placeholder: 'search for teams...',
                icon: 'users'
            },
            {
                name: 'movement',
                label: 'Find a movement',
                placeholder: 'search for movements...',
                icon: 'heartbeat'
            },
            {
                name: 'screening',
                label: 'Find a screening',
                placeholder: 'search for screenings...',
                icon: 'list-alt'
            },
        ];

        // Selectize configuration.
        $scope.config = {
            create: false,
            valueField: 'id',
            maxItems: 1,
            searchField: ['firstName', 'lastName', 'name', 'title'],
            render: {
                /**
                 * Called anytime a search option is being rendered.
                 *
                 * @param object item
                 * @param function escape
                 */
                option: function(item, escape) {

                    // Generate option label based on search type.
                    var data = '';
                    switch ($scope.selectedFilter.name)
                    {
                        case 'profile':
                            data = escape(item.firstName) + ' ' + escape(item.lastName);
                            break;

                        case 'group':
                            data = escape(item.name);
                            break;

                        case 'movement':
                            data = '';
                            break;

                        case 'screening':
                            data = '';
                            break;
                    }

                    return '<div>' + data + '</div>';
                },

                /**
                 * Called anytime an item is inserted into the input box.
                 *
                 * @param object item
                 * @param function escape
                 */
                item: function(item, escape) {

                    // Generate label depending on search type.
                    var label = '';
                    switch ($scope.selectedFilter.name)
                    {
                        case 'profile':
                            label = escape(item.firstName);
                            break;

                        case 'group':
                            label = escape(item.name);
                            break;

                        case 'movement':
                            label = '';
                            break;

                        case 'screening':
                            label = '';
                            break;
                    }

                    return '<div>' + label + '</div>';
                }
            },

            /**
             * Called once the element is initialized.
             *
             * @param object selectize
             */
            onInitialize: function(selectize) {
                $scope.selectize = selectize;
            },

            /**
             * Called anytime the user types into the input box.
             *
             * @param string query
             * @param function callback
             */
            load: function(query, callback) {

                // Performance check.
                if (!query || !query.length) {
                    return callback();
                }

                // Set search options.
                var apiEndpoint = null;
                switch ($scope.selectedFilter.name)
                {
                    case 'profile':
                        Utilities.debug('Looking up profiles...');
                        callback($scope.options);
                        break;

                    case 'group':
                        Utilities.debug('Looking up groups...');
                        callback($scope.options);
                        break;

                    case 'movement':
                        Utilities.debug('Looking up movements...');

                        Utilities.alert('In Development.');
                        callback();
                        break;

                    case 'screening':
                        Utilities.debug('Looking up screenings...');

                        Utilities.alert('In Development.');
                        callback();
                        break;
                }

                // Queries the API.
                if (apiEndpoint)
                {
                    $http.get(apiEndpoint, {
                        params: {
                            query: query,
                            limit: 50
                        }
                    }).then(
                        function(response) {
                            callback(response.data);
                        },
                        function(response) {
                            callback();
                        }
                    );
                }
            },

            /**
             * Called whenver the input is focused.
             *
             */
            onFocus: function() {
                Utilities.debug('onFocus');
                $scope.selectize.clear();
            },

            /**
             * Called whenever an option is selected.
             *
             * @param mixed item
             */
            onChange: function(item) {

                switch ($scope.selectedFilter.name)
                {
                    // Browse to selected profile.
                    case 'profile':
                        $timeout(function() {
                            Rover.browseTo.path('/profiles/' + item);
                        });
                        break;

                    // Browse to selected group.
                    case 'group':
                        $timeout(function() {
                            Rover.browseTo.group('/groups/' + item);
                        });
                        break;
                }
            }
        };

        // Selectize options.
        $scope.options = [];

        // Selects a search filter.
        $scope.filterBy = function(filter) {

            // Reset the selectize model.
            $scope.model = null;

            // Update available options.
            switch (filter.name)
            {
                case 'profile':
                    $scope.options = [];
                    // angular.forEach($scope.global.state.profile.list, function(profile) {
                    angular.forEach(Utilities.getDataList('profile', []), function(profile) {
                        if (profile && profile.id) {
                            $scope.options.push(profile);
                        }
                    });
                    break;

                case 'group':
                    $scope.options = [];
                    // angular.forEach($scope.global.state.group.list, function(group) {
                    angular.forEach(Utilities.getDataList('group', []), function(group) {
                        if (group && group.id) {
                            $scope.options.push(group);
                        }
                    });
                    break;

                default:
                    $scope.options = [];
            }

            // Update input placeholder
            $('.top-nav .selectize-input input').attr('placeholder', filter.placeholder);
            $('.top-nav .selectize-input input').innerWidth(340);

            // Update selected filter.
            $scope.selectedFilter = Utilities.store.searchFilter = filter;
        };

        $timeout(function() {
            $scope.filterBy(Utilities.store.searchFilter || $scope.filters[0]);
        });
    }
]);
