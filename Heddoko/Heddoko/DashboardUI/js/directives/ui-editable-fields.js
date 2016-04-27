/**
 * Copyright Heddoko(TM) 2015, all rights reserved.
 *
 * @brief   Angular directive for editable list tables. Includes action buttons.
 * @author  Francis Amankrah (frank@heddoko.com)
 * @date    November 2015
 * @note    Use as:
 *              <ui-editable-fields data-heading="Some Heading">
 *                  <div ui-editable-list-item label="Some Label" value="data.some.value">
 *                  </div>
 *              </ui-editable-fields>
 *
 *          Or:
 *              <ui-editable-standalone-field
 *                  data-heading="Some Heading"
 *                  data-model="model"
 *                  data-key="key"
 *                  data-empty="Enter a value"
 *                  data-save="someFunction"
 *                  data-save-callback="someCallbackFunction">
 *              </ui-editable-standalone-field>
 */
angular.module('app.directives')

/**
 * Directive for a list of model properties. Contains one or more uiEditableListItem.
 */
.directive('uiEditableFields', function() {
    return {
        restrict: 'AE',
        transclude: true,
        scope: {
            id: '@',
            heading: '@',
            resource: '=model',
            saveResource: '=save',
            saveResourceCallback: '=saveCallback',
            deleteResource: '=delete'
        },
        controller: ['$scope', function($scope) {

            // Represents current state of directive.
            $scope.state = 'idle';

            // Stores some configuration values for this directive.
            $scope.config = $scope.config || {};

            /**
             * Stores list of items in this container.
             *
             * @param object item
             * @return bool
             */
            this.addItem = function(item) {

                // Link item to controller.
                item.state = $scope.state;
                item.model = $scope.resource;
                item.config = $scope.config;
                item.saveResource = $scope.saveResource;
                item.saveResourceCallback = $scope.saveResourceCallback;
                items[item.key] = item;

                return true;
            };
            var items = $scope.items = {};

            /**
             * Switches directive state to "editing".
             */
            $scope.edit = function() {
                $scope.state = 'editing';
                angular.forEach(items, function(item) {
                    item.state = $scope.state;
                });
            };

            /**
             * Updates the model.
             */
            $scope.save = function() {

                // Switch state to "saving".
                $scope.state = 'saving';
                angular.forEach(items, function(item) {
                    item.state = $scope.state;
                });

                // Save resource.
                $scope.saveResource.call($scope.model).then(
                    function(response) {
                        $scope.state = 'idle';
                        $scope.saveResourceCallback.call(response.data, true);

                        angular.forEach(items, function(item) {
                            item.state = $scope.state;
                        });
                    },
                    function(response) {
                        $scope.state = 'idle';
                        $scope.saveResourceCallback.call(response.data, false);

                        angular.forEach(items, function(item) {
                            item.state = $scope.state;
                        });
                    }
                );
            };

            /**
             * Deletes a model.
             */
            $scope.delete = function() {
                $scope.deleteResource.apply();
            };

            /**
             * Watches for updates to model from outside the directive.
             *
             * @param object model
             */
            $scope.$watch('resource', function(model) {
                $scope.state = 'idle';

                angular.forEach(items, function(item) {
                    item.state = $scope.state;
                    item.model = model;
                });
            });
        }],
        templateUrl: 'partials/directives/ui-editable-fields/fields.html'
    };
})

/**
 * Directive for properties of a model. Meant to be used inside uiEditableListContainer.
 */
