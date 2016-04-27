/**
 * Copyright Heddoko(TM) 2015, all rights reserved.
 *
 * @brief   Angular directive for tags.
 * @author  Francis Amankrah (frank@heddoko.com)
 * @date    November 2015
 */
angular.module('app.directives')

/**
 * Tag lookup input.
 */
.directive('uiTaggableInput', function() {
    return {
        restrict: 'E',
        transclude: true,
        templateUrl: 'partials/directives/ui-taggable-input.html',
        scope: {
            model: '=',
            key: '@',
            maxTags: '@'
        },
        controller: ['$scope', '$http', 'Utilities',
            function($scope, $http, Utilities) {

                // Default scope variables.
                $scope.data = [];
                $scope.options = [];
                $scope.maxTags = $scope.maxTags || 10;

                // Updates the model whenever necessary.
                $scope.updateData = function() {

                    // Array of tags.
                    if (angular.isArray($scope.model[$scope.key]))
                    {
                        // Setup data model, available options and displayed value.
                        angular.forEach($scope.model[$scope.key], function(tag) {

                            // Push ID to data model.
                            $scope.data.push(tag.id);

                            // Add tag to options list.
                            $scope.options.push({
                                id: tag.id,
                                title: tag.title
                            });
                        });
                    }

                    // Single tag
                    else if (angular.isObject($scope.model[$scope.key]))
                    {
                        if ($scope.model[$scope.key].id)
                        {
                            $scope.data = $scope.model[scope.key].id;
                            $scope.options = [{
                                id: $scope.model[$scope.key].id,
                                title: $scope.model[$scope.key].title
                            }];
                        }
                    }

                    // Default value.
                    else {
                        $scope.model[$scope.key] = $scope.maxTags > 1 ? [] : {};
                    }
                };
                $scope.updateData();

                // Selectize configuration.
                $scope.config = {
                    create: true,
                    valueField: 'id',
                    labelField: 'title',
                    searchField: ['title'],
                    maxOptions: 15,
                    maxItems: $scope.maxTags || 1,

                    /**
                     * Called anytime the user types into the input box.
                     *
                     * @param string query
                     * @param function callback
                     */
                    load: function(query, callback) {

                        // Performance check.
                        Utilities.debug('Fetching tags...');
                        Utilities.debug(query);
                        if (!query || !query.length) {
                            return callback();
                        }

                        // Queries the API for tags.
                        $http.get('/api/v1/tags', {
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
                    },

                    /**
                     * Called anytime the user creates a new tag.
                     *
                     * @param string value
                     * @param object data
                     */
                    onOptionAdd: function(value, data) {

                        // Performance check.
                        if (value.trim().length < 1 || data.id) {
                            return;
                        }

                        // Create the new tag.
                        Utilities.debug('Creating tag: ' + value);
                        $http.post('/api/v1/tags', {
                            title: value.trim()
                        });
                    },

                    /**
                     * Called anytime the value of the input changes.
                     *
                     * @param array data
                     */
                    onChange: function(data) {
                        $scope.model[$scope.key] = data;
                    }
                };

                // Updates the view whenever the model changes.
                $scope.$watch('model', function(model) {
                    $scope.updateData();
                });
            }
        ]
    };
})

/**
 * Profile lookup input.
 *
 * Basic usage:
 *  <ui-profile-lookup
 *
 *  </ui-profile-lookup>
 */
.directive('uiProfileLookup', function() {
    return {
        restrict: 'E',
        transclude: true,
        templateUrl: 'partials/directives/ui-taggable-input.html',
        scope: {
            model: '=?',
            profiles: '=?',
            select: '&selectProfile'
        },
        controller: ['$scope', '$http', 'Utilities',
            function($scope, $http, Utilities) {

                // Default scope variables.
                $scope.model = $scope.model || {};
                $scope.profiles = $scope.profiles || [];

                // Generates the displayed label.
                $scope.getLabel = function(profile) {

                    var label = profile.firstName;

                    if (profile.lastName && profile.lastName.length) {
                        label += ' ' + profile.lastName;
                    }

                    // TODO: add main group.

                    return label;
                };

                // Format option data for selectize input.
                $scope.options = [];
                if ($scope.profiles.length > 0)
                {
                    angular.forEach($scope.profiles, function(profile) {
                        if (profile && profile.id) {
                            $scope.options.push({
                                id: profile.id,
                                title: $scope.getLabel(profile)
                            });
                        }
                    });
                }

                // Updates the model whenever necessary.
                $scope.updateData = function() {

                    if ($scope.model && $scope.model.id)
                    {
                        $scope.data = $scope.model.id;
                        $scope.options = [{
                            id: $scope.model.id,
                            title: $scope.getLabel($scope.model)
                        }];
                    }

                    // Default value.
                    else {
                        $scope.model = {};
                    }
                };
                $scope.updateData();

                // Selectize configuration.
                $scope.config = {
                    create: false,
                    valueField: 'id',
                    labelField: 'title',
                    searchField: ['title'],
                    maxOptions: 15,
                    maxItems: 1,

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

                        // ...
                        callback($scope.options);
                    },

                    /**
                     * Called anytime the value of the input changes.
                     *
                     * @param array data
                     */
                    onChange: function(data) {
                        $scope.select({profile: parseInt(data)});
                    }
                };

                // Updates the view whenever the model changes.
                // $scope.$watch('model', function(model) {
                //     $scope.updateData();
                // });
            }
        ]
    };
});
