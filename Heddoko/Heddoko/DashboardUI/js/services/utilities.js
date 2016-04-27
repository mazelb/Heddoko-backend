/**
 *
 * Copyright Heddoko(TM) 2015, all rights reserved.
 *
 *
 * @brief   The Utilities service contains general helper methods to be used throughout the app.
 * @author  Francis Amankrah (frank@heddoko.com)
 * @date    November 2015
 */
angular.module('app.utilities', [])

.service('Utilities', ['$window', '$localStorage', '$sessionStorage', '$route', '$location', '$timeout', 'isLocalEnvironment',
    function($window, $localStorage, $sessionStorage, $route, $location, $timeout, isLocalEnvironment) {

        // Dev variables.
        this.timestamp = Date.now();
        this.isLocal = isLocalEnvironment;

        // User-specific hash. Used for user-specific data.
        this.userHash = $('meta[name="user-hash"]').attr('content');

        // User-namespaced storage.
        $localStorage[this.userHash] = $localStorage[this.userHash] || {};
        $sessionStorage[this.userHash] = $sessionStorage[this.userHash] || {};
        this.store = $localStorage[this.userHash];
        this.state = $sessionStorage[this.userHash];

        // Ephemeral storage.
        this.data = {};

        // Configuration object.
        this.store.config = this.store.config || {};

        // Theme colours (American spelling).
        this.color =
        {
            blue: '#2c3a46',
            darkBlue: '#1c242c',
            danger: '#db5031',
            heddokoGreen: '#3bd6b2',
            info: '#8170ca',
            orange: '#fabd39',
            silver: '#cbd4e3',
            textColor: '#ddd',
            textColorBlue: '#5b707d',
            warning: '#eec95a'
        };

        /**
         * Retrieves a configuration value.
         *
         * @param string key
         * @param mixed defaultValue
         * @return mixed
         */
        this.getConfig = function(key, defaultValue) {
            return this.store.config[key] ? this.store.config[key] : defaultValue;
        }.bind(this);

        /**
         * Sets a configuration value.
         *
         * @param string key
         * @param mixed value
         * @return mixed
         */
        this.setConfig = function(key, value) {
            this.store.config[key] = value;
            return value;
        }.bind(this);


        ///
        /// State variables are stored in the sessionStorage. The following methods deal with
        /// associative arrays, usually keyed by ID.
        ///
        ////////////////////////////////////////////////////////////////////////////////////////////


        /**
         * Checks whether a key exists within an associative array.
         *
         * @param string namespace
         * @param string id
         * @return bool
         */
        this.hasStateKey = function(namespace, id) {
            return (this.state[namespace] && this.state[namespace].list['_' + id]) ? true : false;
        }.bind(this);

        /**
         * Retrieves a state variable.
         *
         * @param string namespace
         * @param string id
         * @param mixed def
         * @return bool
         */
        this.getState = function(namespace, id, def) {
            return this.hasStateKey(namespace, id) ? this.state[namespace].list['_' + id] : def;
        }.bind(this);

        /**
         * Sets a state variable.
         *
         * @param string namespace
         * @param string id
         * @param mixed value
         */
        this.setState = function(namespace, id, value) {

            // Setup namespace.
            if (!this.state[namespace]) {
                this.state[namespace] = {
                    list: {
                        length: 0
                    }
                };
            }

            // Update namespace counter.
            if (!this.hasState(namespace, id)) {
                this.state[namespace].list.length++;
            }

            // Add an underscore to the state key, so that we may store objects by ID
            // without any problems.
            this.state[namespace].list['_' + id] = value;
        }.bind(this);


        ///
        /// Ephemeral variables are stored in Utilities.data. The following methods deal with
        /// associative arrays, usually keyed by ID.
        ///
        ////////////////////////////////////////////////////////////////////////////////////////////


        /**
         * Checks whether a namespace is configured in the ephemeral storage.
         *
         * @param string namespace
         * @return bool
         */
        this.hasDataNamespace = function(namespace) {
            return (this.data[namespace]);
        }.bind(this);

        /**
         * Checks the length of a list in the specified namespace.
         *
         * @param string namespace
         * @return int
         */
        this.getDataLength = function(namespace) {
            return this.hasDataNamespace(namespace) ? this.data[namespace].length : 0;
        }.bind(this);

        /**
         * Checks whether an ephemeral variable is defined within a namespace.
         *
         * @param string namespace
         * @param string id
         * @return bool
         */
        this.hasData = function(namespace, id) {
            return (this.hasDataNamespace(namespace) && this.data[namespace]['_' + id]);
        }.bind(this);

        /**
         * Retrieves an ephemeral variable from the specified namespace.
         *
         * @param string namespace
         * @param string id
         * @param mixed def
         * @return bool
         */
        this.getData = function(namespace, id, def) {
            return this.hasData(namespace, id) ? this.data[namespace]['_' + id] : def;
        }.bind(this);

        /**
         * Retrieves all ephemeral variables within the specified namespace.
         *
         * @param string namespace
         * @param mixed def
         * @return mixed
         */
        this.getDataList = function(namespace, def) {
            return this.hasDataNamespace(namespace) ? this.data[namespace] : def;
        }.bind(this);

        /**
         * Converts an ephemeral namespace to an array.
         *
         * @param string namespace
         * @param mixed def
         * @return mixed
         */
        this.getDataArray = function(namespace) {
            if (this.hasDataNamespace(namespace))
            {
                var array = [];
                for (var key in this.data[namespace]) {
                    if (this.data[namespace].hasOwnProperty(key) && key[0] == '_' && this.data[namespace][key] !== null) {
                        array.push(this.data[namespace][key]);
                    }
                }

                return array;
            }

            else {
                return [];
            }
        }.bind(this);

        /**
         * Sets an ephemeral variable in the specified namespace.
         *
         * @param string namespace
         * @param string id
         * @param mixed value
         */
        this.setData = function(namespace, id, value) {

            // Setup namespace.
            if (!this.hasDataNamespace(namespace)) {
                this.createDataNamespace(namespace);
            }

            // Update namespace counter.
            if (!this.hasData(namespace, id)) {
                this.data[namespace].length++;
            }

            // We add an underscore to the key so that we may store objects by ID without any problems.
            this.data[namespace]['_' + id] = value;

            // Update object length.
            if (value === null) {
                this.data[namespace].length--;
            }
        };

        /**
         * Resets an ephemeral namespace.
         *
         * @param string namespace
         */
        this.createDataNamespace = function(namespace) {
            this.data[namespace] = {
                length: 0
            };
        }.bind(this);
        this.resetDataNamespace = this.createDataNamespace;


        ///
        /// Helper methods.
        ///
        ////////////////////////////////////////////////////////////////////////////////////////////


        /**
         * Formats an embed parameter for an API query.
         *
         * @param array|string embed
         * @return string
         */
        this.getEmbedParameter = function(embed) {

            // Concatenate arrays into a string.
            if (angular.isArray(embed)) {
                return embed.join(',');
            }

            // If embed is a string, assume it's already properly formatted.
            if (angular.isString(embed)) {
                return embed;
            }

            return '';
        };

        /**
         * Calculates the length of an associative array.
         *
         * @param object obj
         * @return int
         */
        this.getObjectLength = function(obj) {

            // Performance check.
            if (obj === null || obj === undefined || !obj.hasOwnProperty) {
                return 0;
            }

            var size = 0, key;
            for (key in obj) {
                if (obj.hasOwnProperty(key) && key != 'length' && key[0] != '_') {
                    size++;
                }
            }

            return size;
        };

        /**
         * Retrieves the ID from an object.
         *
         * @param mixed obj
         * @return int
         */
        this.getId = function(obj) {

            // Performance check.
            if (!obj) {
                return 0;
            }

            // If we have an object, return the ID property.
            if (typeof obj == 'object') {
                return Number(obj.id);
            }

            // If we have a string or number, assume its an ID.
            if (['string', 'number'].indexOf(typeof obj) !== -1) {
                return Number(obj);
            }

            // In any other case, assume the arguments are invalid.
            return 0;
        };

        /**
         * Logs a message to the console.
         *
         * @param mixed msg
         */
        this.log = function(msg) {
            if (this.isLocal && typeof console.log == 'function') {
                console.log(msg);
            }
        };
        this.debug = function(msg) {
            this.log('Utilities.debug is deprecated...');
            this.log(msg);
        }.bind(this);

        /**
         * Logs an info message to the console.
         *
         * @param mixed msg
         */
        this.info = function(msg) {
            if (this.isLocal && typeof console.info == 'function') {
                console.info(msg);
            }
        };

        /**
         * Logs an error message to the console.
         *
         * @param mixed msg
         */
        this.error = function(msg) {
            if (this.isLocal && typeof console.error == 'function') {
                console.error(msg);
            }
        };

        /**
         *
         * @param mixed msg
         */
        this.alert = function(msg) {
            $window.alert(msg);
        };

        /**
         * Performance timer. See: https://developer.mozilla.org/en-US/docs/Web/API/Console/time
         *
         * @param string label
         */
        this.time = function(label) {
            if (this.isLocal && typeof console.time == 'function') {
                console.time(label);
            }
        }.bind(this);

        /**
         * Performance timer. See: https://developer.mozilla.org/en-US/docs/Web/API/Console/timeEnd
         *
         * @param string label
         */
        this.timeEnd = function(label) {
            if (this.isLocal && typeof console.timeEnd == 'function') {
                console.timeEnd(label);
            }
        }.bind(this);

        /**
         * Displays the loading animation.
         */
        this.showLoading = function() {
            $('.page-loading-overlay').removeClass('loaded');
            $('.load_circle_wrapper').removeClass('loaded');
        };

        /**
         * Hides the loading animation.
         */
        this.hideLoading = function() {
            $('.page-loading-overlay').addClass('loaded');
            $('.load_circle_wrapper').addClass('loaded');
        };
    }
]);