.directive('uiEditableField', ['$filter', '$timeout', '$http', 'Utilities',
    function($filter, $timeout, $http, Utilities) {
        return {
            require: '^uiEditableFields',
            restrict: 'AE',
            scope: {
                label: '@',
                display: '@',
                key: '@',
                empty: '@',
                inputType: '@type',
                maxTags: '@',
                isRequired: '=required',
                isDisabled: '=disabled'
            },
            link: function(scope, element, attrs, controller) {

                // Link item to controller.
                if (!controller.addItem(scope)) {
                    return;
                }

                /**
                 * Switches directive state to "editing".
                 */
                scope.edit = function() {
                    scope.state = 'editing';
                };

                /**
                 * Switch state to "idle".
                 */
                scope.cancel = function() {
                    scope.state = 'idle';
                };

                /**
                 * Saves changes.
                 */
                scope.save = function() {

                    // Switch state to "saving".
                    scope.state = 'saving';

                    // Save resource.
                    scope.saveResource.call(scope.model).then(
                        function(response) {
                            scope.state = 'idle';
                            scope.saveResourceCallback.call(response.data, true);
                        },
                        function(response) {
                            scope.state = 'idle';
                            scope.saveResourceCallback.call(response.data, false);
                        }
                    );
                };

                // Data object.
                scope.data = {};

                // Empty state.
                scope.empty = scope.empty || '...';

                // Initialize fields.
                switch (attrs.type)
                {
                    // Date, Datetime
                    // TODO: set timezone elsewhere
                    case 'date':
                    case 'datetime':

                        // Default date value.
                        scope.model[scope.key] = scope.model[scope.key] || '';

                        // Format display label.
                        var angularFormat = 'MMMM d, yyyy', momentFormat = 'MMMM D, YYYY';
                        if (attrs.type == 'datetime') {
                            momentFormat += ' (h:mm a)';
                            angularFormat += ' (h:mm a)';
                        }

                        scope.format = angularFormat;
                        scope.updateTimestamp = function() {

                            if (scope.model[scope.key].length)
                            {
                                scope.timestamp = scope.model[scope.key].replace(' ', 'T') + '-05:00';
                                scope.timestamp = $filter('date')(scope.timestamp, angularFormat);
                            }
                            else
                            {
                                scope.timestamp = '';
                            }
                        };
                        scope.updateTimestamp();

                        // Create a datetime picker.
                        if (!attrs.disabled) {
                            $timeout(function() {

                                // Create date picker.
                                $(element).find('input[type="datetime"]').datetimepicker({
                                    format: momentFormat,
                                    viewMode: 'months'
                                })

                                // Attach "onChange" event.
                                .on('dp.change', function(e) {
                                    scope.model[scope.key] = e.date.format('YYYY-MM-DD HH:mm:ss');
                                })

                                // Set date.
                                .data('DateTimePicker').date(scope.timestamp);
                            });
                        }

                        // Updates the view whenever the model changes.
                        scope.$watch('model', function(model) {
                            scope.updateTimestamp();
                        });
                        break;

                    // Gender.
                    case 'gender':

                        scope.updateDisplay = function() {

                            // Default gender value.
                            scope.model[scope.key] = scope.model[scope.key] || '';

                            if (scope.model[scope.key].length > 0) {
                                scope.display = scope.model[scope.key].charAt(0).toUpperCase() +
                                    scope.model[scope.key].slice(1);
                            }

                            else {
                                scope.display = '';
                            }
                        };

                        scope.updateDisplay();

                        // Updates the view whenever the model changes.
                        scope.$watch('model', function(model) {
                            scope.updateDisplay();
                        });
                        break;

                    // Height, Length
                    case 'length':

                        // Supported length units.
                        scope.config.unitForLength = scope.config.unitForLength || 'ft/in';
                        scope.units = [/*'mm', */'cm', 'm', 'in', 'ft/in'];

                        // Default length value.
                        scope.model[scope.key] = scope.model[scope.key] || 0.0;

                        // Formats length according to selected unit.
                        scope.updateUnit = function(unit) {
                            switch (unit) {
                                case 'mm':
                                    scope.config.unitForLength = unit;
                                    scope.data.lengthVal = scope.model[scope.key] * 1000;
                                    scope.data.displayStr = $filter('number')(scope.data.lengthVal) + ' mm';
                                    break;

                                case 'cm':
                                    scope.config.unitForLength = unit;
                                    scope.data.lengthVal = scope.model[scope.key] * 100;
                                    scope.data.displayStr = $filter('number')(scope.data.lengthVal) + ' cm';
                                    break;

                                case 'in':
                                    scope.config.unitForLength = unit;
                                    scope.data.lengthVal = scope.model[scope.key] * 39.3701;
                                    scope.data.displayStr = $filter('number')(scope.data.lengthVal) + '"';
                                    break;

                                case 'm':
                                    scope.config.unitForLength = 'm';
                                    scope.data.lengthVal = scope.model[scope.key];
                                    scope.data.displayStr = $filter('number')(scope.data.lengthVal) + ' m';
                                    break;

                                default:
                                    scope.config.unitForLength = unit;
                                    scope.data.lengthFeet = Math.floor(scope.model[scope.key] * 3.28084);
                                    scope.data.lengthInches = Math.floor(scope.model[scope.key] * 39.3701 - 12 * scope.data.lengthFeet);
                                    scope.data.displayStr = scope.data.lengthFeet + '\' '+ scope.data.lengthInches +'"';
                                    break;
                            }
                        };

                        // Updates length value on model.
                        scope.updateModel = function() {
                            Utilities.log('Updating length value for "' + scope.key + '" ...');

                            switch (scope.config.unitForLength)
                            {
                                case 'mm':
                                    scope.model[scope.key] = scope.data.lengthVal / 1000;
                                    break;

                                case 'cm':
                                    scope.model[scope.key] = scope.data.lengthVal / 100;
                                    break;

                                case 'in':
                                    scope.model[scope.key] = scope.data.lengthVal / 39.3701;
                                    break;

                                case 'ft/in':
                                    scope.model[scope.key] =
                                        scope.data.lengthFeet / 3.28084 + scope.data.lengthInches / 39.3701;
                                    break;

                                default:
                                    scope.model[scope.key] = scope.data.lengthVal;
                            }

                            Utilities.log('From ' + scope.data.displayStr + ' to ' + scope.model[scope.key] + ' m');
                        };

                        // Calculate length in desired units on first load.
                        scope.updateUnit(scope.config.unitForLength);

                        // Watches the model for any changes and updates the length string accordingly.
                        scope.$watch('model', function(model) {
                            scope.updateUnit(scope.config.unitForLength);
                        });
                        break;

                    // Weight, mass
                    case 'mass':

                        // Supported mass units.
                        scope.config.unitForMass = scope.config.unitForMass || 'lbs';
                        scope.units = [/*'g', */'kg', 'lbs', 'stone'];

                        // Default mass value.
                        scope.model[scope.key] = scope.model[scope.key] || 0.0;

                        /**
                         * Formats length according to selected unit.
                         *
                         * @param string unit
                         */
                        scope.updateUnit = function(unit) {
                            switch (unit) {
                                case 'g':
                                    scope.config.unitForMass = unit;
                                    scope.data.massVal = scope.model[scope.key] * 1000;
                                    scope.data.displayStr = $filter('number')(scope.data.massVal, 2) + ' g';
                                    break;

                                case 'kg':
                                    scope.config.unitForMass = 'kg';
                                    scope.data.massVal = scope.model[scope.key];
                                    scope.data.displayStr = $filter('number')(scope.data.massVal, 0) + ' kg';
                                    break;

                                case 'stone':
                                    scope.config.unitForMass = unit;
                                    scope.data.massVal = scope.model[scope.key] * 0.157473;
                                    scope.data.displayStr = $filter('number')(scope.data.massVal, 2) + ' stone';
                                    break;

                                default:
                                    scope.config.unitForMass = unit;
                                    scope.data.massVal = scope.model[scope.key] * 2.20462;
                                    scope.data.displayStr = $filter('number')(scope.data.massVal, 0) + ' lbs';
                                    break;
                            }
                        };

                        // Updates mass value on model.
                        scope.updateModel = function() {
                            Utilities.log('Updating mass value for "' + scope.key + '" ...');

                            switch (scope.config.unitForMass)
                            {
                                case 'g':
                                    scope.model[scope.key] = scope.data.massVal / 1000;
                                    break;

                                case 'lbs':
                                    scope.model[scope.key] = scope.data.massVal / 2.20462;
                                    break;

                                case 'stone':
                                    scope.model[scope.key] = scope.data.massVal / 0.157473;
                                    break;

                                default:
                                    scope.model[scope.key] = scope.data.massVal;
                            }

                            Utilities.log('From ' + scope.data.displayStr + ' to ' + scope.model[scope.key] + ' kg');
                        };

                        // Calculate mass in desired units on first load.
                        scope.updateUnit(scope.config.unitForMass);

                        // Watches the model for any changes and updates the mass string accordingly.
                        scope.$watch('model', function(model) {
                            scope.updateUnit(scope.config.unitForMass);
                        });
                        break;

                    case 'tag':

                        scope.updateData = function() {

                            // Array of tags.
                            if (angular.isArray(scope.model[scope.key]))
                            {
                                Utilities.log('Looping through tags array...');

                                // Setup data model, available options and displayed value.
                                scope.data = [];
                                scope.display = [];
                                scope.options = [];
                                angular.forEach(scope.model[scope.key], function(tag) {

                                    // Push ID to data model.
                                    scope.data.push(tag.id);

                                    // Push title to displayed array.
                                    scope.display.push(tag.title);

                                    // Add tag to options list.
                                    scope.options.push({
                                        id: tag.id,
                                        title: tag.title
                                    });
                                });

                                // Generate display value.
                                scope.display =
                                    scope.model[scope.key].length ? scope.display.join(', ') :
                                    (scope.empty || '');
                            }

                            // Single tag
                            else if (angular.isObject(scope.model[scope.key]))
                            {
                                if (scope.model[scope.key].id)
                                {
                                    scope.data = scope.model[scope.key].id;
                                    scope.display = scope.model[scope.key].title;
                                    scope.options = [{
                                        id:scope.model[scope.key].id,
                                        title: scope.model[scope.key].title
                                    }];
                                }
                                else {
                                    scope.display = (scope.empty || '');
                                }
                            }

                            // Default value.
                            else {
                                scope.model[scope.key] = scope.maxTags > 1 ? [] : {};
                                scope.display = (scope.empty || '');
                            }
                        };
                        scope.updateData();

                        // Config object for Selectize.
                        scope.config = {
                            create: true,
                            valueField: 'id',
                            labelField: 'title',
                            searchField: ['title'],
                            maxOptions: 15,
                            maxItems: scope.maxTags || 1,

                            /**
                             * Called anytime the user types into the input box.
                             *
                             * @param string query
                             * @param function callback
                             */
                            load: function(query, callback) {

                                // Performance check.
                                Utilities.log('Fetching tags...');
                                Utilities.log(query);
                                if (!query || !query.length) {
                                    return callback();
                                }

                                // Queries the API for tags.
                                $http.get('/api/v1/tags', {
                                    params: {
                                        query: query,
                                        limit: 15
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
                                Utilities.log('Creating tag: ' + value);
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
                                Utilities.log('updating data');
                                Utilities.log(data);
                                // scope.model[scope.key] = scope.maxTags > 1 ? data : data[0];
                                scope.model[scope.key] = data;
                            }
                        };

                        // Updates the view whenever the model changes.
                        scope.$watch('model', function(model) {
                            scope.updateData();
                        });
                        break;

                    // Temporary value.
                    case 'placeholder':
                        break;

                    default:

                        scope.init = function() {
                            scope.model[scope.key] = scope.model[scope.key] || '';
                        };
                        scope.init();

                        // Updates the view whenever the model changes.
                        scope.$watch('model', function(model) {
                            scope.init();
                        });
                }
            },
            templateUrl: 'partials/directives/ui-editable-fields/field-vertical.html'
        };
    }
])

/**
 * Editable panel.
 */
.directive('uiEditableStandaloneField', function() {
    return {
        restrict: 'AE',
        scope: {
            heading: '@?',
            model: '=',
            key: '@',
            empty: '@',
            inputType: '@?',
            maxTags: '@?',
            saveResource: '=save',
            saveResourceCallback: '=saveCallback',
            deleteResource: '=delete'
        },
        controller: ['$scope', '$http', 'Utilities', function($scope, $http, Utilities) {

            // Represents current state of directive.
            $scope.state = 'idle';

            // Controller setup.
            switch ($scope.inputType)
            {
                case 'tag':
                case 'tag-title':

                    // Data & display strings
                    $scope.display = $scope.model[$scope.key].join(', ');
                    $scope.options = [];
                    angular.forEach($scope.model[$scope.key], function(tag) {
                        $scope.options.push({
                            title: tag
                        });
                    });

                    // Config object for Selectize.
                    $scope.selectizeConfig = {
                        create: true,
                        valueField: 'title',
                        labelField: 'title',
                        searchField: ['title'],
                        options: $scope.options,
                        items: $scope.options,
                        maxItems: $scope.maxTags || 1,

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

                            // Queries the API for tags.
                            $http.get('/api/v1/tags', {
                                params: {
                                    query: query,
                                    limit: 15
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
                         * Called anytime the value of the input changes.
                         *
                         * @param array data
                         */
                        onChange: function(data) {
                            $scope.model[$scope.key] = data;
                            $scope.display = data.join(', ');
                        }
                    };
                    break;

                case 'textarea':
                case 'text':
                    break;

                default:
                    $scope.inputType = 'textarea';
            }

            /**
             * Switch state to "editing".
             */
            $scope.edit = function() {
                $scope.state = 'editing';
            };

            /**
             * Switch state to "idle".
             */
            $scope.cancel = function() {
                $scope.state = 'idle';
            };

            /**
             * Saves changes.
             */
            $scope.save = function() {

                // Switch state to "saving".
                $scope.state = 'saving';

                // Save resource.
                $scope.saveResource.call($scope.model).then(
                    function(response) {
                        $scope.state = 'idle';
                        $scope.saveResourceCallback.call(response.data, true);
                    },
                    function(response) {
                        $scope.state = 'idle';
                        $scope.saveResourceCallback.call(response.data, false);
                    }
                );
            };

            /**
             * Deletes a model.
             */
            $scope.delete = function() {
                $scope.deleteResource.apply();
            };

            // Watches for updates to model from outside the directive.
            $scope.$watch('model', function(model) {
                $scope.state = 'idle';
            });
        }],
        templateUrl: 'partials/directives/ui-editable-fields/standalone-field.html'
    };
});
